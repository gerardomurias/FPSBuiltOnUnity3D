using UnityEngine;

namespace BehaviorComponents
{
    public class SpinBehavior : MonoBehaviour
    {
        [SerializeField]
        private float _rotation = 0.5f;

        public float RotationProperty
        {
            get { return _rotation; }
            set { _rotation = value; }
        }

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(0, _rotation, 0);
        }
    }
}