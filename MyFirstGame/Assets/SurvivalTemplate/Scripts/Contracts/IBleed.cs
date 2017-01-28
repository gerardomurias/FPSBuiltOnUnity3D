using UnityEngine;

namespace Assets.SurvivalTemplate.Scripts.Contracts
{
    public interface IBleed
    {
        ParticleSystem ParticleSystem { get; set; }

        void Bleed();
    }
}
