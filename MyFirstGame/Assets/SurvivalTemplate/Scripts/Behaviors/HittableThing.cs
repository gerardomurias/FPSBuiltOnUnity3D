using UnityEngine;

public class HittableThing : MonoBehaviour, IHittable
{
    [SerializeField]
    [HideInInspector]
    private AudioSource _audioSource;

    public AudioSource AudioSource
    {
        get { return _audioSource; }
        set { _audioSource = value; }
    }

    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    public void Hit()
    {
        AudioSource.PlayOneShot(AudioSource.clip);
    }
}