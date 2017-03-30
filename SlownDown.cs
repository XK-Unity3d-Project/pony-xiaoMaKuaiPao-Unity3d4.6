using UnityEngine;
using System.Collections;

public class SlownDown : MonoBehaviour 
{
	public GameObject ExplorEffect;
	private float timmer = 0.0f;
	public bool IsTrue = false;
	public AudioSource m_Audio;
	void Start () 
	{
		ExplorEffect.SetActive (false);
	}
	void Update () 
	{
		if(IsTrue)
		{
			ExplorEffect.SetActive (true);
			timmer+=Time.deltaTime;
		}
		if(timmer>0.5f)
		{
			ExplorEffect.SetActive (false);
			IsTrue = false;
			timmer = 0.0f;
		}
	}
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "player")
		{
			IsTrue = true;
			m_Audio.Play();
			PlayerController.speed = PlayerController.speed * 0.85f;
		}
	}
}
