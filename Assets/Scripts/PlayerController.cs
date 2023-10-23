using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	public float speed;
	private Vector2 moveVector;
	private Rigidbody2D rb;
	public Animator healthAnim;

	public float minEffectDelay;
	public float maxEffectDelay;
	private float effectDelay;
	public GameObject moveEffect;
	private Vector2 mousePos = Vector2.zero;
	public GameObject deathEffect;

	public int maxHealth;
	public int health;
	public int score;

	public AudioSource healSound;
	public AudioSource damageSound;


	public Slider healthDisplay;
	public Text scoreDisplay;
	 // Start is called before the first frame update
    void Start()
    {
		rb = GetComponent<Rigidbody2D> ();
		Respawn ();
    }

	void Respawn()
	{
		health = maxHealth;
		scoreDisplay.text = "" + score;
		healthDisplay.maxValue = maxHealth;
		healthDisplay.minValue = 0;
		healthDisplay.value = health;
		healthAnim.SetInteger ("Health", health*100/maxHealth);
	}
    // Update is called once per frame
    void Update()
    {
		mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		moveVector = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical")).normalized;
		if (moveVector != Vector2.zero) {
			if (effectDelay <= 0) {
				effectDelay = Random.Range (minEffectDelay, maxEffectDelay);
				Instantiate (moveEffect, transform.position, Quaternion.identity);
			} else {
				effectDelay -= Time.deltaTime;
			}
		} else {
			effectDelay = 0;
		}
		rb.velocity = moveVector * speed;
    }
	void FixedUpdate(){
		Vector2 lookDir = mousePos - rb.position;
		float angle = Mathf.Atan2 (lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
		rb.rotation = angle;
	}
	public void HurtPlayer(int damage){
		health -= damage;
		if (health > maxHealth)
			health = maxHealth;
		healthDisplay.value = health;
		healthAnim.SetInteger ("Health", health*100/maxHealth);
		//Debug.Log(healthAnim.GetInteger("Health"));
		if (health <= 0) {
			Instantiate (deathEffect, transform.position, Quaternion.identity);
			FindObjectOfType<CameraController> ().EndGame ();
			Destroy (gameObject);
		} else {
			if (damage > 0) {
				damageSound.Play ();
			} else if (damage < 0) {
				healSound.Play ();
			}
		}
	}
	public void AddScore(int points)
	{
		score += points;
		scoreDisplay.text = "" + score;
		PlayerPrefs.SetInt ("CurrentScore", score);
		int highscore = 0;
		if(PlayerPrefs.HasKey("HighScore")) highscore = PlayerPrefs.GetInt("HighScore");
		if(score>highscore)PlayerPrefs.SetInt ("HighScore", score);
	}
}
