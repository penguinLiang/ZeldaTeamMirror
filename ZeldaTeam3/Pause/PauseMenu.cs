using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Zelda.Blocks;
using Zelda.Dungeon;
using Zelda.Enemies;
using Zelda.Items;
using Zelda.Music;
using Zelda.Player;
using Zelda.Projectiles;

namespace Zelda.Pause
{
    public class PauseMenu
    {
        private readonly Texture2D _image;
        private readonly SpriteBatch _spriteBatch;
        private IPlayer _player;
        private DungeonManager _dungeonManager;

        private Inventory _inventory = null;
        private int x;
        private int y;

        public bool Visible;

        public PauseMenu(SpriteBatch spriteBatch, ContentManager content, IPlayer player, DungeonManager dungeon)
        {
            _spriteBatch = spriteBatch;
            _image = content.Load<Texture2D>("PauseScreen");
            _player = player;
            _dungeonManager = dungeon;
            Visible = true;
            x = 0;
            y = 0;
            _inventory = _player.Inventory;
        }

        public void Draw()
        {
            if (Visible)
            {
                _spriteBatch.Draw(_image, new Rectangle(0, 0, 512, 352), Color.White);
                if (_inventory.HasMap)
                {
                    
                } else
                {
                    _spriteBatch.Draw(_image, new Rectangle(190, 158, 270, 176), Color.Black);
                }
                if (_inventory.HasCompass)
                {
                    //display compass
                }
                if (_inventory.HasArrow)
                {
                    //display arrow??
                }
                if (_inventory.HasBow)
                {
                    //display bow
                }
            }
        }

        public void unpause()
        {
            if(Visible == false)
            {
                Visible = true;
            } else
            {
                Visible = false;
            }
            System.Threading.Thread.Sleep(100);
        }

        public void selectUp()
        {
            if(y != 0)
            {
                y--;
            }
        }

        public void selectDown()
        {
            if(y != 1)
            {
                y++;
            }
        }

        public void selectLeft()
        {
            if (x != 0)
            {
                x--;
            }
        }

        public void selectRight()
        {
            if (x != 3)
            {
                x++;
            }
        }
    }
}

