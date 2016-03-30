using UnityEngine;
using System.Collections;

public class PlayerActions : MonoBehaviour {

    public GameObject bulletPrefab;

    private int MAX_BULLETS = 1;
    private bool canShoot;
    private GameObject[] bullets;
    private int nextBullet;
    private float bulletSpeed;

    private Ray ray;
    private RaycastHit hit;

	// Use this for initialization
	void Start () {
        canShoot = true;
        bulletSpeed = 10.0f;
        
        bullets = new GameObject[MAX_BULLETS];
        nextBullet = 0;
        for (int i = 0; i < MAX_BULLETS; i++)
        {
            bullets[i] = (GameObject)Instantiate(bulletPrefab);
            bullets[i].SetActive(false);
        }
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            //Vector2 screenCenterPoint = new Vector2(Screen.width / 2, Screen.height / 2);
            //ray = Camera.main.ScreenPointToRay(screenCenterPoint);
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Camera.main.farClipPlane))
            {
                // A collision was detected please deal with it
                Vector3 hitPosition = hit.point;
                Debug.Log(hitPosition);
                GameObject temp = bullets[nextBullet++];
                if (nextBullet >= bullets.Length) nextBullet = 0;
                temp.SetActive(true);
                temp.GetComponent<Rigidbody>().velocity = Vector3.zero;
                temp.transform.position = hitPosition;
            }
        }
	}
}
