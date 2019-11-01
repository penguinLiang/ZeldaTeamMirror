using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace Zelda.Music
{
    public class MusicManager
    {
        private SoundEffect _labryinthMusic;
        private SoundEffect _gameOverMusic;
        private SoundEffect _winMusic;
        private SoundEffect _triforceMusic;
        private SoundEffectInstance _activeMusic;
        private MusicType _playing = MusicType.None;

        public static MusicManager Instance { get; } = new MusicManager();

        public void LoadAllSounds(ContentManager content)
        {
            _labryinthMusic = content.Load<SoundEffect>("Music/Dungeon");
            _gameOverMusic = content.Load<SoundEffect>("Music/07_Game_Over");
            _winMusic = content.Load<SoundEffect>("Music/10_Ending");
            _triforceMusic = content.Load<SoundEffect>("Music/06_Triforce");
        }

        public void PlayLabryinthMusic()
        {
            if (_playing == MusicType.Labryinth)
            {
                _activeMusic.Resume();
                return;
            }

            _playing = MusicType.Labryinth;
            _activeMusic?.Stop();
            _activeMusic = _labryinthMusic.CreateInstance();
            _activeMusic.IsLooped = true;
            _activeMusic.Play();
        }

        public void PlayGameOverMusic()
        {
            if (_playing == MusicType.GameOver)
            {
                _activeMusic.Resume();
                return;
            }

            _playing = MusicType.GameOver;
            _activeMusic?.Stop();
            _activeMusic = _gameOverMusic.CreateInstance();
            _activeMusic.IsLooped = false;
            _activeMusic.Play();
        }

        public void PlayWinMusic()
        {
            if (_playing == MusicType.Win)
            {
                _activeMusic.Resume();
                return;
            }

            _playing = MusicType.Win;
            _activeMusic?.Stop();
            _activeMusic = _winMusic.CreateInstance();
            _activeMusic.IsLooped = true;
            _activeMusic.Play();
        }

        public void PlayTriforceMusic()
        {
            if (_playing == MusicType.Triforce)
            {
                _activeMusic.Resume();
                return;
            }

            _playing = MusicType.Triforce;
            _activeMusic?.Stop();
            _activeMusic = _triforceMusic.CreateInstance();
            _activeMusic.IsLooped = false;
            _activeMusic.Play();
        }

        public void PauseMusic()
        {
            _activeMusic?.Pause();
        }

        public void StopMusic()
        {
            _playing = MusicType.None;
        }

    }
}
