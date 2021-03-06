﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMPLib;

namespace WinFormPlayer
{   public enum Status
    {
        
    }
    class Song
    {
       public string Name { get; set; }
        public double Duration { get; set; }
        public string SourceUrl { get; set; }
        public IWMPMedia Media { get; set; } 
        
        public Song(IWMPMedia media)
        {
            Media = media;
            Name = Path.GetFileNameWithoutExtension(Media.sourceURL);
            Duration = Media.duration;
            SourceUrl = Media.sourceURL;
        }

    }
    class AudioPlayer
    {
        private WindowsMediaPlayer wmp;
        private List<Song> playlist;
        private int currentIndex;
        public Song CurrentSong => playlist[currentIndex];
        public AudioPlayer()
        {   
            wmp = new WindowsMediaPlayer();
            playlist = new List<Song>();
            wmp.settings.volume = 50;
        }

        public void LoadAudio(params string[] filepaths)
        {
            foreach (var file in filepaths)
                LoadAudio(file);
        }

        public void LoadAudio(string filepath)
        {
            playlist.Add(new Song(wmp.newMedia(filepath)));
        }

        public void SelectAudio(int index)
        {
            currentIndex = index;
            wmp.currentMedia = CurrentSong.Media;
        }

        public string[] Playlist => playlist.Select((a) => a.Name).ToArray();
        public int Volume
        {
            get { return wmp.settings.volume; }
            set { wmp.settings.volume = value; }
        }
        public void Play()
        {
            wmp.controls.play();
        }
        public void Pause()
        {
            wmp.controls.pause();
        }


        public void Stop()
        {
            wmp.controls.stop();
        }

        public void Next()
        {   if (currentIndex < playlist.Count - 1)
                SelectAudio(++currentIndex);
            else SelectAudio(0);

        }

        public void Previous()
        {
            if (currentIndex > 0)
                SelectAudio(--currentIndex);
            else SelectAudio(playlist.Count);
        }

    }
}
