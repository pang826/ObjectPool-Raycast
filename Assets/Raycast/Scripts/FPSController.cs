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
        // 마우스 커서를 중앙에 고정하도록 하는 명령어
        Cursor.lockState = CursorLockMode.Locked;
        // None : 마우스가 자유롭게 움직임
        // Locked : 마우스가 정중앙에 고정
        // Confined : 마우스가 윈도우 창안에서 못벗어남
    }
    void Update()
    {
        Move();
        Look();

        if(Input.GetMouseButtonDown(0))
        {
            // 코루틴 시작
            autoFire = StartCoroutine(AutoFire(fireDelayTime));
        }
        else if(Input.GetMouseButtonUp(0))
        {
            // 코루틴 해제
            StopCoroutine(autoFire); // StopCoroutine 의 매개변수는 Start와 다르게 함수가 아닌 코루틴 인스턴스이름이 들어가야함
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
        float x = Input.GetAxis("Mouse X"); // 마우스 좌우 움직임
        float y = Input.GetAxis("Mouse Y"); // 마우스 상하 움직임

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
        // 안보고 입력하기 연습
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
