using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace WinFormPlayer
{

    public partial class RemotePlayer : Form
    {
        private SocketManager sk=new SocketManager("127.0.0.1",8000);
        private AudioPlayer Player = new AudioPlayer();
        private SongInfo sf = new SongInfo();
        private Commander com=new Commander();
        public delegate void Play();
        public event Play OnPlay;
        //ContainerForSongInfo cfsi = new ContainerForSongInfo();

        public object JsonSerializer { get; private set; }

        public RemotePlayer()
        {

            InitializeComponent();
            button3.Image = Properties.Resources.Старт1;

            Thread ServTh = new Thread(
                new ThreadStart(sk.Run));
            ServTh.Start();
            
           
            Player.VolumeChanged += () =>
              {
                  Invoke((MethodInvoker)delegate
                  {
                      trackBar1.Value = Player.Volume;
                  });
              };

            sk.DataCome += ()=>{
                try
                {
                    switch (sk.cm.command)
                    {
                        case "play":

                            //Player.Play();
                            Invoke((MethodInvoker)delegate
                            {
                                button3_Click(button3, EventArgs.Empty);
                            });

                            break;




                        case "pause":
                            Invoke((MethodInvoker)delegate
                            {
                                button3_Click(button3, EventArgs.Empty);
                            });

                            break;





                        case "next":
                            Invoke((MethodInvoker)delegate
                            {
                                buNext_Click(buNext, EventArgs.Empty);
                            });

                            break;

                        case "volume":

                            Player.Volume = int.Parse(sk.cm.value);
                            Console.WriteLine($"Playervol {Player.Volume}");

                            break;

                        case "prev":
                            Invoke((MethodInvoker)delegate
                            {
                                buPrev_Click(buPrev, EventArgs.Empty);
                            });

                            break;


                    }
                }
                catch(Exception e)
                { }

            };
            Player.AudioSelected += (s, e) =>
              {
                  laName.Text = e.Name;
                  sk.cm.name = e.Name;
                  
                  
              };

        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            //cfsi.songInfos = new List<SongInfo>();
            using (OpenFileDialog dialog = new OpenFileDialog() { Multiselect = true, Filter = "Audio Files|*.mp3" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Player.LoadAudio(dialog.FileNames);
                    listBox1.Items.Clear();
                    listBox1.Items.AddRange(Player.Playlist);

                    
                }
            }

            
        }



        private void button3_Click(object sender, EventArgs e)
        {
            
            
            if (button3.Text == "►")
            {   
                sk.cm.command = "play";
                
                
                Player.Play();
                sk.cm.name = Player.CurrentSong.Name;
                sk.SendData();
                button3.Text = "||";
                button3.Image = WinFormPlayer.Properties.Resources.Старт2;
                return;
            }

            if (button3.Text == "||")
            {
                sk.cm.command = "pause";
                Player.Pause();
                sk.cm.name = Player.CurrentSong.Name;
                sk.SendData();
                button3.Text = "►";
                button3.Image = Properties.Resources.Старт1;
                return;
            }
            

        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (((ListBox)sender).SelectedItem == null)
                return;
            Player.SelectAudio(((ListBox)sender).SelectedIndex);
            button3.Text = "||";
            button3.Image = WinFormPlayer.Properties.Resources.Старт2;
            sk.SendData();

        }


        private void buNext_Click(object sender, EventArgs e)
        {
            Player.Next();
            if (listBox1.SelectedIndex < listBox1.Items.Count - 1)
                listBox1.SetSelected(++listBox1.SelectedIndex, true);
            else
                listBox1.SetSelected(0, true);
            button3.Text = "||";
            sk.cm.command = "next";
            button3.Image = WinFormPlayer.Properties.Resources.Старт2;
            sk.SendData();
        }

        private void buPrev_Click(object sender, EventArgs e)
        {
            Player.Previous();
            if (listBox1.SelectedIndex > 0)
                listBox1.SetSelected(--listBox1.SelectedIndex, true);
            else
                listBox1.SetSelected(listBox1.Items.Count - 1, true);
            button3.Text = "||";
            sk.cm.command = "prev";
            button3.Image = WinFormPlayer.Properties.Resources.Старт2;
            sk.SendData();

        }



        private void trackBar1_Scroll(object sender, EventArgs e)
            {

            Player.Volume = ((TrackBar) sender).Value;
            sk.cm.value = Player.Volume.ToString();
            sk.SendData();
           


        }




        

        private void laName_DockChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
