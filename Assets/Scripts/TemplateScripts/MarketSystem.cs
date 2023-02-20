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

    public void MarketStart()
    {
        TextPlacement();
        MarketButtonPlacement();
    }

    public void GameStart()
    {
        ButtonColorPlacement();
        marketPanel.gameObject.SetActive(true);
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

    private void MarketButtonPlacement()
    {
        marketMainField.PlayerImageButton[0].onClick.AddListener(() => FieldBuy(0));
        marketMainField.PlayerImageButton[1].onClick.AddListener(() => FieldBuy(1));
        marketMainField.PlayerImageButton[2].onClick.AddListener(() => FieldBuy(2));
        marketMainField.PlayerImageButton[3].onClick.AddListener(() => FieldBuy(3));
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
                    ParticalSystem.Instance.CallNewObjectPartical();
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
                    ParticalSystem.Instance.CallNewObjectPartical();
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
                    ParticalSystem.Instance.CallNewObjectPartical();
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
                    ParticalSystem.Instance.CallNewObjectPartical();
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
