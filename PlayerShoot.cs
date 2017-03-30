using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour 
{
	public GameObject rocket;
	public GameObject cube;
	public Transform shootPointObj;
	private Vector3 mousePos;
	private Vector3 shootPointPos;
	public float speed;
	private GameObject bulletInstance;
	//public Transform mycamera;
	public float timmer = 1.0f;
	private float timmerReset = 0.0f;
	private  LayerMask mask;
	public AudioSource m_AudioShoot;

	public Animator m_ShootAnim;
	private float m_ShootTimmer = 0.0f;
	private bool m_IsReset = false;
	public Transform xiaonvhai;
	public UIController m_UIController;
	public bool m_IsFire = false;
	public static PlayerShoot Instance = null; 


	void Start () 
	{
		mask = 1<<( LayerMask.NameToLayer("shexianjiance"));
		Instance = this;
	}
	public static PlayerShoot GetInstance()
	{
		return Instance;
	}
	void Update ()
	{
		if(!pcvr.bIsHardWare && Input.GetButton("Fire1") && PlayerController.IsKaiqiang && !m_UIController.m_IsXubi && !m_UIController.IsGameOver)
		{
			m_IsFire = true;
			if(!m_ShootAnim.GetBool("Isshoot"))
			{
				m_ShootAnim.SetBool("Isshoot",true);
				m_ShootAnim.SetBool("Isroot",false);
			}
			m_ShootTimmer = 0.0f; 
			timmerReset += Time.deltaTime;
			Ray ray=Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;	
			if (Physics.Raycast(ray,out hit, 500.0f,mask.value))
			{
				//Debug.Log(hit.transform.name);
				Debug.DrawLine(shootPointObj.position,hit.point,Color.red,2);
				Debug.DrawLine(Camera.main.transform.position,hit.point,Color.blue,2);
			}
			if(timmerReset > timmer)
			{
				m_AudioShoot.Play();
				shootPointObj.LookAt(hit.point);
				timmerReset = 0.0f;
				bulletInstance = Instantiate(rocket, shootPointObj.position, shootPointObj.rotation) as GameObject;				
				UIController.m_ShootNum++;
				Rigidbody temp = bulletInstance.GetComponent<Rigidbody>();
				temp.velocity = (hit.point - shootPointObj.position).normalized * speed;
				Destroy(bulletInstance, 3.0f);
			}

			float x = xiaonvhai.localEulerAngles.x;
			float z = xiaonvhai.localEulerAngles.z;
			xiaonvhai.LookAt(hit.point);
			//Debug.Log(xiaonvhai.localEulerAngles.y);
			if(xiaonvhai.localEulerAngles.y >= 90.0f &&xiaonvhai.localEulerAngles.y <= 240.0f )
			{
				xiaonvhai.localEulerAngles = new Vector3(x,-120.0f,z);
			}
			else if((xiaonvhai.localEulerAngles.y >= 300.0f && xiaonvhai.localEulerAngles.y <= 360.0f)
			        ||(xiaonvhai.localEulerAngles.y >= 0.0f && xiaonvhai.localEulerAngles.y < 90.0f))
			{
				xiaonvhai.localEulerAngles = new Vector3(x,-60.0f,z);
			}
			else
			{
				xiaonvhai.localEulerAngles = new Vector3(x,xiaonvhai.localEulerAngles.y,z);
			}
			xiaonvhai.localEulerAngles = new Vector3(x,xiaonvhai.localEulerAngles.y,z);
		}
		if(!m_IsFire)
		{
			timmerReset = timmer;
			if(xiaonvhai.localEulerAngles.y != -90.0f)
			{
				xiaonvhai.localEulerAngles = new Vector3(xiaonvhai.localEulerAngles.x,-90.0f,xiaonvhai.localEulerAngles.z);
			}
			if(m_ShootAnim.GetBool("Isshoot"))
			{
				m_ShootAnim.SetBool("Isshoot",false);
			}
			if(!m_ShootAnim.GetBool("Isroot") && m_ShootTimmer<2.0f)
			{
				m_ShootAnim.SetBool("Isroot",true);
			}
			m_ShootTimmer+=Time.deltaTime;
			if(m_ShootTimmer>=5.0f && !m_IsReset)
			{
				m_IsReset = true;
				int n = Random.Range(0,4);
				if(n == 0)
				{
					m_ShootAnim.SetBool("Isroot1",true);
				}
				else if(n == 1)
				{
					m_ShootAnim.SetBool("Isroot2",true);
				}
				else
				{
					m_ShootAnim.SetBool("Isroot3",true);
				}
			}
			if(m_ShootTimmer>=5.92f)
			{
				m_IsReset = false;
				m_ShootTimmer = 0.0f;
				m_ShootAnim.SetBool("Isroot1",false);
				m_ShootAnim.SetBool("Isroot2",false);
				m_ShootAnim.SetBool("Isroot3",false);
			}
		}
	}

	public void OnClickFire()
	{
		if(PlayerController.IsKaiqiang && !m_UIController.m_IsXubi && !m_UIController.IsGameOver)
		{
			m_IsFire = true;
			if(!m_ShootAnim.GetBool("Isshoot"))
			{
				m_ShootAnim.SetBool("Isshoot",true);
				m_ShootAnim.SetBool("Isroot",false);
			}
			m_ShootTimmer = 0.0f; 
			timmerReset += Time.deltaTime;
			//Debug.Log("Input.mousePosition Input.mousePosition Input.mousePosition " +Input.mousePosition);
			//Debug.Log("PlayerController.m_ShootPoint PlayerController.m_ShootPoint " +PlayerController.m_ShootPoint);
			Ray ray=Camera.main.ScreenPointToRay(PlayerController.m_ShootPoint);
			RaycastHit hit;	
			if (Physics.Raycast(ray,out hit, 500.0f,mask.value))
			{
				//Debug.Log(hit.transform.name);
				Debug.DrawLine(shootPointObj.position,hit.point,Color.red,2);
				Debug.DrawLine(Camera.main.transform.position,hit.point,Color.blue,2);
			}
			if(timmerReset > timmer)
			{
				m_AudioShoot.Play();
				shootPointObj.LookAt(hit.point);
				timmerReset = 0.0f;
				bulletInstance = Instantiate(rocket, shootPointObj.position, shootPointObj.rotation) as GameObject;				
				UIController.m_ShootNum++;
				Rigidbody temp = bulletInstance.GetComponent<Rigidbody>();
				temp.velocity = (hit.point - shootPointObj.position).normalized * speed;
				Destroy(bulletInstance, 3.0f);
			}
			
			float x = xiaonvhai.localEulerAngles.x;
			float z = xiaonvhai.localEulerAngles.z;
			xiaonvhai.LookAt(hit.point);
			//Debug.Log(xiaonvhai.localEulerAngles.y);
			if(xiaonvhai.localEulerAngles.y >= 90.0f &&xiaonvhai.localEulerAngles.y <= 240.0f )
			{
				xiaonvhai.localEulerAngles = new Vector3(x,-120.0f,z);
			}
			else if((xiaonvhai.localEulerAngles.y >= 300.0f && xiaonvhai.localEulerAngles.y <= 360.0f)
			        ||(xiaonvhai.localEulerAngles.y >= 0.0f && xiaonvhai.localEulerAngles.y < 90.0f))
			{
				xiaonvhai.localEulerAngles = new Vector3(x,-60.0f,z);
			}
			else
			{
				xiaonvhai.localEulerAngles = new Vector3(x,xiaonvhai.localEulerAngles.y,z);
			}
			xiaonvhai.localEulerAngles = new Vector3(x,xiaonvhai.localEulerAngles.y,z);
		}
	}
}
