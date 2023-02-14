using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Buttons : MonoSingleton<Buttons>
{
    //managerde bulunacak
    [Header("Global_Panel")]
    [Space(10)]

    [SerializeField] private GameObject _globalPanel;
    public TMP_Text moneyText, levelText;

    [Header("Start_Panel")]
    [Space(10)]

    public GameObject startPanel;
    [SerializeField] Button _startButton;

    [Header("Setting_Panel")]
    [Space(10)]

    [SerializeField] private Button _settingButton;
    [SerializeField] private GameObject _settingGame;

    [SerializeField] private Sprite _red, _green;
    [SerializeField] private Button _settingBackButton;
    [SerializeField] private Button _soundButton, _vibrationButton;

    [Header("Finish_Panel")]
    [Space(10)]

    public GameObject winPanel;
    public GameObject failPanel;
    public GameObject barPanel;
    [SerializeField] private Button _winPrizeButton, _winEmptyButton, _failButton;
    [SerializeField] int finishWaitTime;
    [SerializeField] int bossModLevel;

    public TMP_Text finishGameMoneyText;

    [Header("Loading_Panel")]
    [Space(10)]

    [SerializeField] GameObject _loadingPanel;
    [SerializeField] int _loadingScreenCountdownTime;
    [SerializeField] int _startSceneCount;

    private void Start()
    {
        ButtonPlacement();
        SettingPlacement();
        levelText.text = GameManager.Instance.level.ToString();
    }

    public IEnumerator LoadingScreen()
    {

        _loadingPanel.SetActive(true);
        _globalPanel.SetActive(false);
        startPanel.SetActive(false);
        yield return new WaitForSeconds(_loadingScreenCountdownTime);
        _loadingPanel.SetActive(false);
        _globalPanel.SetActive(true);
        startPanel.SetActive(true);

        MarketSystem.Instance.MarketStart();
    }
    public IEnumerator NoThanxOnActive()
    {
        _winEmptyButton.gameObject.SetActive(false);
        yield return new WaitForSeconds(3);
        _winEmptyButton.gameObject.SetActive(true);
    }

    private void SettingPlacement()
    {
        SoundSystem soundSystem = SoundSystem.Instance;
        GameManager gameManager = GameManager.Instance;
        Image soundImage = _soundButton.gameObject.GetComponent<Image>();
        Image vibrationImage = _vibrationButton.gameObject.GetComponent<Image>();

        if (gameManager.sound == 1)
        {
            soundImage.sprite = _green;
            soundSystem.MainMusicPlay();
        }
        else
        {
            soundImage.sprite = _red;
            soundSystem.MainMusicStop();
        }

        if (gameManager.vibration == 1)
            vibrationImage.sprite = _green;
        else
            vibrationImage.sprite = _red;
    }
    private void ButtonPlacement()
    {
        _settingButton.onClick.AddListener(SettingButton);
        _settingBackButton.onClick.AddListener(SettingBackButton);
        _soundButton.onClick.AddListener(SoundButton);
        _vibrationButton.onClick.AddListener(VibrationButton);
        _winPrizeButton.onClick.AddListener(() => StartCoroutine(WinPrizeButton()));
        _winEmptyButton.onClick.AddListener(() => StartCoroutine(WinButton()));
        _failButton.onClick.AddListener(() => StartCoroutine(FailButton()));
        _startButton.onClick.AddListener(StartButton);
    }

    private void StartButton()
    {
        GameManager.Instance.gameStat = GameManager.GameStat.start;
        startPanel.SetActive(false);

    }
    private IEnumerator WinButton()
    {
        GameManager gameManager = GameManager.Instance;

        _winPrizeButton.enabled = false;
        gameManager.SetLevel();
        if (gameManager.level % bossModLevel == 0)
        {
            BarSystem.Instance.BarStopButton(0);
            MoneySystem.Instance.MoneyTextRevork(gameManager.addedMoney);
        }
        yield return new WaitForSeconds(finishWaitTime);
        SceneManager.LoadScene(_startSceneCount);
    }
    private IEnumerator WinPrizeButton()
    {
        GameManager gameManager = GameManager.Instance;

        _winPrizeButton.enabled = false;
        if (gameManager.level % bossModLevel == 0)
            BarSystem.Instance.BarStopButton(gameManager.addedMoney);
        gameManager.SetLevel();
        yield return new WaitForSeconds(finishWaitTime);
        SceneManager.LoadScene(_startSceneCount);
    }
    private IEnumerator FailButton()
    {
        MoneySystem.Instance.MoneyTextRevork(GameManager.Instance.addedMoney);
        yield return new WaitForSeconds(finishWaitTime);
        SceneManager.LoadScene(_startSceneCount);
    }
    private void SettingButton()
    {
        startPanel.SetActive(false);
        _settingGame.SetActive(true);
        _settingButton.gameObject.SetActive(false);
        _globalPanel.SetActive(false);
    }
    private void SettingBackButton()
    {
        if (GameManager.Instance.gameStat == GameManager.GameStat.intro)
            startPanel.SetActive(true);
        _settingGame.SetActive(false);
        _settingButton.gameObject.SetActive(true);
        _globalPanel.SetActive(true);
    }
    private void SoundButton()
    {
        GameManager gameManager = GameManager.Instance;

        if (gameManager.sound == 1)
        {
            _soundButton.gameObject.GetComponent<Image>().sprite = _red;
            SoundSystem.Instance.MainMusicStop();
            gameManager.sound = 0;
        }
        else
        {
            _soundButton.gameObject.GetComponent<Image>().sprite = _green;
            SoundSystem.Instance.MainMusicPlay();
            gameManager.sound = 1;
        }

        gameManager.SetSound();
    }
    private void VibrationButton()
    {
        GameManager gameManager = GameManager.Instance;

        if (gameManager.vibration == 1)
        {
            _vibrationButton.gameObject.GetComponent<Image>().sprite = _red;
            gameManager.vibration = 0;
        }
        else
        {
            _vibrationButton.gameObject.GetComponent<Image>().sprite = _green;
            gameManager.vibration = 1;
        }

        gameManager.SetVibration();
    }

}
