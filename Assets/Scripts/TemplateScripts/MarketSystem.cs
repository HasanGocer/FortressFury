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
    }

    [Header("Market_Main_Field")]
    [Space(10)]

    public MarketMainField marketMainField;

    [Header("Market_UI_Field")]
    [Space(10)]

    [SerializeField] private Button _marketButton;
    [SerializeField] private GameObject _marketOpenPos, _marketClosePos;
    [SerializeField] GameObject _upImage, _downImage;
    public RectTransform marketPanel;
    [SerializeField] GameObject _marketScrollPanel;
    [SerializeField] float _panelLerpMinDistance;
    [SerializeField] int _panelLerpFactor;
    public bool isOpen = false;

    public void MarketStart()
    {
        MarketOnOffPlacement();
        TextPlacement();
        MarketButtonPlacement();
    }

    public void GameStart()
    {
        _marketScrollPanel.SetActive(true);
        marketPanel.gameObject.SetActive(true);
    }

    public void GameFinish()
    {
        _marketScrollPanel.SetActive(false);
        marketPanel.gameObject.SetActive(false);
    }

    public void MarketPanelOff()
    {
        _downImage.SetActive(false);
        _upImage.SetActive(true);
        StartCoroutine(MarketPanelMove());
    }

    private void MarketButton()
    {
        if (!isOpen)
        {
            Buttons.Instance.SettingPanelOff();
            _downImage.SetActive(true);
            _upImage.SetActive(false);
            StartCoroutine(MarketPanelMove());
        }
        else
        {
            _downImage.SetActive(false);
            _upImage.SetActive(true);
            StartCoroutine(MarketPanelMove());
        }
    }
    private void MarketButtonPlacement()
    {
        marketMainField.PlayerImageButton[0].onClick.AddListener(() => FieldBuy(0));
        marketMainField.PlayerImageButton[1].onClick.AddListener(() => FieldBuy(1));
        marketMainField.PlayerImageButton[2].onClick.AddListener(() => FieldBuy(2));
        marketMainField.PlayerImageButton[3].onClick.AddListener(() => FieldBuy(3));
    }
    private IEnumerator MarketPanelMove()
    {
        float lerpCount = 0;
        GameObject tempPos;
        if (isOpen)
        {
            tempPos = _marketClosePos;
            while (isOpen)
            {
                lerpCount += Time.deltaTime * _panelLerpFactor;
                marketPanel.position = Vector2.Lerp(marketPanel.position, tempPos.transform.position, lerpCount);
                yield return new WaitForSeconds(Time.deltaTime);
                if (_panelLerpMinDistance >= Vector2.Distance(marketPanel.position, tempPos.transform.position)) break;
            }
            isOpen = false;
        }
        else
        {
            tempPos = _marketOpenPos;
            while (!isOpen)
            {
                lerpCount += Time.deltaTime * _panelLerpFactor;
                marketPanel.position = Vector2.Lerp(marketPanel.position, tempPos.transform.position, lerpCount);
                yield return new WaitForSeconds(Time.deltaTime);
                if (_panelLerpMinDistance >= Vector2.Distance(marketPanel.position, tempPos.transform.position)) break;
            }
            isOpen = true;
        }


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
                    marketMainField.MarketMainFieldPrice[0].text = moneySystem.NumberTextRevork(itemData.fieldPrice.castleHealth);
                    marketMainField.MarketMainFieldLevel[0].text = "Level " + itemData.factor.castleHealth;
                }
                break;
            case 1:
                if (gameManager.money >= itemData.fieldPrice.gunAtackPower)
                {
                    moneySystem.MoneyTextRevork(itemData.fieldPrice.gunAtackPower * -1);
                    itemData.SetGunAtackPower();
                    marketMainField.MarketMainFieldPrice[1].text = moneySystem.NumberTextRevork(itemData.fieldPrice.gunAtackPower);
                    marketMainField.MarketMainFieldLevel[1].text = "Level " + itemData.factor.gunAtackPower;
                }
                break;
            case 2:
                if (gameManager.money >= itemData.fieldPrice.gunDistance)
                {
                    moneySystem.MoneyTextRevork(itemData.fieldPrice.gunDistance * -1);
                    itemData.SetGunDistance();
                    marketMainField.MarketMainFieldPrice[2].text = moneySystem.NumberTextRevork(itemData.fieldPrice.gunDistance);
                    marketMainField.MarketMainFieldLevel[2].text = "Level " + itemData.factor.gunDistance;
                }
                break;
            case 3:
                if (gameManager.money >= itemData.fieldPrice.gunReloadTime)
                {
                    moneySystem.MoneyTextRevork((int)itemData.fieldPrice.gunReloadTime * -1);
                    itemData.SetGunReloadTime();
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
    private void MarketOnOffPlacement()
    {
        _marketButton.onClick.AddListener(MarketButton);
    }
}
