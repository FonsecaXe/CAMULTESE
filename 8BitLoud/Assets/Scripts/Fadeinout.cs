using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using XInputDotNetPure;

public class Fadeinout : MonoBehaviour {

    bool lockFade = false;


    bool playerIndexSet = false;
    public static PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;
    // Use this for initialization
    void Start () {
        
	}

    // can ignore the update, it's just to make the coroutines get called for example
    void Update()
    {
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


        if (state.Buttons.Start==ButtonState.Pressed)
        {
            SceneManager.LoadScene("8bitloud");
        }
        if (!lockFade)
        {
            lockFade = true;
            StartCoroutine(FadeTextToFullAlpha(1f, GetComponent<SpriteRenderer>()));
        }


    }



    public IEnumerator FadeTextToFullAlpha(float t, SpriteRenderer i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0.5f);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }

        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.5f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
        lockFade = false;
    }

}
