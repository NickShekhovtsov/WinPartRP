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
using System.IO;

namespace WinFormPlayer
{

    public partial class RemotePlayer : Form
    {
        private SocketManager sk=new SocketManager("127.0.0.1",8000);
        private AudioPlayer Player = new AudioPlayer();

        private Commander com=new Commander();
        private string folderPath;
       
        //ContainerForSongInfo cfsi = new ContainerForSongInfo();

        public object JsonSerializer { get; private set; }

        public RemotePlayer()
        {

            InitializeComponent();
            buPlay.Image = Properties.Resources.Старт1;

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
                                buPlay_Click(buPlay, EventArgs.Empty);
                            });

                            break;

                        case "volume":

                            Player.Volume = int.Parse(sk.cm.value);
                            Console.WriteLine($"Playervol {Player.Volume}");

                            break;


                        case "pause":
                            Invoke((MethodInvoker)delegate
                            {
                                buPlay_Click(buPlay, EventArgs.Empty);
                            });

                            break;





                        case "next":
                            Invoke((MethodInvoker)delegate
                            {
                                buNext_Click(buNext, EventArgs.Empty);
                            });

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
            string dir = Directory.GetCurrentDirectory();
            if (File.Exists($"{dir}/note.txt"))
            {
                using (FileStream fstream = File.OpenRead($"{dir}/note.txt"))
                {
                    // преобразуем строку в байты
                    byte[] array = new byte[fstream.Length];
                    // считываем данные
                    fstream.Read(array, 0, array.Length);
                    // декодируем байты в строку
                    folderPath = System.Text.Encoding.Default.GetString(array);
                    Console.WriteLine($"Текст из файла: {folderPath}");
                }
                try
                {
                    string[] allfiles = Directory.GetFiles(folderPath, "*.mp3", SearchOption.TopDirectoryOnly);
                    Player.LoadAudio(allfiles);
                    listBox1.Items.Clear();
                listBox1.Items.AddRange(Player.Playlist);
                laPath.Text = $"Current path-{folderPath}";
                }
                catch (Exception e)
                {
                    Console.WriteLine("wrong path");
                }
                
                
            }
        }

        

        private void buOpen_Click(object sender, EventArgs e)
        {

            //using (OpenFileDialog dialog = new OpenFileDialog() { Multiselect = true, Filter = "Audio Files|*.mp3" })
            //{
            //    if (dialog.ShowDialog() == DialogResult.OK)
            //    {
                    
            //        Player.LoadAudio(dialog.FileNames);
            //        listBox1.Items.Clear();
            //        listBox1.Items.AddRange(Player.Playlist);


            //    }
            //}
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog()==DialogResult.OK)
            {
                Console.WriteLine(fbd.SelectedPath);
            }
            string[] allfiles = Directory.GetFiles(fbd.SelectedPath,"*.mp3",SearchOption.TopDirectoryOnly);
            Player.LoadAudio(allfiles);
            listBox1.Items.Clear();
            listBox1.Items.AddRange(Player.Playlist);

            string dir = Directory.GetCurrentDirectory();
            using (FileStream fstream = new FileStream($"{dir}/note.txt", FileMode.Create))
            {
                // преобразуем строку в байты
                byte[] array = System.Text.Encoding.Default.GetBytes(fbd.SelectedPath);
                // запись массива байтов в файл
                
                fstream.Write(array, 0, array.Length);
                Console.WriteLine("Текст записан в файл");
                laPath.Text = fbd.SelectedPath;
            }

            Player.Stop();
            listBox1.Items.Clear();
            Player.playlist.Clear();
            laName.Text = "";
            laPath.Text = fbd.SelectedPath;
            buPlay.Image = Properties.Resources.Старт1;
            folderPath = "";

            Player.LoadAudio(allfiles);
            listBox1.Items.Clear();
            listBox1.Items.AddRange(Player.Playlist);


        }



        private void buPlay_Click(object sender, EventArgs e)
        {

            if (listBox1.Items.Count > 0)
            {
                if (buPlay.Text == "►")
                {
                    sk.cm.command = "play";


                    Player.Play();
                    sk.cm.name = Player.CurrentSong.Name;
                    sk.SendData();
                    buPlay.Text = "||";
                    buPlay.Image = WinFormPlayer.Properties.Resources.Старт2;
                    return;
                }

                if (buPlay.Text == "||")
                {
                    sk.cm.command = "pause";
                    Player.Pause();
                    sk.cm.name = Player.CurrentSong.Name;
                    sk.SendData();
                    buPlay.Text = "►";
                    buPlay.Image = Properties.Resources.Старт1;
                    return;
                }
            }

        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (((ListBox)sender).SelectedItem == null)
                return;
            Player.SelectAudio(((ListBox)sender).SelectedIndex);
            buPlay.Text = "||";
            buPlay.Image = WinFormPlayer.Properties.Resources.Старт2;
            sk.SendData();
            
        }


        private void buNext_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count > 0)
            {
                Player.Next();
                if (listBox1.SelectedIndex < listBox1.Items.Count - 1)
                    listBox1.SetSelected(++listBox1.SelectedIndex, true);
                else
                    listBox1.SetSelected(0, true);
                buPlay.Text = "||";
                sk.cm.command = "next";
                buPlay.Image = WinFormPlayer.Properties.Resources.Старт2;
                sk.SendData();
            }
        }

        private void buPrev_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count > 0)
            {
                Player.Previous();
                if (listBox1.SelectedIndex > 0)
                    listBox1.SetSelected(--listBox1.SelectedIndex, true);
                else
                    listBox1.SetSelected(listBox1.Items.Count - 1, true);
                buPlay.Text = "||";
                sk.cm.command = "prev";
                buPlay.Image = WinFormPlayer.Properties.Resources.Старт2;
                sk.SendData();
            }
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

        private void RemotePlayer_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void buDel_Click(object sender, EventArgs e)
        {   
            Player.Stop();
            listBox1.Items.Clear();
            Player.playlist.Clear();
            laName.Text = "";
            laPath.Text = "";
            buPlay.Image = Properties.Resources.Старт1;
            folderPath = "";
            string dir = Directory.GetCurrentDirectory();
            using (FileStream fstream = new FileStream($"{dir}/note.txt", FileMode.Create))
            {
                // преобразуем строку в байты
                byte[] array = System.Text.Encoding.Default.GetBytes("///");
                // запись массива байтов в файл
                fstream.Write(array, 0, array.Length);
                Console.WriteLine("Текст записан в файл");
                
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
