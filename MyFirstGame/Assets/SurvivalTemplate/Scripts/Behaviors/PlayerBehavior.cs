using System;
using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.SceneManagement;

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

    [HideInInspector]
    [SerializeField]
    private Action _hasTakenDamageAction;

    [HideInInspector]
    [SerializeField]
    private SpiderBehavior _spiderReference;



    public ParticleSystem ParticleSystem { get; set; }

    public Stats PlayerStats
    {
        get { return _playerStats; }
        set { _playerStats = value; }
    }

    public Action HasDiedAction { get; set; }

    public Action HasTakenDamageAction
    {
        get { return _hasTakenDamageAction; }
        set { _hasTakenDamageAction = value; }
    }

    public AudioSource[] AudioSources
    {
        get { return _audioSources; }
        set { _audioSources = value; }
    }

    public SpiderBehavior SpiderReference
    {
        get { return _spiderReference; }
        set { _spiderReference = value; }
    }



    void Awake()
    {
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
        //InitializeEnemiesReference();
    }

    private void InitializeEnemiesReference()
    {
        if (GameObject.FindGameObjectWithTag("Spider") == null)
        {
            throw new MissingComponentException("No Enemies Located");
        }

        SpiderReference = GameObject.FindGameObjectWithTag("Spider").GetComponent<SpiderBehavior>();
        if (SpiderReference == null)
        {
            throw new MissingComponentException("No Enemies Located");
        }
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
        { throw new MissingComponentException("Missing Player Stats"); }
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
        PlayerStats.Health -= SpiderReference.DamagePerAttack;
        
        if (PlayerStats.Health <= 0)
        {
            Die();
        }

        PlaySound();

        if (HasTakenDamageAction != null)
        { HasTakenDamageAction(); }
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
        SceneManager.LoadSceneAsync("GameOver", LoadSceneMode.Single);
    }

    public void Bleed()
    {
        throw new NotImplementedException();
    }
}