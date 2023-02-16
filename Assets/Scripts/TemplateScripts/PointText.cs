using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using DamageNumbersPro;

public class PointText : MonoSingleton<PointText>
{
    [Header("Text_Field")]
    [Space(10)]

    [SerializeField] private int _OPDamageTextCount;
    [SerializeField] private int _OPCoinTextCount;
    [SerializeField] private float _textMoveTime;

    public void CallDamageText(GameObject Pos, int count)
    {
        StartCoroutine(CallPointDamageText(Pos, count));
    }

    public void CallCoinText(GameObject Pos, int count)
    {
        StartCoroutine(CallPointCoinText(Pos, count));
    }

    private IEnumerator CallPointDamageText(GameObject Pos, int count)
    {
        GameObject obj = ObjectPool.Instance.GetPooledObject(_OPDamageTextCount);

        obj.GetComponent<DamageNumberMesh>().number = count;
        obj.transform.position = Pos.transform.position;
        yield return new WaitForSeconds(_textMoveTime);
        ObjectPool.Instance.AddObject(_OPDamageTextCount, obj);
    }
    private IEnumerator CallPointCoinText(GameObject Pos, int count)
    {
        GameObject obj = ObjectPool.Instance.GetPooledObject(_OPCoinTextCount);

        obj.GetComponent<DamageNumberMesh>().number = count;
        obj.transform.position = Pos.transform.position;
        yield return new WaitForSeconds(_textMoveTime);
        ObjectPool.Instance.AddObject(_OPCoinTextCount, obj);
    }
}
