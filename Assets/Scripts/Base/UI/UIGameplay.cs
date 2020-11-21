using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using TTBreakOut;
using System;

public class UIGameplay : UIPanel
{
    [Header("Activables")]
    public GameObject GroupPause;

    private CharacterController cc;

    [Header("Labels")]
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI raccoonText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI tempsText;

    [Header("Transform for anim")]
    public Transform gem;

    [Header("Animators")]
    public Animator animCoins;
    public Animator animRaccoons;

    [Header("Debug")]
    public GameObject InvicibilityButtonsON, InvicibilityButtonsOFF;
    public GameObject InvicibilityVehiculeButtonsON, InvicibilityVehciuleButtonsOFF;

    // TMP DEBUG
    public TextMeshProUGUI textSegDebug;
    public TextMeshProUGUI SceneName;

    [Header("Powerups")]
    public RectTransform powerupZone;
    public GameObject PowerupIconPrefab;

    [Header("Buttons")]
    public Button pauseButton;
    public Button resumeButton;
    public Button closeButton;
    public Button menuButton;

    public override void ResetPanel()
    {
        //cc = CharacterController.Instance;
     
        //cc.coinsChanged = UpdateCoins;
        //cc.raccoonsChanged = UpdateRaccoons;

        //StageManager.Instance.currentSegmentChanged = ChangeSeg;
        //StageManager.Instance.currentSpeedChanged = ChangeSpeed;
        //ChangeSpeed(StageManager.Instance.speedData.SpeedList[0]);

        //if (SceneName != null)
        //{
        //    SceneName.text = SceneManager.GetActiveScene().name;
        //}

//#if !UNITY_EDITOR

//    OnOffInv(false);
//    OnOffVInv(false);
//#endif

        //InitInviciblityButton();
        //InitVehiculeInviciblityButton();
    }


    //public void ChangeSeg(Stage seg)
    //{
    //    textSegDebug.text = seg.gameObject.name;
    //}

    //public void PreviousZone()
    //{
    //    var previousScene = (SceneManager.GetActiveScene().buildIndex+ 1)  % (SceneManager.sceneCountInBuildSettings);
    //    SceneManager.LoadScene(previousScene);
    //}

    //public void IncreaseZone()
    //{
    //    var nextScene = (SceneManager.GetActiveScene().buildIndex -1) % (SceneManager.sceneCountInBuildSettings);
    //    if (nextScene < 0)
    //        nextScene = SceneManager.sceneCountInBuildSettings - 1;
    //    SceneManager.LoadScene(nextScene);
    //}
    //// END TMP DEBUG


    //public void UpdateCoins(int c)
    //{
    //    coinText.text = cc.coins.ToString();
    //    if (c > 0 && animCoins != null)
    //        animCoins.SetTrigger("Anim");
    //}

    //public void UpdateRaccoons(int c)
    //{
    //    raccoonText.text = cc.raccoons.ToString();
    //    if (c > 0 && animRaccoons != null)
    //    {
    //        animRaccoons.SetTrigger("Anim");
    //    }
    //}

    //public void AnimCoin(bool anim = false, int nb = 1)
    //{
    //    PlayerDataOld.Instance.Coins += nb;
    //    CharacterController.Instance.coins += nb;
    //}

    //public void AnimGem(bool anim = false, int nb = 1)
    //{
    //    if (anim)
    //    {
    //        if (!gem.GetChild(0).gameObject.activeSelf)
    //        {
    //            gem.GetChild(0).gameObject.SetActive(true);
    //        }
    //    }
    //}

    //public void Restart()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //}

    //bool hasChangedColor = false;
    //public void Tick()
    //{
    //    distanceText.text = Mathf.RoundToInt(StageManager.Instance.worldDistance).ToString() + "m";
    //    tempsText.text = StageManager.Instance.timeSinceStart.ToString(("F2"));

    //    if (!hasChangedColor && StageManager.Instance.timeSinceStart > 120)
    //    {
    //        tempsText.color = Color.red;
    //        hasChangedColor = true;
    //    }

    //}

    //public void OnOffInv(bool b)
    //{
    //    GameManager.Instance.isInvincible = b;
    //}

    //public void OnOffVInv(bool b)
    //{
    //    GameManager.Instance.isVehiculeInvincible = b;
    //}

    //public void InitInviciblityButton()
    //{
    //    if (GameManager.Instance.isInvincible)
    //    {
    //        InvicibilityButtonsON.SetActive(true);
    //        InvicibilityButtonsOFF.SetActive(false);
    //    }
    //    else
    //    {
    //        InvicibilityButtonsON.SetActive(false);
    //        InvicibilityButtonsOFF.SetActive(true);
    //    }
    //}

    //public void InitVehiculeInviciblityButton()
    //{
    //    if (GameManager.Instance.isVehiculeInvincible)
    //    {
    //        InvicibilityVehiculeButtonsON.SetActive(true);
    //        InvicibilityVehciuleButtonsOFF.SetActive(false);
    //    }
    //    else
    //    {
    //        InvicibilityVehiculeButtonsON.SetActive(false);
    //        InvicibilityVehciuleButtonsOFF.SetActive(true);
    //    }
    //}


    //public void ChangeSpeed(float newSpeed)
    //{
    //    speedText.text = newSpeed + "m/s";
    //}

}
