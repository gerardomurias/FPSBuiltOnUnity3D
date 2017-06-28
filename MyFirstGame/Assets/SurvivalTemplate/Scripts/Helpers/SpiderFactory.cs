using UnityEngine;

public class SpiderFactory : MonoBehaviour, IChildrenInitializable
{
    [HideInInspector]
    [SerializeField]
    private GameObject _spiderReference;

    [HideInInspector]
    [SerializeField]
    private GameObject _playerReference;

    private float _spawnOffset = 3.0f;


    public Vector3 RandomSpawnPoinTransform
    {
        get
        {
            var randIndex = Random.Range(0, transform.childCount - 1);
            var position = transform.GetChild(randIndex).position + Random.insideUnitSphere * _spawnOffset;
            position.y = 0;

            return position;
        }
    }

    public GameObject SpiderReference
    {
        get { return _spiderReference; }
        set { _spiderReference = value; }
    }

    public GameObject PlayerReference
    {
        get { return _playerReference; }
        set { _playerReference = value; }
    }

    void Awake()
    {
    }

    public GameObject CreateNewSpider()
    {
        var spider = Instantiate(SpiderReference, RandomSpawnPoinTransform, Quaternion.identity);
        spider.SetActive(true);

        return spider;
    }

    public GameObject SpawnFirstSpider()
    {
        InitializeChildrenReferences();

        var cloneSpider = Instantiate(SpiderReference, RandomSpawnPoinTransform, Quaternion.identity);
        cloneSpider.SetActive(true);

        return cloneSpider;
    }

    public void CheckNullTransform(Transform objectTransform)
    {
        if (objectTransform == null)
        { throw new MissingComponentException("Missing Reference on Spider Factory Class"); }
    }

    public void InitializeChildrenReferences()
    {
        var spiderTransform = transform.Find("Spider");
        CheckNullTransform(spiderTransform);
        SpiderReference = spiderTransform.gameObject;

        var spiderReferenceBehavior = SpiderReference.GetComponent<SpiderBehavior>();
        spiderReferenceBehavior.PlayerReference = PlayerReference;
    }

    public void ActivateChildrenObjects()
    {
    }
}