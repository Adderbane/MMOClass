using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MyNetworkManager : NetworkManager {

    //Global Variables
    public static NetworkClient myClient;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void InitHost(){
        NetworkManager.singleton.StartHost();
    }

    void InitClient(){
        NetworkManager.singleton.StartClient();
    }
}
