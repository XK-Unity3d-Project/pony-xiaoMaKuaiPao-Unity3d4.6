using UnityEngine;
using System.Collections;

public class RenMaController : MonoBehaviour 
{
	public Animator m_PersonAnim;
	public GameObject buwawa;
	public BoxCollider box;
	private Transform PlayerTransFormRecord;
	private CameraShake camerashake;
	public Rigidbody body;
	private bool IsZhuangche = false;
	private float timmer = 0.0f;

	private RaycastHit hit;
	private LayerMask mask;
	public GameObject particle;
	public AudioSource[] m_DiedAudio;
	private int ShotNum = 0;
	public int shotNumSet = 1;
	public SkinnedMeshRenderer m_MeshRender;

	void Start () 
	{
		camerashake = Camera.main.GetComponent<CameraShake>();
		mask = 1 << (LayerMask.NameToLayer ("shexianjiance"));
		buwawa.SetActive(false);
		BoxCollider ParentBox = transform.parent.GetComponent<BoxCollider>();
		if(ParentBox!=null)
		{
			ParentBox.enabled = false;
		}
	}

	void Update () 
	{
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
			m_MeshRender.enabled = false;
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
			DestroyObject(buwawa);
			IsZhuangche = false;
			timmer = 0.0f;
		}
	}
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "player")
		{
			PlayerController.m_IsShowDunPai = true;
			IsZhuangche = true;
			buwawa.transform.parent = null;
			m_PersonAnim.enabled = false;
			buwawa.SetActive(true);
			box.enabled = false;
			PlayerTransFormRecord = other.transform;
			body.isKinematic = false;
			camerashake.setCameraShakeImpulseValue();
			body.AddForce(PlayerTransFormRecord.forward*6000.0f, ForceMode.Acceleration);
			body.AddForce(Vector3.up*2000.0f, ForceMode.Acceleration);
			if(m_DiedAudio.Length>0)
			{
				int index = Random.Range(0,100)%m_DiedAudio.Length;
				m_DiedAudio[index].Play();
			}
		}
		if(other.tag == "ziDan")
		{
			ShotNum++;
			UIController.m_QuiverNum++;
			if(ShotNum >= shotNumSet)
			{
				IsZhuangche = true;
				buwawa.transform.parent = null;
				m_PersonAnim.enabled = false;
				buwawa.SetActive(true);
				box.enabled = false;
				PlayerTransFormRecord = other.transform;
				body.isKinematic = false;
				camerashake.setCameraShakeImpulseValue();
				body.AddForce(PlayerTransFormRecord.forward*6000.0f, ForceMode.Acceleration);
				body.AddForce(Vector3.up*2000.0f, ForceMode.Acceleration);
				if(m_DiedAudio.Length>0)
				{
					int index = Random.Range(0,100)%m_DiedAudio.Length;
					m_DiedAudio[index].Play();
				}
			}
		}
	}
}
