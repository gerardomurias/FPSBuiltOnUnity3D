using UnityEngine;

namespace Assets.SurvivalTemplate.Scripts.Behaviors
{
    public class LabelCountBehavior : MonoBehaviour
    {
        [HideInInspector]
        [SerializeField]
        private SpiderBehavior _spiderReference;

        [HideInInspector]
        [SerializeField]
        private UILabel _enemiesLeftLabelReference;

        [SerializeField]
        private SpawnerBehavior _spawnerReference;

        private const string _enemiesLeftLiteral = "Enemies Left:";



        public SpiderBehavior SpiderReference
        {
            get { return _spiderReference; }
            set { _spiderReference = value; }
        }

        public UILabel EnemiesLeftLabelReference
        {
            get { return _enemiesLeftLabelReference; }
            set { _enemiesLeftLabelReference = value; }
        }

        public SpawnerBehavior SpawnerReference
        {
            get { return _spawnerReference; }
            set { _spawnerReference = value; }
        }



        void Start()
        {
            InitializeReferences();

            SpawnerReference.UpdateEnemyCount = UpdateEnemiesLeftLabelCount;
        }

        private void InitializeReferences()
        {
            var sceneManager = GameObject.Find("SceneManager");
            if (sceneManager == null)
            { throw new MissingComponentException("No Scene Manager Reference Present"); }

            SpiderReference = sceneManager.GetComponent<SpiderBehavior>();
            if (SpiderReference == null)
            {
                throw new MissingComponentException("Missing Enemy Reference");
            }

            EnemiesLeftLabelReference = GetComponent<UILabel>();
            if (EnemiesLeftLabelReference == null)
            {
                throw new MissingComponentException("No Label Reference available");
            }
        }

        private void UpdateEnemiesLeftLabelCount()
        {
            EnemiesLeftLabelReference.text = _enemiesLeftLiteral + SpawnerReference.CurrentEnemiesLeftCount;
        }
    }
}