using UnityEngine;
using System.Collections;

public class NGUIHealthBehavior : MonoBehaviour
{
    UILabel label;
    UIButton button;

	// Use this for initialization
	void Start () {
        label = GetComponent<UILabel>();
	    button = GetComponent<UIButton>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
