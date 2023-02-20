using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HelicopterSystem : MonoSingleton<HelicopterSystem>
{
    [System.Serializable]
    public class StringPos
    {
        public List<GameObject> _StringsPos = new List<GameObject>();
    }

    [Header("Finish_Field")]
    [Space(10)]

    public int helicopterCount;
    [SerializeField] float _spawnDelayTime, _stringDownTime;
    [SerializeField] float _stringDownDistance;
    [SerializeField] List<GameObject> _Strings = new List<GameObject>();
    [SerializeField] List<StringPos> _StringPos = new List<StringPos>();

    public IEnumerator HelicopterSystemStart(int ID)
    {
        for (int i1 = 0; i1 < _Strings.Count; i1++)
        {
            StartCoroutine(HelicopterTimeEnum(ID, _Strings[i1], _StringPos[i1]._StringsPos));
            yield return new WaitForSeconds(_spawnDelayTime);
        }

    }

    private IEnumerator HelicopterTimeEnum(int ID, GameObject mainString, List<GameObject> mainSpawnPos)
    {
        List<GameObject> walker = new List<GameObject>();
        List<WalkerID> walkerID = new List<WalkerID>();

        for (int i = 0; i < mainSpawnPos.Count; i++)
        {
            walker.Add(WalkerManager.Instance.GetObject(ID));
            walker[walker.Count - 1].transform.position = mainSpawnPos[i].transform.position;
            walker[walker.Count - 1].transform.SetParent(mainString.transform);
            walkerID.Add(walker[walker.Count - 1].GetComponent<WalkerID>());
            walkerID[walkerID.Count - 1].animController.calLDownAnim();
        }



        mainString.transform.DOMove(new Vector3(mainString.transform.position.x, mainString.transform.position.y - _stringDownDistance, mainString.transform.position.z), _stringDownTime);
        yield return new WaitForSeconds(_stringDownTime);

        for (int i = 0; i < mainSpawnPos.Count; i++)
            walker[i].transform.SetParent(null);

        mainString.transform.DOMove(new Vector3(mainString.transform.position.x, mainString.transform.position.y + _stringDownDistance, mainString.transform.position.z), _stringDownTime);
        for (int i = 0; i < mainSpawnPos.Count; i++)
            WalkerManager.Instance.WalkerStatPlacement(walker[i], walkerID[i], ID, ItemData.Instance.field.walkerHealth, true);
    }

}
