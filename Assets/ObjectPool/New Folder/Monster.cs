using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField]
    float hp;
    [SerializeField]
    float spawnIndex;
    private void Awake()
    {
        hp = 3f;
        // 가상의 오브젝트로 재어보니 plane의 x좌표 및 z좌표가 대략 -25부터 25까지인것을 알아냄
        // 안전하게 24로 결정
        spawnIndex = 24;
    }
    private void Start()
    {
        Spawn();
    }
    public void HitDmg(float dmg)
    {
        hp -= dmg;
        if(hp <= 0)
        {
            Spawn();
        }
    }

    public void Spawn()
    {
        transform.position = new Vector3(Random.Range(-spawnIndex, spawnIndex), 0, Random.Range(-spawnIndex, spawnIndex));
    }
}
