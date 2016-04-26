using UnityEngine;
using System.Collections;

public class Boom : MonoBehaviour {

    public float explosionForce = 1000;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

    //Collision
    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.transform.gameObject.GetComponent<Rigidbody>().AddExplosionForce(1000, transform.position, 2.5f, 3.0f);
        }
    }
}
