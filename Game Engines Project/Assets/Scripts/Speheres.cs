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

    public AudioSource audioSource;
    public float updateStep = 0.01f;
    public int sampleDataLength;

    private float currentUpdateTime = 1f;

    public float clipLoudness;
    private float[] clipSampleDate;

    public GameObject planet;
    public float sizeFactor;
    
    public float minSize = 0;
    public float maxSize = 500;

    private void Awake()
    {
        clipSampleDate = new float[sampleDataLength];
    }
    
    private void Start()
    {
        //sets the planets starting position
        this.transform.position = new Vector3(Random.Range(5, 50), 0, Random.Range(5, 50));
        //sets the size of each planet randomly
        //_size = Random.Range(0, 10);
        //this.gameObject.transform.localScale = new Vector3(_size,_size,_size);
    }

    void Update()
    {
        //makes the planets rotate around the centre of the scene (where the Controller gO is)
        transform.RotateAround(centre.transform.position, Vector3.up, Random.Range(0, 50) * Time.deltaTime);

        //setting object size relative to the loudness of an audio clip
        currentUpdateTime += Time.deltaTime;
        if (currentUpdateTime >= updateStep)
        {
            currentUpdateTime = 0;
            audioSource.clip.GetData(clipSampleDate, audioSource.timeSamples);
            clipLoudness = 0f;
            
            foreach (var sample in clipSampleDate)
            {
                clipLoudness += Math.Abs(sample);
            }

            clipLoudness /= sampleDataLength;

            clipLoudness *= sizeFactor;
            clipLoudness = Mathf.Clamp(clipLoudness, minSize, maxSize);
            this.transform.localScale = new Vector3(clipLoudness, clipLoudness, clipLoudness);

        }
    }
}
