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
            //Instantiate(failBurst, transform.position, failBurst.rotation);
        }

        if (other.gameObject.name == "sucessOne" && StringControlOne.lockInput)
        {
            Destroy(gameObject);
            Instantiate(sucessBurst, transform.position, sucessBurst.rotation);
        }
        if (other.gameObject.name == "sucessTwo" && StringControlTwo.lockInput)
        {
            Destroy(gameObject);
            Instantiate(sucessBurst, transform.position, sucessBurst.rotation);
        }
    }
}
