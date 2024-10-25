using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Textmanager : MonoBehaviour
{
    public float fadeDuration = 2.0f;//定义淡入时间为2.0
    public GameObject player;//玩家
    public GameObject textObject;
    TextMeshPro textComponent;//空物体中的tmp文本
    public GameObject TextTrigger;//触发器
    Vector3 TextTrigger_postion;//触发器位置
    Vector3 Player_Position;//获取玩家位置
    int isCover = 0;
    public float dialogueRange = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        textComponent = textObject.GetComponent<TextMeshPro>();
        TextTrigger_postion = TextTrigger.transform.position;
        //获取触发器的位置
        if (textComponent != null)
        {
            // iftextComponent已经被赋值  
            Color initialColor = textComponent.color;
            initialColor.a = 0f; // 将Alpha设置为0，表示完全透明  
            textComponent.color = initialColor;

        }
    }

    // Update is called once per frame
    private void Update()
    {
        Debug.Log(textComponent);
        playerPosition();//判断player是否进入触发范围
        if(isCover >= 1)
        StartCoroutine(FadeInRoutine());
        //Debug.Log(isCover);
    }
    private IEnumerator FadeInRoutine()
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < fadeDuration)
        {
            // 计算当前的透明度  
            float alpha = elapsedTime / fadeDuration;

            // 设置Text组件的颜色，只改变Alpha值  
            Color fadeColor = textComponent.color;
            fadeColor.a = alpha;
            textComponent.color = fadeColor;

            // 等待一帧  
            yield return null;

            // 增加已过去的时间  
            elapsedTime += Time.deltaTime;
        }

        // 最终透明度为1  
        Color finalColor = textComponent.color;
        finalColor.a = 1.0f;
        textComponent.color = finalColor;
    }
    private void playerPosition()//判断player是否在触发器里面
    {
        Player_Position = player.transform.position;
        isCover = 0;
        //Debug.Log(Player_Position + "     " + TextTrigger_postion);
        if (Player_Position.x < (TextTrigger_postion.x + dialogueRange) && Player_Position.x > (TextTrigger_postion.x - dialogueRange))
        {
            if (Player_Position.y <(TextTrigger_postion.y + dialogueRange) && Player_Position.y > (TextTrigger_postion.y - dialogueRange))
                isCover++;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;//红色边框
        Gizmos.DrawWireCube(TextTrigger.transform.position,Vector3.one * dialogueRange);
    }
}

