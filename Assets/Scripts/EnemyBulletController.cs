using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
	public float speed;
	public int damage;
	private Rigidbody2D rb;
	public float lifetime=10f;

	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody2D> ();
		rb.velocity = transform.up * speed;
	}
	void Update(){
		lifetime -= Time.deltaTime;
		if (lifetime < 0) {
			Destroy (gameObject);
		}
	}
	void OnTriggerEnter2D(Collider2D col){
		PlayerController pc = col.GetComponent<PlayerController> ();
		if (pc != null) {
			pc.HurtPlayer (damage);
			Destroy (gameObject);
		}
	}

}
