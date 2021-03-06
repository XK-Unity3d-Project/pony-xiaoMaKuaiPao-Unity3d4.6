﻿using UnityEngine;
using System.Collections;

public class CameraSmooth : MonoBehaviour 
{
	public Transform target;
	private bool IsNormal = true;
	public Transform[] ShootPos;
	public PlayerShoot m_Shoot;

	public float FallowSpeedPos = 5f;
	public float FallowSpeedRot = 4f;
	void LateUpdate () 
	{
		if (!target)
				return;
//		if(Input.GetKeyDown(KeyCode.F1))
//		{
//			IsNormal = false;
//		}
//		if(Input.GetKeyDown(KeyCode.F2))
//		{
//			IsNormal = true;
//		}
		if(IsNormal)
		{
			transform.position = Vector3.Lerp(transform.position,target.position - target.forward*6.0f+target.up*5.0f,FallowSpeedPos*Time.deltaTime);
			Vector3 forwardVal = (target.position + target.forward*10.0f) - transform.position;
			transform.forward = Vector3.Lerp(transform.forward, forwardVal.normalized, FallowSpeedRot*Time.deltaTime);
			m_Shoot.shootPointObj.position = ShootPos[0].position;
		}
		else
		{
			transform.position = Vector3.Lerp(transform.position,target.position - target.forward*1.2f+target.up*3.50f,15.0f*Time.deltaTime);
			//transform.LookAt (target.position + target.forward*15.0f);
			transform.parent = target;
			transform.localEulerAngles = new Vector3(8.0f,transform.localEulerAngles.y,transform.localEulerAngles.z);
			m_Shoot.shootPointObj.position = ShootPos[1].position;
		}
	}
}
