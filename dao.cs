using UnityEngine;
using System.Collections;

public class dao : MonoBehaviour
{
	private float lifetimmer = 0.0f;
	private Rigidbody myRig;
	//private int addforce = 0;
	public Transform ChildDao;
	void Start ()
	{
		//addforce = Random.Range(10,15);
		myRig = transform.GetComponent<Rigidbody>();
	}
	void Update () 
	{
		ChildDao.Rotate(0.0f,540.0f*Time.deltaTime,0.0f);
		lifetimmer+=Time.deltaTime;
		if(lifetimmer>3.0f)
		{
			DestroyObject(this.gameObject);
		}
	}
	void FixedUpdate()
	{
		//myRig.AddForce(transform.forward*addforce,ForceMode);
		myRig.velocity = transform.forward*10.0f;
	}
}
