using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchcamera : MonoBehaviour
{
    // Start is called before the first frame updat

       public GameObject Camera;
        public Transform Signport;
        [SerializeField] private Vector3 targetPoint;
    [SerializeField] private float ratio;
    [SerializeField] private bool moving = false;
        Vector3 position;
        private void Awake()
        {
            position = Signport.position;//signport
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name == "player")
            {
                targetPoint = position;
            moving = true;
            }
        }
    private void Update()
    {
        if (moving)
        {
            Camera.transform.position = Vector3.Lerp(Camera.transform.position, targetPoint, ratio);
            if (Vector3.Distance(Camera.transform.position, targetPoint) < ratio * 10) moving = false;
        }
    }
}


    // Update is called once per frame
    
