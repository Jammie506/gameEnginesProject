using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class perlinNoise : MonoBehaviour
{
    //Variables for the creation of Perlin distorted terrain
    [Header("Terrain Gen")]
    [SerializeField] private float perlinMultiplier;
    [SerializeField] private float perlinRefinemnt;
    [SerializeField] private int perlinRowsAndColumns;
    [SerializeField] private float perlinNoisel;
    [SerializeField] private float[,] terrainHights;
    public Terrain terrain;
    
    void Start()
    {
        //Storing each of the values for the different height values in an array to keep code neat and allow everything
        //to run smoother than having to creat more individual variables
        terrainHights = new float[perlinRowsAndColumns, perlinRowsAndColumns];
        terrain = terrain.GetComponent<Terrain>();

        for(int e = 0; e < perlinRowsAndColumns; e++)
        {
            for(int f = 0; f < perlinRowsAndColumns; f++)
            {
                perlinNoisel = Mathf.PerlinNoise(e * perlinRefinemnt, f * perlinRefinemnt);
                terrainHights[e, f] = perlinNoisel * perlinMultiplier;
            }
        }
        
        terrain.terrainData.SetHeights(0, 0, terrainHights);
    }
}
