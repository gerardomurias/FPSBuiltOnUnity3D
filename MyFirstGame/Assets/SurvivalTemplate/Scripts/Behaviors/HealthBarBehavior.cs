using System;
using UnityEngine;

public class HealthBarBehavior : MonoBehaviour
{
    [HideInInspector]
    [SerializeField]
    private Stats _playerStats;

    [HideInInspector]
    [SerializeField]
    private UILabel _healthLabel;

    [SerializeField]
    private UISlider _progressBarReference;

    [SerializeField]
    private PlayerBehavior _playerReference;

    [HideInInspector]
    [SerializeField]
    private SpiderBehavior _spiderReference;



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

    public SpiderBehavior SpiderReference
    {
        get { return _spiderReference; }
        set { _spiderReference = value; }
    }



    void Start()
    {
        GetReferencesFromSceneManager();
        InitializeReferences();

        HealthLabel.text = PlayerStatsReference.Health.ToString(System.Globalization.CultureInfo.InvariantCulture);
    }

    private void GetReferencesFromSceneManager()
    {
        var sceneManagerReference = GameObject.Find("SceneManager");
        if (sceneManagerReference == null)
        {
            throw new MissingComponentException("Main Scene Manager reference not present for Spider");
        }

        var sceneManagerBehavior = sceneManagerReference.GetComponent<SceneManagerBehavior>();

        PlayerReference = sceneManagerBehavior.PlayerReference.GetComponent<PlayerBehavior>();
        PlayerStatsReference = sceneManagerBehavior.PlayerReference.GetComponent<Stats>();
    }

    private void InitializeReferences()
    {
        //PlayerReference = GameObject.Find("Player").GetComponent<PlayerBehavior>();
        //if (PlayerReference == null)
        //{
        //    throw new MissingComponentException("Missing Player Reference");
        //}

        //PlayerStatsReference = PlayerReference.GetComponent<Stats>();
        //if (PlayerStatsReference == null)
        //{
        //    throw new MissingComponentException("Missing Player Stats Reference");
        //}

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

        SpiderReference = GameObject.Find("Spider").GetComponent<SpiderBehavior>();
        if (SpiderReference == null)
        {
            throw new MissingComponentException("Missing Enemy Reference");
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
