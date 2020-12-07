using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormPlayer
{
    public partial class Form1 : Form
    {
        private AudioPlayer Player;
        
        public Form1()
        {

            InitializeComponent();
            Player = new AudioPlayer();
            
        }
        private void PlayButtonState (Button button3)
        {
            if (button3.Text == "Play")
            {
                Player.Play();
                button3.Text = "Stop";
                return;
            }

            if (button3.Text == "Stop")
            {
                Player.Pause();
                button3.Text = "Play";
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog() { Multiselect = true, Filter="Audio Files|*.mp3" })
            {
                if (dialog.ShowDialog()==DialogResult.OK)
                {
                    Player.LoadAudio(dialog.FileNames);
                    listBox1.Items.Clear();
                    listBox1.Items.AddRange(Player.Playlist);
                }
            }
        }



        private void button3_Click(object sender, EventArgs e)
        {
            PlayButtonState(button3);
            //Player.Pause();
        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (((ListBox)sender).SelectedItem == null)
                return;
            Player.SelectAudio(((ListBox)sender).SelectedIndex);
            PlayButtonState(button3);
        }
        

        private void buNext_Click(object sender, EventArgs e)
        {
            Player.Next();
            if (listBox1.SelectedIndex<listBox1.Items.Count-1)
            listBox1.SetSelected(++listBox1.SelectedIndex, true);
            else
                listBox1.SetSelected(0, true);
            PlayButtonState(button3);
        }

        private void buPrev_Click(object sender, EventArgs e)
        {
            Player.Previous();
            if (listBox1.SelectedIndex > 1)
                listBox1.SetSelected(--listBox1.SelectedIndex, true);
            else
                listBox1.SetSelected(listBox1.Items.Count, true);
            PlayButtonState(button3);
        }
    }
}
