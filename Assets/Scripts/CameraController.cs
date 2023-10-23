using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour {

	public Transform target;
	public float speed;
	//private static bool isCreated;

	private Camera theCamera;
	//private AudioListener listener;

	// Use this for initialization
	void Start () {
		//if (isCreated) {
		//	Destroy (transform.gameObject);
		//} else {
		//	DontDestroyOnLoad (transform.gameObject);
		//	isCreated = true;
		//}
		theCamera = GetComponent<Camera> ();
		//listener = FindObjectOfType<AudioListener> ();
		if(PlayerPrefs.HasKey ("Volume"))AudioListener.volume = PlayerPrefs.GetFloat ("Volume");
	}

	// Update is called once per frame
	void FixedUpdate () {
		if(target!=null)theCamera.transform.position = Vector3.Lerp (transform.position, new Vector3 (target.position.x, target.position.y, theCamera.transform.position.z),speed*Time.deltaTime);
	}

	public void EndGame ()
	{
		StartCoroutine("Gameover");		
	}
	IEnumerator Gameover()
	{
		//gameObject.SetActive (false);
		yield return new WaitForSeconds(2);
		SceneManager.LoadScene ("gameover");
	}
}
