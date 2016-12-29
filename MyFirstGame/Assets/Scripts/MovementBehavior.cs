using UnityEngine;
using System.Collections;

public class MovementBehavior : MonoBehaviour
{
    [SerializeField]
    private float _xAxis;

    [SerializeField]
    private float _yAxis;

    //public float tuvieja = 0f;

    public float XAxis
    {
        get { return _xAxis; }
        set { _xAxis = value; }
    }

    public float YAxis
    {
        get { return _yAxis; }
        set { _yAxis = value; }
    }

    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
        XAxis = Input.GetAxis("Horizontal");
        YAxis = Input.GetAxis("Vertical");

        transform.Translate(XAxis, YAxis, 0);
    }
}