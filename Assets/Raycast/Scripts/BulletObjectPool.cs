using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObjectPool : MonoBehaviour
{
    public static BulletObjectPool Instance;
    [SerializeField]
    Bullet bulletPrefab;
    [SerializeField]
    int bulletMount;
    Queue<Bullet> queue;

    private void Awake()
    {
        Instance = this;
        bulletMount = 30;
        Init(bulletMount);
    }

    public Bullet CreateBullet(Bullet bullet)
    {
        Bullet instance = Instantiate(bullet).GetComponent<Bullet>();
        instance.gameObject.SetActive(false);
        return instance;
    }

    public void Init(int count)
    {
        queue = new Queue<Bullet>(count * 2);
        for(int i = 0; i < count; i++)
        {
            queue.Enqueue(CreateBullet(bulletPrefab));
        }
    }

    public static Bullet GetBullet(Bullet bullet)
    {
        if (Instance.queue.Count > 0)
        {
            Bullet instance = Instance.queue.Dequeue();
            instance.gameObject.SetActive(true);
            instance.transform.SetParent(null);
            return instance;
        }
        else
        {
            Debug.Log("장전이 필요합니다");
            return null;
        }
    }

    public static void ReturnBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        bullet.transform.SetParent(Instance.transform);
        Instance.queue.Enqueue(bullet);
    }
}
