using UnityEngine;
using System.Collections.Generic;
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

    private int MAX_BULLETS = 2;
    private GameObject[] bulletPool;
    private int nextBullet;

    private GameObject quitGame;
    private bool gameRunning;

    // Use this for initialization
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        //bulletPool = new GameObject[MAX_BULLETS];
        //nextBullet = 0;
        //for (int i = 0; i < MAX_BULLETS; i++)
        //{
        //    bulletPool[i] = Instantiate(bullet) as GameObject;
        //    NetworkServer.Spawn(bulletPool[i]);
        //    bulletPool[i].SetActive(false);
        //}

        //quit ui
        Vector2 position = new Vector2(Screen.width / 2, Screen.height / 2);
        Quaternion rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
        // Create a ExitGameUI Prefab. Then Exit the game
        quitGame = (GameObject)Instantiate(Resources.Load("ExitGameUI"), position, rotation);
        //hide it
        gameRunning = true;
        quitGame.SetActive(!gameRunning);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(bulletPool.Length);
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameRunning)
            {
                gameRunning = !gameRunning;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                quitGame.SetActive(!gameRunning);
                NetworkManager.singleton.StopClient();
                Application.Quit();
            }
            else
            {
                gameRunning = !gameRunning;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                quitGame.SetActive(!gameRunning);
            }

        }

    }

    [Command]
    void CmdShootBullet(Vector3 pos)
    {
        GameObject temp = Instantiate(bullet, pos, Quaternion.identity) as GameObject;
        NetworkServer.Spawn(temp);

        //GameObject temp = bulletPool[nextBullet++];
        //if (nextBullet >= bulletPool.Length) nextBullet = 0;
        //
        //temp.SetActive(true);
        //temp.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //temp.transform.position = pos;
    }

}
