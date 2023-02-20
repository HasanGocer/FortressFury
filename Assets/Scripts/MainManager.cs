using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoSingleton<MainManager>
{
    [System.Serializable]
    public class AllGuns
    {
        public GameObject ParentGun;
        public List<GameObject> Guns = new List<GameObject>();
        public List<GunID> GunIDs = new List<GunID>();
    }

    [Header("Main_Field")]
    [Space(10)]



    public AllGuns allGuns = new AllGuns();
    public int gunCount;
    public int mainHealth;
    public GameObject castle;

    [Header("Main_Component")]
    [Space(10)]

    public GunFire gunFire;

    public void GunStartPlacement()
    {
        if (PlayerPrefs.HasKey("gunCount"))
            gunCount = PlayerPrefs.GetInt("gunCount");
        else
            PlayerPrefs.SetInt("gunCount", gunCount);

        allGuns.Guns[gunCount].SetActive(true);
    }

    public void StartMainManager()
    {
        mainHealth = ItemData.Instance.field.castleHealth;
    }
}
