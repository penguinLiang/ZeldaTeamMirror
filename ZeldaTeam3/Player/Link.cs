using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda.Player
{
    public class Link : IPlayer
    {
        private readonly SpriteBatch _spriteBatch;
        private readonly MovementStateMachine _movementStateMachine;
        private readonly SpriteStateMachine _spriteStateMachine;
        private readonly HealthStateMachine _healthStateMachine = new HealthStateMachine();

        public Link(SpriteBatch spriteBatch, Vector2 location)
        {
            _spriteBatch = spriteBatch;
            _movementStateMachine = new MovementStateMachine(location);
            _movementStateMachine.Idle();
            _spriteStateMachine = new SpriteStateMachine(_movementStateMachine.Facing);
        }

        public void FaceUp()
        {
            _movementStateMachine.FaceUp();
            _spriteStateMachine.Aim(_movementStateMachine.Facing);
        }

        public void FaceDown()
        {
            _movementStateMachine.FaceDown();
            _spriteStateMachine.Aim(_movementStateMachine.Facing);
        }

        public void FaceLeft()
        {
            _movementStateMachine.FaceLeft();
            _spriteStateMachine.Aim(_movementStateMachine.Facing);
        }

        public void FaceRight()
        {
            _movementStateMachine.FaceRight();
            _spriteStateMachine.Aim(_movementStateMachine.Facing);
        }

        public void MoveUp()
        {
            if (!_spriteStateMachine.UsingItem) _movementStateMachine.MoveUp();
        }

        public void MoveDown()
        {
            if (!_spriteStateMachine.UsingItem) _movementStateMachine.MoveDown();
        }

        public void MoveLeft()
        {
            if (!_spriteStateMachine.UsingItem) _movementStateMachine.MoveLeft();
        }

        public void MoveRight()
        {
            if (!_spriteStateMachine.UsingItem) _movementStateMachine.MoveRight();
        }

        public void Idle()
        {
            _movementStateMachine.Idle();
        }

        public void Spawn()
        {
            _healthStateMachine.Spawn();
        }

        public void TakeDamage()
        {
            _healthStateMachine.TakeDamage();
        }

        public void Kill()
        {
            _healthStateMachine.Kill();
        }

        public void UsePrimaryItem()
        {
            _spriteStateMachine.UsePrimaryItem();
        }

        public void UseSecondaryItem()
        {
            _spriteStateMachine.UseSecondaryItem();
        }

        public void AssignPrimaryItem(Items.Primary item)
        {
            _spriteStateMachine.AssignPrimaryItem(item);
        }

        public void AssignSecondaryItem(Items.Secondary item)
        {
            _spriteStateMachine.AssignSecondaryItem(item);
        }

        public void Update()
        {
            if (!_spriteStateMachine.UsingItem && _movementStateMachine.Idling)
            {
                _spriteStateMachine.Sprite.PauseAnimation();
            }
            else
            {
                _spriteStateMachine.Sprite.PlayAnimation();
            }

            if (_healthStateMachine.Hurt)
            {
                _spriteStateMachine.Sprite.PaletteShift();
            }

            _spriteStateMachine.Update();
            if (!_spriteStateMachine.UsingItem) _movementStateMachine.Update();
            _healthStateMachine.Update();
        }

        public void Draw()
        {
            int yBoundOffset = 0;
            int xBoundOffset = 0;
            // When using an item, the texture atlas offsets the Left and Up directions by 16 pixels from origin
            if (_spriteStateMachine.UsingItem)
            {
                if (_movementStateMachine.Facing == Direction.Left)
                {
                    xBoundOffset = 16;
                } else if (_movementStateMachine.Facing == Direction.Up)
                {
                    yBoundOffset = 16;
                }
            }
            _spriteStateMachine.Sprite.Draw(_spriteBatch, new Vector2(_movementStateMachine.Location.X - xBoundOffset, _movementStateMachine.Location.Y - yBoundOffset));
        }
    }
}
