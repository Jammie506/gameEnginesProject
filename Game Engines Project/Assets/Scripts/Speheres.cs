using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class Speheres : MonoBehaviour
{
    public GameObject centre;
    private float _size;
    

    private void Awake()
    {
        
    }
    
    private void Start()
    {
        //sets the planets starting position
        this.transform.position = new Vector3(Random.Range(5, 50), 0, Random.Range(5, 50));
        //sets the size of each planet randomly
        _size = Random.Range(0, 10);
        this.gameObject.transform.localScale = new Vector3(_size,_size,_size);
    }

    void Update()
    {
        //makes the planets rotate around the centre of the scene (where the Controller gO is)
        transform.RotateAround(centre.transform.position, Vector3.up, Random.Range(0, 50) * Time.deltaTime);

            this.transform.localScale = new Vector3();

        }
    }
}
