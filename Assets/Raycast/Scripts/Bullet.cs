using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    Rigidbody rigid; // ������ٵ�
    [SerializeField]
    float speed; // �Ѿ˼ӵ�
    [SerializeField]
    float reloadTime; // �Ѿ˻����ð�
    [SerializeField]
    GameObject shotPoint; // �Ѿ˹߻� ��ġ
    
    Coroutine reloadBullet; // ������ ���� �ڷ�ƾ
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
