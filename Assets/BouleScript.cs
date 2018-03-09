using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;


public class BouleScript : MonoBehaviour {

    //System.IntPtr <ModelName>_create(???)
    //System.IntPtr <ModelName>_remove(???)
    //void <ModelName>_train_classification(System.IntPrt, ???)
    //void <ModelName>_train_regression(System.IntPrt, ???)

    [SerializeField]
    private Transform[] sphereTransform;
    [SerializeField]
    private int iter;
    [SerializeField]
    private double alpha;
    [SerializeField]
    private int type;
    [SerializeField]
    private double gamma;

    // Use this for initialization
    void Start () {
        Debug.Log("teeest");
        sphereTransform = UnityEngine.Object.FindObjectsOfType<Transform>();

        var red = Resources.Load("Materials/Red") as Material;
        var blue = Resources.Load("Materials/Blue") as Material;
        var white = Resources.Load("Materials/White") as Material;
        var green = Resources.Load("Materials/Green") as Material;

        var blueBalls = ExtractBallsByMaterial(blue);
        var redBalls = ExtractBallsByMaterial(red);
        var whiteBalls = ExtractBallsByMaterial(white);
        var greenBalls = ExtractBallsByMaterial(green);

        Debug.Log("Rouge: " + redBalls.Count);
        Debug.Log("Bleu: " + blueBalls.Count);
        Debug.Log("Blanches: " + whiteBalls.Count);

        double[] tab = buildTab(blueBalls, redBalls);
        int elemsize = 3;
        int elem = blueBalls.Count + redBalls.Count;

        
        Debug.Log(elemsize);
        Debug.Log(elem);
        Debug.Log(tab);

        //double[] W = { 0.71, -0.44, 0.12 };
        System.IntPtr WP = DemoCPPTOUnityLibWrapper.linear_create(type, elem);

        double[] Wtt = new double[3];
        
        Marshal.Copy(WP, Wtt, 0, 3);
        Debug.Log("WTtt[0]:" + Wtt[0]);
        Debug.Log("WTtt[1]:" + Wtt[1]);
        Debug.Log("WTtt[2]:" + Wtt[2]);
        switch (type)
        {
            case 0:
                DemoCPPTOUnityLibWrapper.linear_train_classification(WP, elem, elemsize, tab, alpha, iter);
                double[] W = new double[3];

                Marshal.Copy(WP, W, 0, 3);
                Debug.Log("W[0]:" + W[0]);
                Debug.Log("W[1]:" + W[1]);
                Debug.Log("W[2]:" + W[2]);

                foreach (var ball in whiteBalls)
                {
                    double test = DemoCPPTOUnityLibWrapper.linear_classify(WP, sphereTransform[ball].position.x, sphereTransform[ball].position.z);
                    Debug.Log("value double" + +test);
                    Debug.Log("Pos y before: " + sphereTransform[ball].position.y);
                    if (test > 0)
                    {
                        Debug.Log("==");
                        sphereTransform[ball].position += Vector3.up * 1f;
                    }
                    else
                    {
                        Debug.Log("!=");
                        sphereTransform[ball].position += Vector3.down * 1f;
                    }

                    Debug.Log("Pos y after: " + sphereTransform[ball].position.y);

                }
                DemoCPPTOUnityLibWrapper.linear_delete(WP);
                break;
            case 1:
                DemoCPPTOUnityLibWrapper.linear_train_regression(WP, elem, elemsize, tab);
                double[] Wtab = new double[3];
                Marshal.Copy(WP, Wtab, 0, 3);
                Debug.Log("Wtab[0]:" + Wtab[0]);
                Debug.Log("Wtab[1]:" + Wtab[1]);
                Debug.Log("Wtab[2]:" + Wtab[2]);
                foreach (var ball in whiteBalls)
                {
                    var ballObj = sphereTransform[ball];
                    var vector = new Vector3(ballObj.position.x, ballObj.position.y, ballObj.position.z);
                    //y = w0 + w1x1 + w2x2
                    vector.y = (float)(Wtab[0] + Wtab[1] * Convert.ToDouble(vector.x) + Wtab[2]* Convert.ToDouble(vector.z));
                    sphereTransform[ball].position = vector;
                }
                break;
            case 2:
                Debug.Log(gamma);
                DemoCPPTOUnityLibWrapper.linear_train_RBF(WP, elem, elemsize, tab, gamma);
                double[] WRBF = new double[elem];
                Marshal.Copy(WP, WRBF, 0, 3);
                for(int i=0; i<elem; i++)
                {
                    Debug.Log("WRBF["+i+"]: " + WRBF[i]);
                }
                /*Debug.Log("WRBF[0]:" + WRBF[0]);
                Debug.Log("WRBF[1]:" + WRBF[1]);
                Debug.Log("WRBF[2]:" + WRBF[2]);*/
                foreach (var ball in whiteBalls)
                {
                    var ballObj = sphereTransform[ball];
                    var vector = new Vector3(ballObj.position.x, ballObj.position.y, ballObj.position.z);
                    double[] xz = { vector.x, vector.z };
                    //y = w0 + w1x1 + w2x2
                    vector.y = (float)DemoCPPTOUnityLibWrapper.execute_RBF(WP, xz, tab, elem, elemsize, gamma);
                    sphereTransform[ball].position = vector;
                }
                break;
            default:
                break;
        }

        /*foreach (var ball in whiteBalls)
        {
            var ballObj = sphereTransform[ball];
            var vector = new Vector3(ballObj.position.x, ballObj.position.y, ballObj.position.z);
            vector.y = (float) ((-W[0] * -W[1] * Convert.ToDouble(vector.x)) / W[2]);
            sphereTransform[ball].position = vector;
        }*/





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
        for(int i=0; i< elemcount; i+=3)
        {
            Debug.Log("index: "+ i);
            if(i< (blueBalls.Count*3))
            {
                returnBalls[i] = sphereTransform[blueBalls[i/3]].position.x;
                returnBalls[i + 1] = sphereTransform[blueBalls[i/3]].position.z;
                returnBalls[i + 2] = (type==0)? (double)-1 : sphereTransform[blueBalls[i / 3]].position.y;
                Debug.Log("blue: x:" + returnBalls[i] + " y: " + returnBalls[i + 1] + " val: " + returnBalls[i + 2] + " i: "+ i);
            }
            else
            {
                returnBalls[i] = sphereTransform[redBalls[i/3 - blueBalls.Count]].position.x;
                returnBalls[i + 1] = sphereTransform[redBalls[i/3 - blueBalls.Count]].position.z;
                returnBalls[i + 2] = (type == 0) ? (double)1 : sphereTransform[redBalls[i / 3 - blueBalls.Count]].position.y;
                Debug.Log("red: x:" + returnBalls[i] + " y: " + returnBalls[i + 1] + " val: " + returnBalls[i + 2] + " i: " + i);
            }
        }
        for(int j=0; j<elemcount; j++)
        {
            Debug.Log(returnBalls[j]);
        }
        return returnBalls;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
