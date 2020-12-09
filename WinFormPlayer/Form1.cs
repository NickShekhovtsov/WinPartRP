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


namespace WinFormPlayer
{
   
    public partial class Form1 : Form
    {
        private SocketManager client = new SocketManager();
        private AudioPlayer Player=new AudioPlayer();
        ContainerForSongInfo cfsi = new ContainerForSongInfo();

        public object JsonSerializer { get; private set; }

        public Form1()
        {

            InitializeComponent();
            InputServerWorkAsync();
            Player.AudioSelected += (s, e) =>
              {
                  laName.Text = e.Name;
              };
            
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog() { Multiselect = true, Filter = "Audio Files|*.mp3" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Player.LoadAudio(dialog.FileNames);
                    listBox1.Items.Clear();
                    listBox1.Items.AddRange(Player.Playlist); 
                    
                    //for (int i = 0; i < listBox1.Items.Count; i++)
                    //{
                    //    cfsi.songInfos.Add(new SongInfo());
                    //}
                    for (int i=0;i<listBox1.Items.Count;i++)
                    {
                        cfsi.songInfos.Add(new SongInfo(Player.playlist[i].Name, Player.playlist[i].Duration));
                    }
                }
            }

            byte[] outputdata;
            outputdata = new byte[1024];
            string outputjson=System.Text.Json.JsonSerializer.Serialize<ContainerForSongInfo>(cfsi);
            
            outputdata = Encoding.Unicode.GetBytes(outputjson);

            try
            {
                client.client.Send(outputdata);
            } 
            catch (System.Net.Sockets.SocketException d)
            {

            };
        }



        private void button3_Click(object sender, EventArgs e)
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

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (((ListBox)sender).SelectedItem == null)
                return;
            Player.SelectAudio(((ListBox)sender).SelectedIndex);

            button3.Text = "Stop";
            

        }


        private void buNext_Click(object sender, EventArgs e)
        {
            Player.Next();
            if (listBox1.SelectedIndex < listBox1.Items.Count - 1)
                listBox1.SetSelected(++listBox1.SelectedIndex, true);
            else
                listBox1.SetSelected(0, true);
            button3.Text = "Stop";
           
        }

        private void buPrev_Click(object sender, EventArgs e)
        {
            Player.Previous();
            if (listBox1.SelectedIndex > 0)
                listBox1.SetSelected(--listBox1.SelectedIndex, true);
            else
                listBox1.SetSelected(listBox1.Items.Count-1, true);
            button3.Text = "Stop";
           
        }



        private void trackBar1_Scroll(object sender, EventArgs e) => Player.Volume = ((TrackBar)sender).Value;



        private void InputServerWork()
        {

            while (true)
                if (client.client.Available > 0)
                {
                    byte[] data;
                    // получаем ответ
                    data = new byte[256]; // буфер для ответа
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0; // количество полученных байт

                    do
                    {
                        bytes = client.client.Receive(data, data.Length, 0);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (client.client.Available > 0);

                    client.msg = builder.ToString();

                    if (client.msg == "play" || client.msg == "stop")
                    {
                        Invoke((MethodInvoker)delegate {
                            button3_Click(button3, EventArgs.Empty);
                        });
                    }
                    else if (client.msg == "next")
                    {
                        Invoke((MethodInvoker)delegate {
                            buNext_Click(buNext, EventArgs.Empty);
                        });
                    }
                    else if (client.msg == "prev")
                    {
                        Invoke((MethodInvoker)delegate {
                            buPrev_Click(buPrev, EventArgs.Empty);
                        });
                    }

                }
                else Thread.Sleep(100);
        }

        private async void InputServerWorkAsync()
        {
            await Task.Run(() => InputServerWork());
        }

        private void laName_DockChanged(object sender, EventArgs e)
        {

        }
    }
}
