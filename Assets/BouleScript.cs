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
        var white = Resources.Load("Materials/White") as Material;

        var blueBalls = ExtractBallsByMaterial(blue);
        var redBalls = ExtractBallsByMaterial(red);
        var whiteBalls = ExtractBallsByMaterial(white);

        Double[] tab = buildTab(blueBalls, redBalls);
        int elemsize = 3;
        int elem = blueBalls.Count + redBalls.Count;

        Debug.Log("Rouge: " + redBalls.Count);
        Debug.Log("Bleu: " + blueBalls.Count);
        Debug.Log("Blanches: " + whiteBalls.Count);
        Debug.Log(elemsize);
        Debug.Log(elem);
        Debug.Log(tab);

        double[] W = { 0.71, -0.44, 0.12 };

        foreach(var ball in whiteBalls)
        {
            var ballObj = sphereTransform[ball];
            var vector = new Vector3(ballObj.position.x, ballObj.position.y, ballObj.position.z);
            vector.y = (float) ((-W[0] * -W[1] * Convert.ToDouble(vector.x)) / W[2]);
            sphereTransform[ball].position = vector;
        }





        /*var rng = new System.Random();
        foreach (var sphere in sphereTransform)
        {
            sphere.position += Vector3.up * (float)rng.Next(1, 30)/10;
        }*/
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
	
    private List<int> ExtractBallsByMaterial(Material material)
    {
        List<int> balls = new List<int>();
        for (int i=0; i<sphereTransform.Length; i++)
        {
            if (sphereTransform[i].GetComponent<Renderer>() != null)
            {
                if (sphereTransform[i].GetComponent<Renderer>().material.color == material.color)
                    balls.Add(i);
            }
        }
        return balls;
    }

    private Double[] buildTab(List<int> blueBalls, List<int> redBalls)
    {
        int elemcount = (blueBalls.Count + redBalls.Count) * 3;
        Double[] returnBalls = new Double[elemcount];
        for(int i=0; i< blueBalls.Count + redBalls.Count; i++)
        {
            if(i< blueBalls.Count)
            {
                returnBalls[i] = sphereTransform[blueBalls[i]].position.x;
                returnBalls[i + 1] = sphereTransform[blueBalls[i]].position.y;
                returnBalls[i + 2] = (double)1;
            }
            else
            {
                returnBalls[i] = sphereTransform[redBalls[i - blueBalls.Count]].position.x;
                returnBalls[i + 1] = sphereTransform[redBalls[i - blueBalls.Count]].position.y;
                returnBalls[i + 2] = (double)-1;
            }
        }
        return returnBalls;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
