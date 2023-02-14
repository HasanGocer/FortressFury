using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoSingleton<MainManager>
{
    [Header("Main_Field")]
    [Space(10)]

    public List<GameObject> Guns = new List<GameObject>();
    public List<GunID> GunIDs = new List<GunID>();
    public int gunCount;
    public int mainHealth;

    [Header("Main_Component")]
    [Space(10)]

    public GunFire gunFire;

    public void GunStartPlacement()
    {
        if (PlayerPrefs.HasKey("gunCount"))
            gunCount = PlayerPrefs.GetInt("gunCount");
        else
            PlayerPrefs.SetInt("gunCount", gunCount);

        Guns[gunCount].SetActive(true);
    }

    public void StartMainManager()
    {
        mainHealth = ItemData.Instance.field.castleHealth;
    }
}
