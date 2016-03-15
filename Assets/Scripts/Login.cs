using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Login : MonoBehaviour {

    public Canvas canvas;

	// Use this for initialization
	void Start () {
        //Debug.Log("Start!");
    }
	
	// Update is called once per frame
	void Update () {

    }

    
   /* void OnGUI()
    {
        GameObject.Find("Join Room").GetComponent<Canvas>().onClick.AddListener(() => { 
            Debug.Log("Joining Room!"); });
        GameObject.Find("Create Room").GetComponent<Button>().onClick.AddListener(() => { Debug.Log("Creating Room!"); });
    }*/

    public void JoinRoom()
    {
        Debug.Log("Joining Room!");
        print("Test");
    }

    public void CreateRoom()
    {
        Debug.Log("Creating Room!");
    }
}
