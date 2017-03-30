using UnityEngine;
using System.Collections;

public class maqiang : MonoBehaviour 
{
	public Animator myAnimaController;
	public GameObject myParticle;
	private bool IsCreate = false;
	private AnimalController myController;
	public AudioSource m_AudioShoot;
	void Start () 
	{
		myParticle.SetActive (false);
		myController = gameObject.GetComponent<AnimalController>();
	}
	void Update () 
	{
		if(!myController.IsZhuangche && !myController.IsTaopao && myAnimaController.enabled)
		{
			AnimatorStateInfo stateInfo = myAnimaController.GetCurrentAnimatorStateInfo(0);
			Vector3 lookAt = Camera.main.transform.position - Vector3.up * 5.0f;
			if(myAnimaController.GetBool("IsFire") && myAnimaController.GetBool("IsFire2"))
			{
				myAnimaController.SetBool("IsFire",true);	
				myAnimaController.SetBool("IsFire2",false);	
			}
			if (stateInfo.nameHash == Animator.StringToHash ("Base Layer.fire"))
			{
				float x = transform.localEulerAngles.x;
				float z = transform.localEulerAngles.z;
				transform.LookAt(lookAt);
				transform.localEulerAngles = new Vector3(x,transform.localEulerAngles.y,z);
				if(stateInfo.normalizedTime % 1.0f >= 0.7f && stateInfo.normalizedTime % 1.0f < 0.8f && !IsCreate)
				{
					IsCreate = true;
					myParticle.SetActive (true);
					m_AudioShoot.Play();
				}
				if(stateInfo.normalizedTime % 1.0f >= 0.95f)
				{
					IsCreate =false;
					myParticle.SetActive (false);
					myAnimaController.SetBool("IsFire",false);	
					myAnimaController.SetBool("IsFire2",true);	
				}
			}
			if(stateInfo.nameHash == Animator.StringToHash ("Base Layer.fire2"))
			{
				float x = transform.localEulerAngles.x;
				float z = transform.localEulerAngles.z;
				transform.LookAt(lookAt);
				transform.localEulerAngles = new Vector3(x,transform.localEulerAngles.y,z);
				if(stateInfo.normalizedTime % 1.0f >= 0.17f && stateInfo.normalizedTime % 1.0f < 0.19f && !IsCreate)
				{
					IsCreate = true;
					myParticle.SetActive (true);
					m_AudioShoot.Play();
				}
				if(stateInfo.normalizedTime % 1.0f >= 0.95f)
				{
					IsCreate = false;
					myParticle.SetActive (false);
					myAnimaController.SetBool("IsFire",true);	
					myAnimaController.SetBool("IsFire2",false);	
				}
			}
		}
	}
}
