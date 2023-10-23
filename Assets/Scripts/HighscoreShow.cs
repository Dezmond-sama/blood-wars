using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreShow : MonoBehaviour
{
	// Start is called before the first frame update
    void Start()
    {
		int highscore = 0;
		if(PlayerPrefs.HasKey("HighScore"))highscore = PlayerPrefs.GetInt ("HighScore");

		GetComponent<Text>().text = "HIGHSCORE "+highscore;
    }
}
