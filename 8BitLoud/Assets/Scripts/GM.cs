using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GM : MonoBehaviour {


    public static bool ended = false;

    float timer;

    static float tempo = 1.18f;

    static float sb=tempo;
    static float m = tempo / 2;
    static float sm = tempo / 4;
    static float c = tempo  / 8;
    static float sc = tempo / 16;
    static float f = tempo / 32;
    static float sf = tempo / 64;

    List<float> noteTime = new List<float>(){sb,
        sm,sm,sm,sm,
        sm,c,c,sm,sm,
        c,sm,c,c,c,sm,
        m,sm,c,c,
        c,sm,c,c,sm,c,
        sm,c,c,sm,sm,
        c,sm,c,c,c,sm,
        m,sm,c,c,
        c,sm,c,c,sm,c,
        sm,c,c,c,sm,c,
        c,c,c,c,c,c,c,c,
        sm,c,c,c,sm,c,
        c,sm,c,m,
        sm,c,c,c,sm,c,
        c,c,c,c,c,c,c,c,
        sm,sm,c,sm,c,
        m,m,
        sm,c,c,c,sm,c,
        c,c,c,c,c,c,c,c,
        sm,c,c,c,sm,c,
        c,sm,c,m,
        sm,c,c,c,sm,c,
        c,c,c,c,c,c,c,c,
        sm,sm,c,sm,c,
        m,m,

        c,sm,c,c,c,sm,
        c,sm,c,m,
        c,sm,c,c,c,c,c,
        sb,
        c,sm,c,c,c,sm,
        c,sm,c,m,
        c,c,c,c,c,c,sm,

        sm,sm,sm,sm,
        sm,c,c,sm,sm,
        c,sm,c,c,c,sm,
        m,sm,c,c,
        c,sm,c,c,sm,c,
        sm,c,c,sm,sm,
        c,sm,c,c,c,sm,
        m,sm,c,c,
        c,sm,c,c,sm,c,

    };

        //sb,sb,sb,sb };
    List<float> notePosition = new List<float>() {1,
        2,3,2,3,
        1,3,1,3,1,
        3,3,3,3,3,3,
        2,3,3,3,
        1,3,3,3,3,3,
        2,3,2,3,2,
        3,3,3,3,3,3,
        2,3,3,3,
        1,3,3,3,3,3,
        2,3,3,1,3,3,
        1,3,3,3,2,3,3,3,
        2,3,3,1,3,3,
        3,2,2,2,
        2,3,3,1,3,3,
        1,3,3,3,2,3,3,3,
        3,1,3,1,3,
        1,3,
        2,3,3,1,3,3,
        1,3,3,3,2,3,3,3,
        2,3,3,1,3,3,
        3,2,2,2,
        2,3,3,1,3,3,
        1,3,3,3,2,3,3,3,
        3,1,3,1,3,
        1,3,

        2,3,3,2,3,3,
        2,3,3,3,
        1,3,3,1,3,3,3,
        1,
        2,3,3,2,3,3,
        2,3,3,3,
        1,3,3,3,1,3,3,

        1,3,2,3,
        1,3,1,3,1,
        3,3,3,3,3,3,
        2,3,3,3,
        1,3,3,3,3,3,
        1,3,1,3,1,
        3,3,3,3,3,3,
        2,3,3,3,
        1,3,3,3,3,3,


    };
        
    //1,2,1,2 };

    public int noteMark = 0;
    public Transform noteObj;
    public bool timerReset = true;

    public float xPos;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Vibration.started)
        {
            timer -= Time.deltaTime;
            spawnNote();
        }
    }

    void spawnNote()
    {
        if (timer <= 0 && noteMark<noteTime.Count)
        {
            bool spawn;
            float xPos;

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

            if (spawn)
                Instantiate(noteObj, new Vector3(xPos, 1.27f, 2.8f), noteObj.rotation);
            timer = noteTime[noteMark];


            noteMark++;
        }
        else if (timer <= -3.6 && noteMark >= noteTime.Count && !ended)
        {
            AudioSource music = GameObject.Find("guitar").GetComponent<AudioSource>();
            music.mute = true;
            AudioSource endMusic = GameObject.Find("endMusic").GetComponent<AudioSource>();
            endMusic.Play();
            GameObject.Find("startText").GetComponent<Text>().text = "THE END";
            ended = true;
            timer = 6f;
        }
        if (timer <= 0 && ended)
        {
            Vibration.started = false;
            ended = false;
            Points.points = 0;
            SceneManager.LoadScene("Intro");
        }

    }


}
