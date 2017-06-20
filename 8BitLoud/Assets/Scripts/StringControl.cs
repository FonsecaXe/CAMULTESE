using UnityEngine;
using System.Collections;

public class StringControl : MonoBehaviour {

    public KeyCode activateString;
    public static bool lockInput = false;
    public static bool releasedKey = false;
    public Transform stand;
    public Transform jump;
    // Use this for initialization
    void Start () {
        jump.GetComponent<SpriteRenderer>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
	    if (!lockInput && Input.GetKeyDown(activateString))
        {
            lockInput = true;
            jump.GetComponent<SpriteRenderer>().enabled = true;
            stand.GetComponent<SpriteRenderer>().enabled = false;
            jump.GetComponent<Rigidbody2D>().velocity= new Vector3(0, 3);
            StartCoroutine(retractCollider());
           //releasedKey = false;
        }
        //if (Input.GetKeyUp(activateString))
        //{
        //    releasedKey = true;
        //}
	}

    IEnumerator retractCollider()
    {
        yield return new WaitForSeconds(.1f);
        jump.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0);
        StartCoroutine(releaseNote());

        //if (releasedKey == false)
        //{
        //    yield return new WaitForSeconds(1);
        //    StartCoroutine(releaseNote());
        //}
        //if (releasedKey == true)
        //{
        //    StartCoroutine(releaseNote());
        //}
        
        
    }

    IEnumerator releaseNote()
    {
        jump.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -3);
        yield return new WaitForSeconds(.1f);
        jump.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0);
        jump.position= new Vector3(-1f, -0.522f,-2.55f);
        jump.GetComponent<SpriteRenderer>().enabled = false;
        stand.GetComponent<SpriteRenderer>().enabled = true;
        lockInput = false;
    }
}
