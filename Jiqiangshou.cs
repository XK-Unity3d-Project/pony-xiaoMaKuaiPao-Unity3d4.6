using UnityEngine;
using System.Collections;

public class Jiqiangshou : MonoBehaviour
{
	public AnimalController myAnimalController;
	public Transform CreatPositon = null;
	public GameObject zidan;
	public AudioSource m_ShootAudio;
	private float timmer = 0.0f;
	void Start () 
	{
	
	}

	void Update ()
	{
		if(!myAnimalController.IsTaopao && !myAnimalController.IsZhuangche && myAnimalController.enabled)
		{
			timmer+=Time.deltaTime;
			if(timmer>0.1f)
			{
				GameObject temp = Instantiate(zidan,CreatPositon.position,CreatPositon.rotation) as GameObject;
				DestroyObject(temp,3.0f);
				timmer = 0.0f;
			}
			float x = transform.localEulerAngles.x;
			float z = transform.localEulerAngles.z;
			transform.LookAt(Camera.main.transform.position);
			transform.localEulerAngles = new Vector3(x,transform.localEulerAngles.y,z);
		}
		else
		{
			m_ShootAudio.Stop();
		}
	}
}
