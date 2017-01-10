using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour
{
    //[SerializeField]
    //private float _xAxis;

    //[SerializeField]
    //private float _yAxis;

    //public float XAxis
    //{
    //    get { return _xAxis; }
    //    set { _xAxis = value; }
    //}

    //public float YAxis
    //{
    //    get { return _yAxis; }
    //    set { _yAxis = value; }
    //} 

    // Use this for initialization
    void Start()
    {

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
}