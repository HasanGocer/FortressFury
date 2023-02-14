using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DamageNumbersPro;

public class WalkerManager : MonoSingleton<WalkerManager>
{
    [Header("Walker_Field")]
    [Space(10)]

    public float walkerDieTime;
    public int bossModLevel;
    public int minPrice, maxPrice;
}
