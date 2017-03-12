using System;
using UnityEngine;
using System.Collections;
using System.Linq;

public class PlayerBehavior : MonoBehaviour, IHittable, IDie
{
    [HideInInspector]
    [SerializeField]
    private Stats _playerStats;

    

    public Stats PlayerStats
    {
        get { return _playerStats; }
        set { _playerStats = value; }
    }

    public Action HasDiedAction { get; set; }



    void Start()
    {
        PlayerStats = GetComponent<Stats>();
        if (PlayerStats == null)
        {
            throw new MissingComponentException("Missing Player Stats");
        }
    }

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

    

    public void Hit()
    {
        PlayerStats.Health -= 10;

        if (PlayerStats.Health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {


    }
}