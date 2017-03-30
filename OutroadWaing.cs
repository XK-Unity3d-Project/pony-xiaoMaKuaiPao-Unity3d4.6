using UnityEngine;
using System.Collections;

public class OutroadWaing : MonoBehaviour 
{
	public UITexture waring;
	private float timmer = 0.0f;
	void Start ()
	{
		waring.enabled =false;
	}
	void Update ()
	{
		timmer+=Time.deltaTime;
		if(timmer > 0.3f)
		{
			if(waring.enabled == false)
			{
				waring.enabled = true;
			}
			else
			{
				waring.enabled = false;
			}
			timmer = 0.0f;
		}
	}
}
