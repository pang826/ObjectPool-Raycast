using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSMover : MonoBehaviour
{
    // ���ӿ��� ���콺 ��Ŭ���ϸ� �ش���ġ�� ĳ���Ͱ� �̵�
    [SerializeField]
    GameObject playerObj;

    [SerializeField]
    PlayerMover playerMover;
    private void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        playerMover = playerObj.GetComponent<PlayerMover>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            MovePlayer();
        }
    }

    public void MovePlayer()
    {
        // ScreenPointToRay : ķ���� ()�� ��ġ�� Ray�� ����
        // Input.MousePosition : ���콺�� ��ġ����
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit))
        {
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red, 0.2f);
            // hit.point : RaycastHit�� �ν���ġ
            Vector3 destination = hit.point;
            playerMover.Move(destination);
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 0.2f);
        }
    }
}
