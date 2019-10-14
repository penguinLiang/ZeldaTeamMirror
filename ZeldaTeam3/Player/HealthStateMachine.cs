namespace Zelda.Player
{
    internal class HealthStateMachine : ISpawnable
    {
        // 60/60 frames = ~1s
        private const int DeathFrameDelay = 60;
        // 120/60 frames = ~2s
        private const int RespawnFrameDelay = 120;
        private const int HurtResetDelay = 120;

        public bool Alive { get; private set; } = true;
        public bool Visible { get; private set; } = true;
        public bool Hurt { get; private set; }

        private int _dyingFramesDelayed;
        private int _respawnFramesDelayed;
        private int _hurtFramesDelayed;

        private int DyingFramesDelayed
        {
            get => _dyingFramesDelayed;
            set
            {
                if (Alive) return;
                _dyingFramesDelayed = value;

                if (_dyingFramesDelayed != DeathFrameDelay) return;
                Visible = false;
                _dyingFramesDelayed = 0;
            }
        }

        private int RespawnFramesDelayed
        {
            get => _respawnFramesDelayed;
            set
            {
                if (Alive) return;
                _respawnFramesDelayed = value;

                if (_respawnFramesDelayed != RespawnFrameDelay) return;
                Alive = true;
                Visible = true;
                _respawnFramesDelayed = 0;
            }
        }

        private int HurtFramesDelayed
        {
            get => _hurtFramesDelayed;
            set
            {
                if (!Hurt) return;
                _hurtFramesDelayed = value;

                if (_hurtFramesDelayed != HurtResetDelay) return;
                Hurt = false;
                _hurtFramesDelayed = 0;
            }
        }

        public void Spawn()
        {
            Alive = true;
            Visible = true;
        }

        public void TakeDamage()
        {
            Hurt = true;
        }

        public void Stun()
        {
            throw new System.NotImplementedException();
        }

        public void Update()
        {
            DyingFramesDelayed++;
            RespawnFramesDelayed++;
            HurtFramesDelayed++;
        }

        public void FullHeal()
        {
            throw new System.NotImplementedException();
        }

        public void Heal()
        {
            throw new System.NotImplementedException();
        }
    }
}
