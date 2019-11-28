using Microsoft.Xna.Framework.Graphics;

namespace Zelda.ShaderEffects
{
    class PartyTime : IUpdatable
    {
        private const float CycleVelocity = 0.01f;
        private const float CycleCap = 0.51f;
        private const float CycleReset = -0.50f;
        private float _cycle = CycleReset;

        public static Effect ShaderEffect { set; private get; }

        public void Apply()
        {
            ShaderEffect.Techniques[0].Passes[0].Apply();
        }

        public void Update()
        {
            if (_cycle > CycleCap)
            {
                _cycle = CycleReset;
            }

            _cycle += CycleVelocity;
            ShaderEffect.Parameters["InSaturationOffset"].SetValue(_cycle);
        }
    }
}
