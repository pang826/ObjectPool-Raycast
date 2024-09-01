using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSMover : MonoBehaviour
{
    // 게임에서 마우스 우클릭하면 해당위치로 캐릭터가 이동
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
        // ScreenPointToRay : 캠에서 ()의 위치로 Ray를 쏴줌
        // Input.MousePosition : 마우스의 위치정보
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit))
        {
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red, 0.2f);
            // hit.point : RaycastHit의 인식위치
            Vector3 destination = hit.point;
            playerMover.Move(destination);
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 0.2f);
        }
    }
}
