using UnityEngine;
using System.Collections;

public class note2xControl : MonoBehaviour {

    // Use this for initialization
    public Transform sucessBurst;
    public Transform failBurst;
    public int noteHP = 10;

    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (noteHP < 1)
        {
            Destroy(gameObject);
            Debug.Log("HEIL!!!!");
            Instantiate(sucessBurst, transform.position, sucessBurst.rotation);
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "failcollider")
        {
            Destroy(gameObject);
            Debug.Log("FAIL!!!!");
            Instantiate(failBurst, transform.position, failBurst.rotation);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "sucess" && StringControl.releasedKey==false)
        {
            noteHP--;
        }
    }
    }
