﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormPlayer
{
    public partial class Form1 : Form
    {
        private SocketManager client = new SocketManager();
        private AudioPlayer Player;

        public Form1()
        {

            InitializeComponent();
            Player = new AudioPlayer();
            InputServerWorkAsync();

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
                }
            }
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
            if (listBox1.SelectedIndex > 1)
                listBox1.SetSelected(--listBox1.SelectedIndex, true);
            else
                listBox1.SetSelected(listBox1.Items.Count, true);
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

                    if (client.msg == "play")
                    {
                        button3_Click(button3, EventArgs.Empty);

                        
                    }

                    if (client.msg == "stop")
                    {
                        button3_Click(button3, EventArgs.Empty);

                        
                    }

                    if (client.msg == "next")
                    {
                        buNext_Click(buNext, EventArgs.Empty);
                    }

                    if (client.msg == "prev")
                    {
                        buPrev_Click(buPrev, EventArgs.Empty);
                    }

                }
                else Thread.Sleep(100);
        }

        private async void InputServerWorkAsync()
        {
            await Task.Run(() => InputServerWork());
        }

    }
}
