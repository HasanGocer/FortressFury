using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PortalSystem : MonoSingleton<PortalSystem>
{
    [Header("Walker_Field")]
    [Space(10)]

    public int portalOpenTime;
    [SerializeField] int _PortalOpenScale, _PortalCloseScale;
    [SerializeField] GameObject _portal;

    public IEnumerator PortalFirstOpen()
    {
        _portal.transform.DOScale(new Vector3(_PortalOpenScale, _PortalOpenScale, _PortalOpenScale), portalOpenTime);
        yield return new WaitForSeconds(portalOpenTime);
        FirstPortalStart();
    }

    public IEnumerator PortalOpen()
    {
        _portal.transform.DOScale(new Vector3(_PortalOpenScale, _PortalOpenScale, _PortalOpenScale), portalOpenTime);
        yield return new WaitForSeconds(portalOpenTime);
    }

    public IEnumerator PortalClose()
    {
        _portal.transform.DOScale(new Vector3(_PortalCloseScale, _PortalCloseScale, _PortalCloseScale), portalOpenTime);
        yield return new WaitForSeconds(portalOpenTime);
    }

    private void FirstPortalStart()
    {
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
