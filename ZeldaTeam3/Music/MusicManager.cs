using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda.Music
{
  public class MusicManager
    {

      private SoundEffect _playLabryinthMusic;
      private SoundEffect _playGameOverMusic;
      private SoundEffect _playWinMusic;
      private SoundEffectInstance labryinthMusicInstance;
      private SoundEffectInstance gameOverMusicInstance;
      private SoundEffectInstance winMusicInstance;

        public static MusicManager Instance { get; } = new MusicManager();

        private MusicType lastPlaying = MusicType.None;
        public void LoadAllSounds(ContentManager Content)
        {
            _playLabryinthMusic = Content.Load<SoundEffect>("Music/04_Labyrinth");
            _playGameOverMusic = Content.Load<SoundEffect>("Music/07_Game_Over");
            _playWinMusic = Content.Load<SoundEffect>("Music/10_Ending");

            labryinthMusicInstance = _playLabryinthMusic.CreateInstance();
            gameOverMusicInstance = _playGameOverMusic.CreateInstance();
            winMusicInstance = _playWinMusic.CreateInstance();

        }


        public void PlayLabryinthMusic()
        {
            if (lastPlaying != MusicType.Labryinth)
            {
                if(lastPlaying == MusicType.GameOver)
                {
                    gameOverMusicInstance.Stop();
                }
                else if (lastPlaying == MusicType.Win)
                {
                    winMusicInstance.Stop();
                }
                lastPlaying = MusicType.Labryinth;  
            }
            labryinthMusicInstance.IsLooped = true;
            labryinthMusicInstance.Play();
        }

        public void PlayGameOverMusic() {

            if (lastPlaying != MusicType.GameOver)
            {
                if (lastPlaying == MusicType.Labryinth)
                {
                    labryinthMusicInstance.Stop();
                }
                else if (lastPlaying == MusicType.Win)
                {
                    winMusicInstance.Stop();
                }
                lastPlaying = MusicType.Labryinth;
            }
            gameOverMusicInstance.IsLooped = true;
            gameOverMusicInstance.Play();
        }

        public void PlayWinMusic()
        {
            if (lastPlaying != MusicType.Win)
            {
                if (lastPlaying == MusicType.GameOver)
                {
                    gameOverMusicInstance.Stop();
                }
                else if (lastPlaying == MusicType.Labryinth)
                {
                    labryinthMusicInstance.Stop();
                }
                lastPlaying = MusicType.Labryinth;
            }
            winMusicInstance.IsLooped = true;
            winMusicInstance.Play();
        }
    }
}
