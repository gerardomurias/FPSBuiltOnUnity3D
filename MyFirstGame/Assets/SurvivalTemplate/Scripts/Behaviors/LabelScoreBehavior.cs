using System;
using UnityEngine;

class LabelScoreBehavior : MonoBehaviour
{
    [SerializeField]
    private SpawnerBehavior _spawnerReference;

    [HideInInspector]
    [SerializeField]
    private UILabel _scoreLabelReference;

    private int _score = 0;

    private const string _scoreLabelLiteral = "Score: ";



    private void Start()
    {
        IntializeReferences();
    }

    private void IntializeReferences()
    {
        _scoreLabelReference = GetComponent<UILabel>();
    }

    public SpawnerBehavior SpawnerReference
    {
        get { return _spawnerReference; }
        set { _spawnerReference = value; }
    }

    public UILabel ScoreLabelReference
    {
        get { return _scoreLabelReference; }
        set { _scoreLabelReference = value; }
    }

    public void UpdateScoreLabel()
    {
        _score += 10;
        _scoreLabelReference.text = _scoreLabelLiteral + _score;
    }
}