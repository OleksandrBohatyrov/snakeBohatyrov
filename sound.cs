using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAudio;

using NAudio.Wave;
using System.Threading.Tasks;

namespace snakeBohatyrov
{
    public class sound
    {
        public async Task Tagaplaanis_Mangida(string Path)
        {
            await Task.Run(() =>
            {
                using (AudioFileReader audioFileReader = new AudioFileReader(Path))
                using (IWavePlayer waveOutDevice = new WaveOutEvent { DesiredLatency = 200 })
                {
                    waveOutDevice.Init(audioFileReader);
                    waveOutDevice.Play();
                    while (waveOutDevice.PlaybackState == PlaybackState.Playing)
                    {
                        Thread.Sleep(1000);
                    }
                }
            });
        }

        public async Task Natuke_mangida(string Path)
        {
            await Task.Run(() =>
            {
                using (AudioFileReader audioFileReader = new AudioFileReader(Path))
                using (IWavePlayer waveOutDevice = new WaveOutEvent())
                {
                    waveOutDevice.Init(audioFileReader);
                    waveOutDevice.Play();
                    while (waveOutDevice.PlaybackState == PlaybackState.Playing)
                    {
                        Thread.Sleep(50);
                    }
                }
            });
        }

    }
}
