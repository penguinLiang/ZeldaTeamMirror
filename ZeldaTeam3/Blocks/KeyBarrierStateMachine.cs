using System;
using Microsoft.Xna.Framework;
using Zelda.Commands;
using Zelda.Dungeon;
using Zelda.ShaderEffects;
using Zelda.SoundEffects;

// ReSharper disable SwitchStatementMissingSomeCases (missing cases handled at run time)
namespace Zelda.Blocks
{
    internal class KeyBarrierStateMachine
    {
        private readonly BlockType _block;
        public bool _unlocked;
        public Rectangle Bounds { get; }
        private Point _location;


        private static BlockType UnlockedType(BlockType block)
        {
            return BlockType.Sand;
        }

        public KeyBarrierStateMachine()
        {
           
         
        }

    }

}
