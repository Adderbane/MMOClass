using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerNetSetup : NetworkBehaviour {

    // Variables
    public Camera cam;
    public AudioListener audioList;

	// Use this for initialization
	void Start () {
        if (isLocalPlayer)
        {
            cam.enabled = true;
            audioList.enabled = true;
            GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().enabled = true;
        }

	}
	
	// Update is called once per frame
	void Update () {
        if (isLocalPlayer)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
