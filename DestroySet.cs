using UnityEngine;
using System.Collections;

public class DestroySet : MonoBehaviour 
{
	public NpcController[] DestroyAnimal = null;
	void Start () 
	{
	
	}
	void Update () 
	{
	
	}
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "player")
		{
			if(DestroyAnimal!=null)
			{
				for(int i=0;i<DestroyAnimal.Length;i++)
				{
					DestroyObject(DestroyAnimal[i].myAniamlParent);
					DestroyAnimal[i].myAniamlParent = null;
					DestroyAnimal[i].enabled =false;
				}
			}
		}
	}
}
