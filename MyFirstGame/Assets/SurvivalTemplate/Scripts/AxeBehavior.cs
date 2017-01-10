using System;
using UnityEngine;
using System.Collections;

public class AxeBehavior : MonoBehaviour
{
    [SerializeField]
    private Animator _axeAnimator;

    [SerializeField]
    private Animation _axeAnimation;

    private float _averageSpeed = 1.0f;
    private float _doubleSpeed = 1.5f;


    public Animator AxeAnimator
    {
        get { return _axeAnimator; }
        set { _axeAnimator = value; }
    }

    public Animation AxeAnimation
    {
        get { return _axeAnimation; }
        set { _axeAnimation = value; }
    }

    public PlayerBehavior Player { get; set; }

    void Start()
    {
        AxeAnimator = GetComponent<Animator>();
        Player = GetComponent<PlayerBehavior>();
        AxeAnimation = GetComponent<Animation>();
    }

    void Update()
    {
        SetAnimator();
    }

    private void SetAnimator()
    {
        if (Player.IsSwinging())
        {
            AxeAnimator.SetBool("CharacterIsSwinging", true);
        }
        else
        {
            AxeAnimator.SetBool("CharacterIsSwinging", false);
            AxeAnimator.SetBool("CharacterIsMoving", Player.IsMoving());
        }

        if (Player.IsRunning())
        {
            AxeAnimator.SetFloat("AnimationSpeed", _doubleSpeed);
        }
        else
        {
            AxeAnimator.SetFloat("AnimationSpeed", _averageSpeed);
        }
    }
}