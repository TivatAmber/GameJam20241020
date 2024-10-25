using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Textmanager : MonoBehaviour
{
    public float fadeDuration = 2.0f;//���嵭��ʱ��Ϊ2.0
    public GameObject player;//���
    public GameObject textObject;
    TextMeshPro textComponent;//�������е�tmp�ı�
    public GameObject TextTrigger;//������
    Vector3 TextTrigger_postion;//������λ��
    Vector3 Player_Position;//��ȡ���λ��
    int isCover = 0;
    public float dialogueRange = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        textComponent = textObject.GetComponent<TextMeshPro>();
        TextTrigger_postion = TextTrigger.transform.position;
        //��ȡ��������λ��
        if (textComponent != null)
        {
            // iftextComponent�Ѿ�����ֵ  
            Color initialColor = textComponent.color;
            initialColor.a = 0f; // ��Alpha����Ϊ0����ʾ��ȫ͸��  
            textComponent.color = initialColor;

        }
    }

    // Update is called once per frame
    private void Update()
    {
        Debug.Log(textComponent);
        playerPosition();//�ж�player�Ƿ���봥����Χ
        if(isCover >= 1)
        StartCoroutine(FadeInRoutine());
        //Debug.Log(isCover);
    }
    private IEnumerator FadeInRoutine()
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < fadeDuration)
        {
            // ���㵱ǰ��͸����  
            float alpha = elapsedTime / fadeDuration;

            // ����Text�������ɫ��ֻ�ı�Alphaֵ  
            Color fadeColor = textComponent.color;
            fadeColor.a = alpha;
            textComponent.color = fadeColor;

            // �ȴ�һ֡  
            yield return null;

            // �����ѹ�ȥ��ʱ��  
            elapsedTime += Time.deltaTime;
        }

        // ����͸����Ϊ1  
        Color finalColor = textComponent.color;
        finalColor.a = 1.0f;
        textComponent.color = finalColor;
    }
    private void playerPosition()//�ж�player�Ƿ��ڴ���������
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
        Gizmos.color = Color.red;//��ɫ�߿�
        Gizmos.DrawWireCube(TextTrigger.transform.position,Vector3.one * dialogueRange);
    }
}

