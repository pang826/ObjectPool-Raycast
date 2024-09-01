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
        // ������ ������Ʈ�� ���� plane�� x��ǥ �� z��ǥ�� �뷫 -25���� 25�����ΰ��� �˾Ƴ�
        // �����ϰ� 24�� ����
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
