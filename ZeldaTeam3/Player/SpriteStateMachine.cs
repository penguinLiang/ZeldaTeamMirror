using System;

namespace Zelda.Player
{
    internal class SpriteStateMachine
    {
        // 50/60 frames = ~5/6s and ~1 full animation
        private const int AttackResetDelay = 50;
        private const int DeathFrameDelay = 7;

        public ISprite Sprite { get; private set; }
        public bool UsingSecondaryItem { get; private set; }
        public bool UsingPrimaryItem { get; private set; }
        public bool Dying {get; set; }
        public int DyingFrames {get; private set;}

        private Direction _facing;
        private Items.Primary _primaryItem;

        // Memoizing the direction prevents sprite thrashing
        private Direction _lastFacing;

        private int _attackResetFramesDelayed;
        private int _deathAnimationFramesDelayed;

        public SpriteStateMachine(Direction facing)
        {
            _facing = facing;
            _lastFacing = _facing;
            ChangeSprite(_facing);
            DyingFrames = 0;
            Dying = false;
        }

        public bool UsingItem => UsingPrimaryItem || UsingSecondaryItem;

        private int AttackResetFramesDelayed
        {
            get => _attackResetFramesDelayed;
            set
            {
                if (!UsingItem) return;
                _attackResetFramesDelayed = value;

                if (_attackResetFramesDelayed != AttackResetDelay) return;
                UsingPrimaryItem = false;
                UsingSecondaryItem = false;
                ChangeSprite(_facing);

                _attackResetFramesDelayed = 0;
            }
        }

        private int DeathAnimationFramesDelayed 
            {
            get => _deathAnimationFramesDelayed;
            set {
                if(!Dying) return;
                _deathAnimationFramesDelayed = value;
                }
        }
        private void ChangePrimaryItemSprite(Direction direction)
        {
            switch (_primaryItem)
            {
                case Items.Primary.Sword:
                    Sprite = LinkSpriteFactory.Instance.CreateWoodenSword(direction);
                    break;
                case Items.Primary.WhiteSword:
                    Sprite = LinkSpriteFactory.Instance.CreateWhiteSword(direction);
                    break;
                case Items.Primary.MagicalSword:
                    Sprite = LinkSpriteFactory.Instance.CreateMagicalSword(direction);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ChangeSprite(Direction direction)
        {
            if (UsingPrimaryItem)
            {
                ChangePrimaryItemSprite(direction);
            }
            else if (UsingSecondaryItem)
            {
                Sprite = LinkSpriteFactory.Instance.CreateUseSecondary(direction);
            }
            else
            {
                Sprite = LinkSpriteFactory.Instance.CreateNoWeapon(direction);
            }
        }

        public void Aim(Direction aim)
        {
            _facing = aim;

            if (_facing == _lastFacing) return;
            ChangeSprite(_facing);
            _lastFacing = _facing;
        }

        public void AssignPrimaryItem(Items.Primary item)
        {
            _primaryItem = item;
        }

        public void AssignSecondaryItem(Items.Secondary item)
        {
            // NO-OP: The sprite maintains the same apperance regardless of item
        }

        public void UsePrimaryItem()
        {
            UsingPrimaryItem = true;
            ChangeSprite(_facing);
        }

        public void UseSecondaryItem()
        {
            UsingSecondaryItem = true;
            ChangeSprite(_facing);
        }

        public void Kill(){
            UsingPrimaryItem = false;
            UsingSecondaryItem = false;
            _lastFacing = _facing;

                switch (_lastFacing) 
                {
                    case Direction.Right:
                    _facing = Direction.Up;
                    break;
                    case Direction.Up:
                    _facing = Direction.Left;
                    break;
                    case Direction.Left:
                    _facing = Direction.Down;
                    break;
                    case Direction.Down:
                    _facing = Direction.Right;
                     break;
                }

            //DyingFrames== current frame of death animation
            //DeathFrameDelayed == how many updates have passed?
            //DeathFrameDelay == how many frames pass before you move on to the next dying frame

            if(DeathAnimationFramesDelayed == DeathFrameDelay) {
                DyingFrames++;
                _deathAnimationFramesDelayed = 0;
                ChangeSprite(_facing);  
            if(DyingFrames > 15) {
            Dying = false;
            //TODO: Add in 'Death Sparkle'
            //Add in Link fading to gray
            //Add in Link Sprite disappearing

            return;
            }
            }
          }   


        public void Update()
        {   

            DeathAnimationFramesDelayed++;     
            AttackResetFramesDelayed++;
            Sprite.Update();
        }
    }
}
