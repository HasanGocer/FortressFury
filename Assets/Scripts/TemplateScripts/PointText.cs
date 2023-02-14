using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class PointText : MonoSingleton<PointText>
{
    public enum PointType
    {
        RedHit = 0,
        GreenHit = 1,
        yellowHit = 2
    }

    [Header("Text_Field")]
    [Space(10)]

    [SerializeField] private int _OPMoneyInt;
    [SerializeField] private float _textMoveTime;
    [SerializeField] private float _moneyJumpDistance;
    [SerializeField] Ease _moveEaseType = Ease.InOutBounce;

    public void CallPointText(GameObject Pos, int count, PointType pointType)
    {
        StartCoroutine(CallPointMoneyText(Pos, count, pointType));
    }

    private IEnumerator CallPointMoneyText(GameObject Pos, int count, PointType pointType)
    {
        GameObject obj = ObjectPool.Instance.GetPooledObject(_OPMoneyInt);

        if (pointType == PointType.GreenHit) obj.GetComponent<TMP_Text>().color = Color.green;
        if (pointType == PointType.RedHit) obj.GetComponent<TMP_Text>().color = Color.red;
        if (pointType == PointType.yellowHit) obj.GetComponent<TMP_Text>().color = Color.yellow;

        obj.GetComponent<TMP_Text>().text = count.ToString();
        obj.transform.position = Pos.transform.position;
        obj.transform.DOMove(new Vector3(Pos.transform.position.x, Pos.transform.position.y + _moneyJumpDistance, Pos.transform.position.z), _textMoveTime).SetEase(_moveEaseType);
        yield return new WaitForSeconds(_textMoveTime);
        ObjectPool.Instance.AddObject(_OPMoneyInt, obj);
    }
}
