using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioClip), typeof(AudioSource))]
public class HitSound : MonoBehaviour, IHittable
{
    [HideInInspector]
    [SerializeField]
    private AudioClip _audioClip;

    [HideInInspector]
    [SerializeField]
    private AudioSource _audioSource;

    public AudioClip AudioClip
    {
        get { return _audioClip; }
        set { _audioClip = value; }
    }

    private AudioSource AudioSource
    {
        get { return _audioSource; }
        set { _audioSource = value; }
    }

    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        AudioClip = AudioSource.clip;
    }

    public void Hit()
    {
        AudioSource.PlayOneShot(AudioClip);

        Debug.Log("Gotcha!");
    }
}
