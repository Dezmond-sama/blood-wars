using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
	//private Camera camera;
	public GameObject bullet;
	//private Rigidbody2D rb;
	public Transform firePoint;
	public float timeBetweenShoot;
	private float timeBetweenShootCooldown;

	public AudioSource fire;


    // Start is called before the first frame update
    void Start()
    {
		//rb = GetComponent<Rigidbody2D> ();
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetMouseButtonDown (0)) {
			Shoot (firePoint);
			timeBetweenShootCooldown = timeBetweenShoot;
		}
		if (Input.GetMouseButton (0)) {
			if (timeBetweenShootCooldown <= 0) {
				Shoot (firePoint);
				timeBetweenShootCooldown = timeBetweenShoot;
			} else {
				timeBetweenShootCooldown -= Time.deltaTime;
			}
		}
    }
	void Shoot (Transform firePoint){
		fire.Play();
		Instantiate (bullet, firePoint.position, firePoint.rotation);
	}
}
