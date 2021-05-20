using SFML.Audio;

namespace Game
{
    class Music
    {
        string[] FileBuffer { get; set; }
        string CurrentMusic { get; set; }
        SFML.Audio.Music Player { get; set; }
        Music() { }
        public Music(string[] filebuffer)
        {
            FileBuffer = filebuffer;
            CurrentMusic = FileBuffer[0];
            Player = new SFML.Audio.Music(CurrentMusic);
            Player.Play();
        } 
        public void RenderMusic() 
        {
            if (Player.Status == SoundStatus.Stopped)
            {
                for(int i = 0; i < FileBuffer.Length; i++)
                {
                    if (CurrentMusic == FileBuffer[i] && i != FileBuffer.Length - 1)
                    {
                        CurrentMusic = FileBuffer[i + 1];
                        break;
                    }
                    else if (CurrentMusic == FileBuffer[i] && i == FileBuffer.Length - 1)
                    {
                        CurrentMusic = FileBuffer[0];
                        break;
                    }
                }
                Player = new SFML.Audio.Music(CurrentMusic);
                Player.Play();
            }
        }
        public void Stop()
        {
            Player.Stop();
        }
    }
}
