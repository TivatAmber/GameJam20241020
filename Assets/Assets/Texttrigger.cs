using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Texttrigger : MonoBehaviour
{
    [SerializeField] private bool isEntered;//定义布尔变量isEntered
    private void OnTriggerEnter2D(Collider2D other)

    {
        if(other.CompareTag("Player"))//玩家进入trigger的检测范围后会显示true
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
