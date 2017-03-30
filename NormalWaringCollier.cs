using UnityEngine;
using System.Collections;

public class NormalWaringCollier : MonoBehaviour
{
	private AnimalController[] MyAnimalController = null;
	private huoqiangshou[] MyHuoqiangshou = null;
	public bool IsRun = false;
	public Transform PathPoint = null;
	private float DelayTimmer = 0.0f;
	private float DelayTime = 0.2f;
	//private float Angle = 0.0f;
	//private float TotalAngle = 0.0f;
	//private float Yangle = 0.0f;
	private bool IsPengzhuang = false;
	private Animator[] myAnimator;
	private bool IsArrivaed = false;
	public float speed = 0.0f;
	public BoxCollider box;
	void Start ()
	{
		myAnimator = new Animator[transform.childCount];
		myAnimator = transform.GetComponentsInChildren<Animator>();
	}
	void LateUpdate () 
	{
		DelayTimmer+=Time.deltaTime;
		if(DelayTimmer>=DelayTime && !IsRun)
		{
			if(PathPoint != null)
			{
				IsRun = true;
				//Angle = Vector3.Angle(transform.forward,PathPoint.forward);
				/*if(Angle >= 0 )
				{
					Yangle = 1.0f;
				}
				else
				{
					Yangle = -1.0f;
				}*/
				//transform.localEulerAngles = new Vector3(transform.localEulerAngles.x,transform.localEulerAngles.y+Yangle*Time.deltaTime*60.0f,transform.localEulerAngles.z);
				transform.LookAt(PathPoint.position);
				transform.localEulerAngles = new Vector3(0.0f,transform.localEulerAngles.y,transform.localEulerAngles.z);
			}
		}
		if(IsRun && !IsPengzhuang && !IsArrivaed)
		{
			transform.position = Vector3.MoveTowards(transform.position,PathPoint.position,Time.deltaTime*speed);
			if(Vector3.Distance(transform.position,PathPoint.position) == 0.0f)
			{
//				Debug.Log("arrived arrived arrived arrived arrived");
				IsArrivaed = true;
				if(transform.tag == "yeren")
				{
					for(int i=0;i<myAnimator.Length;i++)
					{
						if(myAnimator[i])
						{
							myAnimator[i].SetBool("Isrun",false);
							myAnimator[i].SetBool("Isrun1",false);
							myAnimator[i].SetBool("Isrun2",false);
							myAnimator[i].SetBool("Isrun3",false);
							if(i == 0)
							{
								myAnimator[i].SetBool("IsFire",true);
							}
							else if(i==1)
							{
								myAnimator[i].SetBool("IsFire2",true);
							}
							else
							{
								int index = Random.Range(0,2);
								if(index == 0)
								{
									myAnimator[i].SetBool("IsFire",true);
								}
								else if(index == 1)
								{
									myAnimator[i].SetBool("IsFire2",true);
								}
								else
								{
									myAnimator[i].SetBool("IsFire",true);
								}
							}
							float temp = Random.Range(0.6f,1.1f);
							myAnimator[i].speed = temp;
						}
					}
				}
				else if(transform.tag == "animal")
				{
					for(int i=0;i<myAnimator.Length;i++)
					{
						if(myAnimator[i]!=null)
						{
							myAnimator[i].SetBool("Isrun",false);
							myAnimator[i].SetBool("Isrun1",false);
							myAnimator[i].SetBool("Isrun2",false);
							myAnimator[i].SetBool("Isrun3",false);
							if(i == 0)
							{
								myAnimator[i].SetBool("Isroot",true);
							}
							else if(i == 1)
							{
								myAnimator[i].SetBool("Isroot2",true);
							}
							else if(i == 2)
							{
								myAnimator[i].SetBool("Isroot3",true);
							}
							else
							{
								int index = Random.Range(0,3);
								if(index == 0)
								{
									myAnimator[i].SetBool("Isroot",true);
								}
								else if(index == 1)
								{
									myAnimator[i].SetBool("Isroot2",true);
								}
								else if(index == 2)
								{
									myAnimator[i].SetBool("Isroot3",true);
								}
								else
								{
									myAnimator[i].SetBool("Isroot",true);
								}
							}
							float temp = Random.Range(0.6f,1.2f);
							myAnimator[i].speed = temp;
						}
					}
				}
			}
		}	
	}
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "player")
		{
//			Debug.Log("taopao taopao taopao taopao");
			box.enabled = false;
			int length = 0;
			IsPengzhuang = true;
			if(transform.childCount>0)
			{
				MyAnimalController = transform.GetComponentsInChildren<AnimalController>();
				MyHuoqiangshou = transform.GetComponentsInChildren<huoqiangshou>();
				length = MyAnimalController.Length + MyHuoqiangshou.Length; 
				for(int i=0;i<length;i++)
				{
					if(myAnimator[i] !=null)
					{
						if(transform.tag == "yeren")
						{
							myAnimator[i].SetBool("IsFire",false);
							myAnimator[i].SetBool("IsFire2",false);
						}
						else
						{
							myAnimator[i].SetBool("Isroot",false);
							myAnimator[i].SetBool("Isroot2",false);
							myAnimator[i].SetBool("Isroot3",false);
						}
						if(i == 0)
						{
							myAnimator[i].SetBool("Isrun",true);
						}
						else if(i ==1)
						{
							myAnimator[i].SetBool("Isrun1",true);
						}
						else if(i ==2)
						{
							myAnimator[i].SetBool("Isrun2",true);
						}
						else if (i ==3 && transform.tag == "animal")
						{
							myAnimator[i].SetBool("Isrun3",true);
						}
						else
						{
							int k = Random.Range(0,4);
							if(k==0)
							{
								myAnimator[i].SetBool("Isrun",true);
							}
							else if(k==1)
							{
								myAnimator[i].SetBool("Isrun1",true);
							}
							else if(k==2)
							{
								myAnimator[i].SetBool("Isrun2",true);
							}
							else if(k==3 && transform.tag == "animal")
							{
								myAnimator[i].SetBool("Isrun3",true);
							}
							else
							{
								myAnimator[i].SetBool("Isrun",true);
							}
						}
						myAnimator[i].speed = 1.0f;
					}
				}

				for(int j=0;j<MyAnimalController.Length;j++)
				{
					MyAnimalController[j].IsTaopao = true;
				}
				for(int k=0;k<MyHuoqiangshou.Length;k++)
				{
					MyHuoqiangshou[k].IsTaopao = true;
				}
			}
		}
	}
}
