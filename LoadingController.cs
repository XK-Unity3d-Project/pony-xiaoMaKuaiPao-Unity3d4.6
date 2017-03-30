using UnityEngine;
using System.Collections;

public class LoadingController : MonoBehaviour 
{
	private float timmer = 0.0f;
	public UITexture[] myTexture;

	void Start () 
	{
		for(int i=0;i<myTexture.Length;i++)
		{
			myTexture[i].enabled = false;
		}
	}
	void Update () 
	{
		timmer += Time.deltaTime;
		if(timmer > 1.2f)
		{
			MyCOMDevice.ComThreadClass.IsLoadingLevel = true;
			Invoke("beginLoadScence", 0.1f);
			timmer-=1.4f;
		}
		for(int i=0;i<6;i++)
		{
			if(timmer > i*0.2f)
			{
				myTexture[i].enabled = true;
			}
			else
			{
				myTexture[i].enabled = false;
			}
		}
	}
	
	void beginLoadScence()
	{
		StartCoroutine (loadScene());
	}

	IEnumerator loadScene()   
	{
		//MyCOMDevice.ComThreadClass.IsLoadingLevel = true;
		Debug.Log("load level     1");
		AsyncOperation async = Application.LoadLevelAsync(1 + pcvr.chenNum);   
		yield return async;	
	}
}
