using UnityEngine;
using System.Collections;

public class MaController : MonoBehaviour 
{
	public float speed = 16.7f;
	private bool IsMove = false;
	private RaycastHit hit;
	private Vector3 LookTarget;
	public float Myangle = 0.3f;
	public UIController myUI;
	void Start () 
	{
		transform.eulerAngles = new Vector3(0.0f,-47.03293f,0.0f);
	}
	void Update () 
	{
		if(myUI.CountTime <= 0.0f && !myUI.IsGameOver)
		{
			if (pcvr.bIsHardWare)
			{
				if(pcvr.GetInstance().getJiasu())
				{
					IsMove = true;
				}
				else
				{
					IsMove = false;
				}
				if(pcvr.GetInstance().getTurnLeft())
				{
					transform.Rotate(new Vector3(0.0f,-Myangle,0.0f));				
				}
				if(pcvr.GetInstance().getTurnRight())
				{
					transform.Rotate(new Vector3(0.0f,Myangle,0.0f));
				}
			}
			else
			{
				if(Input.GetKey(KeyCode.W))
				{
					IsMove = true;
				}
				else
				{
					IsMove = false;
				}
				if(Input.GetKey(KeyCode.A))
				{
					transform.Rotate(new Vector3(0.0f,-Myangle,0.0f));				
				}
				if(Input.GetKey(KeyCode.D))
				{
					transform.Rotate(new Vector3(0.0f,Myangle,0.0f));
				}
			}

			if(IsMove)
			{
				transform.position += transform.forward * speed *Time.deltaTime;
			}
			LookTarget = transform.position + transform.forward*1.0f + Vector3.up*50.0f;
			if(Physics.Raycast(transform.position+Vector3.up*50.0f,-Vector3.up,out hit,100.0f))
			{
				transform.position = hit.point;
			}
			if(Physics.Raycast(LookTarget,-Vector3.up,out hit,100.0f))
			{
				LookTarget = hit.point;
			}
			transform.LookAt(LookTarget);
			float xangle = transform.eulerAngles.x;
			float yangle = transform.eulerAngles.y;
			transform.eulerAngles = new Vector3(xangle,yangle,0.0f);
		}
	}
}
