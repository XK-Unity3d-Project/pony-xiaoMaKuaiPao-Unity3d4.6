using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour 
{
	public AudioSource m_Audio;
	public float speed = 0.1f;
	void Start () 
	{
		m_Audio.pitch = speed;
		m_Audio.Play ();
	}

	void Update () 
	{
		m_Audio.pitch = speed;
	}
}
