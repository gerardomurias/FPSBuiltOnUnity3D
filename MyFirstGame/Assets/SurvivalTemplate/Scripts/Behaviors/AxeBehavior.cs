using System;
using UnityEngine;
using System.Collections;

public class AxeBehavior : MonoBehaviour
{
    [SerializeField]
    private Animator _axeAnimator;

    [SerializeField]
    private Animation _axeAnimation;



    private const float _averageSpeed = 1.0f;
    private const float _doubleSpeed = 1.5f;



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

        AxeAnimator.SetFloat("AnimationSpeed", Player.IsRunning() ? _doubleSpeed : _averageSpeed);
    }
}