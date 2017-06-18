using UnityEngine;

public class SpiderFactory : MonoBehaviour
{
    public GameObject CreateNewSpider()
    {
        var spider = new GameObject("Spider", typeof(SpiderBehavior));

        return spider;
    }
}