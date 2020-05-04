using System;
using System.Collections.Generic;
using System.Text;
using Plugin.SimpleAudioPlayer;

namespace VictorianMoneyTracker
{
    class SoundEffects
    {
        static ISimpleAudioPlayer click = null;

        public static void PlayClickSound()
        {
            if (click == null)
            {
                click = CrossSimpleAudioPlayer.Current;
                click.Load("click.wav");
            }
            click.Play();
        }
    }
}
