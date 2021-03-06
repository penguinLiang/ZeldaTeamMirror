﻿namespace Zelda.Commands
{
    internal class Transition : ICommand
    {
        private readonly IDungeonManager _dungeonManager;
        private readonly Direction _direction;
        private readonly bool _unlock;

        public Transition(IDungeonManager dungeonManager, Direction direction, bool unlock = false)
        {
            _dungeonManager = dungeonManager;
            _direction = direction;
            _unlock = unlock;
        }

        public void Execute()
        {
            _dungeonManager.Transition(_direction, _unlock);
        }

        public override string ToString() => "Scene Transition via Door/Stairs";
    }
}
