using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GunFire : MonoSingleton<GunFire>
{
    [Header("Hit_Field")]
    [Space(10)]

    public int OPHitCount;
    [SerializeField] float _hitSpeedFactor;
    [SerializeField] float _maxHitDisance;
    [SerializeField] float _gunScalePower;
    [SerializeField] float _gunScale;
    Quaternion tempQuaternion;

    public void BackAddedHit(GameObject hit)
    {
        ObjectPool.Instance.AddObject(OPHitCount, hit);
    }
    public IEnumerator GunFireStart(int gunCount)
    {
        WalkerManager walkerManager = WalkerManager.Instance;
        GameManager gameManager = GameManager.Instance;
        ItemData itemData = ItemData.Instance;
        MainManager mainManager = MainManager.Instance;

        bool isRivalSee = false;
        int tempRivalCount = -1;
        tempQuaternion = mainManager.allGuns[gunCount].Guns[mainManager.gunCount].transform.rotation;

        yield return null;

        while (gameManager.gameStat == GameManager.GameStat.start)
        {
            yield return null;
            if (gameManager.level % WalkerManager.Instance.bossModLevel != 0)
            {
                if (walkerManager.Walker.Count > 0)
                    for (int i = 0; i < walkerManager.Walker.Count; i++)
                        if (itemData.field.gunDistance > Vector3.Distance(walkerManager.Walker[i].transform.position, mainManager.allGuns[gunCount].GunIDs[mainManager.gunCount].mainCharacter.transform.position))
                            if (isRivalSee)
                            {
                                if (Vector3.Distance(walkerManager.Walker[tempRivalCount].transform.position, mainManager.allGuns[gunCount].GunIDs[mainManager.gunCount].mainCharacter.transform.position) > Vector3.Distance(walkerManager.Walker[i].transform.position, mainManager.allGuns[gunCount].GunIDs[mainManager.gunCount].mainCharacter.transform.position))
                                    RivalSeeTrue(ref isRivalSee, ref tempRivalCount, i);
                            }
                            else
                                RivalSeeTrue(ref isRivalSee, ref tempRivalCount, i);

                if (isRivalSee)
                {
                    foreach (GameObject pos in mainManager.allGuns[gunCount].GunIDs[mainManager.gunCount].hitStartPos)
                        HitRival(walkerManager.Walker[tempRivalCount], pos, mainManager, itemData, gunCount);
                    yield return new WaitForSeconds(itemData.field.gunReloadTime);
                    RivalSeeFalse(ref isRivalSee, ref tempRivalCount);
                }
            }
            else
            {
                /* if (bossManager.boss != null)
                 {
                     HitRival(bossManager.boss, mainManager, itemData);
                     yield return new WaitForSeconds(itemData.field.gunReloadTime);
                 }*/
            }
        }
    }

    private void RivalSeeTrue(ref bool isRivalSee, ref int tempRivalCount, int i)
    {
        isRivalSee = true;
        tempRivalCount = i;
    }
    private void RivalSeeFalse(ref bool isRivalSee, ref int tempRivalCount)
    {
        isRivalSee = false;
        tempRivalCount = -1;
    }
    private void HitRival(GameObject focusRival, GameObject pos, MainManager mainManager, ItemData itemData, int gunCount)
    {
        if (focusRival.activeInHierarchy)
        {
            GameObject hit = GetObject();
            HitPlacement(ref hit, pos);
            GunEffect(hit, focusRival);
            GunLookFocusRival(focusRival, mainManager, gunCount);
            GunShake(mainManager, itemData, gunCount);
            StartCoroutine(HitMove(hit, focusRival));
        }
    }
    private GameObject GetObject()
    {
        return ObjectPool.Instance.GetPooledObject(OPHitCount);
    }
    private void HitPlacement(ref GameObject hit, GameObject pos)
    {
        hit.transform.position = pos.transform.position;
    }
    private void GunEffect(GameObject pos, GameObject target)
    {
        ParticalSystem.Instance.CallHitPartical(pos, target);
    }
    private void GunLookFocusRival(GameObject focusRival, MainManager mainManager, int gunCount)
    {
        mainManager.allGuns[gunCount].Guns[mainManager.gunCount].transform.LookAt(focusRival.transform);
        mainManager.allGuns[gunCount].Guns[mainManager.gunCount].transform.rotation = new Quaternion(tempQuaternion.x, mainManager.allGuns[gunCount].Guns[mainManager.gunCount].transform.rotation.y, tempQuaternion.z, mainManager.allGuns[gunCount].Guns[mainManager.gunCount].transform.rotation.w);
    }
    private void GunShake(MainManager mainManager, ItemData itemData, int gunCount)
    {
        mainManager.allGuns[gunCount].Guns[mainManager.gunCount].transform.DOShakeScale(itemData.field.gunReloadTime, _gunScalePower);
    }
    private IEnumerator HitMove(GameObject hit, GameObject pos)
    {
        float lerpCount = 0;

        while (GameManager.Instance.gameStat == GameManager.GameStat.start && pos.activeInHierarchy)
        {
            lerpCount += Time.deltaTime * _hitSpeedFactor;
            hit.transform.position = Vector3.Lerp(hit.transform.position, pos.transform.position, lerpCount);
            yield return new WaitForSeconds(Time.deltaTime);
            if (_maxHitDisance > Vector3.Distance(hit.transform.position, pos.transform.position)) break;
        }
    }
}
