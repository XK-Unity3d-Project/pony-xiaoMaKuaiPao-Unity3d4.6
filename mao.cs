using UnityEngine;
using System.Collections;

public class mao : MonoBehaviour 
{
	private float lifetimmer = 0.0f;
	private Rigidbody myRig;
	private bool  IsMove = true;
	private int anglespeed = 0;
	//private int addforce = 0;
	public float speed = 20.0f;
	void Start ()
	{
		anglespeed = Random.Range(60,80);
		//addforce = Random.Range(1900,2100);
		myRig = transform.GetComponent<Rigidbody>();
	}
	void Update () 
	{
		lifetimmer+=Time.deltaTime;
		if(lifetimmer>3.0f)
		{
			DestroyObject(this.gameObject);
		}
	}
	void FixedUpdate()
	{
		if(IsMove)
		{
			transform.Rotate(new Vector3(0.0f,anglespeed*Time.deltaTime,0.0f));
			//myRig.AddForce(transform.forward*addforce*Time.deltaTime);
			myRig.velocity = transform.forward*speed;
		}
	}
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "dimian")
		{
			myRig.useGravity = false;
			myRig.isKinematic = true;
			IsMove = false;
		}
		if(other.tag == "player")
		{
			//Debug.Log("zhuangji");
			myRig.useGravity = true;
			myRig.isKinematic = false;
			myRig.AddForce(transform.up*200.0f);
			myRig.AddForce(other.transform.forward*1000.0f);
		}
	}
}
