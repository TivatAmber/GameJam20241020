using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waypoint : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypoimtIndex = 0;
    [SerializeField] private float speed = 2f;
    // Start is called before the first frame update
   
   

    // Update is called once per frame
    private void Update()
    {
        if (Vector2.Distance(waypoints[currentWaypoimtIndex].transform.position,transform.position)<.1f)
        {
            currentWaypoimtIndex++;
            if(currentWaypoimtIndex>= waypoints.Length)
            {
                currentWaypoimtIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypoimtIndex].transform.position, Time.deltaTime * speed);
    }
}
