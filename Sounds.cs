using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAudio;
using WMPLib;
using NAudio.Wave;
using System.Threading.Tasks;

namespace snakeBohatyrov
{
    public class Sounds
    {
        WindowsMediaPlayer player = new WindowsMediaPlayer();
        private string pathToMedia;

        public Sounds(string pathToResources)
        {
            pathToMedia = pathToResources;
        }

        public void PLay() {
            player.URL = pathToMedia + "stardust.mp3";
            player.settings.volume = 30;
            player.controls.play();
            player.settings.setMode("loop", true);
        }


        public void Play(string songName)
        {
            player.URL = pathToMedia + songName + ".mp3";
            player.controls.play();
        }

        public void PlayEat() {
            player.URL = pathToMedia + "click.mp3";
            player.settings.volume = 100;
            player.controls.play();
        
        }
    }

}
