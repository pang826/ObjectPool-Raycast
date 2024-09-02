using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    Rigidbody rigid; // 리지드바디
    [SerializeField]
    float speed; // 총알속도
    [SerializeField]
    float reloadTime; // 총알삭제시간
    [SerializeField]
    GameObject shotPoint; // 총알발사 위치
    
    Coroutine reloadBullet; // 삭제를 위한 코루틴
    private void Awake()
    {
        shotPoint = GameObject.FindWithTag("ShotPoint");
        transform.rotation = shotPoint.transform.rotation;
        rigid = gameObject.GetComponent<Rigidbody>();
        reloadTime = 3f;
    }

    private void OnEnable()
    {
        rigid.velocity = transform.forward * speed;
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            reloadBullet = StartCoroutine(ReloadBullet());
        }
    }
    void OnDisable()
    {
        StopCoroutine(reloadBullet);
    }

    IEnumerator ReloadBullet()
    {
        WaitForSeconds delay = new WaitForSeconds(reloadTime);
        yield return delay;
        BulletObjectPool.ReturnBullet(this);
    }
}
