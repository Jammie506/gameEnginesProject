using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
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
    [SerializeField] private float speed;
    [SerializeField] private float size;
    [SerializeField] private float stopDist;
    [SerializeField] private float rotatorsSizeMax;
    [SerializeField] private float rotatorSizeMin;
    [SerializeField] private float rotatorsOrbitMax;
    [SerializeField] private float rotatorOrbitMin;
    public GameObject[] FollowerList;
    public GameObject[] OrbiterList;
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
            sphere.gameObject.transform.localScale = new Vector3(size, size,size);
            sphere.name = "Stalker";
            sphere.gameObject.tag = "Follower";
            
            Stalker = GameObject.FindWithTag("Follower");

            for (int a = 0; a < FollowerList.Length; a++)
            {
                FollowerList[a] = new GameObject();
                FollowerList[a].name = ("Follower " + a.ToString());
            }

            for (int b = 0; b < OrbiterList.Length; b++)
            {
                OrbiterList[b] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                OrbiterList[b].name = ("Orbiter " + b.ToString());

                float theta = (2 * Mathf.PI / OrbiterList.Length) * b;
                    
                    
                OrbiterList[b].transform.position = new Vector3((Mathf.Cos(theta)), Mathf.Sin(theta), (size + (size*b)));


                OrbiterList[b].gameObject.transform.localScale = new Vector3(
                    Random.Range(rotatorSizeMin, rotatorsSizeMax),
                    Random.Range(rotatorSizeMin, rotatorsSizeMax),
                    Random.Range(rotatorSizeMin, rotatorsSizeMax));
            }
            
            created = true;
        }
        
        
        //Nested is loop means that the Entity will move between the randomised waypoints indefinitely within previously
        //defined bounds set by the user
        if (counter < WaypointsList.Length && Stalker.transform.position != WaypointsList[counter].transform.position)
        {
            //Debug.Log(counter);
            //Debug.Log(Stalker.transform.position);
            Stalker.transform.position = Vector3.MoveTowards(Stalker.transform.position,
                WaypointsList[counter].transform.position, speed * Time.deltaTime);
            
            Stalker.transform.LookAt(WaypointsList[counter].transform.position);
            
            if (Stalker.transform.position == WaypointsList[counter].transform.position && counter < (WaypointsList.Length-1))
            {
                counter++;
            }
            if (Stalker.transform.position == WaypointsList[counter].transform.position && counter == (WaypointsList.Length-1))
            {
                counter = 0;
            }
        }

        //makes the orbiter objects follow at the distance defined in the "Stop Dist" assignment, using two if statements
        //because it allows the use of a mathematical formula to make sure that the code works no matter how many 
        //orbiters there are
        for (int c = 0; c < FollowerList.Length; c++)
        {
            if (c == 0)
            {
                Vector3 StalkPos = Stalker.transform.position;
                Vector3 FolowPos = FollowerList[c].transform.position;
                Vector3 dist = StalkPos - FolowPos;
            

                if (dist.magnitude > stopDist)
                {
                    FollowerList[c].transform.position =
                        Vector3.Slerp(FolowPos, StalkPos, speed * Time.deltaTime);
                }
            }

            if (c >= 1)
            {
                Vector3 StalkPos = FollowerList[c-1].transform.position;
                Vector3 FolowPos = FollowerList[c].transform.position;
                Vector3 dist = StalkPos - FolowPos;
            

                if (dist.magnitude > stopDist)
                {
                    FollowerList[c].transform.position =
                        Vector3.Slerp(FolowPos, StalkPos, speed * Time.deltaTime);
                }
            }
        }

        for(int d = 0; d < OrbiterList.Length; d++)
        {
                Vector3 RotatePos = FollowerList[d].transform.position;
                Vector3 FollowerPos = OrbiterList[d].transform.position;
                Vector3 dist = RotatePos - FollowerPos;

                if (dist.magnitude > stopDist)
                {
                    OrbiterList[d].transform.position =
                        Vector3.Slerp(FollowerPos, RotatePos, speed * Time.deltaTime);
                }
                
                OrbiterList[d].transform.Rotate(RotatePos, Space.Self);
        }
    }
}

