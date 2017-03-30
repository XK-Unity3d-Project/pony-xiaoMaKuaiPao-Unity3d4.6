using UnityEngine;
using System.Collections;

public class huoqiang : MonoBehaviour
{
	public Animator myAnimaController;
	private bool IsCreated = false;
	public Transform ChangmaoPositon = null;
	public GameObject zidan;
	private huoqiangshou myController;
	void Start () 
	{
		myController = transform.GetComponent<huoqiangshou> ();
	}
	void Update () 
	{
		if(!myController.IsZhuangche && !myController.IsTaopao && myAnimaController.enabled)
		{
			AnimatorStateInfo stateInfo = myAnimaController.GetCurrentAnimatorStateInfo(0);
			if (stateInfo.nameHash == Animator.StringToHash ("Base Layer.fire"))
			{
				Vector3 angle = transform.localEulerAngles;
				transform.LookAt(Camera.main.transform.position - Vector3.up*5.0f);
				transform.localEulerAngles = new Vector3(angle.x,transform.localEulerAngles.y,angle.z);
				if(stateInfo.normalizedTime % 1.0f >= 0.35f && stateInfo.normalizedTime % 1.0f < 0.42f && !IsCreated)
				{
					GameObject temp = Instantiate(zidan,ChangmaoPositon.position,ChangmaoPositon.rotation) as GameObject;
					DestroyObject(temp,3.0f);
					IsCreated = true;
				}
				if(stateInfo.normalizedTime % 1.0f >= 0.95f)
				{
					IsCreated = false;
					myAnimaController.SetBool("IsFire",false);	
					myAnimaController.SetBool("IsFire2",false);	
					int num = Random.Range(0,2);
					if(num == 0 )
					{
						myAnimaController.SetBool("IsFire2",true);	
					}
					else
					{
						myAnimaController.SetBool("IsFire",true);	
					}
				}
			}
			if(stateInfo.nameHash == Animator.StringToHash ("Base Layer.fire2"))
			{
				Vector3 angle = transform.localEulerAngles;
				transform.LookAt(Camera.main.transform.position - Vector3.up*5.0f);
				transform.localEulerAngles = new Vector3(angle.x,transform.localEulerAngles.y,angle.z);
				if(stateInfo.normalizedTime % 1.0f >= 0.35f && stateInfo.normalizedTime % 1.0f < 0.42f && !IsCreated)
				{
					GameObject temp = Instantiate(zidan,ChangmaoPositon.position,ChangmaoPositon.rotation) as GameObject;
					IsCreated = true;
					DestroyObject(temp,3.0f);
				}
				if(stateInfo.normalizedTime % 1.0f >= 0.95f)
				{
					IsCreated = false;
					myAnimaController.SetBool("IsFire",false);	
					myAnimaController.SetBool("IsFire2",false);	
					int num = Random.Range(0,2);
					if(num == 0 )
					{
						myAnimaController.SetBool("IsFire2",true);	
					}
					else
					{
						myAnimaController.SetBool("IsFire",true);	
					}
				}
			}
		}
	}
}
