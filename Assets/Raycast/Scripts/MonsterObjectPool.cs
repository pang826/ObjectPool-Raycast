using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterObjectPool : MonoBehaviour
{
    public static MonsterObjectPool Instance;

    [SerializeField]
    Monster prefab; // ���� ������
    [SerializeField]
    int size; // ������Ʈ Ǯ�� ������ ���� ��
    Queue<Monster> queue;
    private void Awake()
    {
        Instance = this;
        Init(size);
    }

    public Monster CreateMonster(Monster monster)
    {
        Monster instance = Instantiate(monster).GetComponent<Monster>();
        instance.gameObject.SetActive(false);
        return instance;
    }
    public void Init(int size)
    {
        queue = new Queue<Monster>(size * 2);
        for (int i = 0; i < size; i++)
        {
            queue.Enqueue(CreateMonster(prefab));
        }
    }

    public static Monster GetMonster(Monster monster)
    {
        if (Instance.queue.Count > 0)
        {
            Monster instance = Instance.queue.Dequeue();
            instance.gameObject.SetActive(true);
            instance.transform.SetParent(null);
            return instance;
        }
        else
        {
            return null;
        }
    }

    public static void DestroyMonster(Monster monster)
    {
        monster.gameObject.SetActive(false);
        monster.transform.SetParent(Instance.transform);
        Instance.queue.Enqueue(monster);
    }
}
