using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(Stats),typeof(AudioSource), typeof(Animation))]
public class EnemyBehavior : MonoBehaviour, IHittable, IDie
{
    [SerializeField]
    private Stats _enemyStats;

    [HideInInspector]
    [SerializeField]
    private AudioSource[] _audioSources;

    [HideInInspector]
    [SerializeField]
    private AudioClip _woundedAudioClip;

    [HideInInspector]
    [SerializeField]
    private AudioClip _deadAudioClip;

    [SerializeField]
    private Animation _deathAnimation;

    [SerializeField]
    private AIPath _aiPath;



    public Stats EnemyStats
    {
        get { return _enemyStats; }
        set { _enemyStats = value; }
    }

    public AudioSource[] AudioSources
    {
        get { return _audioSources; }
        set { _audioSources = value; }
    }

    public Animation DeathAnimation
    {
        get { return _deathAnimation; }
        set { _deathAnimation = value; }
    }

    public AIPath AiPath
    {
        get { return _aiPath; }
        set { _aiPath = value; }
    }


    void Start()
    {
        InitializeEnemyStats();
        InitializeAudioSource();
        InitializeAnimations();
        InitalizePathFinding();
    }

    private void InitalizePathFinding()
    {
        AiPath = GetComponent<AIPath>();
    }

    private void InitializeAnimations()
    {
        DeathAnimation = GetComponent<Animation>();

        if (DeathAnimation == null)
        {
            throw new MissingComponentException("DeathAnimation component missing in Enemy Class");
        }
    }

    private void InitializeAudioSource()
    {
        AudioSources = GetComponents<AudioSource>();

        _woundedAudioClip = AudioSources[0].clip;
        _deadAudioClip = AudioSources[1].clip;
    }

    private void InitializeEnemyStats()
    {
        EnemyStats = GetComponent<Stats>();

        if (EnemyStats == null)
        {
            throw new MissingComponentException("Stats component missing in Enemy Class");
        }
    }

    void Update()
    {

    }

    public void Hit()
    {
        EnemyStats.Health -= 10;

        if (EnemyStats.Health <= 0)
        {
            Die();
        }
        else
        {
            AudioSources[0].PlayOneShot(_woundedAudioClip);    
        }
    }

    public void Die()
    {
        PlayDeath();
        DestroyEnemyGameObject();
    }

    private void DestroyEnemyGameObject()
    {
        Destroy(gameObject, _deadAudioClip.length);
    }

    private void PlayDeath()
    {
        AudioSources[1].PlayOneShot(_deadAudioClip);
        AiPath.canMove = false;
        DeathAnimation.CrossFade("death1");
    }
}