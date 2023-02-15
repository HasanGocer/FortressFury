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
            PointText.Instance.CallDamageText(gameObject, itemData.field.gunAtackPower);
            walkerID.CharacterBar.BarUpdate(itemData.field.walkerHealth, walkerID.healthCount, itemData.field.gunAtackPower,MainManager.Instance.castle);
        }
        if (other.CompareTag("Castle") && walkerID.isLive)
        {
            walkerID.isLive = false;
            walkerID.capsuleCollider.enabled = false;

            ItemData itemData = ItemData.Instance;

            FinishSystem.Instance.FinishCheck();
            MainBar.Instance.BarUpdate(itemData.field.castleHealth, MainManager.Instance.mainHealth, itemData.field.walkerCastleHitPower);
            PointText.Instance.CallDamageText(gameObject, itemData.field.walkerCastleHitPower);
            WalkerManager.Instance.RemoveWalker(gameObject);
        }
    }

    private void HealtDown(ItemData itemData)
    {
        walkerID.healthCount -= itemData.field.gunAtackPower;
    }
}
