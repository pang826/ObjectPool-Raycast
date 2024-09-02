using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    [SerializeField]
    float rotateSpeed;
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    Transform camTrans;
    [SerializeField]
    LayerMask layerMask;
    [SerializeField]
    Coroutine autoFire;
    [SerializeField] 
    float fireDelayTime;
    [SerializeField]
    Bullet prefab;
    [SerializeField]
    Transform shotPoint;
    private void Start()
    {
        Cursor.visible = false;
        // ���콺 Ŀ���� �߾ӿ� �����ϵ��� �ϴ� ��ɾ�
        Cursor.lockState = CursorLockMode.Locked;
        // None : ���콺�� �����Ӱ� ������
        // Locked : ���콺�� ���߾ӿ� ����
        // Confined : ���콺�� ������ â�ȿ��� �����
    }
    void Update()
    {
        Move();
        Look();

        if(Input.GetMouseButtonDown(0))
        {
            // �ڷ�ƾ ����
            autoFire = StartCoroutine(AutoFire(fireDelayTime));
        }
        else if(Input.GetMouseButtonUp(0))
        {
            // �ڷ�ƾ ����
            StopCoroutine(autoFire); // StopCoroutine �� �Ű������� Start�� �ٸ��� �Լ��� �ƴ� �ڷ�ƾ �ν��Ͻ��̸��� ������
        }
    }
    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward * z * moveSpeed * Time.deltaTime);
        transform.Translate(Vector3.right * x * moveSpeed * Time.deltaTime);
    }
    private void Look()
    {
        float x = Input.GetAxis("Mouse X"); // ���콺 �¿� ������
        float y = Input.GetAxis("Mouse Y"); // ���콺 ���� ������

        transform.Rotate(Vector3.up, rotateSpeed * x * Time.deltaTime);
        camTrans.Rotate(Vector3.right, rotateSpeed * -y * Time.deltaTime);
    }

    private void Fire()
    {
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //if(Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, 30, layerMask))
        //{
        //    Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 0.2f);
        //    GameObject gameObject = hit.collider.gameObject;
        //    Target target = gameObject.GetComponent<Target>();
        //    Monster monster = gameObject.GetComponent<Monster>();
        //            if (monster != null)
        //    {
        //        monster.HitDmg(1);
        //    }
        //}
        // �Ⱥ��� �Է��ϱ� ����
        Bullet instance = BulletObjectPool.GetBullet(prefab);
        instance.transform.position = shotPoint.position;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, layerMask))
        {
            Monster monster = hit.collider.gameObject.GetComponent<Monster>();
            if(monster != null)
            {
                monster.HitDmg(1);
            }
        }
    }

    private IEnumerator AutoFire(float seconds)
    {
        WaitForSeconds delay = new WaitForSeconds(seconds);
        while(true)
        {
            Fire();
            yield return delay;
        }
    }
}
