using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerTouch : MonoBehaviour
{
    [SerializeField] WalkerID walkerID;
    [SerializeField] GameObject textFinishPos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hit"))
        {
            ItemData itemData = ItemData.Instance;

            HealtDown(itemData);
            PointText.Instance.CallPointText(gameObject, itemData.field.gunAtackPower, PointText.PointType.RedHit);
            ParticalSystem.Instance.CallHitCrashPartical(gameObject);
            walkerID.CharacterBar.BarUpdate(itemData.field.walkerHealth, walkerID.healthCount, itemData.field.gunAtackPower);
        }
        if (other.CompareTag("Castle") && walkerID.isLive)
        {
            walkerID.isLive = false;
            walkerID.capsuleCollider.enabled = false;

            ItemData itemData = ItemData.Instance;

            FinishSystem.Instance.FinishCheck();
            MainBar.Instance.BarUpdate(itemData.field.castleHealth, MainManager.Instance.mainHealth, itemData.field.walkerCastleHitPower);
            PointText.Instance.CallPointText(gameObject, itemData.field.walkerCastleHitPower, PointText.PointType.RedHit);
            ParticalSystem.Instance.CallWalkerHitCastelPartical(gameObject);
            RunnerManager.Instance.RemoveWalker(gameObject);
        }
    }

    private void HealtDown(ItemData itemData)
    {
        walkerID.healthCount -= itemData.field.gunAtackPower;
    }
}
