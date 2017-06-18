using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerBehavior : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerReference;

    [SerializeField]
    private int _currentWaveCount;

    [SerializeField]
    private int _currentEnemiesLeftCount = 0;

    [SerializeField]
    private SpiderFactory _spiderFactoryReference;

    private float _spawnOffset = 3.0f;

    private float _spawnDelay = 5;

    private int _enemiesIncrement = 1;
    
    private Action _hasDied;

    public Action UpdateEnemyCount;

    public Action UpdateWaveCount;

    internal List<GameObject> Enemies = new List<GameObject>();



    public Vector3 RandomSpawnPoinTransform
    {
        get
        {
            int randIndex = Random.Range(0, transform.childCount - 1);
            var position = transform.GetChild(randIndex).position + Random.insideUnitSphere * _spawnOffset;
            position.y = 0;

            return position;
        }
    }

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
        get { return _currentEnemiesLeftCount; }
        set { _currentEnemiesLeftCount = (value < 0) ? 0 : value; }
    }

    public SpiderFactory SpiderFactoryReference
    {
        get { return _spiderFactoryReference; }
        set { _spiderFactoryReference = value; }
    }



    void Start()
    {
        SpawnLoop();
    }

    private void SpawnFirstSpider(GameObject enemySpider)
    {
        throw new NotImplementedException();
    }

    void Update()
    {
    }

    private void SpawnLoop()
    {
        //UpdateEnemiesCount();


        //CurrentWaveCount++;
        //if (UpdateWaveCount != null)
        //{
        //    UpdateWaveCount();
        //}

        //for (int i = 0; i < CurrentEnemiesLeftCount; i++)
        //{
        //    InstantiateNewSpider();
        //}
    }

    private void UpdateEnemiesCount()
    {
        _enemiesIncrement += 1;
        CurrentEnemiesLeftCount = _enemiesIncrement;
    }

    //private void InstantiateNewSpider()
    //{
    //    var spider = Instantiate(EnemySpider, RandomSpawnPoinTransform, Quaternion.identity);
    //    SpawnInstantiatedSpider(spider);
    //}

    private void SpawnInstantiatedSpider(GameObject spider)
    {
        var enemyBehaviorComponent = spider.GetComponent<SpiderBehavior>();
        enemyBehaviorComponent.HasDiedAction = EnemyDies;

        if (UpdateEnemyCount != null)
        {
            UpdateEnemyCount();
        }

        SetupAiPathTarget(spider);
    }

    private void SetupAiPathTarget(GameObject spider)
    {
        var aiPath = spider.GetComponent<AIPath>();
        aiPath.target = PlayerReference.transform;
    }

    public void EnemyDies()
    {
        CurrentEnemiesLeftCount--;
        if (UpdateEnemyCount != null)
        {
            UpdateEnemyCount();
        }

        if (CurrentEnemiesLeftCount <= 0)
        {
            Invoke("SpawnLoop", _spawnDelay);
        }
    }

    public void SpawnFirstSpider()
    {
        var spider = SpiderFactoryReference.CreateNewSpider();
    }
}