using UnityEngine;
using System.Collections;

public class AnimalRootSet : MonoBehaviour 
{
	public Animator myainim;
	public AnimalController myAnimalController;
	void Start ()
	{
	
	}
	void Update () 
	{
		if(!myAnimalController.IsTaopao && !myAnimalController.IsZhuangche && myainim.enabled)
		{
			AnimatorStateInfo stateInfo = myainim.GetCurrentAnimatorStateInfo(0);
			if (stateInfo.nameHash == Animator.StringToHash ("Base Layer.root"))
			{
				myainim.SetBool("Isroot",false);	
				if(stateInfo.normalizedTime % 1.0f >= 0.95f)
				{
					int num = Random.Range(0,2);
					if(num == 0 )
					{
						myainim.SetBool("Isroot2",true);	
					}
					else if(num == 1)
					{
						myainim.SetBool("Isroot3",true);	
					}
				}
			}
			if (stateInfo.nameHash == Animator.StringToHash ("Base Layer.root2"))
			{
				myainim.SetBool("Isroot2",false);	
				if(stateInfo.normalizedTime % 1.0f >= 0.95f)
				{
					int num = Random.Range(0,2);
					if(num == 0 )
					{
						myainim.SetBool("Isroot",true);	
					}
					else if(num == 1)
					{
						myainim.SetBool("Isroot3",true);	
					}
				}
			}
			if (stateInfo.nameHash == Animator.StringToHash ("Base Layer.root3"))
			{
				myainim.SetBool("Isroot3",false);	
				if(stateInfo.normalizedTime % 1.0f >= 0.95f)
				{
					int num = Random.Range(0,2);
					if(num == 0 )
					{
						myainim.SetBool("Isroot2",true);	
					}
					else if(num == 1)
					{
						myainim.SetBool("Isroot",true);	
					}
				}
			}
		}
	}
}
