using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoSingleton<ItemData>
{
    [System.Serializable]
    public class Field
    {
        public int castleHealth, gunDistance, gunAtackPower, walkerHealth, walkerCastleHitPower, walkerCount, walkerTypeCount;
        public float gunReloadTime;
    }

    public Field field;
    public Field standart;
    public Field factor;
    public Field constant;
    public Field maxFactor;
    public Field max;
    public Field fieldPrice;

    public void AwakeID()
    {
        field.castleHealth = standart.castleHealth + (factor.castleHealth * constant.castleHealth);
        fieldPrice.castleHealth = fieldPrice.castleHealth * factor.castleHealth;
        field.gunDistance = standart.gunDistance + (factor.gunDistance * constant.gunDistance);
        fieldPrice.gunDistance = fieldPrice.gunDistance * factor.gunDistance;
        field.gunAtackPower = standart.gunAtackPower + (factor.gunAtackPower * constant.gunAtackPower);
        fieldPrice.gunAtackPower = fieldPrice.gunAtackPower * factor.gunAtackPower;
        field.walkerHealth = standart.walkerHealth + (factor.walkerHealth * constant.walkerHealth);
        fieldPrice.walkerHealth = fieldPrice.walkerHealth * factor.walkerHealth;
        field.gunReloadTime = standart.gunReloadTime - (factor.gunReloadTime * constant.gunReloadTime);
        fieldPrice.gunReloadTime = fieldPrice.gunReloadTime * factor.gunReloadTime;
        field.walkerCastleHitPower = standart.walkerCastleHitPower + (factor.walkerCastleHitPower * constant.walkerCastleHitPower);
        fieldPrice.walkerCastleHitPower = fieldPrice.walkerCastleHitPower * factor.walkerCastleHitPower;
        field.walkerCount = standart.walkerCount + (factor.walkerCount * constant.walkerCount);
        fieldPrice.walkerCount = fieldPrice.walkerCount * factor.walkerCount;
        field.walkerTypeCount = standart.walkerTypeCount + (factor.walkerTypeCount * constant.walkerTypeCount);
        fieldPrice.walkerTypeCount = fieldPrice.walkerTypeCount * factor.walkerTypeCount;

        /*
         field.objectCount = standart.objectCount + (factor.objectCount * constant.objectCount);
        fieldPrice.objectCount = fieldPrice.objectCount * factor.objectCount;
        */

        if (factor.castleHealth > maxFactor.castleHealth)
        {
            factor.castleHealth = maxFactor.castleHealth;
            field.castleHealth = standart.castleHealth + (factor.castleHealth * constant.castleHealth);
            fieldPrice.castleHealth = fieldPrice.castleHealth / (factor.castleHealth - 1);
            fieldPrice.castleHealth = fieldPrice.castleHealth * factor.castleHealth;
        }
        if (factor.gunDistance > maxFactor.gunDistance)
        {
            factor.gunDistance = maxFactor.gunDistance;
            field.gunDistance = standart.gunDistance + (factor.gunDistance * constant.gunDistance);
            fieldPrice.gunDistance = fieldPrice.gunDistance / (factor.gunDistance - 1);
            fieldPrice.gunDistance = fieldPrice.gunDistance * factor.gunDistance;
        }
        if (factor.gunAtackPower > maxFactor.gunAtackPower)
        {
            factor.gunAtackPower = maxFactor.gunAtackPower;
            field.gunAtackPower = standart.gunAtackPower + (factor.gunAtackPower * constant.gunAtackPower);
            fieldPrice.gunAtackPower = fieldPrice.gunAtackPower / (factor.gunAtackPower - 1);
            fieldPrice.gunAtackPower = fieldPrice.gunAtackPower * factor.gunAtackPower;
        }
        if (factor.walkerHealth > maxFactor.walkerHealth)
        {
            factor.walkerHealth = maxFactor.walkerHealth;
            field.walkerHealth = standart.walkerHealth + (factor.walkerHealth * constant.walkerHealth);
            fieldPrice.walkerHealth = fieldPrice.walkerHealth / (factor.walkerHealth - 1);
            fieldPrice.walkerHealth = fieldPrice.walkerHealth * factor.walkerHealth;
        }
        if (factor.gunReloadTime > maxFactor.gunReloadTime)
        {
            factor.gunReloadTime = maxFactor.gunReloadTime;
            field.gunReloadTime = standart.gunReloadTime - (factor.gunReloadTime * constant.gunReloadTime);
            fieldPrice.gunReloadTime = fieldPrice.gunReloadTime / (factor.gunReloadTime - 1);
            fieldPrice.gunReloadTime = fieldPrice.gunReloadTime * factor.gunReloadTime;
        }
        if (factor.walkerCastleHitPower > maxFactor.walkerCastleHitPower)
        {
            factor.walkerCastleHitPower = maxFactor.walkerCastleHitPower;
            field.walkerCastleHitPower = standart.walkerCastleHitPower + (factor.walkerCastleHitPower * constant.walkerCastleHitPower);
            fieldPrice.walkerCastleHitPower = fieldPrice.walkerCastleHitPower / (factor.walkerCastleHitPower - 1);
            fieldPrice.walkerCastleHitPower = fieldPrice.walkerCastleHitPower * factor.walkerCastleHitPower;
        }
        if (factor.walkerCount > maxFactor.walkerCount)
        {
            factor.walkerCount = maxFactor.walkerCount;
            field.walkerCount = standart.walkerCount + (factor.walkerCount * constant.walkerCount);
            fieldPrice.walkerCount = fieldPrice.walkerCount / (factor.walkerCount - 1);
            fieldPrice.walkerCount = fieldPrice.walkerCount * factor.walkerCount;
        }
        if (factor.walkerTypeCount > maxFactor.walkerTypeCount)
        {
            factor.walkerTypeCount = maxFactor.walkerTypeCount;
            field.walkerTypeCount = standart.walkerTypeCount + (factor.walkerTypeCount * constant.walkerTypeCount);
            fieldPrice.walkerTypeCount = fieldPrice.walkerTypeCount / (factor.walkerTypeCount - 1);
            fieldPrice.walkerTypeCount = fieldPrice.walkerTypeCount * factor.walkerTypeCount;
        }

        /*
          if (factor.objectCount > maxFactor.objectCount)
        {
            factor.objectCount = maxFactor.objectCount;
            field.objectCount = standart.objectCount + (factor.objectCount * constant.objectCount);
            fieldPrice.objectCount = fieldPrice.objectCount / (factor.objectCount - 1);
            fieldPrice.objectCount = fieldPrice.objectCount * factor.objectCount;
        }
        */

        StartCoroutine(Buttons.Instance.LoadingScreen());
    }

    public void SetCastleHealth()
    {
        factor.castleHealth++;

        field.castleHealth = standart.castleHealth + (factor.castleHealth * constant.castleHealth);
        fieldPrice.castleHealth = fieldPrice.castleHealth / (factor.castleHealth - 1);
        fieldPrice.castleHealth = fieldPrice.castleHealth * factor.castleHealth;

        if (factor.castleHealth > maxFactor.castleHealth)
        {
            factor.castleHealth = maxFactor.castleHealth;
            field.castleHealth = standart.castleHealth + (factor.castleHealth * constant.castleHealth);
            fieldPrice.castleHealth = fieldPrice.castleHealth / (factor.castleHealth - 1);
            fieldPrice.castleHealth = fieldPrice.castleHealth * factor.castleHealth;
        }

        GameManager.Instance.FactorPlacementWrite(factor);
    }

    public void SetGunDistance()
    {
        factor.gunDistance++;

        field.gunDistance = standart.gunDistance + (factor.gunDistance * constant.gunDistance);
        fieldPrice.gunDistance = fieldPrice.gunDistance / (factor.gunDistance - 1);
        fieldPrice.gunDistance = fieldPrice.gunDistance * factor.gunDistance;

        if (factor.gunDistance > maxFactor.gunDistance)
        {
            factor.gunDistance = maxFactor.gunDistance;
            field.gunDistance = standart.gunDistance + (factor.gunDistance * constant.gunDistance);
            fieldPrice.gunDistance = fieldPrice.gunDistance / (factor.gunDistance - 1);
            fieldPrice.gunDistance = fieldPrice.gunDistance * factor.gunDistance;
        }

        GameManager.Instance.FactorPlacementWrite(factor);
    }

    public void SetGunAtackPower()
    {
        factor.gunAtackPower++;

        field.gunAtackPower = standart.gunAtackPower + (factor.gunAtackPower * constant.gunAtackPower);
        fieldPrice.gunAtackPower = fieldPrice.gunAtackPower / (factor.gunAtackPower - 1);
        fieldPrice.gunAtackPower = fieldPrice.gunAtackPower * factor.gunAtackPower;

        if (factor.gunAtackPower > maxFactor.gunAtackPower)
        {
            factor.gunAtackPower = maxFactor.gunAtackPower;
            field.gunAtackPower = standart.gunAtackPower + (factor.gunAtackPower * constant.gunAtackPower);
            fieldPrice.gunAtackPower = fieldPrice.gunAtackPower / (factor.gunAtackPower - 1);
            fieldPrice.gunAtackPower = fieldPrice.gunAtackPower * factor.gunAtackPower;
        }

        GameManager.Instance.FactorPlacementWrite(factor);
    }
    public void SetWalkerHealth()
    {
        factor.walkerHealth++;

        field.walkerHealth = standart.walkerHealth + (factor.walkerHealth * constant.walkerHealth);
        fieldPrice.walkerHealth = fieldPrice.walkerHealth / (factor.walkerHealth - 1);
        fieldPrice.walkerHealth = fieldPrice.walkerHealth * factor.walkerHealth;

        if (factor.walkerHealth > maxFactor.walkerHealth)
        {
            factor.walkerHealth = maxFactor.walkerHealth;
            field.walkerHealth = standart.walkerHealth + (factor.walkerHealth * constant.walkerHealth);
            fieldPrice.walkerHealth = fieldPrice.walkerHealth / (factor.walkerHealth - 1);
            fieldPrice.walkerHealth = fieldPrice.walkerHealth * factor.walkerHealth;
        }

        GameManager.Instance.FactorPlacementWrite(factor);
    }
    public void SetGunReloadTime()
    {
        factor.gunReloadTime++;

        field.gunReloadTime = standart.gunReloadTime - (factor.gunReloadTime * constant.gunReloadTime);
        fieldPrice.gunReloadTime = fieldPrice.gunReloadTime / (factor.gunReloadTime - 1);
        fieldPrice.gunReloadTime = fieldPrice.gunReloadTime * factor.gunReloadTime;

        if (factor.gunReloadTime > maxFactor.gunReloadTime)
        {
            factor.gunReloadTime = maxFactor.gunReloadTime;
            field.gunReloadTime = standart.gunReloadTime - (factor.gunReloadTime * constant.gunReloadTime);
            fieldPrice.gunReloadTime = fieldPrice.gunReloadTime / (factor.gunReloadTime - 1);
            fieldPrice.gunReloadTime = fieldPrice.gunReloadTime * factor.gunReloadTime;
        }

        GameManager.Instance.FactorPlacementWrite(factor);
    }
    public void SetWalkerCastleHitPower()
    {
        factor.walkerCastleHitPower++;

        field.walkerCastleHitPower = standart.walkerCastleHitPower + (factor.walkerCastleHitPower * constant.walkerCastleHitPower);
        fieldPrice.walkerCastleHitPower = fieldPrice.walkerCastleHitPower / (factor.walkerCastleHitPower - 1);
        fieldPrice.walkerCastleHitPower = fieldPrice.walkerCastleHitPower * factor.walkerCastleHitPower;

        if (factor.walkerCastleHitPower > maxFactor.walkerCastleHitPower)
        {
            factor.walkerCastleHitPower = maxFactor.walkerCastleHitPower;
            field.walkerCastleHitPower = standart.walkerCastleHitPower + (factor.walkerCastleHitPower * constant.walkerCastleHitPower);
            fieldPrice.walkerCastleHitPower = fieldPrice.walkerCastleHitPower / (factor.walkerCastleHitPower - 1);
            fieldPrice.walkerCastleHitPower = fieldPrice.walkerCastleHitPower * factor.walkerCastleHitPower;
        }

        GameManager.Instance.FactorPlacementWrite(factor);
    }
    public void SetWalkerCount()
    {
        factor.walkerCount++;

        field.walkerCount = standart.walkerCount + (factor.walkerCount * constant.walkerCount);
        fieldPrice.walkerCount = fieldPrice.walkerCount / (factor.walkerCount - 1);
        fieldPrice.walkerCount = fieldPrice.walkerCount * factor.walkerCount;

        if (factor.walkerCount > maxFactor.walkerCount)
        {
            factor.walkerCount = maxFactor.walkerCount;
            field.walkerCount = standart.walkerCount + (factor.walkerCount * constant.walkerCount);
            fieldPrice.walkerCount = fieldPrice.walkerCount / (factor.walkerCount - 1);
            fieldPrice.walkerCount = fieldPrice.walkerCount * factor.walkerCount;
        }

        GameManager.Instance.FactorPlacementWrite(factor);
    }
    public void SetWalkerTypeCount()
    {
        factor.walkerTypeCount++;

        field.walkerTypeCount = standart.walkerTypeCount + (factor.walkerTypeCount * constant.walkerTypeCount);
        fieldPrice.walkerTypeCount = fieldPrice.walkerTypeCount / (factor.walkerTypeCount - 1);
        fieldPrice.walkerTypeCount = fieldPrice.walkerTypeCount * factor.walkerTypeCount;

        if (factor.walkerTypeCount > maxFactor.walkerTypeCount)
        {
            factor.walkerTypeCount = maxFactor.walkerTypeCount;
            field.walkerTypeCount = standart.walkerTypeCount + (factor.walkerTypeCount * constant.walkerTypeCount);
            fieldPrice.walkerTypeCount = fieldPrice.walkerTypeCount / (factor.walkerTypeCount - 1);
            fieldPrice.walkerTypeCount = fieldPrice.walkerTypeCount * factor.walkerTypeCount;
        }

        GameManager.Instance.FactorPlacementWrite(factor);
    }

    /*
     public void SetObjectCount()
    {
        factor.objectCount++;

        field.objectCount = standart.objectCount + (factor.objectCount * constant.objectCount);
        fieldPrice.objectCount = fieldPrice.objectCount / (factor.objectCount - 1);
        fieldPrice.objectCount = fieldPrice.objectCount * factor.objectCount;

        if (factor.objectCount > maxFactor.objectCount)
        {
            factor.objectCount = maxFactor.objectCount;
            field.objectCount = standart.objectCount + (factor.objectCount * constant.objectCount);
            fieldPrice.objectCount = fieldPrice.objectCount / (factor.objectCount - 1);
            fieldPrice.objectCount = fieldPrice.objectCount * factor.objectCount;
        }

        GameManager.Instance.FactorPlacementWrite(factor);
    }
    */
}
