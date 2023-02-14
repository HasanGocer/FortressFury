using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoSingleton<LevelManager>
{
    [Header("Main_Field")]
    [Space(10)]

    [SerializeField] int walkerLevelMod;
    [SerializeField] int walkerCountLevelMod;

    public void LevelCheck()
    {
        GameManager gameManager = GameManager.Instance;
        ItemData itemData = ItemData.Instance;

        if (gameManager.level % walkerLevelMod == 0)
        {
            itemData.SetWalkerTypeCount();
            itemData.SetWalkerHealth();
            itemData.SetWalkerCastleHitPower();
        }
        if (gameManager.level % walkerCountLevelMod == 0)
            itemData.SetWalkerCount();
    }
}
