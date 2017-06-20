using UnityEngine;
using XInputDotNetPure;
using System.Collections.Generic;

public class Vibration : MonoBehaviour {
    //static float tempo = 1f;

    //static float sb = tempo * 4;
    //static float m = tempo * 2;
    //static float sm = tempo;
    //static float c = tempo * 0.5f;
    //static float sc = tempo * 0.25f;
    //static float f = tempo * 0.125f;
    //static float sf = tempo * 0.0625f;

    //List<float> noteTime = new List<float>() { c, c, c, c, c, c, sm, sm };
    //List<float> notePosition = new List<float>() { 2, 2, 3, 2, 3, 1, 2, 2 };

    static float den = 1.2f;

    float timeBWNote = 2f*den;

    int tic = 0;
    bool playingIntro=true;

    bool playerIndexSet = false;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;
    MusicNote[] notes= { new MusicNote(1, 0, 1), new MusicNote(0.5f, 1, 0), new MusicNote(1, 0, 0) };
    int currNote = 0;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        // Find a PlayerIndex, for a single player game
        // Will find the first controller that is connected ans use it
        if (!playerIndexSet || !prevState.IsConnected)
        {
            for (int i = 0; i < 4; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
                    Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
                    playerIndex = testPlayerIndex;
                    playerIndexSet = true;
                }
            }
        }

        playMusic();
        timeBWNote -= Time.deltaTime;
    }

    void intro()
    {
        if (timeBWNote > 1.75f*den)
        {
            GamePad.SetVibration(playerIndex, 0, 1);
        }
        else if(timeBWNote>1.5f * den && timeBWNote <= 1.75f*den) { 
                GamePad.SetVibration(playerIndex, 0, 0);
        }
        else if(timeBWNote>1.25f * den && timeBWNote <= 1.5f * den) { 
                GamePad.SetVibration(playerIndex, 0, 1);
        }
        else if(timeBWNote>1f * den && timeBWNote <= 1.25f * den) { 
                GamePad.SetVibration(playerIndex, 0, 0);
        }
        if (timeBWNote > 0.75f * den && timeBWNote <= 1f * den)
        {
            GamePad.SetVibration(playerIndex, 0, 1);
        }
        else if (timeBWNote > 0.5f * den && timeBWNote <= 0.75f * den)
        {
            GamePad.SetVibration(playerIndex, 0, 0);
        }
        else if (timeBWNote > 0.25f * den && timeBWNote <= 0.5f * den)
        {
            GamePad.SetVibration(playerIndex, 0, 1);
        }
        else if (timeBWNote > 0 && timeBWNote <= 0.25f * den)
        {
            GamePad.SetVibration(playerIndex, 0, 0);
        }
        else
        {
            playingIntro = false;
            timeBWNote = 0.5f * den;

        }
        

    }

    void playMusic()
    {
        if (playingIntro)
        {
            intro();
        }
        else {
            if (timeBWNote > 0.25f * den)
            {
                if (tic == 0)
                {
                    GamePad.SetVibration(playerIndex, 0, 1);
                    tic = 1;
                }
                else if (tic == 1) {
                    GamePad.SetVibration(playerIndex, 0.5f * den, 0);
                    tic = 0;
                }
            }
            else if (timeBWNote > 0 && timeBWNote <= 0.25f * den)
                GamePad.SetVibration(playerIndex, 0, 0);
            else
            {
                timeBWNote = 0.5f * den;
            }
        }

        //if (timeBWNote > 0.15f) {
        //    if (notePosition[currNote]==1)
        //        GamePad.SetVibration(playerIndex, 1, 0);
        //    else if (notePosition[currNote] == 2)
        //        GamePad.SetVibration(playerIndex, 0, 1);
        //    else 
        //        GamePad.SetVibration(playerIndex, 0, 0);
        //}
        //else if (timeBWNote > 0 && timeBWNote <= 0.25f)
        //    GamePad.SetVibration(playerIndex, 0, 0);
        //else
        //{
        //    currNote++;
        //    timeBWNote = noteTime[currNote];
        //}
    }
}

public class MusicNote
{
    public float tempo;
    public float rightV;
    public float leftV;
    public MusicNote(float tempo,float rightV, float leftV)
    {
        this.tempo = tempo;
        this.rightV = rightV;
        this.leftV = leftV;
    }
}
