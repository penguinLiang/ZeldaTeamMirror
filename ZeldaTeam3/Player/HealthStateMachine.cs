namespace Zelda.Player
{
    internal class HealthStateMachine : ISpawnable
    {
        // 60/60 frames = ~1s
        private const int DeathFrameDelay = 300;
        // 120/60 frames = ~2s
        private const int RespawnFrameDelay = 120;
        private const int HurtResetDelay = 60;

        public bool Alive { get; private set; } = true;
        public bool Visible { get; private set; } = true;
        public bool Hurt { get; private set; }
        public int MaxHealth = 6;
        private int _health;

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


        public HealthStateMachine(){
            MaxHealth = 6;
            _health = 6;
        }

        public void Spawn()
        {
            Alive = true;
            Visible = true;
            _health = 6;
            System.Diagnostics.Debug.WriteLine("HEALTH? "+ _health);
        }

        public void TakeDamage()
        {
            if(Alive){
                _health--;
                Hurt = true;
                System.Diagnostics.Debug.WriteLine("HEALTH? "+ _health);

            }
            
            if(_health<=0){
                Kill();
                }     
                        
        }

        public void Kill()
        {
            Hurt = false;
            Alive = false;
            Visible = false;
        }

        public void Update()
        {
            DyingFramesDelayed++;
            RespawnFramesDelayed++;
            HurtFramesDelayed++;
        }

                public void FullHeal()
        {
            _health = MaxHealth;
        }

        public void Heal()
        {
            if(_health<MaxHealth-1){
            _health+=2;
            }
             else {
                     _health = MaxHealth;
                }
        }
    }
}
