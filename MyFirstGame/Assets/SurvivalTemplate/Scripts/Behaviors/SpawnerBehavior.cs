using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerBehavior : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemySpider;

    [SerializeField]
    private GameObject _playerReference;

    private float _spawnOffset = 3.0f;

    private float _spawnDelay = 5;

    private int _currentWaveCount;

    private int _currentWave = 0;

    private Action _hasDied;



    public GameObject EnemySpider
    {
        get { return _enemySpider; }
        set { _enemySpider = value; }
    }

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



    void Start()
    {
        SpawnLoop();
    }

    void Update()
    {
        if (_currentWaveCount <= 0)
        {
            Invoke("SpawnLoop", _spawnDelay);
            _currentWaveCount++;
            _currentWave++;
        }
    }

    private void SpawnLoop()
    {
        _currentWaveCount = _currentWave + 5;

        for (int i = 0; i < _currentWaveCount; i++)
        {
            SpawnNewSpider();
        }
    }

    private void SpawnNewSpider()
    {
        var spider = Instantiate(EnemySpider, RandomSpawnPoinTransform, Quaternion.identity);
        var enemyBehaviorComponent = spider.GetComponent<EnemyBehavior>();
        enemyBehaviorComponent.HasDiedAction = EnemyDies;

        SetupAiPathTarget(spider);
    }

    private void SetupAiPathTarget(GameObject spider)
    {
        var aiPath = spider.GetComponent<AIPath>();
        aiPath.target = PlayerReference.transform;
    }

    void EnemyDies()
    {
        _currentWaveCount--;
    }
}