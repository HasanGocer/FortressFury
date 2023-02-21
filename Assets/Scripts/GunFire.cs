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
    [SerializeField] float _hitDistanceTime;
    [SerializeField] float _hitVelocityPower;
    Quaternion tempQuaternion;

    public void BackAddedHit(GameObject hit, int ID)
    {
        ObjectPool.Instance.AddObject(OPHitCount + ID, hit);
    }

    public IEnumerator ManuelGunStart()
    {
        WalkerManager walkerManager = WalkerManager.Instance;
        GameManager gameManager = GameManager.Instance;
        ItemData itemData = ItemData.Instance;
        MainManager mainManager = MainManager.Instance;

        yield return null;
        tempQuaternion = mainManager.allGuns[0].Guns[mainManager.gunCount].transform.rotation;

        while (gameManager.gameStat == GameManager.GameStat.start)
        {
            yield return null;

            for (int i = 0; i < mainManager.allGuns[0].GunIDs[mainManager.gunCount].hitStartPos.Count; i++)
                if (walkerManager.Walker.Count > 0 && RotateOnTouch.Instance.canRotate)
                    ManuelHitRival(mainManager.allGuns[0].GunIDs[mainManager.gunCount].hitStartPos[i], mainManager, itemData);

            yield return new WaitForSeconds(itemData.field.gunReloadTime);
        }
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

            /*if (gameManager.level % WalkerManager.Instance.bossModLevel != 0)
            {
                
            }
            else
            {
                if (bossManager.boss != null)
                 {
                     HitRival(bossManager.boss, mainManager, itemData);
                     yield return new WaitForSeconds(itemData.field.gunReloadTime);
                 }
            }*/
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

    private void ManuelHitRival(GameObject pos, MainManager mainManager, ItemData itemData)
    {
        GameObject hit = GetObject(mainManager);
        HitPlacement(ref hit, pos);
        GunEffect(hit);
        StartCoroutine(GunShake(mainManager, itemData));
        StartCoroutine(HitMoveRigidbody(hit));

    }
    private void HitRival(GameObject focusRival, GameObject pos, MainManager mainManager, ItemData itemData, int gunCount)
    {
        if (focusRival.activeInHierarchy)
        {
            GameObject hit = GetObject(mainManager);
            HitPlacement(ref hit, pos);
            GunEffect(hit);
            StartCoroutine(HitMove(hit, focusRival));
        }
    }
    private GameObject GetObject(MainManager mainManager)
    {
        return ObjectPool.Instance.GetPooledObject(OPHitCount + mainManager.gunCount);
    }
    private void HitPlacement(ref GameObject hit, GameObject pos)
    {
        hit.transform.position = pos.transform.position;
    }
    private void GunEffect(GameObject pos)
    {
        SoundSystem.Instance.CallHitSound();
        ParticalSystem.Instance.CallHitPartical(pos);
    }
    private void GunLookFocusRival(GameObject focusRival, MainManager mainManager)
    {
        mainManager.allGuns[0].Guns[mainManager.gunCount].transform.LookAt(focusRival.transform);
        mainManager.allGuns[0].Guns[mainManager.gunCount].transform.rotation = new Quaternion(tempQuaternion.x, mainManager.allGuns[0].Guns[mainManager.gunCount].transform.rotation.y, tempQuaternion.z, mainManager.allGuns[0].Guns[mainManager.gunCount].transform.rotation.w);
    }
    private IEnumerator GunShake(MainManager mainManager, ItemData itemData)
    {
        mainManager.allGuns[0].Guns[mainManager.gunCount].transform.DOShakeScale(((itemData.field.gunReloadTime) / 3) * 2, _gunScalePower);
        yield return new WaitForSeconds(itemData.field.gunReloadTime);
        mainManager.allGuns[0].Guns[mainManager.gunCount].transform.localScale = new Vector3(_gunScale, _gunScale, _gunScale);
    }
    private IEnumerator HitMoveRigidbody(GameObject hit)
    {
        float xangle = MainManager.Instance.allGuns[0].ParentGun.transform.GetChild(MainManager.Instance.gunCount).transform.GetChild(0).transform.rotation.eulerAngles.y;
        if (xangle > 300) xangle -= 360;
        xangle /= 6;
        hit.GetComponent<Rigidbody>().velocity = new Vector3(xangle, 0, _hitVelocityPower);
        yield return new WaitForSeconds(_hitDistanceTime);
        BackAddedHit(hit, MainManager.Instance.gunCount);
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
