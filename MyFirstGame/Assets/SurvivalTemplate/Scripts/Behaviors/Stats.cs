using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField]
    private float _health;

    [HideInInspector]
    [SerializeField]
    private float _initialMaxHealth;



    public float InitialMaxHealth
    {
        get { return _initialMaxHealth; }
        set { _initialMaxHealth = value; }
    }

    public float Health
    {
        get { return _health; }
        set { _health = value; }
    }



    public void Start()
    {
        InitialMaxHealth = Health;
    }
}