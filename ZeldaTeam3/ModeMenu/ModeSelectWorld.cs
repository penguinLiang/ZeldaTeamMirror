using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zelda.Music;

namespace Zelda.ModeMenu
{
    internal class ModeSelectWorld : IDrawable
    {
        private const float Scale = 2.0f;

        private readonly SpriteBatch _spriteBatch;
        private readonly MainMenuControllerKeyboard _controllerKeyboard;
        private readonly MainMenu _mainMenu;

        public ModeSelectWorld(ZeldaGame game, SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;
            _mainMenu = new MainMenu(game);
            _controllerKeyboard = new MainMenuControllerKeyboard(_mainMenu);
            MusicManager.Instance.PlayIntroMusic();
        }

        public void Update()
        {
            _controllerKeyboard.Update();
        }

        public void Draw()
        {
            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, Matrix.CreateScale(Scale));
            _mainMenu.Draw();
            _spriteBatch.End();
        }
    }
}
