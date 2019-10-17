using Zelda.Dungeon;

namespace Zelda.Commands
{
    internal class SceneTransition : ICommand
    {
        private readonly DungeonManager _dungeonManager;
        private readonly int _row;
        private readonly int _column;

        public SceneTransition(DungeonManager dungeonManager, int row, int column)
        {
            _dungeonManager = dungeonManager;
            _row = row;
            _column = column;
        }

        public void Execute()
        {
            _dungeonManager.TransitionToRoom(_row, _column);
        }

        public override string ToString() => "Scene Transition to specific row/column";
    }
}
