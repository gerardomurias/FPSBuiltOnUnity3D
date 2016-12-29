using System;
using UnityEngine;
using System.Collections;

public class AxeBehavior : MonoBehaviour
{
    public Animator AxeAnimator { get; set; }

    // Use this for initialization
    void Start()
    {
        AxeAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        SetAnimator();
    }

    private void SetAnimator()
    {
        if (CharacterIsSwinging())
        {
            AxeAnimator.SetBool("CharacterIsSwinging", CharacterIsSwinging());
        }
        else
        {
            AxeAnimator.SetBool("CharacterIsSwinging", CharacterIsSwinging());
            AxeAnimator.SetBool("CharacterIsMoving", CharacterIsMoving());
        }
    }

    private bool CharacterIsSwinging()
    {
        return Input.GetMouseButton(0);
    }

    private bool CharacterIsMoving()
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
}