using UnityEngine;
using System.Collections;

public class PlayerActions : MonoBehaviour {

    private int MAX_BULLETS = 1;
    private bool canShoot;
    private GameObject[] bullets;
    private int nextBullet;
    private float bulletSpeed;

	// Use this for initialization
	void Start () {
        canShoot = true;
        bulletSpeed = 10.0f;
        
        bullets = new GameObject[MAX_BULLETS];
        nextBullet = 0;
        for (int i = 0; i < MAX_BULLETS; i++)
        {
           // bullets[i] = (GameObject)Instantiate(bulletPrefab);
           // bullets[i].SetActive(false);
        }
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            //GameObject temp = bullets[nextBullet++];
            //if (nextBullet >= bullets.Length) nextBullet = 0;
            //temp.SetActive(true);
            //temp.GetComponent<Rigidbody>().velocity = Vector3.zero;
            //temp.transform.position = transform.position;
            //temp.transform.rotation = transform.rotation;
            //temp.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
            Debug.Log("It's shooting");

        }
	}
}
