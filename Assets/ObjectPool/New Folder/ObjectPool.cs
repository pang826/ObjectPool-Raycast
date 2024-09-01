using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;
    [SerializeField]
    BulletState[] prefabs;
    
    Dictionary<BulletState, Queue<BulletState>> bulletDic = new Dictionary<BulletState, Queue<BulletState>>();

    private void Awake()
    {
        Instance = this;
        Initialize(5);
    }
    
    // bullet 오브젝트를 생성
    private BulletState CreateNewObject(BulletState bulletState)
    {
        BulletState newGameobject = Instantiate(bulletState).GetComponent<BulletState>();
        newGameobject.gameObject.SetActive(false);
        return newGameobject;
    }

    // 생성한 bullet 오브젝트를 오브젝트 풀에 보관
    private void Initialize(int count)
    {
        // 프리팹 모음을 처음부터 훑어보면서 프리팹 하나당 그에 맞는 큐를 하나씩 생성함
        // 딕셔너리를 통해 해당 프리팹을 키값으로, 해당하는 큐를 밸류로 가지도록 함
        // 각 큐에 bullet을 타입별로 넣음
        foreach(BulletState curBullet in prefabs)
        {
            Queue<BulletState> queue = new Queue<BulletState>();
            for (int i = 0; i < count; i++)
            {
                BulletState newBullet = CreateNewObject(curBullet);
                queue.Enqueue(newBullet);
            }
            bulletDic.Add(curBullet, queue);
        }
    }

    // 풀 내의 오브젝트를 빌려가는 매서드
    public static BulletState GetObject(BulletState bulletState)
    {
        if (Instance.bulletDic[bulletState].Count > 0 && Instance.bulletDic.ContainsKey(bulletState))
        {
            BulletState bullet = Instance.bulletDic[bulletState].Dequeue();
            bullet.gameObject.SetActive(true);
            bullet.transform.SetParent(null);
            return bullet;
        }
        else
        {
            return null;
        }
    }

    // 사용한 오브젝트를 풀로 반납받는 메서드
    public static void ReturnObject(BulletState bullet)
    {
        bullet.gameObject.SetActive(false);
        bullet.transform.SetParent(null);
        foreach(BulletState curPrefab in Instance.prefabs)
        {
            // 변수명.GetType() : 변수의 타입을 가져온다(여기서는 BulletState)
            // 프리팹배열을 처음부터 타입을 읽어보면서 오브젝트의 타입과 같으면 오브젝트를 반납받음
            if (bullet.GetType() == curPrefab.GetType())
            {
                Instance.bulletDic[curPrefab].Enqueue(bullet);
                return;
            }
        }
    }
}
