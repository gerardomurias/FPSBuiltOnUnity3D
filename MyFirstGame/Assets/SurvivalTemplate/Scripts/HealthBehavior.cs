using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace BehaviorComponents
{
    public class HealthBehavior : MonoBehaviour
    {
        [SerializeField]
        private float _health = 100.0f;

        public float Health
        {
            get { return _health; }
            set { _health = value; }
        }


        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
        }
    } 
}