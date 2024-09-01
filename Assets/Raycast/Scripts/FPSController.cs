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
            Fire();
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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, 30))
        {
            GameObject gameObject = hit.collider.gameObject;
            Target target = gameObject.GetComponent<Target>();
            Monster monster = gameObject.GetComponent<Monster>();

            if (monster != null)
            {
                monster.HitDmg(1);
            }
        }
    }
}
