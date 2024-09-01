using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTest : MonoBehaviour
{
    [SerializeField] 
    Transform muzzlePoint;

    [SerializeField]
    float maxDistance;

    [SerializeField]
    LayerMask layerMask;
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Fire();
        }
        
    }

    public void Fire()
    {
        if (Physics.Raycast(muzzlePoint.position, muzzlePoint.forward, out RaycastHit hit, maxDistance, layerMask))
        {
            Debug.Log($"쏘는 위치에 {hit.collider.gameObject.name}가 있다");
            Debug.DrawRay(muzzlePoint.position, muzzlePoint.forward * hit.distance, Color.red, 0.2f);
        }
        else
        {
            Debug.DrawRay(muzzlePoint.position, muzzlePoint.forward * 100, Color.red, 0.2f);
        }
    }
}
