using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda.Player
{
    internal class LinkSpriteFactory
    {
        private const int FrameDelay = 10;

        private const int Width = 16;
        private const int Height = 16;
        private const int SwordUpDownWidth = 16;
        private const int SwordUpDownHeight = 32;
        private const int SwordLeftRightWidth = 32;
        private const int SwordLeftRightHeight = 16;

        private const int PaletteCount = 4;
        private const int PaletteHeight = Height * 4;
        private const int SwordPaletteHeight = SwordUpDownHeight * 2 + SwordLeftRightHeight * 2;

        private const int FrameCount = 2;
        private const int AttackFrameCount = 4;
        private const int UseSecondaryFrameCount = 1;

        private readonly Dictionary<Direction, Point> _offset;
        private readonly Dictionary<Direction, Point> _swordOffset;
        private readonly Dictionary<Direction, Point> _useSecondaryOffset;

        private readonly Dictionary<Direction, int> _swordWidth;
        private readonly Dictionary<Direction, int> _swordHeight;

        private Texture2D _noWeaponSpritesheet;
        private Texture2D _swordWoodenSpritesheet;
        private Texture2D _swordWhiteSpritesheet;
        private Texture2D _swordMagicalSpritesheet;
        private Texture2D _useSecondarySpritesheet;
        private Texture2D _characterDeathSpritesheet;

        public static LinkSpriteFactory Instance { get; } = new LinkSpriteFactory();

        public LinkSpriteFactory()
        {
            _offset = new Dictionary<Direction, Point>
            {
                { Direction.Up, new Point(0, Height * 3) },
                { Direction.Down, new Point(0, 0) },
                { Direction.Left, new Point(0, Height * 2) },
                { Direction.Right, new Point(0, Height) }
            };

            _swordOffset = new Dictionary<Direction, Point>
            {
                { Direction.Up, new Point(0, SwordUpDownHeight) },
                { Direction.Down, new Point(0, 0) },
                { Direction.Left, new Point(0, SwordUpDownHeight * 2 + SwordLeftRightHeight) },
                { Direction.Right, new Point(0, SwordUpDownHeight * 2) }
            };

            _useSecondaryOffset = new Dictionary<Direction, Point>
            {
                { Direction.Up, new Point(Width * 3, 0) },
                { Direction.Down, new Point(0, 0) },
                { Direction.Left, new Point(Width * 2, 0) },
                { Direction.Right, new Point(Width, 0) }
            };

            _swordWidth = new Dictionary<Direction, int>
            {
                { Direction.Up, SwordUpDownWidth },
                { Direction.Down, SwordUpDownWidth },
                { Direction.Left, SwordLeftRightWidth },
                { Direction.Right, SwordLeftRightWidth }
            };

            _swordHeight = new Dictionary<Direction, int>
            {
                { Direction.Up, SwordUpDownHeight },
                { Direction.Down, SwordUpDownHeight },
                { Direction.Left, SwordLeftRightHeight },
                { Direction.Right, SwordLeftRightHeight }
            };
        }

        public void LoadAllTextures(ContentManager content)
        {
            _noWeaponSpritesheet = content.Load<Texture2D>("LinkNoWeapon");
            _swordWoodenSpritesheet = content.Load<Texture2D>("LinkSwordWooden");
            _swordWhiteSpritesheet = content.Load<Texture2D>("LinkSwordWhite");
            _swordMagicalSpritesheet = content.Load<Texture2D>("LinkSwordMagical");
            _useSecondarySpritesheet = content.Load<Texture2D>("LinkUseSecondary");
            _characterDeathSpritesheet = content.Load<Texture2D>("CharacterDeath");
        }

        public ISprite CreateNoWeapon(Direction direction)
        {
            return new Sprite(_noWeaponSpritesheet, Width, Height, FrameCount, _offset[direction], FrameDelay, PaletteHeight, PaletteCount);
        }

        public ISprite CreateWoodenSword(Direction direction)
        {
            return new Sprite(_swordWoodenSpritesheet, _swordWidth[direction], _swordHeight[direction], AttackFrameCount, _swordOffset[direction], FrameDelay, SwordPaletteHeight, PaletteCount);
        }

        public ISprite CreateWhiteSword(Direction direction)
        {
            return new Sprite(_swordWhiteSpritesheet, _swordWidth[direction], _swordHeight[direction], AttackFrameCount, _swordOffset[direction], FrameDelay, SwordPaletteHeight, PaletteCount);
        }

        public ISprite CreateMagicalSword(Direction direction)
        {
            return new Sprite(_swordMagicalSpritesheet, _swordWidth[direction], _swordHeight[direction], AttackFrameCount, _swordOffset[direction], FrameDelay, SwordPaletteHeight, PaletteCount);
        }

        public ISprite CreateUseSecondary(Direction direction)
        {
            return new Sprite(_useSecondarySpritesheet, Width, Height, UseSecondaryFrameCount, _useSecondaryOffset[direction], FrameDelay, SwordPaletteHeight, PaletteCount);
        }

        public ISprite CreateDeadLink()
        {
            return new Sprite(_noWeaponSpritesheet, 16, 16, 1, new Point(0, 272));
        }

        public ISprite CreateLinkDeathSparkle() {
            return new Sprite(_characterDeathSpritesheet, 16, 16, 8, new Point(0,0), 10);
        }

    }
}
