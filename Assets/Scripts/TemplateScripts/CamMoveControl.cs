using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMoveControl : MonoSingleton<CamMoveControl>
{
    public GameObject target;
    [SerializeField] private Vector3 distance;
    [SerializeField] private float time;

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.transform.position + distance, time * Time.deltaTime);
    }
}