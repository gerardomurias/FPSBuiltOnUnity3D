using UnityEngine;

public interface IBleed
{
    ParticleSystem ParticleSystem { get; set; }

    void Bleed();
}
