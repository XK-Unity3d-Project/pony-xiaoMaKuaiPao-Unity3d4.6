using UnityEngine;
using System.Collections;

public class ShanshuoCtrl : MonoBehaviour 
{
	public UITexture m_pUITexture;
	private float timmer = 0.0f;
	void Start () 
	{
	
	}
	void Update () 
	{
		timmer += Time.deltaTime;
		if(timmer>=0.0f && timmer<=0.5f)
		{
			m_pUITexture.enabled = true;
		}
		else if(timmer>0.5f && timmer<=1.0f)
		{
			m_pUITexture.enabled = false;
		}
		else
		{
			timmer = 0.0f;
		}
	}
}
