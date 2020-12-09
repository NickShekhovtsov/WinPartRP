using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormPlayer
{
    class ContainerForSongInfo
    {
        public List<SongInfo> songInfos=new List<SongInfo>();
        
    }

    class SongInfo
    {   
        public string name = "";
        public double duration = 0.0;

        public SongInfo(string _name,double _duration)
        {
            name = _name;
            duration = _duration;
        }

        public SongInfo()
        {
            
        }
    }
}
