using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPlayer : MonoBehaviour
{
	public int health;
	void OnTriggerEnter2D(Collider2D col)
	{
		PlayerController pc = col.GetComponent<PlayerController> ();
		if (pc != null) {
			pc.HurtPlayer (-health);
			Destroy (gameObject);
		}	
	}
}
