using UnityEngine;
using System.Collections;
using System;

public class AddTimmerNpc : MonoBehaviour 
{
	public int  PathNumIndex;
	private Transform NextPoint;
	private float DelayTimmer = 0.0f;
	private RaycastHit hit;
	private Vector3 LookTarget;
	private int PathNumIndexRecord = -1;
	private float YangleRecord = 0.0f;
	private bool IsRotate = true;
	private float YangleTotal = 0.0f;
	private float LifeTimmer = 0.0f;
	public  float speed = 10.0f;
	private int num = 1;
	public Rigidbody[] lun;
	public Transform[] lunpos;
	private bool IsRotateLun = true;
	public Animator maAnimator;
	public Animation madao;
	public GameObject buwawa;
	public Rigidbody  person;
	private Vector3 ForwardRecord;
	private CameraShake camerashake;
	private  LayerMask mask;
	private int ShotNum = 0;
	public int shotNumSet = 1;
	private bool IsAdd = false;

	public AudioSource m_AudioPz;
	void Start () 
	{
		camerashake = Camera.main.GetComponent<CameraShake>();
		buwawa.SetActive(false);
		mask = 1<<( LayerMask.NameToLayer("shexianjiance"));
	}
	void Update () 
	{
		LifeTimmer+=Time.deltaTime;
		if(LifeTimmer>=30.0f)
		{
			speed = 25.0f;
		}
		if(Vector3.Distance(transform.position,PlayerController.myPlayer.position)>120.0f)
		{
			DestroyObject(this.gameObject);
			for(int i=0;i<lunpos.Length;i++)
			{
				DestroyObject(lunpos[i].gameObject);
			}
			if(buwawa!=null)
			{
				DestroyObject(buwawa);
			}
		}
		if(Vector3.Distance(transform.position,PlayerController.myPlayer.position)<15.0f && num ==1 && IsRotateLun)
		{
			num++;
			if(PlayerController.speed > 19.0f)
			{
				speed = 19.0f;
			}
			else
			{
				speed = PlayerController.speed;
			}
		}
		if(DelayTimmer <=0.2f)
		{
			DelayTimmer+=Time.deltaTime;
		}
		else 
		{
			if(IsRotateLun)
			{
				for(int i=0;i<lunpos.Length;i++)
				{
					lunpos[i].Rotate(new Vector3(0.0f,520.0f*Time.deltaTime,0.0f));
				}
			}
			if(PathNumIndex == PlayerController.PathPoint.Length)
			{
				PathNumIndex = 1;
			}
			NextPoint = PlayerController.PathPoint[PathNumIndex-1];
			if(PathNumIndex != PathNumIndexRecord)
			{
				PathNumIndexRecord = PathNumIndex;
				Vector3 Direction = NextPoint.position - transform.position;
				YangleRecord = Vector3.Angle(transform.forward,Direction);
				float dir = (Vector3.Dot (Vector3.up, Vector3.Cross (transform.forward, Direction)) < 0 ? -1 : 1);
				YangleRecord *= dir;
				IsRotate = true;
				//transform.localEulerAngles = new Vector3(transform.localEulerAngles.x,transform.localEulerAngles.y+YangleRecord,transform.localEulerAngles.z);
			}
			if(IsRotate)
			{
				if(YangleRecord > 0)
				{
					transform.Rotate(new Vector3(0.0f,100.0f*Time.deltaTime,0.0f));
					YangleTotal+=100.0f*Time.deltaTime;
				}
				else
				{
					transform.Rotate(new Vector3(0.0f,-100.0f*Time.deltaTime,0.0f));
					YangleTotal+=-100.0f*Time.deltaTime;
				}
				if(Mathf.Abs(YangleTotal)>=Mathf.Abs(YangleRecord))
				{
					IsRotate = false;
					YangleTotal = 0.0f;
				}
			}
			transform.position+=transform.forward*Time.deltaTime*speed;
			if(Physics.Raycast(transform.position+Vector3.up*50.0f,-Vector3.up,out hit,100.0f,mask.value))
			{
				transform.position = hit.point;
			}
		}
	}
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "npcpathpoint")
		{
			int num = Convert.ToInt32(other.gameObject.name);
			if(num == PlayerController.PathPoint.Length)
			{
				num = 0;
			}
			PathNumIndex = num+1;
		}
		if(other.tag == "player")
		{
			if(!IsAdd)
			{
				PlayerController.m_IsShowDunPai = true;
				PlayerController.m_DunpaiTimmer = 0.0f;
				IsAdd = true;
				PlayerController.IsAddTime = true;
				IsRotateLun = false;
				maAnimator.SetBool("Istaopao",true);
				for(int i=0;i<lunpos.Length;i++)
				{
					lunpos[i].parent = null;
				}
				speed = 25.0f;
				for(int i=0;i<lun.Length;i++)
				{
					lun[i].useGravity = true;
					lun[i].isKinematic = false;
					lun[i].AddForce(other.transform.forward*1000.0f);
				}
				madao.enabled = false;
				buwawa.SetActive(true);
				buwawa.transform.parent = null;
				ForwardRecord = other.transform.forward;
				person.AddForce(ForwardRecord * 5000.0f,ForceMode.Acceleration);
				person.AddForce(Vector3.up * 2000.0f,ForceMode.Acceleration);
				camerashake.setCameraShakeImpulseValue();
				m_AudioPz.Play();
			}
		}
		if(other.tag == "ziDan")
		{
			ShotNum++;
			UIController.m_QuiverNum++;
			if(ShotNum >= shotNumSet && !IsAdd)
			{
				IsAdd = true;
				PlayerController.IsAddTime = true;
				IsRotateLun = false;
				maAnimator.SetBool("Istaopao",true);
				for(int i=0;i<lunpos.Length;i++)
				{
					lunpos[i].parent = null;
				}
				speed = 25.0f;
				for(int i=0;i<lun.Length;i++)
				{
					lun[i].useGravity = true;
					lun[i].isKinematic = false;
					lun[i].AddForce(other.transform.forward*1000.0f);
				}
				madao.enabled = false;
				buwawa.SetActive(true);
				buwawa.transform.parent = null;
				ForwardRecord = other.transform.forward;
				person.AddForce(ForwardRecord * 5000.0f,ForceMode.Acceleration);
				person.AddForce(Vector3.up * 2000.0f,ForceMode.Acceleration);
				m_AudioPz.Play();
			}
		}
	}
}
