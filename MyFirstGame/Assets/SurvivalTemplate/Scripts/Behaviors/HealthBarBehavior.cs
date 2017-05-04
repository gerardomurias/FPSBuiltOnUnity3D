using System;
using UnityEngine;

public class HealthBarBehavior : MonoBehaviour
{
    [HideInInspector]
    [SerializeField]
    private Stats _playerStats;

    [SerializeField]
    private UISlider _progressBarReference;

    [SerializeField]
    private PlayerBehavior _playerReference;

    


    public Stats PlayerStatsReference
    {
        get { return _playerStats; }
        set { _playerStats = value; }
    }

    public UISlider ProgressBarReference
    {
        get { return _progressBarReference; }
        set { _progressBarReference = value; }
    }

    public PlayerBehavior PlayerReference
    {
        get { return _playerReference; }
        set { _playerReference = value; }
    }



    void Start()
    {
        InitializeReferences();
    }

    private void InitializeReferences()
    {
        PlayerReference = GameObject.Find("Player").GetComponent<PlayerBehavior>();
        if (PlayerReference == null)
        {
            throw new MissingComponentException("Missing Player Reference");
        }

        PlayerStatsReference = PlayerReference.GetComponent<Stats>();
        if (PlayerStatsReference == null)
        {
            throw new MissingComponentException("Missing Player Stats Reference");
        }

        ProgressBarReference = GetComponent<UISlider>();
        if (ProgressBarReference == null)
        {
            throw new MissingComponentException("Missing Progress Bar");
        }

        PlayerReference.HasTakenDamageAction = PlayerHasTakenDamage;
    }

    void PlayerHasTakenDamage()
    {
        ProgressBarReference.sliderValue = PlayerStatsReference.Health / PlayerStatsReference.InitialMaxHealth;
    }
}
