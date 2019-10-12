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
        private readonly SecondaryItemAgent _secondaryItemAgent;

        public Link(SpriteBatch spriteBatch, Vector2 location)
        {
            _spriteBatch = spriteBatch;
            _movementStateMachine = new MovementStateMachine(location);
            _movementStateMachine.Idle();
            _spriteStateMachine = new SpriteStateMachine(_movementStateMachine.Facing);
            _secondaryItemAgent = new SecondaryItemAgent(_spriteBatch);
        }

        public void FaceUp()
        {
            if (_spriteStateMachine.UsingItem) return;
            _movementStateMachine.FaceUp();
            _spriteStateMachine.Aim(_movementStateMachine.Facing);
        }

        public void FaceDown()
        {
            if (_spriteStateMachine.UsingItem) return;
            _movementStateMachine.FaceDown();
            _spriteStateMachine.Aim(_movementStateMachine.Facing);
        }

        public void FaceLeft()
        {
            if (_spriteStateMachine.UsingItem) return;
            _movementStateMachine.FaceLeft();
            _spriteStateMachine.Aim(_movementStateMachine.Facing);
        }

        public void FaceRight()
        {
            if (_spriteStateMachine.UsingItem) return;
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

        
        public void Heal()
        {
            _healthStateMachine.Heal();
        }

        public void FullHeal()
        {
            _healthStateMachine.FullHeal();
        }

        public void Spawn()
        {
            _healthStateMachine.Spawn();
        }

        public void TakeDamage()
        {
            if(_healthStateMachine.Hurt) return;
            _healthStateMachine.TakeDamage();
            if(!_healthStateMachine.Alive){
                Kill();
            }
        }

        public void Kill()
        {     
            //if(!_spriteStateMachine.Dying) {
                _spriteStateMachine.Dying = true;
                //}
               if(_spriteStateMachine.Dying){
                 _spriteStateMachine.Kill();
                }

        }

        public void UsePrimaryItem()
        {
            if (_spriteStateMachine.UsingItem) return;
            _spriteStateMachine.UsePrimaryItem();
        }

        public void UseSecondaryItem()
        {
            if (_spriteStateMachine.UsingItem) return;
            _spriteStateMachine.UseSecondaryItem();
            _secondaryItemAgent.UseSecondaryItem(_movementStateMachine.Facing, _movementStateMachine.Location);
        }

        public void AssignPrimaryItem(Items.Primary item)
        {
            _spriteStateMachine.AssignPrimaryItem(item);
        }

        public void AssignSecondaryItem(Items.Secondary item)
        {
            _spriteStateMachine.AssignSecondaryItem(item);
            _secondaryItemAgent.AssignSecondaryItem(item);
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
            _secondaryItemAgent.Update();
            if (!_spriteStateMachine.UsingItem) _movementStateMachine.Update();
            _healthStateMachine.Update();

            if(!_healthStateMachine.Alive&&_spriteStateMachine.Dying){
                Kill();
            }
        }

        // When using a primary item, the sprite is offset from the origin of the bounding box for the Left and Up directions by 16 pixels,
        // this was chosen in contrast to making every sprite 32x32 and calculating the origin of the bounding box for all directions
        private Vector2 AdjustedDrawLocation()
        {
            var drawLocation = _movementStateMachine.Location;
            if (!_spriteStateMachine.UsingPrimaryItem) return drawLocation;

            if (_movementStateMachine.Facing == Direction.Left)
            {
                drawLocation.X -= 16;
            }
            else if (_movementStateMachine.Facing == Direction.Up)
            {
                drawLocation.Y -= 16;
            }

            return drawLocation;
        }

        public void Draw()
        {
            _secondaryItemAgent.Draw();
            _spriteStateMachine.Sprite.Draw(_spriteBatch, AdjustedDrawLocation());
        }
    }
}
