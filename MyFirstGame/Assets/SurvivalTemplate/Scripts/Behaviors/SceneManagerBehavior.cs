using Assets.SurvivalTemplate.Scripts.Behaviors;
using UnityEngine;

public class SceneManagerBehavior : MonoBehaviour, IChildrenInitializable
{
    [SerializeField]
    [HideInInspector]
    private GameObject _spawnerReference;

    [SerializeField]
    [HideInInspector]
    private GameObject _playerReference;

    [SerializeField]
    [HideInInspector]
    private GameObject _astarPathReference;

    [SerializeField]
    [HideInInspector]
    private GameObject _uiSliderReference;

    [SerializeField]
    [HideInInspector]
    private GameObject _waveCountReference;

    [SerializeField]
    [HideInInspector]
    private GameObject _enemiesCountReference;



    public GameObject SpawnerReference
    {
        get { return _spawnerReference; }
        set { _spawnerReference = value; }
    }

    public GameObject PlayerReference
    {
        get { return _playerReference; }
        set { _playerReference = value; }
    }

    public GameObject AstarPathReference
    {
        get { return _astarPathReference; }
        set { _astarPathReference = value; }
    }

    public GameObject UiSliderReference
    {
        get { return _uiSliderReference; }
        set { _uiSliderReference = value; }
    }

    public GameObject WaveCountReference
    {
        get { return _waveCountReference; }
        set { _waveCountReference = value; }
    }

    public GameObject EnemiesCountReference
    {
        get { return _enemiesCountReference; }
        set { _enemiesCountReference = value; }
    }



    void Awake()
    {
        InitializeChildrenReferences();
        ActivateChildrenObjects();
    }

    public void ActivateChildrenObjects()
    {
        SpawnerReference.SetActive(true);
        PlayerReference.SetActive(true);
        AstarPathReference.SetActive(true);
        UiSliderReference.SetActive(true);
        WaveCountReference.SetActive(true);
        EnemiesCountReference.SetActive(true);
    }

    public void InitializeChildrenReferences()
    {
        var playerTransform = transform.Find("Player");
        CheckNullTransform(playerTransform);
        PlayerReference = playerTransform.gameObject;

        var spawnerTransform = transform.Find("Spawner");
        CheckNullTransform(spawnerTransform);
        SpawnerReference = spawnerTransform.gameObject;

        var astarPathTransform = transform.FindDeepChild("A*");
        CheckNullTransform(astarPathTransform);
        AstarPathReference = astarPathTransform.gameObject;

        var uiSliderTransform = transform.FindDeepChild("Progress Bar");
        CheckNullTransform(uiSliderTransform);
        UiSliderReference = uiSliderTransform.gameObject;

        var waveCountTransform = transform.FindDeepChild("WaveCountLabel");
        CheckNullTransform(waveCountTransform);
        WaveCountReference = waveCountTransform.gameObject;

        var enemiesCountTransform = transform.FindDeepChild("EnemiesCountLabel");
        CheckNullTransform(enemiesCountTransform);
        EnemiesCountReference = enemiesCountTransform.gameObject;

        var playerBehavior = PlayerReference.GetComponent<PlayerBehavior>();
        var spawnerBehavior = SpawnerReference.GetComponent<SpawnerBehavior>();
        var healthBarBehavior = UiSliderReference.GetComponent<HealthBarBehavior>();
        var labelCountBehavior = EnemiesCountReference.GetComponent<LabelCountBehavior>();
        var waveCountBehavior = WaveCountReference.GetComponent<WaveCountLabelBehavior>();

        spawnerBehavior.UpdateEnemyCountAction = labelCountBehavior.UpdateEnemiesLeftLabelCount;
        spawnerBehavior.UpdateWaveCountAction = waveCountBehavior.UpdateWaveLabelCount;

        playerBehavior.SpiderReference = spawnerBehavior.SpawnFirstSpider().GetComponent<SpiderBehavior>();
        spawnerBehavior.PlayerReference = PlayerReference;
        healthBarBehavior.PlayerReference = PlayerReference.GetComponent<PlayerBehavior>();
    }

    public void CheckNullTransform(Transform objectTransform)
    {
        if (objectTransform == null)
        { throw new MissingComponentException("Missing Reference on Scene Manager"); }
    }
}