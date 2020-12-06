using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    //Variables used to set the size of the area in which the code will operate
    [Header("Spawn Area")] [SerializeField]
    private float gizmoX;

    [SerializeField] private float gizmoY;
    [SerializeField] private float gizmoZ;

    //Variables used to establish a path for the entity to follow
    [Header("Waypoints")]
    //number for waypoints that the entity can travel through
    public GameObject[] WaypointsList;
    public GameObject waypointHolder;
    private float waypointX; private float waypointY; private float waypointZ;

    [Header("Follower")]
    [SerializeField] private float speed = 1;
    [SerializeField] private float stopDist = 1;
    private GameObject Stalker;
    private bool created = false;
    private int counter = 0;
    
    
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

    private void Update()
    {
        //Calling CoRoutines
        Follower();
    }

    void WaypointSpawn()
    {
        //Creates prefabs to the number specified in the Inspector
        for (int i = 0; i < WaypointsList.Length; i++)
        {
            //values re divided by 2 to prevent them appearing outside the assigned zone, without altering the 
            //previously assigned values set by the user
            waypointX = Random.Range((transform.position.x - gizmoX / 2), (transform.position.x + gizmoX / 2));
            waypointY = Random.Range((transform.position.y - gizmoY / 2), (transform.position.x + gizmoY / 2));
            waypointZ = Random.Range((transform.position.z - gizmoZ / 2), (transform.position.x + gizmoZ / 2));

            //Create an instance of the Waypoint prefab at the randomised location, and adds it to the Array
            WaypointsList[i] = Instantiate(waypointHolder, new Vector3(waypointX, waypointY, waypointZ),
                Quaternion.identity);
        }
    }

    //Creating a base for the Entity that will patrol the above points
    void Follower()
    {
        //Ensuring that only one is spawned as the rest of the CoRoutine functions within the Update Function
        if (!created)
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.name = "Stalker";
            sphere.gameObject.tag = "Follower";
            
            created = true;
        }
        
        //Making the Entity do stuff
        Stalker = GameObject.FindWithTag("Follower");
        
        //Debug.Log(Stalker.transform.position);
        
        if (counter < WaypointsList.Length && Stalker.transform.position != WaypointsList[counter].transform.position)
        {
            Debug.Log(counter);
            Stalker.transform.position = Vector3.MoveTowards(Stalker.transform.position,
                WaypointsList[counter].transform.position, speed * Time.deltaTime);
            if (Stalker.transform.position == WaypointsList[counter].transform.position && counter < (WaypointsList.Length-1))
            {
                counter++;
            }
            if (Stalker.transform.position == WaypointsList[counter].transform.position && counter == (WaypointsList.Length-1))
            {
                counter = 0;
            }
        }
    }
}

