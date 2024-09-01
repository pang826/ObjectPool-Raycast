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
    
    // bullet ������Ʈ�� ����
    private BulletState CreateNewObject(BulletState bulletState)
    {
        BulletState newGameobject = Instantiate(bulletState).GetComponent<BulletState>();
        newGameobject.gameObject.SetActive(false);
        return newGameobject;
    }

    // ������ bullet ������Ʈ�� ������Ʈ Ǯ�� ����
    private void Initialize(int count)
    {
        // ������ ������ ó������ �Ⱦ�鼭 ������ �ϳ��� �׿� �´� ť�� �ϳ��� ������
        // ��ųʸ��� ���� �ش� �������� Ű������, �ش��ϴ� ť�� ����� �������� ��
        // �� ť�� bullet�� Ÿ�Ժ��� ����
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

    // Ǯ ���� ������Ʈ�� �������� �ż���
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

    // ����� ������Ʈ�� Ǯ�� �ݳ��޴� �޼���
    public static void ReturnObject(BulletState bullet)
    {
        bullet.gameObject.SetActive(false);
        bullet.transform.SetParent(null);
        foreach(BulletState curPrefab in Instance.prefabs)
        {
            // ������.GetType() : ������ Ÿ���� �����´�(���⼭�� BulletState)
            // �����չ迭�� ó������ Ÿ���� �о�鼭 ������Ʈ�� Ÿ�԰� ������ ������Ʈ�� �ݳ�����
            if (bullet.GetType() == curPrefab.GetType())
            {
                Instance.bulletDic[curPrefab].Enqueue(bullet);
                return;
            }
        }
    }
}
