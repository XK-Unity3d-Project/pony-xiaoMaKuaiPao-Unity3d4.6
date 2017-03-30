using UnityEngine;
using System.Collections;

public class changmao : MonoBehaviour 
{
	public Animator myAnimaController;
	private bool IsRoot = false;
	private float RootTimmer = 0.0f;
	private bool IsCreated = false;
	public Transform ChangmaoPositon = null;
	public GameObject Changmao;
	private AnimalController myController;
	void Start () 
	{
		//ChangmaoPositon = transform.FindChild("Bip001").FindChild("Bip001 Pelvis").FindChild("Bip001 Spine").FindChild("Bip001 Spine1").FindChild("Bip001 Neck").FindChild("Bip001 R Clavicle").FindChild("Bip001 R UpperArm").FindChild("Bip001 R Forearm").FindChild("Bip001 R Hand").FindChild("Bip001 R Finger0").FindChild("Bip001 R Finger0Nub").FindChild("Cube");
		if(ChangmaoPositon ==null)
		{
			Debug.Log("ChangmaoPositon");
		}
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
					if(Changmao == null)
					{
						Debug.Log("Changmao == null");
					}
					GameObject temp = Instantiate(Changmao,/*transform.position,transform.rotation*/ChangmaoPositon.position,ChangmaoPositon.rotation) as GameObject;
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
					//GameObject temp = Instantiate(Changmao,ChangmaoPositon.position,ChangmaoPositon.rotation) as GameObject;
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
