using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	public float speed;
	public Rigidbody2D target;
	 
	public int minAngularSpeed;
	public int maxAngularSpeed;
	private int angularSpeed;
	private Rigidbody2D rb;

	public float minEffectDelay;
	public float maxEffectDelay;
	private float effectDelay;
	public GameObject moveEffect;
	public GameObject deathEffect;
	public float avoidDistance;
	public float turnSpeed = 5f;
	public int health;
	public int damage;
	public int points;
	public bool enemyWithFace;

	public bool fireEnemy;
	public GameObject bullet;
	public float distanceToFire;
	public float retreatDistance;
	public float timeBetweenShots;
	private float fireTimer;

	private static List<Rigidbody2D> enemies;

	public GameObject[] dropItems;
	public float[] dropChance;
	public float maxDropPower;

    // Start is called before the first frame update
    void Start(){
		PlayerController pc = FindObjectOfType<PlayerController>();
		target = pc.GetComponent<Rigidbody2D> ();
		angularSpeed = Random.Range (minAngularSpeed, maxAngularSpeed + 1);
		if (Random.Range (0, 2) == 1)
			angularSpeed *= -1;
		rb = GetComponent<Rigidbody2D> ();
		if (!enemyWithFace) rb.angularVelocity = angularSpeed;

		if (enemies == null)
		{
			enemies = new List<Rigidbody2D>();
		}
		fireTimer = timeBetweenShots;

		enemies.Add(rb);
    }

	private void OnDestroy()
	{
		enemies.Remove(rb);
	}

    // Update is called once per frame
    void FixedUpdate(){
		if (target == null)
			return;
		//transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed*Time.deltaTime); 
		Vector2 direction = (target.position - rb.position).normalized;
		Vector2 newPos = MoveEnemy(direction);
		if (fireEnemy) {
			fireTimer -= Time.fixedDeltaTime;
			if (fireTimer < 0 && Vector2.Distance(target.position,rb.position) < distanceToFire) {
				fireTimer = timeBetweenShots;
				Fire (direction);
			}
		}
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
		if(enemyWithFace)rb.rotation = Mathf.LerpAngle(rb.rotation, angle, turnSpeed*Time.fixedDeltaTime);

		rb.MovePosition(newPos);

		if (effectDelay <= 0) {
			effectDelay = Random.Range (minEffectDelay, maxEffectDelay);
			Instantiate (moveEffect, transform.position, Quaternion.identity);
		} else {
			effectDelay -= Time.fixedDeltaTime;
		}
    }
	Vector2 MoveEnemy(Vector2 direction){
		Vector2 repelForce = Vector2.zero;
		foreach (Rigidbody2D enemy in enemies)
		{
			if (enemy == rb)
				continue;

			if (Vector2.Distance(enemy.position, rb.position) <= avoidDistance)
			{
				Vector2 repelDir = (rb.position - enemy.position).normalized;
				repelForce += repelDir;
			}
		}
		Vector2 newPos;
		if (enemyWithFace) {
			if (fireEnemy && Vector2.Distance (rb.position, target.position) < retreatDistance){
				newPos = new Vector2 (transform.position.x, transform.position.y) - new Vector2 (transform.up.x, transform.up.y) * Time.fixedDeltaTime * speed;
			} else if (fireEnemy && Vector2.Distance (rb.position, target.position) < distanceToFire){
				newPos = new Vector2 (transform.position.x, transform.position.y) + new Vector2 (transform.right.x, transform.right.y) * Time.fixedDeltaTime * speed;
			}else{
				newPos = new Vector2 (transform.position.x, transform.position.y) + new Vector2 (transform.up.x, transform.up.y) * Time.fixedDeltaTime * speed;
			}
		} else {
			if (fireEnemy && Vector2.Distance (rb.position, target.position) < retreatDistance){
				newPos = new Vector2 (transform.position.x, transform.position.y) - direction.normalized * Time.fixedDeltaTime * speed;
			} else if (fireEnemy && Vector2.Distance (rb.position, target.position) < distanceToFire){
				newPos = new Vector2 (transform.position.x, transform.position.y);
			} else {
				newPos = new Vector2 (transform.position.x, transform.position.y) + direction.normalized * Time.fixedDeltaTime * speed;
			}
		}
		newPos += repelForce * Time.fixedDeltaTime * avoidDistance;
		return newPos;
	}
	void Fire(Vector2 direction){
		float a = Mathf.Atan2 (direction.y, direction.x)*Mathf.Rad2Deg-90f;
		Instantiate(bullet,transform.position,Quaternion.AngleAxis(a,Vector3.forward));
	}
	public int HurtEnemy(int damage,bool hurtByPlayer){
		GetComponent<AudioSource>().Play ();
		health -= damage;
		if (health <= 0) {
			if (hurtByPlayer) {
				for (int i = 0; i < dropItems.Length; i++) {
					float rnd = Random.Range (0f, 100f);
					if (rnd < dropChance [i]) {
						GameObject item = Instantiate (dropItems [i], transform.position, Quaternion.identity) as GameObject;
						Rigidbody2D rigid = item.GetComponent<Rigidbody2D> ();
						if (rigid != null) {
							rigid.AddForce (new Vector2(Random.Range(-1f,1f),Random.Range(-1f,1f)).normalized*Random.Range(0f,maxDropPower));
						}
					}
				}
			}
			Instantiate (deathEffect, transform.position, transform.rotation);
			Destroy (gameObject);
			return points;
		}
		return 0;
	}
	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "Player") {
			col.GetComponent<PlayerController> ().HurtPlayer (damage);
			HurtEnemy (health,false);
		}
	}
}
