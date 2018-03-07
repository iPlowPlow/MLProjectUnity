using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BouleScript : MonoBehaviour {

    //System.IntPtr <ModelName>_create(???)
    //System.IntPtr <ModelName>_remove(???)
    //void <ModelName>_train_classification(System.IntPrt, ???)
    //void <ModelName>_train_regression(System.IntPrt, ???)

    [SerializeField]
    private Transform[] sphereTransform;

	// Use this for initialization
	void Start () {
        Debug.Log("teeest");
        sphereTransform = UnityEngine.Object.FindObjectsOfType<Transform>();
        var rng = new System.Random();
        foreach (var sphere in sphereTransform)
        {
            sphere.position += Vector3.up * (float)rng.Next(1, 30)/10;
        }
        /*
        sphereTransform[0].position += Vector3.down * 2f;
        sphereTransform[1].position += Vector3.up * 2f;
        sphereTransform[2].position += Vector3.forward * 2f;
        */
        //sphereTransform = new Transform[0];
        //var pos = sphereTransform[0].position;
        //sphereTransform[0].position = new Vector3(pos.x + (float)10, pos.y + (float)10, pos.z + (float)10);
        /*for (int i=0; i<351; i++){
            var pos = sphereTransform[i].position;
            sphereTransform[i].position = new Vector3(pos.x + (float)0.1*i, pos.y, pos.z);
        }*/
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
