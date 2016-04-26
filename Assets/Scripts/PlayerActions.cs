using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerActions : NetworkBehaviour
{

    //Shooting
    public GameObject bullet;
    private Ray ray;
    private RaycastHit hit;

    private float fireRate = 2.0f;
    private float nextFire = 0.0f;

    private GameObject quitGame;

	// Use this for initialization
	void Start () {
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time > nextFire)
        {
            Debug.Log("It's shooting");
            Vector2 screenCenterPoint = new Vector2(Screen.width / 2, Screen.height / 2);

            ray = Camera.main.ScreenPointToRay(screenCenterPoint);

            if (Physics.Raycast(ray, out hit, Camera.main.farClipPlane))
            {
                CmdShootBullet(hit.point);
                nextFire = Time.time + fireRate;
            }
        }

        // If the escape button is pressed
        if (Input.GetKeyDown(KeyCode.Escape)){
            //Cursor.lockState = CursorLockMode.None;

            Vector2 position = new Vector2(Screen.width / 2, Screen.height / 2);
            Quaternion rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
            // Create a ExitGameUI Prefab. Then Exit the game
            quitGame = (GameObject)Instantiate(Resources.Load("ExitGameUI"),position, rotation);
        }

	}

    [Command]
    void CmdShootBullet(Vector3 pos)
    {
        GameObject temp = Instantiate(bullet, pos, Quaternion.identity) as GameObject;
        NetworkServer.Spawn(temp);
    }

}
