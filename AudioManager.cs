using NAudio.Wave;

namespace CyberBotApp
{
    public class AudioManager
    {
        public void PlayStartupSound()
        {
            try
            {
                var audioFile = new AudioFileReader("CyberBotgreeting.wav");

                var outputDevice = new WaveOutEvent();

                outputDevice.Init(audioFile);

                outputDevice.Play();
            }
            catch
            {
            }
        }
    }
}
