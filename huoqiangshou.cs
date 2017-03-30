using UnityEngine;
using System.Collections;

public class huoqiangshou : MonoBehaviour
{
	public bool IsTaopao =false;
	private bool IsSetAngle = true;
	//public Transform MyPlayer;
	private Transform MyPlayerRecord;
	private float Angle = 0.0f;
	private float TotalAngle = 0.0f;
	private int myangle = 0;
	private RaycastHit hit;
	public bool IsZhuangche = false;
	private Transform PlayerTransFormRecord;
	private CameraShake camerashake;
	public Rigidbody body;
	public Animator  myanim;
	private float timmer = 0.0f;
	public GameObject particle;
	private LayerMask mask;
	public GameObject buwawa;
	public BoxCollider  box;
	private int ShotNum = 0;
	public int shotNumSet = 1;

	public AudioSource m_Audiopz;


	void Start () 
	{
		buwawa.SetActive(false);
		camerashake = Camera.main.GetComponent<CameraShake>();
		mask = 1<<( LayerMask.NameToLayer("shexianjiance"));
	}
	void Update () 
	{
		if(IsTaopao && !IsZhuangche)
		{
			if(IsSetAngle)
			{
				myangle = Random.Range(60,90);
				int Myselect = Random.Range(1,3);
				if(Myselect == 2)
				{
					Angle = -1.0f;
				}					
				else
				{
					Angle = 1.0f;
				}
				IsSetAngle = false;
			}
			if(Mathf.Abs(TotalAngle)<=myangle)
			{
				transform.localEulerAngles = new Vector3(0.0f,transform.localEulerAngles.y+Angle*Time.deltaTime*90.0f,0.0f);
				TotalAngle+=Angle*Time.deltaTime*90.0f;
			}
			transform.position +=transform.forward*10.0f*Time.deltaTime;
			if(Physics.Raycast(transform.position+Vector3.up*50.0f,-Vector3.up,out hit,100.0f,mask.value))
			{
				transform.position = hit.point;
			}
		}
		if(IsZhuangche)
		{
			timmer+= Time.deltaTime;
		}
		else
		{
			if(Physics.Raycast(transform.position+Vector3.up*50.0f,-Vector3.up,out hit,100.0f,mask.value))
			{
				transform.position = hit.point;
			}
		}
		if(timmer > 1.4f)
		{
			//GameObject temp = Instantiate(particle,body.transform.position,transform.rotation) as GameObject;
			if(particle.name == "arcaneExplosionBase")
			{
				UIController.m_Score+=5;
			}
			else if(particle.name == "arcaneExplosionBase60")
			{
				UIController.m_Score+=10;
			}
			else if(particle.name == "arcaneExplosionBase100")
			{
				UIController.m_Score+=20;
			}
			DestroyObject(gameObject);
		}
//		if(ShotNum >= shotNumSet)
//		{
//			GameObject temp = Instantiate(particle,buwawa.transform.position,transform.rotation) as GameObject;
//			if(particle.name == "arcaneExplosionBase")
//			{
//				UIController.m_Score+=30;
//			}
//			else if(particle.name == "arcaneExplosionBase60")
//			{
//				UIController.m_Score+=60;
//			}
//			else if(particle.name == "arcaneExplosionBase100")
//			{
//				UIController.m_Score+=100;
//			}
//			DestroyObject(gameObject);
//		}
	}
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "player")
		{
			PlayerController.m_IsShowDunPai = true;
			PlayerController.m_DunpaiTimmer = 0.0f;
			IsZhuangche = true;
			myanim.enabled = false;
			box.enabled = false;
			IsTaopao = false;
			PlayerTransFormRecord = other.transform;
			buwawa.gameObject.SetActive(true);
			camerashake.setCameraShakeImpulseValue();
			body.AddForce(PlayerTransFormRecord.transform.forward * 6000.0f,ForceMode.Acceleration);
			body.AddForce(Vector3.up * 2000.0f,ForceMode.Acceleration);
			m_Audiopz.Play();
		}
		else if(other.tag == "ziDan")
		{
			ShotNum++;
			UIController.m_QuiverNum++;
			if(ShotNum >= shotNumSet)
			{
				IsZhuangche = true;
				myanim.enabled = false;
				box.enabled = false;
				IsTaopao = false;
				PlayerTransFormRecord = other.transform;
				buwawa.gameObject.SetActive(true);
				body.AddForce(PlayerTransFormRecord.transform.forward * 2000.0f,ForceMode.Acceleration);
				body.AddForce(Vector3.up * 1000.0f,ForceMode.Acceleration);
				m_Audiopz.Play();
			}
		}
	}
}
