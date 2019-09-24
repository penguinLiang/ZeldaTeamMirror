using System;

namespace Zelda.Player
{
    class SpriteStateMachine
    {
        // 50/60 frames = ~5/6s and ~1 full animation
        private const int AttackResetDelay = 50;

        public ISprite Sprite { get; private set; }

        private Direction _facing;
        private Items.Primary _primaryItem;
        private Items.Secondary _secondaryItem;

        // Memoizing the primary item and direction prevents sprite thrashing
        private Direction _lastFacing;

        private int _attackResetFramesDelayed;
        private bool _usingSecondaryItem;
        private bool _usingPrimaryItem;

        public SpriteStateMachine(Direction facing)
        {
            _facing = facing;
            _lastFacing = _facing;
            ChangeSprite(_facing);
        }

        public bool UsingItem => _usingPrimaryItem || _usingSecondaryItem;

        private int AttackResetFramesDelayed
        {
            get => _attackResetFramesDelayed;
            set
            {
                if (!UsingItem) return;
                _attackResetFramesDelayed = value;

                if (_attackResetFramesDelayed != AttackResetDelay) return;
                _usingPrimaryItem = false;
                _usingSecondaryItem = false;
                ChangeSprite(_facing);

                _attackResetFramesDelayed = 0;
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
            if (_usingPrimaryItem)
            {
                ChangePrimaryItemSprite(direction);
            }
            else if (_usingSecondaryItem)
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
            _secondaryItem = item;
        }

        public void UsePrimaryItem()
        {
            _usingPrimaryItem = true;
            ChangeSprite(_facing);
        }

        public void UseSecondaryItem()
        {
            _usingSecondaryItem = true;
            ChangeSprite(_facing);
        }

        public void Update()
        {
            AttackResetFramesDelayed++;
            Sprite.Update();
        }
    }
}
