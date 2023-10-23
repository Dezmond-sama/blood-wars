using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

	public float spawnDistance;
	public float minSpawnTime;
	public float maxSpawnTime;
	private float spawnTimer;
	public GameObject[] enemies;
	public float[] enemiesChanse;
	private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
		player = FindObjectOfType<PlayerController> ();
		SetSpawnTimer();
    }

    // Update is called once per frame
    void Update()
    {
		spawnTimer -= Time.deltaTime;
		if (spawnTimer < 0) {
			SetSpawnTimer();
			float ch = 0;
			for (int i = 0; i < enemiesChanse.Length; i++) {
				ch += enemiesChanse [i];
			}
			float rnd = Random.Range (0, ch);
			//Debug.Log ("" + rnd + " " + ch);
			int enemyNum = 0;
			for (int i = 0; i < enemiesChanse.Length; i++) {
				rnd -= enemiesChanse [i];
				if (rnd <= 0) {
					enemyNum = i;
					break;
				}
			}
			if (enemyNum >= enemies.Length)
				enemyNum = 0;
			if (player == null)
				return;
			//RaycastHit2D r = Physics2D.Raycast (player.transform.position, new Vector2 (Random.Range (-1, 1), Random.Range (-1, 1)), spawnDistance);
			float a = Random.Range(0f,360f);
			Vector3 v = new Vector3(Mathf.Sin(a*Mathf.Deg2Rad),Mathf.Cos(a*Mathf.Deg2Rad),0).normalized*spawnDistance + player.transform.position;
			Instantiate(enemies[enemyNum], v, Quaternion.identity);
		}
    }
	void SetSpawnTimer ()
	{
		spawnTimer = Random.Range(minSpawnTime, maxSpawnTime);
	}
}
