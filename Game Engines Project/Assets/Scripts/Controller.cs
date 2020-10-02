using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using Random = UnityEngine.Random;

public class Controller : MonoBehaviour
{
    public GameObject[] planetArray; //Sets size and content of the array
    public GameObject[] planets;

    void Start()
    {
        planets = new GameObject[planetArray.Length]; //makes sure they match length
        for (int i = 0; i < planetArray.Length; i++)
        {
            planets[i] = Instantiate(planetArray[i]) as GameObject;
        }
    }
}
