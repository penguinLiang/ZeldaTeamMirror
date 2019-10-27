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
    class MusicManager
    {

      private SoundEffect _playLabryinthMusic;
      private SoundEffect _playGameOverMusic;
      private SoundEffect _playWinMusic;

        public void LoadAllSounds(ContentManager Content)
        {
            _playLabryinthMusic = Content.Load<SoundEffect>("");
            _playGameOverMusic = Content.Load<SoundEffect>("");
            _playWinMusic = Content.Load<SoundEffect>("");

        }

        public void PlayLabryinthMusic()
        {
            
        }

        public void PlayGameOverMusic() {

        }

        public void PlayWinMusic()
        {

        }
    }
}
