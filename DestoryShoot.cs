using UnityEngine;
using System.Collections;

public class DestoryShoot : MonoBehaviour
{
	//private Rigidbody temp;
	public GameObject particle;
	void Start () 
	{
		//temp = transform.GetComponent<Rigidbody>();
	}
	void Update ()
	{
	
	}
	void  OnTriggerEnter(Collider other)
	{
		if(other.tag!="creatpoint" && other.tag!="yeren" && other.tag!="animal" && other.tag!="zhaLan" && other.tag!="ziDan" && other.tag!="npcpathpoint" && other.tag!="pathpoint" &&other.tag!="player" &&other.tag!="outroad" &&other.tag!="slowdown")
		{
			//Debug.Log(other.transform.name);
			//GameObject Myparticle = Instantiate(particle,transform.position - transform.forward*2.0f,transform.rotation) as GameObject;
			DestroyObject(this.gameObject);
		}
	}
}
