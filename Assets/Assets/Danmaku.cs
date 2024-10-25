using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Danmaku : MonoBehaviour
{
    public TextMeshProUGUI textComponent; // 弹幕文本组件  
    public float speed = 100f; // 弹幕移动速度  
    private Vector2 startPosition;
    private Vector2 endPosition;
    [SerializeField] private float declineSpeed;

    void Start()
    {
        // 初始化弹幕的起始和结束位置  
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
        // 计算当前位置到结束位置的插值  
        float t = Mathf.Clamp01((transform.position.x - startPosition.x) / (endPosition.x - startPosition.x));
        transform.position = Vector2.Lerp(startPosition, endPosition, t);

        

        // 如果弹幕已经移出屏幕，则销毁对象  
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