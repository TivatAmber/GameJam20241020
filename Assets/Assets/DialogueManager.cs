using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueMannager : MonoBehaviour
{
    public GameObject player;
    public TMP_FontAsset fontAsset;
    public GameObject dialogueBox;//显示or隐藏整个对话窗口
    public TextMeshProUGUI dialogueText;//输出文字
    public GameObject dialogueTextAsset;
    private TextMeshPro textMeshPro;
    public GameObject TextTrigger;
    [TextArea(1, 3)]//显示文字时不会只显示一行
    public string[] dialogueLines;
    [SerializeField] private int currentLine;//用于实时追踪文字内容输出

    public int textCount = 0;//文本框出现次数
    public RectTransform DBtransform;//文本框的位置
    public float dialogueRange = 0.5f;

    
    Vector3 TextTrigger_postion;//触发器位置
    Vector3 Player_Position;//玩家的位置

    int isCover = 0;//鼠标位置是否到达指定区域
    bool isScolling; //是否滚动 判断状态

    [SerializeField] private float textScollingIntervalTime;//滚动间隔
    [SerializeField] private float StartIntervalTime;//初始间隔

    void Start()
    {
        
        TextTrigger_postion = TextTrigger.transform.position;//获取触发器位置
        dialogueText.text = dialogueLines[currentLine];
        textMeshPro = dialogueTextAsset.GetComponent<TextMeshPro>();
        Debug.Log(TextTrigger_postion);

    }

    // Update is called once per frame
    void Update()
    {    
        Player_Position = player.transform.position;
        playerPosition();//判断player是否进入触发范围
        if (isCover >= 1)
        {
            dialogueBox.SetActive(true);//显示对话框
            if (dialogueBox.activeInHierarchy)//对话框窗口显示时才可以出现文本
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
            dialogueBox.SetActive(false);//隐藏对话框
            StopCoroutine(ScollingText());//关闭协程
            currentLine = 0;//恢复为从第一句开始显示
        }
        
    }

    private IEnumerator ScollingText()
    {
       // yield return new WaitForSeconds(StartIntervalTime);
        isScolling = true;
        dialogueText.text = " ";//保证开始时文本一定为空
        //将每个字符拆分开来 存在一个数组中
        foreach(char letter in dialogueLines[currentLine].ToCharArray())
        {
            dialogueText.text += letter;//一个字母一个字母显示出来
            yield return new WaitForSeconds(textScollingIntervalTime);
            
        }
        isScolling = false;
    }
    
    private void playerPosition()//判断player是否在触发器里面
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