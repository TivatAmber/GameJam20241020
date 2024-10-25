using System.Collections.Generic;
using UnityEngine;

public class DanmakuManager : MonoBehaviour
{
    public GameObject danmakuPrefab; // ��Ļ  
    public Transform spawnPoint; // ��Ļ����λ��  
    public List<string> messages; // ��Ļ��Ϣ�б�  
    private Queue<string> messageQueue;
    private float spawnInterval = 1f; // ��Ļ���ɼ��  
    private float nextSpawnTime;
    void Start()
    {
        messageQueue = new Queue<string>(messages);
        nextSpawnTime = Time.time + spawnInterval;
    }

    void Update()
    {
        if (Time.time > nextSpawnTime && messageQueue.Count > 0)
        {
            SpawnDanmaku(messageQueue.Dequeue());
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnDanmaku(string message)
    {
        GameObject newDanmaku = Instantiate(danmakuPrefab, spawnPoint.position, spawnPoint.rotation);
        newDanmaku.GetComponent<Danmaku>().textComponent.text = message;
    }

    // ����������ʱ�����Ϣ������  
    public void AddMessage(string message)
    {
        messageQueue.Enqueue(message);
    }
    //public void DestroyDanmaku()
    //{
    //    Destroy(gameObject);
    //}
}



