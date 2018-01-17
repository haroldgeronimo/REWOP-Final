using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	public GameObject mainMenuHolder;
	public GameObject optionsMenuHolder;

	public Slider[] volumeSliders;

	void start ()
	{
		volumeSliders [0].value = AudioManager.instance.masterVolumePercent;


	}

	public void Quit()
	{
		Application.Quit ();
	}

	public void OptionsMenu()
	{
		mainMenuHolder.SetActive (false);
		optionsMenuHolder.SetActive (true);
	}

	public void MainMenu ()
	{
		mainMenuHolder.SetActive (true);
		optionsMenuHolder.SetActive (false);
	}

	public void MasterVolume(float value)
	{
		AudioManager.instance.SetVolume (value, AudioManager.AudioChannel.Master);


	}
}
