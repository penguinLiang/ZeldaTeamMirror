using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda.Projectiles
{
    public class Arrow : IDrawable
    {
        private const int FramesToDisappear = 140;

        private readonly SpriteBatch _spriteBatch;
        private readonly ISprite _sprite;
        private readonly ArrowAndSwordBeamStateMachine _arrowStateMachine;

        private int _framesDelayed;

        public Arrow(SpriteBatch spriteBatch, Vector2 location, Direction direction)
        {
            _spriteBatch = spriteBatch;
            switch (direction)
            {
                case Direction.Up:
                    _sprite = ProjectileSpriteFactory.Instance.CreateArrowUp();
                    break;
                case Direction.Down:
                    _sprite = ProjectileSpriteFactory.Instance.CreateArrowDown();
                    break;
                case Direction.Left:
                    _sprite = ProjectileSpriteFactory.Instance.CreateArrowLeft();
                    break;
                case Direction.Right:
                    _sprite = ProjectileSpriteFactory.Instance.CreateArrowRight();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            _arrowStateMachine = new ArrowAndSwordBeamStateMachine(location, direction);
        }

        public void Update()
        {
            _arrowStateMachine.Update();
            if (_framesDelayed++ == FramesToDisappear)
            {
                _sprite.Hide();
            }
            _sprite.Update();
        }

        public void Draw()
        {

            _sprite.Draw(_spriteBatch, _arrowStateMachine.Location);
        }
    }
}
