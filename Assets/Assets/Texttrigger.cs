using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Texttrigger : MonoBehaviour
{
    [SerializeField] private bool isEntered;//���岼������isEntered
    private void OnTriggerEnter2D(Collider2D other)

    {
        if(other.CompareTag("Player"))//��ҽ���trigger�ļ�ⷶΧ�����ʾtrue
        {
            isEntered = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
