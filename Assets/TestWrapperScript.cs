using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWrapperScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log(DemoCPPTOUnityLibWrapper.add_to_42(51));
	}
}
