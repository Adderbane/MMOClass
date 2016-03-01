﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerNetSetup : NetworkBehaviour {

    // Variables
    public Camera cam;
    public AudioListener audioList;

	// Use this for initialization
	void Start () {
        if (isLocalPlayer)
        {
            GetComponent<CharacterController>().enabled = true;
            GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
            cam.enabled = true;
            audioList.enabled = true;
        }

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}