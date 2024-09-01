using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public BulletState bulletPrefab;
    [SerializeField]
    Transform shotPoint;
    
    [SerializeField]
    BulletState[] prefab;
    private void Awake()
    {
        
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            bulletPrefab = prefab[0];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            bulletPrefab = prefab[1];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            bulletPrefab = prefab[2];
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }
    public void Fire()
    {
        BulletState bullet = ObjectPool.GetObject(bulletPrefab);
        bullet.transform.position = shotPoint.position;
        bullet.transform.rotation = shotPoint.rotation;
    }
}
