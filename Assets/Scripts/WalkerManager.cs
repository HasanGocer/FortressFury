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

    public void FirstSpawn()
    {
        ItemData itemData = ItemData.Instance;

        if (GameManager.Instance.level % 10 != 0)
        {
            StartCoroutine(StartWalkerWalk(itemData.field.walkerCount, itemData));
        }
        else
        {
            // StartBossWalk();
        }
    }

    public void RemoveWalker(GameObject walker)
    {
        Walker.Remove(walker);
        ObjectPool.Instance.AddObject(_OPRunnerCount + walker.GetComponent<WalkerID>().ID, walker);
    }

    private IEnumerator StartWalkerWalk(int walkerCount, ItemData itemData)
    {
        for (int i1 = itemData.field.walkerTypeCount; i1 >= 0; i1--)
        {
            walkerCount += walkerCount + (levelModRunnerPlusCount * i1);
            for (int i2 = 0; i2 < walkerCount + (levelModRunnerPlusCount * i1); i2++)
            {
                GameObject obj = GetObject(i1);
                WalkerID walkerID = obj.GetComponent<WalkerID>();

                WalkerStatPlacement(obj, walkerID, i1, itemData.field.walkerHealth - (i1 * itemData.constant.walkerHealth));
                yield return new WaitForSeconds(_spawnCoundownTime);
            }
        }
        yield return null;
    }
    private GameObject GetObject(int ID)
    {
        return ObjectPool.Instance.GetPooledObject(_OPRunnerCount + ID);
    }
    private void WalkerStatPlacement(GameObject obj, WalkerID walkerID, int ID, int health)
    {
        Walker.Add(obj);
        walkerID.CharacterBar.StartCameraLook();
        walkerID.StartWalkerID(ID, health);
        StartNewRunner(obj, walkerID);
    }
    private void StartNewRunner(GameObject walker, WalkerID walkerID)
    {
        WalkerPlacement(ref walker, _walkerStartPos);
        StartCoroutine(WalkPart(walker, _walkerFinishPos, _speedFactor + ItemData.Instance.field.walkerSpeed, walkerID, _maxWalkerDisance));
    }
    private void WalkerPlacement(ref GameObject walker, GameObject pos)
    {
        walker.transform.position = pos.transform.position;
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
