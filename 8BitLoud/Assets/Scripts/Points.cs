using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Points : MonoBehaviour {

    public static int points = 0;

    public Transform pointText;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        Text textObject = GameObject.Find("Points").GetComponent<Text>();
        textObject.text = "Points: "+points;
    }
}
