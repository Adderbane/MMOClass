using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Login : NetworkManager {

    public Canvas canvas;

    public void StartHostGame()
    {
        // Set the network port then start hosting
        NetworkManager.singleton.StopHost();
        SetPort();
        NetworkManager.singleton.StartHost();
    }

    public void StartClientGame()
    {
        // Set the ip address and the network port then join the hosted game.
        SetIPAddress();
        SetPort();
        NetworkManager.singleton.StartClient();
    }

    public void SetPort()
    {
        // Set the port to 7777
        NetworkManager.singleton.networkPort = 7777;
    }

    public void SetIPAddress()
    {
        // Set the ip address to the entered value
        string ipAddress = GameObject.Find("PortInput").transform.FindChild("Text").GetComponent<Text>().text;
        NetworkManager.singleton.networkAddress = ipAddress;
    }

    public void OnLevelWasLoaded(int level)
    {
        // Check if the game is in the lobby.
        if (level == 0)
        {
            // Remove all active listeners of the type and then add a new listener.
            GameObject.Find("JoinRoom").GetComponent<Button>().onClick.RemoveAllListeners();
            GameObject.Find("JoinRoom").GetComponent<Button>().onClick.AddListener(StartClientGame);

            GameObject.Find("CreateRoom").GetComponent<Button>().onClick.RemoveAllListeners();
            GameObject.Find("CreateRoom").GetComponent<Button>().onClick.AddListener(StartHostGame);
        }
        
    }

}
