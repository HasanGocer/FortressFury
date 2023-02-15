using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PortalSystem : MonoSingleton<PortalSystem>
{
    [Header("Walker_Field")]
    [Space(10)]

    [SerializeField] int _portalOpenTime;
    [SerializeField] int _PortalScale;
    [SerializeField] GameObject _portal;

    public IEnumerator PortalOpen()
    {
        _portal.transform.DOScale(new Vector3(_PortalScale, _PortalScale, _PortalScale), _portalOpenTime);
        yield return new WaitForSeconds(_portalOpenTime);


        StartCoroutine(GunFire.Instance.GunFireStart(0));
        if (MarketSystem.Instance.gun2 == 1)
            StartCoroutine(GunFire.Instance.GunFireStart(1));
        if (MarketSystem.Instance.gun3 == 1)
            StartCoroutine(GunFire.Instance.GunFireStart(2));
        MainBar.Instance.MainBarStart();
        MarketSystem.Instance.GameStart();
        WalkerManager.Instance.FirstSpawn();
    }

}
