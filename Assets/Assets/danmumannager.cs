using System.Collections.Generic;
using UnityEngine;

public class DanmakuManager : MonoBehaviour
{
    public GameObject danmakuPrefab; // 弹幕  
    public Transform spawnPoint; // 弹幕生成位置  
    public List<string> messages; // 弹幕消息列表  
    private Queue<string> messageQueue;
    private float spawnInterval = 1f; // 弹幕生成间隔  
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

    // 可以在运行时添加消息到队列  
    public void AddMessage(string message)
    {
        messageQueue.Enqueue(message);
    }
    //public void DestroyDanmaku()
    //{
    //    Destroy(gameObject);
    //}
}



