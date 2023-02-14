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
    Quaternion tempQuaternion;

    public IEnumerator GunFireStart()
    {
        RunnerManager runnerManager = RunnerManager.Instance;
        GameManager gameManager = GameManager.Instance;
        ItemData itemData = ItemData.Instance;
        MainManager mainManager = MainManager.Instance;
        BossManager bossManager = BossManager.Instance;

        bool isRivalSee = false;
        int tempRivalCount = -1;
        tempQuaternion = mainManager.Guns[mainManager.gunCount].transform.rotation;

        yield return null;

        while (gameManager.gameStat == GameManager.GameStat.start)
        {
            yield return null;
            if (gameManager.level % WalkerManager.Instance.bossModLevel != 0)
            {
                if (runnerManager.Walker.Count > 0)
                    for (int i = 0; i < runnerManager.Walker.Count; i++)
                        if (itemData.field.gunDistance > Vector3.Distance(runnerManager.Walker[i].transform.position, mainManager.GunIDs[mainManager.gunCount].mainCharacter.transform.position))
                            if (isRivalSee)
                            {
                                if (Vector3.Distance(runnerManager.Walker[tempRivalCount].transform.position, mainManager.GunIDs[mainManager.gunCount].mainCharacter.transform.position) > Vector3.Distance(runnerManager.Walker[i].transform.position, mainManager.GunIDs[mainManager.gunCount].mainCharacter.transform.position))
                                    RivalSeeTrue(ref isRivalSee, ref tempRivalCount, i);
                            }
                            else
                                RivalSeeTrue(ref isRivalSee, ref tempRivalCount, i);

                if (isRivalSee)
                {
                    HitRival(runnerManager.Walker[tempRivalCount], mainManager, itemData);
                    yield return new WaitForSeconds(itemData.field.gunReloadTime);
                    RivalSeeFalse(ref isRivalSee, ref tempRivalCount);
                }
            }
            else
            {
                if (bossManager.boss != null)
                {
                    HitRival(bossManager.boss, mainManager, itemData);
                    yield return new WaitForSeconds(itemData.field.gunReloadTime);
                }
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
    private void HitRival(GameObject focusRival, MainManager mainManager, ItemData itemData)
    {
        if (focusRival.activeInHierarchy)
        {
            GameObject hit = GetObject();
            HitPlacement(ref hit, mainManager);
            GunEffect(hit, focusRival);
            GunLookFocusRival(focusRival, mainManager);
            GunShake(mainManager, itemData);
            StartCoroutine(HitMove(hit, focusRival));
        }
    }
    private GameObject GetObject()
    {
        return ObjectPool.Instance.GetPooledObject(OPHitCount);
    }
    private void HitPlacement(ref GameObject hit, MainManager mainManager)
    {
        hit.transform.position = mainManager.GunIDs[mainManager.gunCount].hitStartPos.transform.position;
    }
    private void GunEffect(GameObject pos, GameObject target)
    {
        SoundSystem.Instance.CallHitSound();
        ParticalSystem.Instance.CallHitPartical(pos, target);
    }
    private void GunLookFocusRival(GameObject focusRival, MainManager mainManager)
    {
        mainManager.Guns[mainManager.gunCount].transform.LookAt(focusRival.transform);
        mainManager.Guns[mainManager.gunCount].transform.rotation = new Quaternion(tempQuaternion.x, mainManager.Guns[mainManager.gunCount].transform.rotation.y, tempQuaternion.z, mainManager.Guns[mainManager.gunCount].transform.rotation.w);
    }
    private void GunShake(MainManager mainManager, ItemData itemData)
    {
        mainManager.GunIDs[mainManager.gunCount].hitStartPos.transform.DOShakeScale(itemData.field.gunReloadTime, _gunScalePower);
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
        BackAddedHit(hit);
    }
    private void BackAddedHit(GameObject hit)
    {
        ObjectPool.Instance.AddObject(GunFire.Instance.OPHitCount, hit);
    }
}
