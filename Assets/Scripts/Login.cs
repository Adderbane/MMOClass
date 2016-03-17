using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Login : MonoBehaviour {

    public Canvas canvas;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void JoinRoom(){
        // Routes to the specified scene
        Application.LoadLevel(1); // JoinRoom Scene
    }

    public void CreateRoom(){
        // Routes to the specified scene
        Application.LoadLevel(2); // CreateRoom Scene
    }

    public void StartRoom(){
       

    }
}
