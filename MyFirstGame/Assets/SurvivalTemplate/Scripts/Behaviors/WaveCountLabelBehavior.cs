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



    void Start()
    {
        //SpawnerReference.UpdateWaveCountAction = UpdateWaveLabelCount;
        UpdateWaveLabelCount();
    }

    void Update()
    {

    }

    public void UpdateWaveLabelCount()
    {
        InitializeReferences();

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

}