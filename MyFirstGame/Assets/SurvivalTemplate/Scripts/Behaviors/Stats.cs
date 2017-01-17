using UnityEngine;

public class Stats: MonoBehaviour
{
    [SerializeField]
    private float _health;

    public float Health
    {
        get { return _health; }
        set { _health = value; }
    }
}