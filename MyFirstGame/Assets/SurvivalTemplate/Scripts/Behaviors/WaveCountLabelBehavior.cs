using System;
using UnityEngine;

public class WaveCountLabelBehavior : MonoBehaviour
{
    [SerializeField]
    private SpawnerBehavior _spawnerReference;

    [HideInInspector]
    [SerializeField]
    private UILabel _waveCountLabelReference;

    private const string _waveCountLabelLiteral = "Wave Nº: ";



    public SpawnerBehavior SpawnerReference
    {
        get { return _spawnerReference; }
        set { _spawnerReference = value; }
    }

    public UILabel WaveCountLabelReference
    {
        get { return _waveCountLabelReference; }
        set { _waveCountLabelReference = value; }
    }

    // Use this for initialization
    void Start()
    {
        InitializeReferences();

        SpawnerReference.UpdateWaveCount = UpdateWaveLabelCount;
        UpdateWaveLabelCount();
    }

    

    private void UpdateWaveLabelCount()
    {
        WaveCountLabelReference.text = _waveCountLabelLiteral + SpawnerReference.CurrentWaveCount;
    }

    private void InitializeReferences()
    {
        WaveCountLabelReference = GetComponent<UILabel>();
        if (WaveCountLabelReference == null)
        {
            throw new MissingComponentException("No Label Reference available");
        }
    }

    void Update()
    {

    }
}