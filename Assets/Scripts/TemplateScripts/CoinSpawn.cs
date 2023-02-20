using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawn : MonoSingleton<CoinSpawn>
{
    [SerializeField] private int _OPCoinCount;
    public GameObject finishPos;

    public void Spawn(GameObject pos, GameObject finishPos)
    {
        StartCoroutine(SpawnEnum(pos, finishPos));
    }

    private IEnumerator SpawnEnum(GameObject pos, GameObject finishPos)
    {
        int coinCount;
        coinCount = Random.Range(5, 12);
        List<GameObject> coins = new List<GameObject>();

        for (int i = 0; i < coinCount; i++)
        {
            GameObject obj = ObjectPool.Instance.GetPooledObject(_OPCoinCount);
            Rigidbody rb = obj.GetComponent<Rigidbody>();

            obj.transform.position = pos.transform.position;
            obj.transform.position += new Vector3(0, 2, 0);
            rb.velocity = new Vector3(Random.Range(-3, 3), 0, Random.Range(-3, 3));
            rb.useGravity = true;
            coins.Add(obj);
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(1);

        for (int i = 0; i < coinCount; i++)
            StartCoroutine(Walk(coins[i], finishPos));
    }
    private IEnumerator Walk(GameObject obj, GameObject Finish)
    {
        float lerpCount = 0;

        obj.GetComponent<Rigidbody>().useGravity = false;
        while (true)
        {
            lerpCount += Time.deltaTime;
            obj.transform.position = Vector3.Lerp(obj.transform.position, Finish.transform.position + new Vector3(0, 2, 0), lerpCount);
            yield return new WaitForSeconds(Time.deltaTime);
            if (2 > Vector3.Distance(obj.transform.position, Finish.transform.position + new Vector3(0, 2, 0)))
            {
                SoundSystem.Instance.CallCoinSound();
                ObjectPool.Instance.AddObject(_OPCoinCount, obj);
                break;
            }
        }
    }
}