using UnityEngine;
using System.Collections;

public class madao : MonoBehaviour
{
	public Animator myAnimaController;
	private bool IsRoot = false;
	private float RootTimmer = 0.0f;
	private bool IsCreated = false;
	public Transform Fire1Positon = null;
	public Transform Fire2Positon = null;
	public GameObject dao;
	private AnimalController myController;
	void Start () 
	{
		myController = gameObject.GetComponent<AnimalController>();
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
				if(stateInfo.normalizedTime % 1.0f >= 0.50f && stateInfo.normalizedTime % 1.0f <= 0.55f && !IsCreated)
				{
					if(dao == null)
					{
						Debug.Log("Changmao == null");
					}
					GameObject temp = Instantiate(dao,/*transform.position,transform.rotation*/Fire1Positon.position,Fire1Positon.rotation) as GameObject;
					if(temp == null)
					{
						Debug.Log("temp == null");
					}
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
						myAnimaController.SetBool("Isroot",true);	
					}
					else
					{
						myAnimaController.SetBool("Isroot2",true);	
					}
					IsRoot = true;
				}
			}
			if(stateInfo.nameHash == Animator.StringToHash ("Base Layer.fire2"))
			{
				Vector3 angle = transform.localEulerAngles;
				transform.LookAt(Camera.main.transform.position - Vector3.up*5.0f);
				transform.localEulerAngles = new Vector3(angle.x,transform.localEulerAngles.y,angle.z);
				if(stateInfo.normalizedTime % 1.0f >= 0.72f && stateInfo.normalizedTime % 1.0f <= 0.75f && !IsCreated)
				{
					//GameObject temp = Instantiate(dao,Fire2Positon.position,Fire2Positon.rotation) as GameObject;
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
						myAnimaController.SetBool("Isroot",true);	
					}
					else
					{
						myAnimaController.SetBool("Isroot2",true);	
					}
					IsRoot = true;
				}
			}
			if(IsRoot && !myController.IsTaopao)
			{
				RootTimmer+=Time.deltaTime;
				if(RootTimmer > 2.5f)
				{
					IsRoot = false;
					RootTimmer = 0.0f;
					myAnimaController.SetBool("Isroot",false);
					myAnimaController.SetBool("Isroot2",false);
					int rand = Random.Range(0,2);
					if(rand == 0)
					{
						myAnimaController.SetBool("IsFire2",true);
					}
					else if(rand == 1)
					{
						myAnimaController.SetBool("IsFire",true);
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
