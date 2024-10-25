using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Danmaku : MonoBehaviour
{
    public TextMeshProUGUI textComponent; // ��Ļ�ı����  
    public float speed = 100f; // ��Ļ�ƶ��ٶ�  
    private Vector2 startPosition;
    private Vector2 endPosition;
    [SerializeField] private float declineSpeed;

    void Start()
    {
        // ��ʼ����Ļ����ʼ�ͽ���λ��  
        startPosition = transform.position;
        endPosition = new Vector2(Screen.width, startPosition.y);
        var players = FindObjectsByType<playermove>(FindObjectsSortMode.InstanceID);
        foreach (var player in players)
        {
            player.ChangeSpeed(-declineSpeed);
        }
    }

    void Update()
    {
        // ���㵱ǰλ�õ�����λ�õĲ�ֵ  
        float t = Mathf.Clamp01((transform.position.x - startPosition.x) / (endPosition.x - startPosition.x));
        transform.position = Vector2.Lerp(startPosition, endPosition, t);

        

        // �����Ļ�Ѿ��Ƴ���Ļ�������ٶ���  
        if (transform.position.x > endPosition.x)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        var players = FindObjectsByType<playermove>(FindObjectsSortMode.InstanceID);
        foreach (var player in players)
        {
            player.ChangeSpeed(declineSpeed);
        }
    }

}