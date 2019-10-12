using System;

namespace Zelda.Player
{
    internal class SpriteStateMachine
    {
        // 50/60 frames = ~5/6s and ~1 full animation
        private const int AttackResetDelay = 50;
        private const int DeathFrameDelay = 3;
        //Right now, the only way to get this to display all frames is to use 3 as the delay. Still figuring that one out

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
                _lastFacing = _facing;

                if(DyingFrames == 0){
                _lastFacing = Direction.Down;
                }


                switch (_lastFacing) 
                {
                    case Direction.Left:
                    _facing = Direction.Up;
                    break;
                    case Direction.Up:
                    _facing = Direction.Right;
                    break;
                    case Direction.Right:
                    _facing = Direction.Down;
                    break;
                    case Direction.Down:
                    _facing = Direction.Left;
                     break;
                }

            //DyingFrames== current frame of death animation
            //DeathFrameDelayed == how many updates have passed?
            //DeathFrameDelay == how many frames pass before you move on to the next dying frame

           // System.Diagnostics.Debug.WriteLine("DeathAnimationFramesDelayed: "+DeathAnimationFramesDelayed);
            if(DeathAnimationFramesDelayed == DeathFrameDelay) {
                //With diff delay numbers, this never triggers? Potential bug with the way I'm updating the numbers?
                System.Diagnostics.Debug.WriteLine("DyingFrames: " + DyingFrames);
                _deathAnimationFramesDelayed = 0;
            if(DyingFrames < 17) {
                     ChangeSprite(_facing);
                    }
             else if(DyingFrames>=17 && DyingFrames<20){
                    Sprite = LinkSpriteFactory.Instance.CreateNoWeapon(Direction.Down);
                    //fade to gray
                }
               else if(DyingFrames>=20 && DyingFrames<25){
                    //Death Sparkle
                    System.Diagnostics.Debug.WriteLine("Death Sparkle");
                }
                 else {
                    Sprite.Hide();
                   Dying = false;
                }
                            DyingFrames++;
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
