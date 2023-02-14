using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundFlip : MonoSingleton<BackgroundFlip>
{
    [SerializeField] private RectTransform background;

    public IEnumerator Flip()
    {
        float count = 0;
        while (true)
        {
            count += Time.deltaTime * 10;
            background.rotation = Quaternion.Euler(new Vector3(0, 0, count));
            yield return new WaitForEndOfFrame();
        }
    }
}
