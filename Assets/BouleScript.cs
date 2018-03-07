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

        var red = Resources.Load("Materials/Red") as Material;
        var blue = Resources.Load("Materials/Blue") as Material;

        var blueBalls = ExtractBallsByMaterial(blue);
        var redBalls = ExtractBallsByMaterial(red);

        Double[] tab = buildTab(blueBalls, redBalls);
        int elemsize = 3;
        int elem = blueBalls.Count + redBalls.Count;

        Debug.Log(elemsize);
        Debug.Log(elem);
        Debug.Log(tab);



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
	
    private List<Transform> ExtractBallsByMaterial(Material material)
    {
        List<Transform> balls = new List<Transform>();
        foreach(var ball in sphereTransform)
        {
            if (ball.GetComponent<Renderer>() != null)
            {
                if (ball.GetComponent<Renderer>().material.color == material.color)
                    balls.Add(ball);
            }
        }
        return balls;
    }

    private Double[] buildTab(List<Transform> blueBalls, List<Transform> redBalls)
    {
        int elemcount = (blueBalls.Count + redBalls.Count) * 3;
        Double[] returnBalls = new Double[elemcount];
        for(int i=0; i< blueBalls.Count + redBalls.Count; i++)
        {
            if(i< blueBalls.Count)
            {
                returnBalls[i] = blueBalls[i].position.x;
                returnBalls[i + 1] = blueBalls[i].position.y;
                returnBalls[i + 2] = (double)1;
            }
            else
            {
                returnBalls[i] = redBalls[i - blueBalls.Count].position.x;
                returnBalls[i+1] = redBalls[i - blueBalls.Count].position.y;
                returnBalls[i + 2] = (double)-1;
            }
        }
        return returnBalls;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
