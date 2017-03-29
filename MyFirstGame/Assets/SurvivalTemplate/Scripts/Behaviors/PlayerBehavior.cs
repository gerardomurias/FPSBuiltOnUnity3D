using System;
using UnityEngine;
using System.Collections;
using System.Linq;

[RequireComponent(typeof(AudioSource))]
public class PlayerBehavior : MonoBehaviour, IHittable, IDie, IBleed
{
    [HideInInspector]
    [SerializeField]
    private Stats _playerStats;

    [SerializeField]
    private AudioSource[] _audioSources;

    [HideInInspector]
    [SerializeField]
    private AudioClip _woundedAudioClip;

    [HideInInspector]
    [SerializeField]
    private AudioClip _deadAudioClip;

    public ParticleSystem ParticleSystem { get; set; }

    public Stats PlayerStats
    {
        get { return _playerStats; }
        set { _playerStats = value; }
    }

    public Action HasDiedAction { get; set; }

    public AudioSource[] AudioSources
    {
        get { return _audioSources; }
        set { _audioSources = value; }
    }



    void Start()
    {
        InitializeReferences();
    }

    void Update()
    {

    }

    private void InitializeReferences()
    {
        InitializeStats();
        InitializeAudioSources();
    }

    private void InitializeAudioSources()
    {
        AudioSources = GetComponents<AudioSource>();
        if (AudioSources == null)
        {
            throw new MissingComponentException("Missing AudioSources");
        }

        _woundedAudioClip = AudioSources[0].clip;
        _deadAudioClip = AudioSources[1].clip;
    }

    private void InitializeStats()
    {
        PlayerStats = GetComponent<Stats>();
        if (PlayerStats == null)
        {
            throw new MissingComponentException("Missing Player Stats");
        }
    }

    public bool IsSwinging()
    {
        return Input.GetMouseButton(0);
    }

    public bool IsMoving()
    {
        return ((Input.GetKey("left")) ||
                ((Input.GetKey("right"))) ||
                ((Input.GetKey("up"))) ||
                ((Input.GetKey("down"))) ||
                ((Input.GetKey("w"))) ||
                ((Input.GetKey("s"))) ||
                ((Input.GetKey("a"))) ||
                ((Input.GetKey("d"))));
    }

    public bool IsRunning()
    {
        return IsMoving() && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));
    }

    public void Hit()
    {
        Debug.Log("player has been hit");
        PlayerStats.Health -= 10;

        if (PlayerStats.Health <= 0)
        {
            Die();
        }

        PlaySound();
    }

    private void PlaySound()
    {
        if (PlayerStats.Health > 0)
        {
            AudioSources[0].PlayOneShot(_woundedAudioClip);
        }
        else
        {
            AudioSources[1].PlayOneShot(_deadAudioClip);
        }
    }

    public void Die()
    {
        
    }

    public void Bleed()
    {
        throw new NotImplementedException();
    }
}