using System;
using UnityEngine;
using System.Collections;

public class DecreaseHealthBehavior : MonoBehaviour
{
    public UILabel _label;

    private int _health = 100;

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnPress()
    {
        _health--;
        _label.text = _health.ToString();
    }
}