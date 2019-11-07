using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace Zelda.SoundEffects
{
    class SoundEffectManager
    {
        public static SoundEffectManager Instance { get; } = new SoundEffectManager();

        private SoundEffect _playSwordSlash;
        private SoundEffect _playSwordShoot;
        private SoundEffect _playArrowBoomerangShoot;
        private SoundEffect _playBombDrop;
        private SoundEffect _playBombExplode;
        private SoundEffect _playEnemyHit;
        private SoundEffect _playEnemyDie;
        private SoundEffect _playLinkHurt;
        private SoundEffect _playLowHealth;
        private SoundEffectInstance lowHealthInstance;
            //For looping and stopping the sound
        private SoundEffect _playPickupItem;
        //Also used for Fairy sound effect
        private SoundEffect _playPickupNewItem;
        private SoundEffect _playPickupDoppedHeartKey;
        private SoundEffect _playPickupRupee;
        private SoundEffect _playKeyAppear;
        private SoundEffect _playDoorUnlock;
        private SoundEffect _playBossSpawn;
        private SoundEffect _playBossDead;
        private SoundEffect _playBossHurt;
        private SoundEffect _playUseStairs;
        private SoundEffect _playPuzzleSolved;

        public void LoadAllSounds(ContentManager Content)
        {
            _playArrowBoomerangShoot = Content.Load<SoundEffect>("Sounds/LOZ_Arrow_Boomerang");
            _playBombDrop = Content.Load<SoundEffect>("Sounds/LOZ_Bomb_Drop");
            _playBombExplode = Content.Load<SoundEffect>("Sounds/LOZ_Bomb_Blow");
            _playBossSpawn = Content.Load<SoundEffect>("Sounds/LOZ_Boss_Scream1");
            _playBossHurt = Content.Load<SoundEffect>("Sounds/LOZFDS_Boss_Hit");
            _playBossDead = Content.Load<SoundEffect>("Sounds/LOZFDS_Boss_Scream1_Distant");
            _playDoorUnlock = Content.Load<SoundEffect>("Sounds/LOZ_Door_Unlock");
            _playEnemyDie = Content.Load<SoundEffect>("Sounds/LOZ_Enemy_Die");
            _playEnemyHit = Content.Load<SoundEffect>("Sounds/LOZ_Enemy_Hit");
            _playKeyAppear = Content.Load<SoundEffect>("Sounds/LOZ_Key_Appear");
            _playLinkHurt = Content.Load<SoundEffect>("Sounds/LOZ_Link_Hurt");
            _playLowHealth = Content.Load<SoundEffect>("Sounds/LOZ_LowHealth");
            _playPickupDoppedHeartKey = Content.Load<SoundEffect>("Sounds/LOZ_Get_Heart");
            _playPickupItem = Content.Load<SoundEffect>("Sounds/LOZ_Get_Item");
            _playPickupRupee = Content.Load<SoundEffect>("Sounds/LOZ_Get_Rupee");
            _playPickupNewItem = Content.Load<SoundEffect>("Sounds/LOZ_Fanfare");
            _playPuzzleSolved = Content.Load<SoundEffect>("Sounds/LOZ_Secret");
            _playSwordShoot = Content.Load<SoundEffect>("Sounds/LOZ_Sword_Shoot");
            _playSwordSlash = Content.Load<SoundEffect>("Sounds/LOZ_Sword_Slash");
            _playUseStairs = Content.Load<SoundEffect>("Sounds/LOZ_Stairs");

            lowHealthInstance = _playLowHealth.CreateInstance();

        }



        public void PlayArrowBoomerangShoot()
        {
            SoundEffectInstance arrowBoomerangInstance = _playArrowBoomerangShoot.CreateInstance();
            arrowBoomerangInstance.Play();
        }

        public void PlayBombDrop()
        {
            SoundEffectInstance bombDropInstance = _playBombDrop.CreateInstance();
            bombDropInstance.Play();
        }

        public void PlayBombExplode()
        {
            SoundEffectInstance bombExplodeInstance = _playBombExplode.CreateInstance();
            bombExplodeInstance.Play();
        }

        public void PlayBossSpawn()
        {
            SoundEffectInstance bossSpawnInstance = _playBossSpawn.CreateInstance();
            bossSpawnInstance.Play();
        }

        public void PlayBossHurt()
        {
            SoundEffectInstance bossHurtInstance = _playBossHurt.CreateInstance();
            bossHurtInstance.Play();
        }

        public void PlayBossDead()
        {
            SoundEffectInstance bossDeadInstance = _playBossDead.CreateInstance();
            bossDeadInstance.Play();
        }

        public void PlayDoorUnlock()
        {
            SoundEffectInstance doorUnlockInstance = _playDoorUnlock.CreateInstance();
            doorUnlockInstance.Play();
        }

        public void PlayEnemyDie()
        {
            SoundEffectInstance enemyDieInstance = _playEnemyDie.CreateInstance();
            enemyDieInstance.Play();
        }

        public void PlayEnemyHit()
        {
            SoundEffectInstance enemyHitInstance = _playEnemyHit.CreateInstance();
            enemyHitInstance.Play();
        }

        public void PlayKeyAppear()
        {
            SoundEffectInstance keyAppearInstance = _playKeyAppear.CreateInstance();
            keyAppearInstance.Play();
        }

        public void PlayLinkHurt()
        {
            SoundEffectInstance linkHurtInstance = _playLinkHurt.CreateInstance();
            linkHurtInstance.Play();
        }

        public void PlayLowHealth()
        {
            lowHealthInstance.IsLooped = true;
            lowHealthInstance.Play();
        }

        public void StopLowHealthSound()
        {
            lowHealthInstance.Stop();
        }

        public void PlayPickupDroppedHeartKey()
        {
            SoundEffectInstance pickupDroppedHeartKeyInstance = _playPickupDoppedHeartKey.CreateInstance();
            pickupDroppedHeartKeyInstance.Play();
        }

        public void PlayPickupItem()
        {
            SoundEffectInstance pickupItemInstance = _playPickupItem.CreateInstance();
            pickupItemInstance.Play();
        }

        public void PlayPickupRupee()
        {
            SoundEffectInstance pickupRupeeInstance = _playPickupRupee.CreateInstance();
            pickupRupeeInstance.Play();
        }

        public void PlayPickupNewItem()
        {
            SoundEffectInstance pickupNewItem = _playPickupNewItem.CreateInstance();
            pickupNewItem.Play();
        }

        public void PlayPuzzleSolved()
        {
            SoundEffectInstance puzzleSolvedInstance = _playPuzzleSolved.CreateInstance();
            puzzleSolvedInstance.Play();
        }

        public void PlaySwordShoot()
        {
            SoundEffectInstance swordShootInstance = _playSwordShoot.CreateInstance();
            swordShootInstance.Play();
        }

        public void PlaySwordSlash()
        {
            SoundEffectInstance swordSlashInstance = _playSwordSlash.CreateInstance();
            swordSlashInstance.Play();
        }

        public void PlayUseStairs()
        {
            SoundEffectInstance useStairsInstance = _playUseStairs.CreateInstance();
            useStairsInstance.Play();
        }

    }
}


