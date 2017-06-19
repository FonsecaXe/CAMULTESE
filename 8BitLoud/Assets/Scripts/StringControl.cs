using UnityEngine;
using System.Collections;

public class StringControl : MonoBehaviour {

    public KeyCode activateString;
    public bool lockInput = false;
    public static bool releasedKey = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (!lockInput && Input.GetKeyDown(activateString))
        {
            lockInput = true;
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 4);
            StartCoroutine(retractCollider());
            releasedKey = false;
        }
        if (Input.GetKeyUp(activateString))
        {
            releasedKey = true;
        }
	}

    IEnumerator retractCollider()
    {
        yield return new WaitForSeconds(.2f);
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);

        if (releasedKey == false)
        {
            yield return new WaitForSeconds(1);
            StartCoroutine(releaseNote());
        }
        if (releasedKey == true)
        {
            StartCoroutine(releaseNote());
        }
        
        
    }

    IEnumerator releaseNote()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -4);
        yield return new WaitForSeconds(.2f);
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        lockInput = false;
    }
}
