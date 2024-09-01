using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{   
    [SerializeField] 
    float moveSpeed;
    [SerializeField]
    float rotateSpeed;

    float minVerticalRotate = 0f;
    float maxVerticalRotate = 60f;
    float verticalHeadRotate;
    float horizontalHeadRoatate;

    [SerializeField]
    Transform head;
    [SerializeField] 
    Rigidbody rigid;

    Vector3 MoveDir;

    private void Update()
    {
        Move();
        HeadRotate();
    }

    public void Move()
    {
        float forward = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");
        MoveDir = new Vector3(horizontal, 0f, forward);
        //
        // transform.position += MoveDir * moveSpeed * Time.deltaTime;
        // transform.LookAt(transform.position + MoveDir);
        rigid.velocity = MoveDir.normalized * moveSpeed;

        // 부드럽게 회전
        if (MoveDir != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(MoveDir, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * 10f);
        }
    }

    public void HeadRotate()
    {
        float vertical = Input.GetAxisRaw("VerticalRotate");
        float horizontal = Input.GetAxisRaw("HorizontalRotate");
        
        //head.transform.Rotate(Vector3.up * horizontal * rotateSpeed * Time.deltaTime);
        //head.transform.Rotate(Vector3.right * vertical * rotateSpeed * Time.deltaTime);

        // yAimAngle = Mathf.Clamp(yAimAngle + y * aimRotateSpeed * Time.deltaTime, -maxAimRotate, maxAimRotate);
        // xAimAngle = Mathf.Clamp(xAimAngle - x * aimRotateSpeed * Time.deltaTime, -maxAimRotate, 0);
        // 
        // turretTransform.localRotation = Quaternion.Euler(xAimAngle, yAimAngle, 0);
        verticalHeadRotate = Mathf.Clamp(verticalHeadRotate - vertical * rotateSpeed * Time.deltaTime, -maxVerticalRotate, 0);
        horizontalHeadRoatate = Mathf.Clamp(horizontalHeadRoatate + horizontal * rotateSpeed * Time.deltaTime, -maxVerticalRotate, maxVerticalRotate);

        head.localRotation = Quaternion.Euler(verticalHeadRotate, horizontalHeadRoatate,  0);
    }
}
