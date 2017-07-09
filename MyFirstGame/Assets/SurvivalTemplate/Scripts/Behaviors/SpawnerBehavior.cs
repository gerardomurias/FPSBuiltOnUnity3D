using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Threading;

public class SpawnerBehavior : MonoBehaviour, IChildrenInitializable
{
    [SerializeField]
    [HideInInspector]
    private GameObject _playerReference;

    [SerializeField]
    private int _currentWaveCount;

    [SerializeField]
    [HideInInspector]
    private GameObject _spiderFactoryGameObject;

    [SerializeField]
    [HideInInspector]
    private SpiderFactory _spiderFactoryBehavior;

    private float _spawnDelay = 5f;

    private int _numberOfEnemiesPerWave = 4;

    private Action _hasDied;

    public Action UpdateEnemyCountAction;

    public Action UpdateWaveCountAction;

    public Action UpdateScoreAction;

    internal List<GameObject> Spiders = new List<GameObject>();



    public GameObject PlayerReference
    {
        get { return _playerReference; }
        set { _playerReference = value; }
    }

    public int CurrentWaveCount
    {
        get { return _currentWaveCount; }
        set { _currentWaveCount = value; }
    }

    public int CurrentEnemiesLeftCount
    {
        get { return Spiders.Count; }
    }

    public GameObject SpiderFactoryGameObject
    {
        get { return _spiderFactoryGameObject; }
        set { _spiderFactoryGameObject = value; }
    }

    public SpiderFactory SpiderFactoryBehavior
    {
        get { return _spiderFactoryBehavior; }
        set { _spiderFactoryBehavior = value; }
    }



    void Awake()
    {

    }

    void Start()
    {
        //Instantiate first wave spiders
        CurrentWaveCount++;
        SpawnLoop();
    }

    private void SpawnLoop()
    {
        for (var cont = 0; cont < _numberOfEnemiesPerWave; cont++)
        {
            var newSpider = SpiderFactoryBehavior.CreateNewSpider();
            var enemyBehaviorComponent = newSpider.GetComponent<SpiderBehavior>();
            enemyBehaviorComponent.HasDiedAction = EnemyDies;

            Spiders.Add(newSpider);

            UpdateEnemiesCount();
        }
    }

    void Update()
    {
    }

    private void SetupAiPathTarget(GameObject spider)
    {
        var aiPath = spider.GetComponent<AIPath>();
        aiPath.target = PlayerReference.transform;
    }

    public void EnemyDies()
    {
        var spiderToRemove = Spiders.FirstOrDefault(x => x.GetComponent<SpiderBehavior>().SpiderStats.Health <= 0);

        if (spiderToRemove != null)
        {
            Spiders.Remove(spiderToRemove);
            spiderToRemove.GetComponent<SpiderBehavior>().DestroyEnemyGameObject();

            UpdateEnemiesCount();
        }

        if (UpdateEnemyCountAction != null)
        { UpdateEnemyCountAction(); }

        if (UpdateScoreAction != null)
        { UpdateScoreAction(); }

        if (CurrentEnemiesLeftCount <= 0)
        {
            UpdateNextWave();

            Invoke("SpawnLoop", _spawnDelay);
        }
    }

    private void UpdateNextWave()
    {
        CurrentWaveCount++;

        if (UpdateWaveCountAction != null)
        {
            UpdateWaveCountAction();
        }

        _numberOfEnemiesPerWave += 2;
    }

    public GameObject SpawnFirstSpider()
    {
        InitializeChildrenReferences();
        ActivateChildrenObjects();

        var spider = SpiderFactoryBehavior.SpawnFirstSpider();
        var enemyBehaviorComponent = spider.GetComponent<SpiderBehavior>();
        enemyBehaviorComponent.HasDiedAction = EnemyDies;

        Spiders.Add(spider);
        UpdateEnemiesCount();
        SetupAiPathTarget(spider);

        return spider;
    }

    private void UpdateEnemiesCount()
    {
        if (UpdateEnemyCountAction != null)
        { UpdateEnemyCountAction(); }
    }

    public void CheckNullTransform(Transform objectTransform)
    {
        if (objectTransform == null)
        { throw new MissingComponentException("Missing Reference on Spawner Class"); }
    }

    public void InitializeChildrenReferences()
    {
        var spiderFactoryTransform = transform.Find("SpiderFactory");
        CheckNullTransform(spiderFactoryTransform);
        SpiderFactoryGameObject = spiderFactoryTransform.gameObject;
        SpiderFactoryBehavior = SpiderFactoryGameObject.GetComponent<SpiderFactory>();
        SpiderFactoryBehavior.PlayerReference = PlayerReference;
    }

    public void ActivateChildrenObjects()
    {
        SpiderFactoryGameObject.SetActive(true);
    }
}