using UnityEngine;
using UnityEngine.UI;
using XInputDotNetPure;
using System.Collections;

public class Vibration : MonoBehaviour {


    public Transform vibOn;
    public Transform vibOff;
    public Transform musOn;
    public Transform musOff;
    float timeMusic;
    float timeVibration;
    bool vibrateLock = false;
    bool soundLock = true;
    bool vibrate = true;
    public static int vibLev = 1;

    static float den = 1.175f;

    float timeBWNote = 3.6f*den;
    bool tic = false;
    bool playingIntro=true;
    public static bool started = false;



    public static bool noSound = false;
    bool playerIndexSet = false;
    public static PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;
    int currNote = 0;

    // Use this for initialization
    void Start () {
        vibOn.GetComponent<SpriteRenderer>().enabled = false;
        vibOff.GetComponent<SpriteRenderer>().enabled = false;
        musOn.GetComponent<SpriteRenderer>().enabled = false;
        musOff.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
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
        prevState = state;
        state = GamePad.GetState(playerIndex);

        if (state.Buttons.Start == ButtonState.Pressed && !started)
        {
            started = true;
            Text startText = GameObject.Find("startText").GetComponent<Text>();
            startText.text = "";
        }

        // Detect if a button was pressed this frame
        if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed && !vibrateLock)
        {
            vibrate = !vibrate;
            vibrateLock = true;
            timeVibration = 1;
        }
        // Detect if a button was released this frame
        if (prevState.Buttons.B == ButtonState.Released && state.Buttons.B == ButtonState.Pressed && !soundLock)
        {
            noSound = !noSound;
            GetComponent<AudioSource>().mute = noSound;
            soundLock = true;
            timeMusic = 1;

        }

        if (started && !GM.ended) { 
            if (vibrate)
                vibLev = 1;
            else
                vibLev = 0;

            playMusic();
            timeBWNote -= Time.deltaTime;

            timeMusic -= Time.deltaTime;
            timeVibration -= Time.deltaTime;

            ShowMusic();
            ShowVibration();
        }
    }

    void intro()
    {
        if (timeBWNote > 2f * den)
        {
            GamePad.SetVibration(playerIndex, 0, 0);
        }
        else if (timeBWNote > 1.75f*den && timeBWNote <= 2f * den)
        {
            GamePad.SetVibration(playerIndex, 0, vibLev);
        }
        else if(timeBWNote>1.5f * den && timeBWNote <= 1.75f*den) { 
                GamePad.SetVibration(playerIndex, 0, 0);
        }
        else if(timeBWNote>1.25f * den && timeBWNote <= 1.5f * den) { 
                GamePad.SetVibration(playerIndex, 0, vibLev);
        }
        else if(timeBWNote>1f * den && timeBWNote <= 1.25f * den) { 
                GamePad.SetVibration(playerIndex, 0, 0);
        }
        if (timeBWNote > 0.75f * den && timeBWNote <= 1f * den)
        {
            
            GamePad.SetVibration(playerIndex, 0, vibLev);
        }
        else if (timeBWNote > 0.5f * den && timeBWNote <= 0.75f * den)
        {
            GetComponent<AudioSource>().Play();
            GamePad.SetVibration(playerIndex, 0, 0);
        }
        else if (timeBWNote > 0.25f * den && timeBWNote <= 0.5f * den)
        {

            
            GamePad.SetVibration(playerIndex, vibLev, vibLev);
        }
        else if (timeBWNote > -0.5*den && timeBWNote <= 0.25f * den)
        {
            GamePad.SetVibration(playerIndex, 0, 0);
        }
        else if (timeBWNote <= -0.5*den)
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
                if (!tic)
                {
                    GamePad.SetVibration(playerIndex, 0, vibLev);
                }
                else if (tic) {
                    GamePad.SetVibration(playerIndex, vibLev*0.2f, 0);
                }
            }
            else if (timeBWNote > 0 && timeBWNote <= 0.25f * den)
                GamePad.SetVibration(playerIndex, 0, 0);
            else
            {
                tic = !tic;
                timeBWNote = 0.5f * den;
            }
        }


    }

    void ShowVibration()
    {
        if (vibrate && vibrateLock && timeVibration>0)
        {
            vibOn.GetComponent<SpriteRenderer>().enabled = true;
            return;

        }
        else if (!vibrate && vibrateLock && timeVibration > 0)
        {
            vibOff.GetComponent<SpriteRenderer>().enabled = true;
            return;

        }
        else
        {
            vibOff.GetComponent<SpriteRenderer>().enabled = false;
            vibOn.GetComponent<SpriteRenderer>().enabled = false;
            vibrateLock = false;
        }
    }

    void ShowMusic()
    {
        if (!noSound && soundLock && timeMusic> 0)
        {
            musOn.GetComponent<SpriteRenderer>().enabled = true;
            return;

        }
        else if (noSound && soundLock && timeMusic > 0)
        {
            musOff.GetComponent<SpriteRenderer>().enabled = true;
            return;
        }
        else
        {
            musOff.GetComponent<SpriteRenderer>().enabled = false;
            musOn.GetComponent<SpriteRenderer>().enabled = false;
            soundLock = false;
        }

    }
}



