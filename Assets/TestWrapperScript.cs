using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWrapperScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		double W = DemoCPPTOUnityLibWrapper.linear_create ();
		Debug.Log(W[0]);
	}
}
