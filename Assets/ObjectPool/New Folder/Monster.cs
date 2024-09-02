using System.Collections;
using UnityEngine;

public class Monster : MonoBehaviour
{
    // ��������..
    [SerializeField]
    Monster prefab;
    [SerializeField]
    float hp;
    [SerializeField]
    float spawnIndex;
    Coroutine spawnDelayTime;
    private void Awake()
    {
        prefab = this;
        hp = 5f;
        // ������ ������Ʈ�� ���� plane�� x��ǥ �� z��ǥ�� �뷫 -25���� 25�����ΰ��� �˾Ƴ�
        // �����ϰ� 24�� ����
        spawnIndex = 24;
        transform.position = new Vector3(Random.Range(-spawnIndex, spawnIndex), 0, Random.Range(-spawnIndex, spawnIndex));
    }

    public void HitDmg(float dmg)
    {
        hp -= dmg;
        if (hp <= 0)
        {
            MonsterObjectPool.DestroyMonster(this);
            spawnDelayTime = StartCoroutine(SpawnDelayTime());
        }
    }
    private void OnDisable()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //spawnDelayTime = StartCoroutine(SpawnDelayTime());
            //StopCoroutine(spawnDelayTime);
        }
    }
    public Monster Spawn()
    {
        Monster instance = MonsterObjectPool.GetMonster(this);
        instance.transform.position = new Vector3(Random.Range(-spawnIndex, spawnIndex), 0, Random.Range(-spawnIndex, spawnIndex));

        //hp = 5f;
        return instance;
    }

    public IEnumerator SpawnDelayTime()
    {
        
        WaitForSeconds delay = new WaitForSeconds(3);
        yield return delay;
        Spawn();
    }
}
