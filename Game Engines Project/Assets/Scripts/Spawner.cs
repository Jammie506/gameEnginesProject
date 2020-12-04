using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //Variables used to set the size of the area in which the code will operate
    [Header("Spawn Area")]
    [SerializeField] private float gizmoX;
    [SerializeField] private float gizmoY;
    [SerializeField] private float gizmoZ;

    //Variables used to establish a path for the entity to follow
    [Header("Waypoints")] 
    //number for waypoints that the entity can travel through
    public GameObject[] WaypointsList;
    public GameObject waypointHolder;
    private float waypointX;
    private float waypointY;
    private float waypointZ;

    void OnDrawGizmosSelected()
    {
        // Draw a yellow cube at the transform position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(gizmoX, gizmoY, gizmoZ));
    }

    void Start()
    {
        //Calling CoRoutines
        WaypointSpawn();
    }
    
    void WaypointSpawn()
    {
        //Creates prefabs to the number specified in the Inspector
        for(int i = 0; i < WaypointsList.Length; i++)
        {
            //values re divided by 2 to prevent them appearing outside the assigned zone, without altering the 
            //previously assigned values set by the user
            waypointX = Random.Range((transform.position.x - gizmoX/2), (transform.position.x + gizmoX/2));
            waypointY = Random.Range((transform.position.y - gizmoY/2), (transform.position.x + gizmoY/2));
            waypointZ = Random.Range((transform.position.z - gizmoZ/2), (transform.position.x + gizmoZ/2));
            
            //Create an instance of the Waypoint prefab at the randomised location
            WaypointsList[i] = Instantiate(waypointHolder, new Vector3(waypointX, waypointY, waypointZ), Quaternion.identity);
        }
    }
}
