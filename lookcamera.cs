using UnityEngine;
using System.Collections;

public class lookcamera : MonoBehaviour 
{
	public Transform Mycamera;
	void Start () 
	{
	
	}

	void Update () 
	{
		transform.LookAt (Mycamera.position);
	}
}
