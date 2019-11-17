namespace Zelda.Survival.GameState
{
    internal class PlayerLockCamera
    {
        private readonly IPlayer _player;

        public PlayerLockCamera(IPlayer player)
        {
            _player = player;
        }

        public float X => (Dimensions.MapWidth - Dimensions.PlayerWidth) / 2.0f - _player.Location.X;
        public float Y => (Dimensions.MapHeight - Dimensions.PlayerHeight) / 2.0f - _player.Location.Y;
    }
}
