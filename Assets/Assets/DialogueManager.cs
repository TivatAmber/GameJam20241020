using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueMannager : MonoBehaviour
{
    public GameObject player;
    public TMP_FontAsset fontAsset;
    public GameObject dialogueBox;//��ʾor���������Ի�����
    public TextMeshProUGUI dialogueText;//�������
    public GameObject dialogueTextAsset;
    private TextMeshPro textMeshPro;
    public GameObject TextTrigger;
    [TextArea(1, 3)]//��ʾ����ʱ����ֻ��ʾһ��
    public string[] dialogueLines;
    [SerializeField] private int currentLine;//����ʵʱ׷�������������

    public int textCount = 0;//�ı�����ִ���
    public RectTransform DBtransform;//�ı����λ��
    public float dialogueRange = 0.5f;

    
    Vector3 TextTrigger_postion;//������λ��
    Vector3 Player_Position;//��ҵ�λ��

    int isCover = 0;//���λ���Ƿ񵽴�ָ������
    bool isScolling; //�Ƿ���� �ж�״̬

    [SerializeField] private float textScollingIntervalTime;//�������
    [SerializeField] private float StartIntervalTime;//��ʼ���

    void Start()
    {
        
        TextTrigger_postion = TextTrigger.transform.position;//��ȡ������λ��
        dialogueText.text = dialogueLines[currentLine];
        textMeshPro = dialogueTextAsset.GetComponent<TextMeshPro>();
        Debug.Log(TextTrigger_postion);

    }

    // Update is called once per frame
    void Update()
    {    
        Player_Position = player.transform.position;
        playerPosition();//�ж�player�Ƿ���봥����Χ
        if (isCover >= 1)
        {
            dialogueBox.SetActive(true);//��ʾ�Ի���
            if (dialogueBox.activeInHierarchy)//�Ի��򴰿���ʾʱ�ſ��Գ����ı�
            {
                if (isScolling == false)
                {
                    currentLine++;
                    if (currentLine < dialogueLines.Length)
                    {
                        //dialogueText.text = dialogueLines[currentLine];
                        StartCoroutine(ScollingText());
                    }  
                }
            }
        }
        else 
        {
            dialogueBox.SetActive(false);//���ضԻ���
            StopCoroutine(ScollingText());//�ر�Э��
            currentLine = 0;//�ָ�Ϊ�ӵ�һ�俪ʼ��ʾ
        }
        
    }

    private IEnumerator ScollingText()
    {
       // yield return new WaitForSeconds(StartIntervalTime);
        isScolling = true;
        dialogueText.text = " ";//��֤��ʼʱ�ı�һ��Ϊ��
        //��ÿ���ַ���ֿ��� ����һ��������
        foreach(char letter in dialogueLines[currentLine].ToCharArray())
        {
            dialogueText.text += letter;//һ����ĸһ����ĸ��ʾ����
            yield return new WaitForSeconds(textScollingIntervalTime);
            
        }
        isScolling = false;
    }
    
    private void playerPosition()//�ж�player�Ƿ��ڴ���������
    {
        isCover = 0;
        Debug.Log(Player_Position + "     " + TextTrigger_postion);
        if (Player_Position.x < (TextTrigger_postion.x + dialogueRange) && Player_Position.x > (TextTrigger_postion.x - dialogueRange))
        {
            if(Player_Position.y < (TextTrigger_postion.y + dialogueRange) && Player_Position.y > (TextTrigger_postion.y - dialogueRange))
                isCover ++;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(TextTrigger.transform.position, Vector3.one * dialogueRange);
    }
}