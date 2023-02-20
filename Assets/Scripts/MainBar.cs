using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainBar : MonoSingleton<MainBar>
{
    [Header("Castle_Bar_Field")]
    [Space(10)]

    [SerializeField] private Image _bar;
    [SerializeField] private GameObject mainBar;
    GameObject mainCamera;

    public void MainBarStart()
    {
        if (GameManager.Instance.level % WalkerManager.Instance.bossModLevel != 0)
        {
            mainCamera = Camera.main.gameObject;
            mainBar.SetActive(true);
        }
    }

    public void BarUpdate(int max, int count, int down)
    {
        float nowBar = (float)count / (float)max;
        float afterBar = ((float)count - (float)down) / (float)max;
        if (afterBar < 0) afterBar = 0;
        MainManager.Instance.mainHealth -= down;
        StartCoroutine(BarUpdateIenumurator(nowBar, afterBar));
    }

    private IEnumerator LookCamera()
    {
        while (true)
        {
            if (GameManager.Instance.gameStat == GameManager.GameStat.start)
            {
                mainBar.transform.LookAt(mainCamera.transform);
                yield return new WaitForSeconds(Time.deltaTime);
            }
            yield return null;
        }
    }
    private IEnumerator BarUpdateIenumurator(float start, float finish)
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
                FinishGame();
                break;
            }
            if (_bar.fillAmount <= finish) break;
        }
    }

    private void FinishGame()
    {
        if (GameManager.Instance.gameStat == GameManager.GameStat.start)
        {
            Buttons.Instance.SettingPanelOff();
            MarketSystem.Instance.GameFinish();
            WalkerDanceTime();
            ParticalSystem.Instance.CallGunCrashPartical(MainManager.Instance.allGuns.Guns[MainManager.Instance.gunCount].gameObject);
            GameManager.Instance.gameStat = GameManager.GameStat.finish;
            Buttons.Instance.failPanel.SetActive(true);
        }
    }
    private void WalkerDanceTime()
    {
        foreach (GameObject item in WalkerManager.Instance.Walker)
            item.GetComponent<AnimController>().CallDanceAnim();
    }
}
