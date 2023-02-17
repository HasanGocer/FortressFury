using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MarketSystem : MonoSingleton<MarketSystem>
{
    [System.Serializable]
    public class FieldBool
    {
        public List<bool> MarketFieldBuyed = new List<bool>();
    }

    [System.Serializable]
    public class MarketMainField
    {
        public List<TMP_Text> MarketMainFieldLevel = new List<TMP_Text>();
        public List<TMP_Text> MarketMainFieldPrice = new List<TMP_Text>();
        public List<Button> PlayerImageButton = new List<Button>();
        public List<Image> PlayerImageButtonImage = new List<Image>();
    }

    [Header("Market_Main_Field")]
    [Space(10)]

    public MarketMainField marketMainField;

    [Header("Market_UI_Field")]
    [Space(10)]

    public RectTransform marketPanel;

    [Header("Turret_Field")]
    [Space(10)]

    [SerializeField] GameObject _turretBuyPanel;
    public int gun2, gun3;
    [SerializeField] GameObject _gun2GO, _gun3GO;
    [SerializeField] int _gunPrice;
    [SerializeField] Button _gun2Button, _gun3Button;

    public void MarketStart()
    {
        TextPlacement();
        MarketButtonPlacement();
        PlayerPrefPlacement();
    }

    public void GameStart()
    {
        if (gun2 == 1 && gun3 == 1)
        {
            ButtonColorPlacement();
            marketPanel.gameObject.SetActive(true);
            _turretBuyPanel.SetActive(false);
        }
        else
        {
            marketPanel.gameObject.SetActive(false);
            _turretBuyPanel.SetActive(true);
            if (gun2 == 1)
            {
                _gun2Button.gameObject.SetActive(false);
                _gun2GO.SetActive(true);
            }
            if (gun3 == 1)
            {
                _gun3Button.gameObject.SetActive(false);
                _gun3GO.SetActive(true);
            }
        }
    }

    public void ButtonColorPlacement()
    {
        ItemData itemData = ItemData.Instance;
        GameManager gameManager = GameManager.Instance;

        if (gameManager.money >= itemData.fieldPrice.castleHealth)
            marketMainField.PlayerImageButtonImage[0].color = Color.white;
        else
            marketMainField.PlayerImageButtonImage[0].color = Color.gray;
        if (gameManager.money >= itemData.fieldPrice.gunAtackPower)
            marketMainField.PlayerImageButtonImage[1].color = Color.white;
        else
            marketMainField.PlayerImageButtonImage[1].color = Color.gray;
        if (gameManager.money >= itemData.fieldPrice.gunDistance)
            marketMainField.PlayerImageButtonImage[2].color = Color.white;
        else
            marketMainField.PlayerImageButtonImage[2].color = Color.gray;

        if (gameManager.money >= itemData.fieldPrice.gunReloadTime)
            marketMainField.PlayerImageButtonImage[3].color = Color.white;
        else
            marketMainField.PlayerImageButtonImage[3].color = Color.gray;
    }

    public void GameFinish()
    {
        marketPanel.gameObject.SetActive(false);
    }

    public void PlayerPrefPlacement()
    {
        if (PlayerPrefs.HasKey("Gun2"))
            gun2 = 1;
        if (PlayerPrefs.HasKey("Gun3"))
            gun3 = 1;
    }

    private void GunBuy(int i)
    {
        if (i == 2 && _gunPrice <= GameManager.Instance.money)
        {
            _gun2GO.SetActive(true);
            ParticalSystem.Instance.CallNewObjectPartical();
            MoneySystem.Instance.MoneyTextRevork(-1 * _gunPrice);
            PlayerPrefs.SetInt("Gun2", 1);
            gun2 = 1;
            SoundSystem.Instance.CallUpgradeSound();
            StartCoroutine(GunFire.Instance.GunFireStart(1));
        }
        if (i == 3 && _gunPrice <= GameManager.Instance.money)
        {
            _gun3GO.SetActive(true);
            ParticalSystem.Instance.CallNewObjectPartical();
            MoneySystem.Instance.MoneyTextRevork(-1 * _gunPrice);
            PlayerPrefs.SetInt("Gun3", 1);
            gun3 = 1;
            SoundSystem.Instance.CallUpgradeSound();
            StartCoroutine(GunFire.Instance.GunFireStart(2));
        }
        GameStart();
    }
    private void MarketButtonPlacement()
    {
        marketMainField.PlayerImageButton[0].onClick.AddListener(() => FieldBuy(0));
        marketMainField.PlayerImageButton[1].onClick.AddListener(() => FieldBuy(1));
        marketMainField.PlayerImageButton[2].onClick.AddListener(() => FieldBuy(2));
        marketMainField.PlayerImageButton[3].onClick.AddListener(() => FieldBuy(3));
        _gun2Button.onClick.AddListener(() => GunBuy(2));
        _gun3Button.onClick.AddListener(() => GunBuy(3));
    }
    private void FieldBuy(int fieldCount)
    {
        ItemData itemData = ItemData.Instance;
        GameManager gameManager = GameManager.Instance;
        MoneySystem moneySystem = MoneySystem.Instance;

        switch (fieldCount)
        {
            case 0:
                if (gameManager.money >= itemData.fieldPrice.castleHealth)
                {
                    moneySystem.MoneyTextRevork(itemData.fieldPrice.castleHealth * -1);
                    itemData.SetCastleHealth();
                    SoundSystem.Instance.CallUpgradeSound();
                    marketMainField.MarketMainFieldPrice[0].text = moneySystem.NumberTextRevork(itemData.fieldPrice.castleHealth);
                    marketMainField.MarketMainFieldLevel[0].text = "Level " + itemData.factor.castleHealth;
                }
                break;
            case 1:
                if (gameManager.money >= itemData.fieldPrice.gunAtackPower)
                {
                    moneySystem.MoneyTextRevork(itemData.fieldPrice.gunAtackPower * -1);
                    itemData.SetGunAtackPower();
                    SoundSystem.Instance.CallUpgradeSound();
                    marketMainField.MarketMainFieldPrice[1].text = moneySystem.NumberTextRevork(itemData.fieldPrice.gunAtackPower);
                    marketMainField.MarketMainFieldLevel[1].text = "Level " + itemData.factor.gunAtackPower;
                }
                break;
            case 2:
                if (gameManager.money >= itemData.fieldPrice.gunDistance)
                {
                    moneySystem.MoneyTextRevork(itemData.fieldPrice.gunDistance * -1);
                    itemData.SetGunDistance();
                    SoundSystem.Instance.CallUpgradeSound();
                    marketMainField.MarketMainFieldPrice[2].text = moneySystem.NumberTextRevork(itemData.fieldPrice.gunDistance);
                    marketMainField.MarketMainFieldLevel[2].text = "Level " + itemData.factor.gunDistance;
                }
                break;
            case 3:
                if (gameManager.money >= itemData.fieldPrice.gunReloadTime)
                {
                    moneySystem.MoneyTextRevork((int)itemData.fieldPrice.gunReloadTime * -1);
                    itemData.SetGunReloadTime();
                    SoundSystem.Instance.CallUpgradeSound();
                    marketMainField.MarketMainFieldPrice[3].text = moneySystem.NumberTextRevork((int)itemData.fieldPrice.gunReloadTime);
                    marketMainField.MarketMainFieldLevel[3].text = "Level " + itemData.factor.gunReloadTime;
                }
                break;
        }
    }
    private void TextPlacement()
    {
        ItemData itemData = ItemData.Instance;
        MoneySystem moneySystem = MoneySystem.Instance;

        marketMainField.MarketMainFieldPrice[0].text = moneySystem.NumberTextRevork(itemData.fieldPrice.castleHealth);
        marketMainField.MarketMainFieldLevel[0].text = "Level " + itemData.factor.castleHealth;

        marketMainField.MarketMainFieldPrice[1].text = moneySystem.NumberTextRevork(itemData.fieldPrice.gunAtackPower);
        marketMainField.MarketMainFieldLevel[1].text = "Level " + itemData.factor.gunAtackPower;

        marketMainField.MarketMainFieldPrice[2].text = moneySystem.NumberTextRevork(itemData.fieldPrice.gunDistance);
        marketMainField.MarketMainFieldLevel[2].text = "Level " + itemData.factor.gunDistance;

        marketMainField.MarketMainFieldPrice[3].text = moneySystem.NumberTextRevork((int)itemData.fieldPrice.gunReloadTime);
        marketMainField.MarketMainFieldLevel[3].text = "Level " + itemData.factor.gunReloadTime;
    }
}
