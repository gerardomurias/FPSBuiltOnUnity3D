using System;
using UnityEngine;
using System.Collections;
using System.Linq;

public class PlayerBehavior : MonoBehaviour
{
    public Transform AttackPoint;

    void Start()
    {
        AttackPoint = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void Attack()
    {
        var hits = Physics.OverlapSphere(AttackPoint.position, 0.8f);

        foreach (var hitable in hits.Select(hit => hit.GetComponents(typeof (IHittable))))
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
}