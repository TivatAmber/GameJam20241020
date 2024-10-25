using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class mapdisappear : MonoBehaviour
{
    private float tempTime;
    public GameObject player;//���
    public GameObject MisingMapTrigger;//������
    [SerializeField] Vector3 MisingMapTrigger_postion;//��ȡ������λ��
    [SerializeField] Vector3 Player_Position;//��ȡ���λ��
    [SerializeField] int isCover = 0;
    public float destroytime = 10.0f;
    public float dialogueRange = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        tempTime = 0;//��ȡ�տ�ʼ��a
        this.GetComponent<Renderer>().material.color = new Color(
             //��Ҫ�ı��Aֵ

             GetComponent<Renderer>().material.color.r,
             GetComponent<Renderer>().material.color.g,
             GetComponent<Renderer>().material.color.b,
             GetComponent<Renderer>().material.color.a
             );

    }
    // Update is called once per frame
    void Update()
    {

        //�ж�player�Ƿ���봥����Χ
        if (tempTime < 1)
        { tempTime = tempTime + Time.deltaTime; }
        playerPosition();
        if (isCover >= 1)
        {
            if (this.GetComponent<Renderer>().material.color.a <= 1)
            {
                this.GetComponent<Renderer>().material.color = new Color
                    (
                   GetComponent<Renderer>().material.color.r,
                   GetComponent<Renderer>().material.color.g,
                   GetComponent<Renderer>().material.color.b,
           gameObject.GetComponent<Renderer>().material.color.a - tempTime / 30 * Time.deltaTime
           );
            }
            Destroy(this.gameObject, destroytime);
        }
    }
    
    private void playerPosition()//�ж�player�Ƿ��ڴ���������
    {
        Player_Position = player.transform.position;
        MisingMapTrigger_postion = MisingMapTrigger.transform.position;
        isCover = 0;
       
        if (Player_Position.x < (MisingMapTrigger_postion.x + dialogueRange) && Player_Position.x > (MisingMapTrigger_postion.x - dialogueRange))
        {
            if (Player_Position.y < (MisingMapTrigger_postion.y + dialogueRange) && Player_Position.y > (MisingMapTrigger_postion.y - dialogueRange))
                isCover++;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;//��ɫ�߿�
        if (!MisingMapTrigger) return;
        Gizmos.DrawWireCube(MisingMapTrigger.transform.position, Vector3.one * dialogueRange);
    }
}

