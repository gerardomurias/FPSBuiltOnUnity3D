using System;
using UnityEngine;

public class HealthBarBehavior : MonoBehaviour
{
    [HideInInspector]
    [SerializeField]
    private UILabel _healthLabel;

    [SerializeField]
    private UISlider _progressBarReference;

    [SerializeField]
    private PlayerBehavior _playerReference;

    [HideInInspector]
    [SerializeField]
    private Stats _playerStats;



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

    public UILabel HealthLabel
    {
        get { return _healthLabel; }
        set { _healthLabel = value; }
    }



    void Start()
    {
        InitializeReferences();

        HealthLabel.text = PlayerStatsReference.Health.ToString(System.Globalization.CultureInfo.InvariantCulture);
    }

    private void InitializeReferences()
    {
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

        HealthLabel = GetComponentInChildren<UILabel>();
        if (HealthLabel == null)
        {
            throw new MissingComponentException("Missing Health Label Reference");
        }

        PlayerReference.HasTakenDamageAction = PlayerHasTakenDamage;
    }

    void PlayerHasTakenDamage()
    {
        ProgressBarReference.sliderValue = PlayerStatsReference.Health / PlayerStatsReference.InitialMaxHealth;
        UpdateHealthLabel();
    }

    private void UpdateHealthLabel()
    {
        HealthLabel.text = PlayerStatsReference.Health.ToString(System.Globalization.CultureInfo.InvariantCulture);
    }
}
