using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Speheres : MonoBehaviour
{
    public GameObject centre;
    
    //user set total for how long the pause between size changes should be,lower number means faster transition
    public float _wait;
    //user ebeter totals for how big and small the objects may be
    public float _maxSize;
    public float _minSize;
    
    //manages when the spheres should change size
    private float _size;
    private float _timer;
    
    //decides whether or not the spheres should increase or decrease in size
    private bool grow;
    private void Start()
    {
        //sets the spheres starting position
        this.transform.position = new Vector3(Random.Range(5, 50), 0, Random.Range(5, 50));
        //sets the size of each sphere randomly
        _size = Random.Range(1, 10);
        this.gameObject.transform.localScale = new Vector3(_size,_size,_size);
    }

    void Update()
    {
        //makes the spheres rotate around the centre of the scene (where the Controller gO is)
        transform.RotateAround(centre.transform.position, Vector3.up, Random.Range(0, 50) * Time.deltaTime);

        SizeChange();
    }

    void SizeChange()
    {
        //controls the change in size of the spheres using variables above
        
        this.transform.localScale = new Vector3(_size, _size, _size);

        _timer += Time.deltaTime;

        if (_size >= _maxSize)
        {
            grow = false;
        }

        if (_size <= _minSize)
        {
            grow = true;
        }
            

        if (_timer >= _wait)
        {
            if (grow)
            {
                _size += 1; //Random.Range(1, 10);
            }
            if (!grow)
            {
                _size -= 1; //Random.Range(1, 10);
            }
                
            _timer = 0;
        }
    }
}
