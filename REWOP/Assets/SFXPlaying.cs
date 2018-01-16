using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlaying : MonoBehaviour {

	public AudioSource MM;
	public AudioSource W1;
	public AudioSource W2;
	public AudioSource W3;
	public AudioSource Attack;
	public AudioSource Jump;
	public AudioSource Enemy1;
	public AudioSource Enemy2;
	public AudioSource Enemy3;
	public AudioSource Enemy4;
	public AudioSource Enemy5;
	public AudioSource Enemy6;
	public AudioSource Enemy7;

	public static SFXPlaying instance;
	private void Awake()
	{
		instance = this;
	}

	public void PlaySound(AudioSource sound)
	{
		sound.Play ();
	}

}
