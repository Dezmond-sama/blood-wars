using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
	public MeshRenderer firstBG;
	public float firstBGSpeed = 0.01f;

	public MeshRenderer secondBG;
	public float secondBGSpeed = 0.05f;

	public MeshRenderer thirdBG;
	public float thirdBGSpeed = 0.1f;

	private Vector2 savedFirst;
	private Vector2 savedSecond;
	private Vector2 savedThird;

	public Rigidbody2D pl;

	void Start()
	{
		PlayerController p = FindObjectOfType<PlayerController>();
		if (p != null)
			pl = p.GetComponent<Rigidbody2D>();
	}
	void Awake()
	{
		if (firstBG) savedFirst = firstBG.sharedMaterial.GetTextureOffset("_MainTex");
		if (secondBG) savedSecond = secondBG.sharedMaterial.GetTextureOffset("_MainTex");
		if (thirdBG) savedThird = thirdBG.sharedMaterial.GetTextureOffset("_MainTex");
	}

	void Move(MeshRenderer mesh, Vector2 savedOffset, float speed, Vector2 playerMove)
	{
		//Vector2 offset = Vector2.zero;
		savedOffset = new Vector2(savedOffset.x + playerMove.x * speed * Time.deltaTime, savedOffset.y + playerMove.y * speed * Time.deltaTime);
		mesh.sharedMaterial.SetTextureOffset("_MainTex", savedOffset);
	}

	void Update()
	{
		if (pl == null)
			return;
		if (firstBG)
		{
			savedFirst = firstBG.sharedMaterial.GetTextureOffset("_MainTex");
			Move(firstBG, savedFirst, firstBGSpeed, pl.velocity);
		}
		if (secondBG)
		{
			savedSecond = secondBG.sharedMaterial.GetTextureOffset("_MainTex");
			Move(secondBG, savedSecond, secondBGSpeed, pl.velocity);
		}
		if (thirdBG)
		{
			savedThird = thirdBG.sharedMaterial.GetTextureOffset("_MainTex");
			Move(thirdBG, savedThird, thirdBGSpeed, pl.velocity);
		}
	}

	void OnDisable()
	{
		if (firstBG) firstBG.sharedMaterial.SetTextureOffset("_MainTex", savedFirst);
		if (secondBG) secondBG.sharedMaterial.SetTextureOffset("_MainTex", savedSecond);
		if (thirdBG) thirdBG.sharedMaterial.SetTextureOffset("_MainTex", savedThird);
	}

}
