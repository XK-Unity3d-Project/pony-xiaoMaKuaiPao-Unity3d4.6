using UnityEngine;
using System.Collections;

public class ZhujuemaController : MonoBehaviour 
{
	public Animator myAnim;
	public bool IsShache = false;
	public static bool m_IsBrake = false;
	public Animator NvzhujiaoAnim;
	public static ZhujuemaController Instance = null;
	void Start () 
	{
		Instance = this;
	}
	public static ZhujuemaController GetInstance()
	{
		return Instance;
	}
	void Update () 
	{
		AnimatorStateInfo stateInfo = myAnim.GetCurrentAnimatorStateInfo(0);
		if (stateInfo.nameHash == Animator.StringToHash ("Base Layer.brake"))
		{
			if(stateInfo.normalizedTime % 1.0f >= 0.99f)
			{
				if(PlayerController.speed  == 0.0f) //root
				{
					myAnim.SetBool("Isroot",true);
					myAnim.SetBool("Isrootleft",false);
					myAnim.SetBool("Isrootright",false);
					myAnim.SetBool("Isrun",false);
					myAnim.SetBool("Isrunleft",false);
					myAnim.SetBool("Isrunright",false);
					myAnim.SetBool("Isrun1",false);
					myAnim.SetBool("Isrun1left",false);
					myAnim.SetBool("Isrun1right",false);
					myAnim.SetBool("Isbrake",false);

					NvzhujiaoAnim.SetBool("Isroot",true);
					NvzhujiaoAnim.SetBool("Isrun",false);
					NvzhujiaoAnim.SetBool("Isrun2",false);
					NvzhujiaoAnim.SetBool("Isleft",false);
					NvzhujiaoAnim.SetBool("Isright",false);
					NvzhujiaoAnim.SetBool("Isbrake",false);
				}
				else if(PlayerController.speed >0.0f && PlayerController.speed <=10.0f)//run
				{
					myAnim.SetBool("Isroot",false);
					myAnim.SetBool("Isrootleft",false);
					myAnim.SetBool("Isrootright",false);
					myAnim.SetBool("Isrun",true);
					myAnim.SetBool("Isrunleft",false);
					myAnim.SetBool("Isrunright",false);
					myAnim.SetBool("Isrun1",false);
					myAnim.SetBool("Isrun1left",false);
					myAnim.SetBool("Isrun1right",false);
					myAnim.SetBool("Isbrake",false);

					NvzhujiaoAnim.SetBool("Isroot",false);
					NvzhujiaoAnim.SetBool("Isrun",true);
					NvzhujiaoAnim.SetBool("Isrun2",false);
					NvzhujiaoAnim.SetBool("Isleft",false);
					NvzhujiaoAnim.SetBool("Isright",false);
					NvzhujiaoAnim.SetBool("Isbrake",false);
				}
				else
				{
					myAnim.SetBool("Isroot",false);
					myAnim.SetBool("Isrootleft",false);
					myAnim.SetBool("Isrootright",false);
					myAnim.SetBool("Isrun",false);
					myAnim.SetBool("Isrunleft",false);
					myAnim.SetBool("Isrunright",false);
					myAnim.SetBool("Isrun1",true);
					myAnim.SetBool("Isrun1left",false);
					myAnim.SetBool("Isrun1right",false);
					myAnim.SetBool("Isbrake",false);
					
					NvzhujiaoAnim.SetBool("Isroot",false);
					NvzhujiaoAnim.SetBool("Isrun",false);
					NvzhujiaoAnim.SetBool("Isrun2",true);
					NvzhujiaoAnim.SetBool("Isleft",false);
					NvzhujiaoAnim.SetBool("Isright",false);
					NvzhujiaoAnim.SetBool("Isbrake",false);
				}
			}
		}
		else if(!m_IsBrake)
		{
			if(PlayerController.speed >10.0f)// run1
			{
				myAnim.SetBool("Isroot",false);
				myAnim.SetBool("Isrootleft",false);
				myAnim.SetBool("Isrootright",false);
				myAnim.SetBool("Isrun",false);
				myAnim.SetBool("Isrunleft",false);
				myAnim.SetBool("Isrunright",false);
				myAnim.SetBool("Isrun1",true);
				myAnim.SetBool("Isrun1left",false);
				myAnim.SetBool("Isrun1right",false);
				myAnim.SetBool("Isbrake",false);

				NvzhujiaoAnim.SetBool("Isroot",false);
				NvzhujiaoAnim.SetBool("Isrun",false);
				NvzhujiaoAnim.SetBool("Isrun2",true);
				NvzhujiaoAnim.SetBool("Isleft",false);
				NvzhujiaoAnim.SetBool("Isright",false);
				NvzhujiaoAnim.SetBool("Isbrake",false);
			}
			else if(PlayerController.speed >0.0f && PlayerController.speed <=10.0f)//run
			{
				myAnim.SetBool("Isroot",false);
				myAnim.SetBool("Isrootleft",false);
				myAnim.SetBool("Isrootright",false);
				myAnim.SetBool("Isrun",true);
				myAnim.SetBool("Isrunleft",false);
				myAnim.SetBool("Isrunright",false);
				myAnim.SetBool("Isrun1",false);
				myAnim.SetBool("Isrun1left",false);
				myAnim.SetBool("Isrun1right",false);
				myAnim.SetBool("Isbrake",false);

				NvzhujiaoAnim.SetBool("Isroot",false);
				NvzhujiaoAnim.SetBool("Isrun",true);
				NvzhujiaoAnim.SetBool("Isrun2",false);
				NvzhujiaoAnim.SetBool("Isleft",false);
				NvzhujiaoAnim.SetBool("Isright",false);
				NvzhujiaoAnim.SetBool("Isbrake",false);
			}
			else //root
			{
				myAnim.SetBool("Isroot",true);
				myAnim.SetBool("Isrootleft",false);
				myAnim.SetBool("Isrootright",false);
				myAnim.SetBool("Isrun",false);
				myAnim.SetBool("Isrunleft",false);
				myAnim.SetBool("Isrunright",false);
				myAnim.SetBool("Isrun1",false);
				myAnim.SetBool("Isrun1left",false);
				myAnim.SetBool("Isrun1right",false);
				myAnim.SetBool("Isbrake",false);

				NvzhujiaoAnim.SetBool("Isroot",true);
				NvzhujiaoAnim.SetBool("Isrun",false);
				NvzhujiaoAnim.SetBool("Isrun2",false);
				NvzhujiaoAnim.SetBool("Isleft",false);
				NvzhujiaoAnim.SetBool("Isright",false);
				NvzhujiaoAnim.SetBool("Isbrake",false);
			}
			if(Input.GetKey(KeyCode.A)) //左转
			{
				OnClickLeftBt();
			}
			if(Input.GetKey(KeyCode.D)) //右转
			{
				OnClickRightBt();
			}
			if(Input.GetKey(KeyCode.X) /*&& !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A)*/ && !m_IsBrake)//刹车
			{
				OnClickShacheBt();
			}
		}
	}
	public void OnClickShacheBt()
	{
		NvzhujiaoAnim.SetBool("Isroot",false);
		NvzhujiaoAnim.SetBool("Isrun",false);
		NvzhujiaoAnim.SetBool("Isrun2",false);
		NvzhujiaoAnim.SetBool("Isleft",false);
		NvzhujiaoAnim.SetBool("Isright",false);
		NvzhujiaoAnim.SetBool("Isbrake",true);
		
		myAnim.SetBool("Isroot",false);
		myAnim.SetBool("Isrootleft",false);
		myAnim.SetBool("Isrootright",false);
		myAnim.SetBool("Isrun",false);
		myAnim.SetBool("Isrunleft",false);
		myAnim.SetBool("Isrunright",false);
		myAnim.SetBool("Isrun1",false);
		myAnim.SetBool("Isrun1left",false);
		myAnim.SetBool("Isrun1right",false);
		myAnim.SetBool("Isbrake",true);
		//IsShache = true;
		m_IsBrake = true;
	}
	public void OnClickRightBt()
	{
		NvzhujiaoAnim.SetBool("Isroot",false);
		NvzhujiaoAnim.SetBool("Isrun",false);
		NvzhujiaoAnim.SetBool("Isrun2",false);
		NvzhujiaoAnim.SetBool("Isleft",false);
		NvzhujiaoAnim.SetBool("Isright",true);
		NvzhujiaoAnim.SetBool("Isbrake",false);
		
		if(PlayerController.speed >10.0f) // run1
		{
			myAnim.SetBool("Isroot",false);
			myAnim.SetBool("Isrootleft",false);
			myAnim.SetBool("Isrootright",false);
			myAnim.SetBool("Isrun",false);
			myAnim.SetBool("Isrunleft",false);
			myAnim.SetBool("Isrunright",false);
			myAnim.SetBool("Isrun1",false);
			myAnim.SetBool("Isrun1left",false);
			myAnim.SetBool("Isrun1right",true);
			myAnim.SetBool("Isbrake",false);
		}
		else if(PlayerController.speed >0.0f && PlayerController.speed <=10.0f)//run
		{
			myAnim.SetBool("Isroot",false);
			myAnim.SetBool("Isrootleft",false);
			myAnim.SetBool("Isrootright",false);
			myAnim.SetBool("Isrun",false);
			myAnim.SetBool("Isrunleft",false);
			myAnim.SetBool("Isrunright",true);
			myAnim.SetBool("Isrun1",false);
			myAnim.SetBool("Isrun1left",false);
			myAnim.SetBool("Isrun1right",false);
			myAnim.SetBool("Isbrake",false);
		}
		else//root
		{
			myAnim.SetBool("Isroot",false);
			myAnim.SetBool("Isrootleft",false);
			myAnim.SetBool("Isrootright",true);
			myAnim.SetBool("Isrun",false);
			myAnim.SetBool("Isrunleft",false);
			myAnim.SetBool("Isrunright",false);
			myAnim.SetBool("Isrun1",false);
			myAnim.SetBool("Isrun1left",false);
			myAnim.SetBool("Isrun1right",false);
			myAnim.SetBool("Isbrake",false);
		}
	}
	public void OnClickLeftBt()
	{
		AnimatorStateInfo stateInfo = myAnim.GetCurrentAnimatorStateInfo(0);
		if (stateInfo.nameHash != Animator.StringToHash ("Base Layer.brake") && !m_IsBrake)
		{
			NvzhujiaoAnim.SetBool("Isroot",false);
			NvzhujiaoAnim.SetBool("Isrun",false);
			NvzhujiaoAnim.SetBool("Isrun2",false);
			NvzhujiaoAnim.SetBool("Isleft",true);
			NvzhujiaoAnim.SetBool("Isright",false);
			NvzhujiaoAnim.SetBool("Isbrake",false);
			
			if(PlayerController.speed >10.0f) // run1
			{
				myAnim.SetBool("Isroot",false);
				myAnim.SetBool("Isrootleft",false);
				myAnim.SetBool("Isrootright",false);
				myAnim.SetBool("Isrun",false);
				myAnim.SetBool("Isrunleft",false);
				myAnim.SetBool("Isrunright",false);
				myAnim.SetBool("Isrun1",false);
				myAnim.SetBool("Isrun1left",true);
				myAnim.SetBool("Isrun1right",false);
				myAnim.SetBool("Isbrake",false);
			}
			else if(PlayerController.speed >0.0f && PlayerController.speed <=10.0f)//run
			{
				myAnim.SetBool("Isroot",false);
				myAnim.SetBool("Isrootleft",false);
				myAnim.SetBool("Isrootright",false);
				myAnim.SetBool("Isrun",false);
				myAnim.SetBool("Isrunleft",true);
				myAnim.SetBool("Isrunright",false);
				myAnim.SetBool("Isrun1",false);
				myAnim.SetBool("Isrun1left",false);
				myAnim.SetBool("Isrun1right",false);
				myAnim.SetBool("Isbrake",false);
			}
			else //root
			{
				myAnim.SetBool("Isroot",false);
				myAnim.SetBool("Isrootleft",true);
				myAnim.SetBool("Isrootright",false);
				myAnim.SetBool("Isrun",false);
				myAnim.SetBool("Isrunleft",false);
				myAnim.SetBool("Isrunright",false);
				myAnim.SetBool("Isrun1",false);
				myAnim.SetBool("Isrun1left",false);
				myAnim.SetBool("Isrun1right",false);
				myAnim.SetBool("Isbrake",false);
			}
		}
	}
}
