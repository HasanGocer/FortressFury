using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HelicopterSystem : MonoSingleton<HelicopterSystem>
{
    [Header("Finish_Field")]
    [Space(10)]

    public int helicopterCount;
    [SerializeField] float _spawnDelayTime, _stringDownTime;
    [SerializeField] float _stringDownDistance;
    [SerializeField] List<GameObject> _Strings = new List<GameObject>();
    [SerializeField] List<GameObject> _StringsPos = new List<GameObject>();


    public IEnumerator HelicopterSystemStart(int ID)
    {
        for (int i = 0; i < _Strings.Count; i++)
        {
            StartCoroutine(HelicopterTimeEnum(ID, _Strings[i], _StringsPos[i]));
            yield return new WaitForSeconds(_spawnDelayTime);
        }
    }

    private IEnumerator HelicopterTimeEnum(int ID, GameObject mainString, GameObject mainSpawnPos)
    {
        GameObject walker = WalkerManager.Instance.GetObject(ID);
        WalkerID walkerID = walker.GetComponent<WalkerID>();

        walker.transform.position = mainSpawnPos.transform.position;
        walker.transform.SetParent(mainString.transform);
        walkerID.animController.calLDownAnim();

        mainString.transform.DOMove(new Vector3(mainString.transform.position.x, mainString.transform.position.y - _stringDownDistance, mainString.transform.position.z), _stringDownTime);
        yield return new WaitForSeconds(_stringDownTime);

        walker.transform.SetParent(null);
        mainString.transform.DOMove(new Vector3(mainString.transform.position.x, mainString.transform.position.y + _stringDownDistance, mainString.transform.position.z), _stringDownTime);
        WalkerManager.Instance.WalkerStatPlacement(walker, walkerID, ID, ItemData.Instance.field.walkerHealth, true);
    }

}
