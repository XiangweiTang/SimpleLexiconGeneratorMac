using System;
using AVFoundation;
using Foundation;
using System.IO;

namespace SimpleLexiconGeneratorMac
{
    public static class AudioPlayer
    {
        static AVAudioPlayer Player = null;
        public static void Play(string path)
        {
            var url = new NSUrl(path);
            NSError e;
            Player = new AVAudioPlayer(url, "wav", out e);
            Player.Play();
        }

        public static void Stop()
        {
            if (Player != null)
                Player.Stop();
        }
    }
}
