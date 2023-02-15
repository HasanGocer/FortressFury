using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBuy : MonoBehaviour
{
    [SerializeField] int gunCount;
    [SerializeField] int gunPrice;
    [SerializeField] GameObject gun;

    private void OnMouseDown()
    {
        GunPriceBuy();
    }

    public void GunOpen()
    {
        MarketSystem.Instance.GunBuy(gunCount);
        gun.SetActive(true);
        gameObject.SetActive(false);
    }

    public void GunPriceBuy()
    {
        GameManager gameManager = GameManager.Instance;
        if (gameManager.money >= gunPrice)
        {
            ParticalSystem.Instance.CallNewObjectPartical();
            MoneySystem.Instance.MoneyTextRevork(-1 * gunPrice);
            GunOpen();
        }
    }
}
