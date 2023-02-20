using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalSystem : MonoSingleton<ParticalSystem>
{
    [Header("Partical_Field")]
    [Space(10)]

    [SerializeField] int _OPWalkerDieParticalCount;
    [SerializeField] int _OPWalkerHitParticalCount;
    [SerializeField] int _OPHitParticalCount;
    [SerializeField] int _OPGunCrashFirstParticalCount;
    [SerializeField] int _OPGunCrashSecondParticalCount;
    [SerializeField] int _OPNewObjectParticalCount;
    [SerializeField] int _OPHitFinishParticalCount;
    [SerializeField] int _walkerHitCastleParticalTime, _walkerDieParticalTime, _hitParticalTime, _gunCrashWaitTime, _newObjectTime, _hitFinishTime;
    [SerializeField] float _walkerDieDistance;
    [SerializeField] GameObject newObjectParticalPos;

    public void CallWalkerHitCastelPartical(GameObject pos)
    {
        StartCoroutine(CallWalkerHitCastelParticalEnum(pos));
    }
    public void CallWalkerDiePartical(GameObject pos)
    {
        StartCoroutine(CallWalkerDieParticalEnum(pos));
    }
    public void CallHitPartical(GameObject pos)
    {
        StartCoroutine(CallHitParticalEnum(pos));
    }
    public void CallGunCrashPartical(GameObject pos)
    {
        StartCoroutine(CallCrashGunEnum(pos));
    }
    public void CallNewObjectPartical()
    {
        StartCoroutine(CallNewObjectParticalEnum());
    }
    public void CallHitFinishPartical(GameObject pos)
    {
        StartCoroutine(CallHitFinishParticalEnum(pos));
    }

    private IEnumerator CallWalkerDieParticalEnum(GameObject pos)
    {
        GameObject partical = ObjectPool.Instance.GetPooledObject(_OPWalkerDieParticalCount);
        partical.transform.position = pos.transform.position;
        partical.transform.position += new Vector3(0, _walkerDieDistance, 0);
        yield return new WaitForSeconds(_walkerDieParticalTime);
        ObjectPool.Instance.AddObject(_OPWalkerDieParticalCount, partical);
    }
    private IEnumerator CallWalkerHitCastelParticalEnum(GameObject pos)
    {
        GameObject partical = ObjectPool.Instance.GetPooledObject(_OPWalkerHitParticalCount);
        partical.transform.position = pos.transform.position;
        yield return new WaitForSeconds(_walkerHitCastleParticalTime);
        ObjectPool.Instance.AddObject(_OPWalkerHitParticalCount, partical);
    }
    private IEnumerator CallHitParticalEnum(GameObject pos)
    {
        GameObject partical = ObjectPool.Instance.GetPooledObject(_OPHitParticalCount);
        partical.transform.position = pos.transform.position;
        yield return new WaitForSeconds(_hitParticalTime);
        ObjectPool.Instance.AddObject(_OPHitParticalCount, partical);
    }
    private IEnumerator CallNewObjectParticalEnum()
    {
        GameObject partical = ObjectPool.Instance.GetPooledObject(_OPNewObjectParticalCount);
        partical.transform.position = newObjectParticalPos.transform.position;
        yield return new WaitForSeconds(_newObjectTime);
        ObjectPool.Instance.AddObject(_OPNewObjectParticalCount, partical);
    }
    private IEnumerator CallHitFinishParticalEnum(GameObject pos)
    {
        GameObject partical = ObjectPool.Instance.GetPooledObject(_OPHitFinishParticalCount);
        partical.transform.position = pos.transform.position;
        yield return new WaitForSeconds(_hitFinishTime);
        ObjectPool.Instance.AddObject(_OPHitFinishParticalCount, partical);
    }
    private IEnumerator CallCrashGunEnum(GameObject pos)
    {
        GameObject partical1 = ObjectPool.Instance.GetPooledObject(_OPGunCrashFirstParticalCount);
        partical1.transform.position = pos.transform.position;
        yield return new WaitForSeconds(_gunCrashWaitTime);
        GameObject partical2 = ObjectPool.Instance.GetPooledObject(_OPGunCrashSecondParticalCount);
        partical2.transform.position = pos.transform.position;
    }

}
