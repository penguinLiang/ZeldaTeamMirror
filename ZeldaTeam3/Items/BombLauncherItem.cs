using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.SoundEffects;

namespace Zelda.Items
{
    internal class BombLauncherItem : Item
    {

        public int _price;
        public BombLauncherItem(Point location, int price = 0) : base(location, price)
        {
           _price = price;
        }

        protected override ISprite Sprite { get; } = ItemSpriteFactory.Instance.CreateMap();
        //TODO: Fix this with the proper sprite
        
        public override ICommand PlayerEffect(IPlayer player)
        {
            Used = true;
            if(_price>0){
                if(player.Inventory.TryRemoveRupee(_price)){
                    SoundEffectManager.Instance.PlayPickupNewItem();
                    return new AddSecondaryItem(player, Secondary.BombLauncher);
                }
                Used = false;
                return new NoOp();
            }
            SoundEffectManager.Instance.PlayPickupItem();
            return new AddSecondaryItem(player, Secondary.BombLauncher);
        }
    }
}
