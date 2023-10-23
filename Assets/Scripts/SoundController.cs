using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
	private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
		slider = GetComponent<Slider> ();
		if(!PlayerPrefs.HasKey("Volume"))
		//	AudioListener.volume = PlayerPrefs.GetFloat("Volume");
		//else
			PlayerPrefs.SetFloat ("Volume",slider.value);
		slider.value = PlayerPrefs.GetFloat ("Volume");
    }

    // Update is called once per frame
    void Update()
    {
		//AudioListener.volume = slider.value;
		PlayerPrefs.SetFloat ("Volume",slider.value);
    }
}
