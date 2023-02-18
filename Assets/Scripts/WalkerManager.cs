using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DamageNumbersPro;

public class WalkerManager : MonoSingleton<WalkerManager>
{
    [Header("Walker_Field")]
    [Space(10)]


    public List<GameObject> Walker = new List<GameObject>();
    public int walkerCount;
    public int bossModLevel;
    public int minPrice, maxPrice;
    [SerializeField] float _maxWalkerDisance;
    [SerializeField] float _speedFactor;
    [SerializeField] int levelModRunnerPlusCount;
    [SerializeField] float _spawnCoundownTime;
    [SerializeField] int _OPRunnerCount;
    [SerializeField] GameObject _walkerStartPos, _walkerFinishPos;
    [SerializeField] float _spawnDistance;
    [SerializeField] int _deadTime;

    public void FirstSpawn()
    {
        ItemData itemData = ItemData.Instance;
        PortalSystem portalSystem = PortalSystem.Instance;

        StartCoroutine(StartWalkerWalk(itemData.field.walkerCount, itemData, portalSystem));

        if (GameManager.Instance.level % 10 != 0)
        {
        }
        else
        {
            // StartBossWalk();
        }
    }

    public GameObject GetObject(int ID)
    {
        return ObjectPool.Instance.GetPooledObject(_OPRunnerCount + ID);
    }

    public IEnumerator RemoveWalker(GameObject walker, WalkerID walkerID)
    {
        walkerID.animController.CallDeadAnim();
        Walker.Remove(walker);
        yield return new WaitForSeconds(_deadTime);
        ObjectPool.Instance.AddObject(_OPRunnerCount + walker.GetComponent<WalkerID>().ID, walker);
    }

    private IEnumerator StartWalkerWalk(int walkerCount, ItemData itemData, PortalSystem portalSystem)
    {
        for (int i1 = itemData.field.walkerTypeCount; i1 >= 0; i1--)
        {
            for (int i2 = 0; i2 < walkerCount + (levelModRunnerPlusCount * i1); i2++)
            {
                this.walkerCount++;
                yield return null;
            }
            if (i1 != 0) this.walkerCount += HelicopterSystem.Instance.helicopterCount;
        }


        for (int i1 = itemData.field.walkerTypeCount; i1 >= 0; i1--)
        {
            if (i1 != 0)
            {
                portalSystem.PortalOpen();
                yield return new WaitForSeconds(portalSystem.portalOpenTime);
            }

            for (int i2 = 0; i2 < walkerCount + (levelModRunnerPlusCount * i1); i2++)
            {
                GameObject obj = GetObject(i1);
                WalkerID walkerID = obj.GetComponent<WalkerID>();

                WalkerStatPlacement(obj, walkerID, i1, itemData.field.walkerHealth - (i1 * itemData.constant.walkerHealth));
                yield return new WaitForSeconds(_spawnCoundownTime);
            }

            StartCoroutine(HelicopterSystem.Instance.HelicopterSystemStart(i1));

            portalSystem.PortalClose();
            yield return new WaitForSeconds(portalSystem.portalOpenTime);
        }
        yield return null;
    }
    public void WalkerStatPlacement(GameObject obj, WalkerID walkerID, int ID, int health)
    {
        Walker.Add(obj);
        walkerID.animController.CallRunAnim();
        walkerID.CharacterBar.StartCameraLook();
        walkerID.StartWalkerID(ID, health);
        StartNewRunner(obj, walkerID);
    }
    private void StartNewRunner(GameObject walker, WalkerID walkerID)
    {
        WalkerPlacement(ref walker, _walkerStartPos);
        StartCoroutine(WalkPart(walker, _walkerFinishPos, _speedFactor, walkerID, _maxWalkerDisance));
    }
    private void WalkerPlacement(ref GameObject walker, GameObject pos)
    {
        walker.transform.position = pos.transform.position;
        walker.transform.position += new Vector3(Random.Range(-1 * _spawnDistance, _spawnDistance), 0, 0);
    }
    private IEnumerator WalkPart(GameObject walker, GameObject pos, float factor, WalkerID walkerID, float maxWalkerDisance)
    {
        while (walkerID.isLive && GameManager.Instance.gameStat == GameManager.GameStat.start && walker.activeInHierarchy)
        {
            Vector3 direction = (pos.transform.position - walker.transform.position).normalized;
            walker.transform.position += direction * factor * Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
            if (maxWalkerDisance > Vector3.Distance(walker.transform.position, pos.transform.position)) break;
        }
    }
}
