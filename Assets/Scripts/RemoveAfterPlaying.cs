using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveAfterPlaying : MonoBehaviour {

	private ParticleSystem[] anims;
	// Use this for initialization
	void Start () {
		anims = GetComponentsInChildren<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
		foreach(ParticleSystem p in anims){
			if (p.isPlaying)return;
		}
		Destroy (transform.gameObject);
	}
}
