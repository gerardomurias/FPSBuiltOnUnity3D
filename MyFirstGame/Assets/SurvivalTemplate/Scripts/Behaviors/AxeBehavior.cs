using System;
using UnityEngine;
using System.Collections;
using System.Linq;

public class AxeBehavior : MonoBehaviour, IAttacker
{
    private const float _averageSpeed = 1.0f;

    private const float _doubleSpeed = 1.5f;

    [SerializeField]
    private Animator _axeAnimator;

    [SerializeField]
    private Animation _axeAnimation;

    [SerializeField]
    public PlayerBehavior _player;

    public Transform AttackPoint;



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

    public PlayerBehavior Player
    {
        get { return _player; }
        set { _player = value; }
    }

    public void Attack()
    {
        var hits = Physics.OverlapSphere(AttackPoint.position, 0.8f);

        foreach (var hitable in hits.Select(hit => hit.GetComponents(typeof(IHittable))))
        {
            if (hitable == null)
            {
                return;
            }

            foreach (IHittable component in hitable)
            {
                component.Hit();
            }
        }
    }

    void Start()
    {
        AxeAnimator = GetComponent<Animator>();
        AttackPoint = GetComponent<Transform>();
        AxeAnimation = GetComponent<Animation>();

        CheckNullComponents();
    }

    private void CheckNullComponents()
    {
        if ((AxeAnimator == null) || (AttackPoint == null) || (Player == null) || (AxeAnimation ==null))
        {
            throw new MissingComponentException("Missing components on AxeBehavior");
        }
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