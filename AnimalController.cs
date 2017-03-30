using UnityEngine;
using System.Collections;

public class AnimalController : MonoBehaviour 
{
	public bool IsTaopao =false;
	private bool IsSetAngle = true;
	private Transform MyPlayerRecord;
	private float Angle = 0.0f;
	//private bool IsZhengzhi = true;
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
	public GameObject buwawa;
	private LayerMask mask;
	public BoxCollider  box;
	private int ShotNum = 0;
	public int shotNumSet = 1;
	public AudioSource m_AudioPZ;
	void Start () 
	{
		camerashake = Camera.main.GetComponent<CameraShake>();
		mask = 1<<( LayerMask.NameToLayer("shexianjiance"));
		buwawa.SetActive(false);
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
			transform.position +=transform.forward*13.0f*Time.deltaTime;
			if(Physics.Raycast(transform.position+Vector3.up*50.0f,-Vector3.up,out hit,100.0f,mask.value))
			{
				Debug.DrawLine(transform.position+Vector3.up*50.0f,hit.point,Color.red,2);
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
				Debug.DrawLine(transform.position+Vector3.up*50.0f,hit.point,Color.red,2);
				transform.position = hit.point;
			}
		}
		if(timmer > 1.4f)
		{
			//GameObject temp = Instantiate(particle,buwawa.transform.position,transform.rotation) as GameObject;
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
	}
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "player")
		{
			if (pcvr.GetInstance())
			{
				pcvr.GetInstance().zhendong();
			}

			PlayerController.m_IsShowDunPai = true;
			PlayerController.m_DunpaiTimmer = 0.0f;
			IsZhuangche = true;
			IsTaopao = false;
			myanim.enabled = false;
			buwawa.SetActive(true);
			box.enabled = false;
			PlayerTransFormRecord = other.transform;
			body.isKinematic = false;
			camerashake.setCameraShakeImpulseValue();
			body.AddForce(PlayerTransFormRecord.forward*6000.0f, ForceMode.Acceleration);
			body.AddForce(Vector3.up*2000.0f, ForceMode.Acceleration);
			m_AudioPZ.Play();
		}
		if(other.tag == "ziDan")
		{
			ShotNum++;
			UIController.m_QuiverNum++;
			if(ShotNum >= shotNumSet)
			{
				IsZhuangche = true;
				IsTaopao = false;
				myanim.enabled = false;
				buwawa.SetActive(true);
				box.enabled = false;
				PlayerTransFormRecord = other.transform;
				body.isKinematic = false;
				body.AddForce(PlayerTransFormRecord.forward*2000.0f, ForceMode.Acceleration);
				body.AddForce(Vector3.up*1000.0f, ForceMode.Acceleration);
				m_AudioPZ.Play();
			}
		}
	}
}
