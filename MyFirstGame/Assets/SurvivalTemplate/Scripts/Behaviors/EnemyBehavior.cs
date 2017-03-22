﻿using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

[RequireComponent(typeof(Stats), typeof(AudioSource), typeof(Animation))]
public class EnemyBehavior : MonoBehaviour, IHittable, IBleed, IDie, IAutomaticAttacker
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

    [SerializeField]
    private ParticleSystem _particleSystem;

    [SerializeField]
    [HideInInspector]
    private Action _hasDiedAction;

    [SerializeField]
    private GameObject _playerReference;

    [SerializeField]
    private Transform _attackPoint;

    private float _attackRange = 1.2f;



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

    public ParticleSystem ParticleSystem
    {
        get { return _particleSystem; }
        set { _particleSystem = value; }
    }

    public Action HasDiedAction
    {
        get { return _hasDiedAction; }
        set { _hasDiedAction = value; }
    }

    public GameObject PlayerReference
    {
        get { return _playerReference; }
        set { _playerReference = value; }
    }

    public Transform AttackPoint
    {
        get { return _attackPoint; }
        set { _attackPoint = value; }
    }



    void Start()
    {
        InitializeEnemyStats();
        InitializePosition();
        InitializeAudioSource();
        InitializeAnimations();
        InitalizePathFinding();
        InitializeExternReferences();
    }

    void Update()
    {
        CheckDistanceWithObjetive();
    }

    private void InitializePosition()
    {
        AttackPoint = GetComponent<Transform>();
    }

    private void InitializeExternReferences()
    {
        PlayerReference = GameObject.Find("Player");

        if (PlayerReference == null) { throw new MissingComponentException("EnemyBehavior: Missing Player reference"); }
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

    public void Hit()
    {
        EnemyStats.Health -= 10;

        Bleed();

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
        BroadcastMessageToSpawner();
        DestroyEnemyGameObject();
    }

    private void BroadcastMessageToSpawner()
    {
        if (HasDiedAction != null)
        {
            HasDiedAction();
        }
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

    public void Bleed()
    {
        ParticleSystem.Play();
    }

    public void CheckDistanceWithObjetive()
    {
        if (CanAttack())
        {
            Attack();
        }
    }

    public void Attack()
    {
        var hittableComponent = PlayerReference.GetComponent<IHittable>(); 
        hittableComponent.Hit();
    }

    public bool CanAttack()
    {
        return (Vector3.Distance(PlayerReference.transform.position, transform.position) < _attackRange);
    }
}