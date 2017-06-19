using UnityEngine;
using System.Collections;

public class Note : MonoBehaviour {

    public Transform sucessBurst;
    public Transform failBurst;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        GetComponent<Rigidbody>().velocity = new Vector3(0, -0.4f, -1.45f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "failcollider")
        {
            Destroy(gameObject);
            Debug.Log("FAIL!!!!");
            Instantiate(failBurst, transform.position, failBurst.rotation);
        }

        if (other.gameObject.name == "sucess")
        {
            Destroy(gameObject);
            Debug.Log("HEIL!!!!");
            Instantiate(sucessBurst, transform.position, sucessBurst.rotation);
        }
    }
}
