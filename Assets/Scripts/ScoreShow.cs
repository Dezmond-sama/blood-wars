using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreShow : MonoBehaviour
{
	void Start()
	{
		int score = 0;
		if(PlayerPrefs.HasKey("CurrentScore"))score = PlayerPrefs.GetInt ("CurrentScore");

		GetComponent<Text>().text = "SCORE "+score;
	}
}
