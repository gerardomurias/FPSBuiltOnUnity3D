using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Stats),typeof(AudioSource))]
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

    void Start()
    {
        InitializeEnemyStats();
        InitializeAudioSource();
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
        AudioSources[1].PlayOneShot(_deadAudioClip);
        Destroy(gameObject, _deadAudioClip.length);
    }
}