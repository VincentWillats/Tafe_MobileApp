using System;
using System.Collections.Generic;
using System.Text;
using Plugin.SimpleAudioPlayer;
using Xamarin.Forms;

namespace VictorianMoneyTracker
{
    class SoundEffects
    {
        static ISimpleAudioPlayer click = null;

        public static void PlayClickSound()
        {
            if ((bool)Application.Current.Properties["soundOn"])
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
}
