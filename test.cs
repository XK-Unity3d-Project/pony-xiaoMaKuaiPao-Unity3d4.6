using UnityEngine;
using System.Collections;

public class test : MonoBehaviour 
{
	void Start () 
	{
		
	}
	void Update () 
	{
		transform.position += transform.forward * 30.0f * Time.deltaTime;	
	}
}
