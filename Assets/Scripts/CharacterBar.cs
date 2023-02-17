using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterBar : MonoBehaviour
{
    [Header("Walker_Bar_Field")]
    [Space(10)]

    [SerializeField] GameObject _barPanel;
    GameObject mainCamera;
    [SerializeField] private Image _bar;
    [SerializeField] WalkerID walkerID;

    public void StartCameraLook()
    {
        mainCamera = Camera.main.gameObject;
        StartCoroutine(LookCamera());
    }

    public void BarUpdate(int max, int count, int down, GameObject finishPos)
    {
        float nowBar = (float)count / (float)max;
        float afterBar = ((float)count - (float)down) / (float)max;
        if (afterBar < 0) afterBar = 0;
        StartCoroutine(BarUpdateIenumurator(nowBar, afterBar, finishPos));
    }
    public void BarRestart()
    {
        _bar.fillAmount = 1;
    }

    private IEnumerator LookCamera()
    {
        while (true)
        {
            if (GameManager.Instance.gameStat == GameManager.GameStat.start)
            {
                _barPanel.transform.LookAt(mainCamera.transform);
                yield return new WaitForSeconds(Time.deltaTime);
            }
            yield return null;
        }
    }
    private IEnumerator BarUpdateIenumurator(float start, float finish, GameObject finishPos)
    {
        yield return null;
        float temp = 0;
        while (true)
        {
            temp += Time.deltaTime;
            _bar.fillAmount = Mathf.Lerp(start, finish, temp);
            yield return new WaitForEndOfFrame();
            if (_bar.fillAmount == 0)
            {
                FinishGame(finishPos);
                break;
            }
            if (_bar.fillAmount <= finish) break;
        }
    }
    private void FinishGame(GameObject finishPos)
    {
        WalkerManager walkerManager = WalkerManager.Instance;

        if (walkerID.isLive)
        {
            walkerID.isLive = false;
            int money = Random.Range(walkerManager.minPrice, walkerManager.maxPrice);
            CoinSpawn.Instance.Spawn(gameObject, finishPos);
            MoneySystem.Instance.MoneyTextRevork(money);
            MarketSystem.Instance.ButtonColorPlacement();
            PointText.Instance.CallCoinText(gameObject, money);
            ParticalSystem.Instance.CallWalkerDiePartical(gameObject);
            FinishSystem.Instance.FinishCheck();
            Vibration.Vibrate(30);
            StartCoroutine(walkerManager.RemoveWalker(gameObject, walkerID));
        }
    }
}
