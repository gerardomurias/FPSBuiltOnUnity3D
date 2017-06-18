using System.Linq;
using System.Threading;
using UnityEngine;

public class SceneManagerBehavior : MonoBehaviour
{
    [SerializeField]
    private SpawnerBehavior _spawnerReference;

    [SerializeField]
    private GameObject _playerReference;

    [SerializeField]
    private Stats _spiderStats;

    [SerializeField]
    private AudioSource[] _spiderAudioSources;

    [SerializeField]
    private Animation _spiderAnimation;

    [SerializeField]
    private AIPath _pathFinding;

    [SerializeField]
    private UISlider _progressBarReference;

    [SerializeField]
    private SpiderBehavior _spiderReference;

    [SerializeField]
    private SpiderFactory _spiderFactoryReference;



    public SpawnerBehavior SpawnerReference
    {
        get { return _spawnerReference; }
        set { _spawnerReference = value; }
    }

    public GameObject PlayerReference
    {
        get { return _playerReference; }
        set { _playerReference = value; }
    }

    public Stats SpiderStats
    {
        get { return _spiderStats; }
        set { _spiderStats = value; }
    }

    public AudioSource[] SpiderAudioSources
    {
        get { return _spiderAudioSources; }
        set { _spiderAudioSources = value; }
    }

    public Animation SpiderAnimation
    {
        get { return _spiderAnimation; }
        set { _spiderAnimation = value; }
    }

    public AIPath PathFinding
    {
        get { return _pathFinding; }
        set { _pathFinding = value; }
    }

    public UISlider ProgressBarReference
    {
        get { return _progressBarReference; }
        set { _progressBarReference = value; }
    }

    public SpiderBehavior SpiderReference
    {
        get { return _spiderReference; }
        set { _spiderReference = value; }
    }

    public SpiderFactory SpiderFactoryReference
    {
        get { return _spiderFactoryReference; }
        set { _spiderFactoryReference = value; }
    }

    void Awake()
    {
        InitializeReferences();
        CallToStartSpawningEnemies();
    }

    private void InitializeReferences()
    {
        SpiderFactoryReference = GetComponent<SpiderFactory>();
        if (SpiderFactoryReference == null)
        { throw new MissingComponentException("No Spider Factory Reference Present"); }
    }

    private void CallToStartSpawningEnemies()
    {
        SpawnerReference.SpawnFirstSpider();
        SpiderReference = SpawnerReference.Enemies[0].GetComponent<SpiderBehavior>();
    }
}