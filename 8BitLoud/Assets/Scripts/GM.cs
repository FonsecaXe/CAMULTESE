using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GM : MonoBehaviour {

    static float tempo = 0.3f;

    static float sb=tempo*4;
    static float m = tempo * 2;
    static float sm = tempo;
    static float c = tempo * 0.5f;
    static float sc = tempo * 0.25f;
    static float f = tempo * 0.125f;
    static float sf = tempo * 0.0625f;

    List<float> noteTime = new List<float>() { c, c, c, c, c, c, sm,sm};
    List<float> notePosition = new List<float>() { 2, 2, 3, 1, 3, 2, 1, 2};

    public int noteMark = 0;
    public Transform noteObj;
    public bool timerReset = true;

    public float xPos;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (timerReset)
        {
            StartCoroutine(spawnNote());
            timerReset = false;
        }
    }

    IEnumerator spawnNote()
    {
        bool spawn;
        float xPos;
        yield return new WaitForSeconds(noteTime[noteMark]);
        if (notePosition[noteMark] == 1)
        {
            xPos = -1f;
            spawn = true;
        }
        else if (notePosition[noteMark] == 2)
        {
            xPos = 1f;
            spawn = true;
        }
        else
        {
            spawn = false;
            xPos = 0;
        }
        
        if(spawn)
            Instantiate(noteObj, new Vector3(xPos, 1.27f, 2.8f), noteObj.rotation);
        noteMark++;
        timerReset = true;
    }


}
