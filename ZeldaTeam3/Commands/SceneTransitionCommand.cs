namespace Zelda.Commands
{
    internal class SceneTransitionCommand : ICommand
    {
        private readonly IScene _scene;
        private int _row;
        private int _column;

        public SceneTransitionCommand(IScene scene, int row, int column)
        {
            _scene = scene;
            _row = row;
            _column = column;
        }

        public void Execute()
        {
            _scene.TransitionToRoom(_row, _column);
        }

        public override string ToString() => "Scene Transition to specific row/column";
    }
}
