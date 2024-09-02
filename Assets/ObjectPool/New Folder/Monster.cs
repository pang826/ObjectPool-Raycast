using System.Collections;
using UnityEngine;

public class Monster : MonoBehaviour
{
    // 구현실패..
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
        // 가상의 오브젝트로 재어보니 plane의 x좌표 및 z좌표가 대략 -25부터 25까지인것을 알아냄
        // 안전하게 24로 결정
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
