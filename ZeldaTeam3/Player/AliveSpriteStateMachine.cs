using System;

namespace Zelda.Player
{
    /*
     * Manages the sprite used for Link while alive given:
     *  - Direction
     *  - Primary weapon
     *  - Using an item
     */
    internal class AliveSpriteStateMachine : IUpdatable
    {
        public ISprite Sprite { get; private set; }
        public bool UsingSecondaryItem { get; private set; }
        public bool UsingPrimaryItem { get; private set; }

        private Direction _facing;
        private Items.Primary _primaryItem;

        // Memoizing the direction prevents sprite thrashing
        private Direction _lastFacing;

        public AliveSpriteStateMachine(Direction facing)
        {
            _facing = facing;
            _lastFacing = _facing;
            ChangeSprite(_facing);
        }

        public bool UsingItem => UsingPrimaryItem || UsingSecondaryItem;

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

        public void UsePrimaryItem(Items.Primary primaryItem)
        {
            _primaryItem = primaryItem;
            UsingPrimaryItem = true;
            ChangeSprite(_facing);
        }

        public void UseSecondaryItem()
        {
            UsingSecondaryItem = true;
            ChangeSprite(_facing);
        }

        public void Update()
        {
            Sprite.Update();
        }
    }
}
