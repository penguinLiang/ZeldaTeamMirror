using Zelda.Dungeon;

namespace Zelda.Commands
{
    internal class Transition : ICommand
    {
        private readonly DungeonManager _dungeonManager;
        private Direction _direction;

        public Transition(DungeonManager dungeonManager, Direction direction)
        {
            _dungeonManager = dungeonManager;
            _direction = direction;
        }

        public void Execute()
        {
            _dungeonManager.Transition(_direction);
        }

        public override string ToString() => "Scene Transition via Door/Stairs";
    }
}
