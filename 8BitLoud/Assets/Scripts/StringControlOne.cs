using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class StringControlOne : MonoBehaviour {

    public KeyCode activateString;
    public static bool lockInput = false;
    public static bool releasedKey = false;
    public Transform stand;
    public Transform jump;
    public float pos;
    // Use this for initialization
    void Start () {
        jump.GetComponent<SpriteRenderer>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        GamePadState state = GamePad.GetState(Vibration.playerIndex);

        // Detect if a button was pressed this frame
        if (!lockInput && state.Buttons.LeftShoulder == ButtonState.Pressed)
        {
            lockInput = true;
            jump.GetComponent<SpriteRenderer>().enabled = true;
            stand.GetComponent<SpriteRenderer>().enabled = false;
            jump.GetComponent<Rigidbody2D>().velocity= new Vector3(0, 3);
            StartCoroutine(retractCollider());
        }

	}

    IEnumerator retractCollider()
    {
        yield return new WaitForSeconds(.1f);
        jump.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0);
        StartCoroutine(releaseNote());


        
        
    }

    IEnumerator releaseNote()
    {
        jump.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -7);
        yield return new WaitForSeconds(.1f);
        jump.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0);
        jump.position= new Vector3(pos, -0.522f,-2.55f);
        jump.GetComponent<SpriteRenderer>().enabled = false;
        stand.GetComponent<SpriteRenderer>().enabled = true;
        lockInput = false;
    }
}
