using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour 
{
	/*void OnGUI()
	{
		GUI.Box (new Rect (0, 0, 100, 30), speed.ToString());
	}*/
	void OnGUI1()
	{
		if (pcvr.IsJiOuJiaoYanFailed) {
			//JiOuJiaoYanFailed
			string jiOuJiaoYanStr = "*********************************************************\n"
				+ "1489+1871624537416876467816684dtrsd3541sy3t6f654s68t4r8t416saf4bf164ve7t868\n"
					+ "1489+1871624537416876467816684dtrsd3541sy3t6f654s68t4r8t416saf4bf164ve7t868\n"
					+ "1489+1871624537416876467816684dtrsd3541sy3t6f654s68t4r8t416saf4bf164ve7t868\n"
					+ "1489+1871624537416876467816684dtrsd3541sy3t6f654s68t4r8t416saf4bf164ve7t868\n"
					+ "1489+1871624537416876467816684dtrsd3541sy3t6f654s68t4r8t416saf4bf164ve7t868\n"
					+ "1489+1871624537416876467816684dtrsd3541sy3t6f654s68t4r8t416saf4bf164ve7t868\n"
					+ "1489+1871624537416876467816684dtrsd3541sy3t6f654s68t4r8t416saf4bf164ve7t868\n"
					+ "1489+1871624537416876467816684dtrsd3541sy3t6f654s68t4r8t416saf4bf164ve7t868\n"
					+ "1489+1871624537416876467816684dtrsd3541sy3t6f654s68t4r8t416saf4bf164ve7t868\n"
					+ "1489+1871624537416876467816684dtrsd3541sy3t6f654s68t4r8t416saf4bf164ve7t868\n"
					+ "1489+1871624537416876467816684dtrsd3541sy3t6f654s68t4r8t416saf4bf164ve7t868\n"
					+ "1489+1871624537416876467816684dtrsd3541sy3t6f654s68t4r8t416saf4bf164ve7t868\n"
					+ "1489+1871624537416876467816684dtrsd3541sy3t6f654s68t4r8t416saf4bf164ve7t868\n"
					+ "1489+1871624537416876467816684dtrsd3541sy3t6f654s68t4r8t416saf4bf164ve7t868\n"
					+ "1489+1871624537416876467816684dtrsd3541sy3t6f654s68t4r8t416saf4bf164ve7t868\n"
					+ "1489+1871624537416876467816684dtrsd3541sy3t6f654s68t4r8t416saf4bf164ve7t868\n"
					+ "1489+1871624537416876467816684dtrsd3541sy3t6f654s68t4r8t416saf4bf164ve7t868\n"
					+ "1489+1871624537416876467816684dtrsd3541sy3t6f654s68t4r8t416saf4bf164ve7t868\n"
					+ "1489+1871624537416876467816684dtrsd3541sy3t6f654s68t4r8t416saf4bf164ve7t868\n"
					+ "1489+1871624537416876467816684dtrsd3541sy3t6f654s68dkfgt4saf4JOJYStr45dfssd\n"
					+ "*********************************************************";
			GUI.Box(new Rect(0f, 0f, Screen.width, Screen.height), jiOuJiaoYanStr);
		}
		else if (pcvr.IsJiaMiJiaoYanFailed) {
			
			string JMJYStr = "*********************************************************\n"
				+ "sdkgfksfgsdfggf64h76hg4j35dhghdga3f5sd34f3ds35135d4g5ds6g4sd6a4fg564dafg64f\n"
					+ "sdkgfksfgsdfggf64h76hg4j35dhghdga3f5sd34f3ds35135d4g5ds6g4sd6a4fg564dafg64f\n"
					+ "sdkgfksfgsdfggf64h76hg4j35dhghdga3f5sd34f3ds35135d4g5ds6g4sd6a4fg564dafg64f\n"
					+ "sdkgfksfgsdfggf64h76hg4j35dhghdga3f5sd34f3ds35135d4g5ds6g4sd6a4fg564dafg64f\n"
					+ "sdkgfksfgsdfggf64h76hg4j35dhghdga3f5sd34f3ds35135d4g5ds6g4sd6a4fg564dafg64f\n"
					+ "sdkgfksfgsdfggf64h76hg4j35dhghdga3f5sd34f3ds35135d4g5ds6g4sd6a4fg564dafg64f\n"
					+ "sdkgfksfgsdfggf64h76hg4j35dhghdga3f5sd34f3ds35135d4g5ds6g4sd6a4fg564dafg64f\n"
					+ "sdkgfksfgsdfggf64h76hg4j35dhghdga3f5sd34f3ds35135d4g5ds6g4sd6a4fg564dafg64f\n"
					+ "sdkgfksfgsdfggf64h76hg4j35dhghdga3f5sd34f3ds35135d4g5ds6g4sd6a4fg564dafg64f\n"
					+ "sdkgfksfgsdfggf64h76hg4j35dhghdga3f5sd34f3ds35135d4g5ds6g4sd6a4fg564dafg64f\n"
					+ "sdkgfksfgsdfggf64h76hg4j35dhghdga3f5sd34f3ds35135d4g5ds6g4sd6a4fg564dafg64f\n"
					+ "sdkgfksfgsdfggf64h76hg4j35dhghdga3f5sd34f3ds35135d4g5ds6g4sd6a4fg564dafg64f\n"
					+ "sdkgfksfgsdfggf64h76hg4j35dhghdga3f5sd34f3ds35135d4g5ds6g4sd6a4fg564dafg64f\n"
					+ "sdkgfksfgsdfggf64h76hg4j35dhghdga3f5sd34f3ds35135d4g5ds6g4sd6a4fg564dafg64f\n"
					+ "sdkgfksfgsdfggf64h76hg4j35dhghdga3f5sd34f3ds35135d4g5ds6g4sd6a4fg564dafg64f\n"
					+ "sdkgfksfgsdfggf64h76hg4j35dhghdga3f5sd34f3ds35135d4g5ds6g4sd6a4fg564dafg64f\n"
					+ "sdkgfksfgsdfggf64h76hg4j35dhghdga3f5sd34f3ds35135d4g5ds6g4sd6a4fg564dafg64f\n"
					+ "sdkgfksfgsdfggf64h76hg4j35dhghdga3f5sd34f3ds35135d4g5ds6g4sd6a4fg564dafg64f\n"
					+ "sdkgfksfgsdfggf64h76hg4j35dhghdga3f5sd34f3ds35135d4g5ds6g4sd6a4fg564dafg64f\n"
					+ "gh4j1489+1871624537416876467816684dtrsd3541sy3t6f654s68t4saf4JMJYStr45dfssd\n"
					+ "*********************************************************";
			GUI.Box(new Rect(0f, 0f, Screen.width, Screen.height), JMJYStr);
		}
	}

	public UIController myUI;
	public static float speed = 0.0f;
	public bool IsMove = false;
	private RaycastHit hit;
	private Vector3 LookTarget;
	public Transform CubeForCamera;
	public static bool IsAddTime = false;
	public float AddTime = 10.0f;
	public float Myangle = 40.0f;
	public TweenPosition Win;
	public Transform cube;
	public static Transform[] PathPoint;
	public Transform Path;
	public int PathNum = 0;
	private float OutTimmer = 0.0f;
	private float ErrorDirectionTimmer = 0.0f;
	private bool IsOut = false;
	public UITexture Tishi;
	public GameObject OutroadWarning;
	private bool IsEnterZhangai = false;
	public CameraShake myCameraShake;
	public CameraSmooth myCameraSmooth;

	public static Transform myPlayer;
	public Transform zhunxin;

	public static bool IsKaiqiang = false;
	private int PathNumIndexRecord = 0;
	private int NextPathNum = 0;
	private float YangleRecord = 0.0f;
	private bool IsRotate = false;
	private float YangleTotal = 0.0f;
	private Vector3 RecordPosition;
	public float Distance = 9950.0f;
	private int qushu = 1;
	private  LayerMask mask;
	private bool IsReset = false;
	public Transform testPositionR ;
	public Transform testPositionL ;
	public Transform xiaoma;

	public AudioSource m_AudioPZ;


	public static float m_DunpaiTimmer = 0.0f;
	public static bool m_IsShowDunPai = false;
	public GameObject[] m_pDunpai;

	public Transform m_Nvzhujue;
	public Transform m_Xiaonvhai;
	public GameObject m_pChentuParticle;
	public static PlayerController Instance;
	public static Vector3 m_ShootPoint;
	public static bool inGame = false;


	void Start () 
	{
		int volumNum = Convert.ToInt32(ReadGameInfo.GetInstance ().ReadVolumeNum ());
		AudioListener.volume = volumNum * 0.1f;

		pcvr.bZhendong = false;
		pcvr.dianboState = 0;
		MyCOMDevice.ComThreadClass.IsLoadingLevel = false;
		inGame = true;
		speed = 0.0f;
		Win.enabled = false;
		PathPoint = new Transform[Path.childCount];
		for(int i = 0;i<Path.childCount;i++)
		{
			string str = (i+1).ToString();
			PathPoint[i] = Path.FindChild(str);
		}
		transform.position = PathPoint[0].position;
		transform.eulerAngles = new Vector3(PathPoint[0].eulerAngles.x,PathPoint[0].eulerAngles.y + 180.0f,PathPoint[0].eulerAngles.z);
		Tishi.enabled = false;
		float yAngle = transform.eulerAngles.y;
		CubeForCamera.eulerAngles = new Vector3 (0.0f, yAngle, 0.0f);
		CubeForCamera.position =  transform.position /*+ CubeForCamera.forward*10.0f*/;
		myPlayer = transform;
		RecordPosition = transform.position;
		OutroadWarning.SetActive(false);
		mask = 1<<( LayerMask.NameToLayer("shexianjiance"));
		if(Physics.Raycast(transform.position+Vector3.up*50.0f,-Vector3.up,out hit,100.0f,mask.value))
		{
			transform.position = hit.point;
		}
		//float yrotate = transform.eulerAngles.y;			 
		CubeForCamera.localEulerAngles = new Vector3(0.0f,transform.localEulerAngles.y,0.0f);
		CubeForCamera.position = transform.position;
		xiaoma.localPosition = new Vector3(0.0f,0.0f,2.32f);
		IsKaiqiang = false;
		m_pChentuParticle.SetActive (false);
		Instance = this;
	}
	public static PlayerController GetInstance()
	{
		return Instance;
	}
	void Update () 
	{
		CheckPlayerIsIntoZhangAi();
		myPlayer = transform;
		m_Xiaonvhai.localPosition = new Vector3 (0.0f,0.0f,0.4990992f);
		m_Xiaonvhai.localEulerAngles = new Vector3(0.0f,0.0f,0.0f);
		m_Nvzhujue.localPosition = new Vector3 (0.02401735f,1.031035f,-0.2965085f);
		m_Nvzhujue.localEulerAngles = new Vector3(0.0f,-90.0f,0.0f);
		float DistanceTemp = Vector3.Distance(transform.position,RecordPosition);
		if(DistanceTemp > 0)
		{
			Distance+=DistanceTemp;
			RecordPosition= transform.position;
		}
		if(IsKaiqiang && !pcvr.bIsHardWare)
		{
			zhunxin.gameObject.SetActive(true);
			Vector3 ScreenPoint = new Vector3(Input.mousePosition.x - Screen.width/2.0f,Input.mousePosition.y-Screen.height/2.0f,0.0f);
			zhunxin.localPosition = ScreenPoint;
		}
		/*else
		{
			zhunxin.gameObject.SetActive(false);
		}*/
		if(myCameraSmooth.enabled == false)
		{
			myCameraSmooth.enabled = true;
		}
		if(m_IsShowDunPai)
		{
			m_DunpaiTimmer+=Time.deltaTime;
			m_pDunpai[0].SetActive(true);
			m_pDunpai[1].SetActive(true);
		}
		if(m_DunpaiTimmer>=2.0f)
		{
			m_IsShowDunPai = false;
			m_DunpaiTimmer = 0.0f;
			m_pDunpai[0].SetActive(false);
			m_pDunpai[1].SetActive(false);
		}
		if(speed>=10.0f)
		{
			m_pChentuParticle.SetActive(true);
		}
		else
		{
			m_pChentuParticle.SetActive(false);
		}
		IsResetPlayer();
		if (!pcvr.bIsAutoMoveSelf)
		{
			MoveByPlayer();
		}
		else
		{
			MoveBySelf ();
		}
	}

	/*void LateUpdate()
	{

	}
	void FixedUpdate()
	{
		//Debug.Log(PathNum);
	}*/
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "zhangai")
		{
			if (pcvr.GetInstance())
			{
				pcvr.GetInstance().zhendong();
			}

			myCameraSmooth.enabled = false;
			myCameraShake.enabled = true;
			if(speed >= 8.0f)
			{
				myCameraShake.setCameraShakeImpulseValue();
			}
			speed = 0.0f;
			IsEnterZhangai = true;
			m_AudioPZ.Play();
		}
		 if(other.tag == "zhaLan")
		{
			if (pcvr.GetInstance())
			{
				pcvr.GetInstance().zhendong();
			}

			myCameraSmooth.enabled = false;
			myCameraShake.enabled = true;
			if(speed >= 8.0f)
			{
				myCameraShake.setCameraShakeImpulseValue();
			}
			speed = 0.0f;
			IsEnterZhangai = true;
			m_AudioPZ.Play();
		}
		 if(other.tag == "win")
		{
			Win.enabled = true;
		}
		 if(other.tag == "outroad")
		{
			IsOut = false;
			OutroadWarning.SetActive(false);
			OutTimmer = 0.0f;
		}
		 if(other.tag == "pathpoint")
		{
//			PathNum = Convert.ToInt32(other.gameObject.name)-1;
//			Debug.Log("PathNum" + PathNum);
			PathNum  = Convert.ToInt32(other.gameObject.name)-1;
//			if(PathNum < num )
//			{
//				PathNum = num;
//			}
//			Debug.Log("pathnum "  + PathNum);
			if(PathNum == PathPoint.Length-1)
			{
				qushu++;
			}
			CreatPointInfo temp = other.GetComponent<CreatPointInfo>();
			if(temp)
			{
				for(int i=0;i<temp.WarnPoint.Length;i++)
				{
					NpcController p  = temp.WarnPoint[i].GetComponent<NpcController>();
					p.enabled = true;
					if(qushu>1)
					{
						//Debug.Log("qushu>1");
						p.IsEnterTrigger = true;
					}
				}
			}
		}
		 if(other.tag == "creatpoint")
		{
			CreatPointInfo temp = other.GetComponent<CreatPointInfo>();
			if(temp)
			{
				for(int i=0;i<temp.WarnPoint.Length;i++)
				{
					NpcController p  = temp.WarnPoint[i].GetComponent<NpcController>();
					p.enabled = true;
					if(qushu>1)
					{
						p.IsEnterTrigger = true;
					}
				}
			}
		}
	}
	void OnTriggerExit(Collider other)
	{
		if(other.tag == "zhangai")
		{
			IsEnterZhangai = false;
			speed = 3.0f;
		}
		if(other.tag == "zhaLan")
		{
			IsEnterZhangai = false;
			speed = 3.0f;
		}
		if(other.tag == "outroad")
		{
			IsOut = true;
			OutroadWarning.SetActive(true);
		}
	}
	void IsResetPlayer()
	{
		Vector3 Direction;
		if(PathNum == PathPoint.Length - 1)
		{
			Direction = PathPoint[0].position - PathPoint[PathNum].position;
		}
		else
		{
			Direction = PathPoint[PathNum+1].position - PathPoint[PathNum].position;
		}
		float angle = Vector3.Angle(Direction,transform.forward);
		if(IsOut)
		{
			OutTimmer+=Time.deltaTime;
		}
		if(Mathf.Abs(angle)>=90.0f)
		{
			if(!OutroadWarning.activeInHierarchy)
			{
				Tishi.enabled = true;
			}
			else
			{
				Tishi.enabled = false;
			}
			ErrorDirectionTimmer += Time.deltaTime;
		}
		if(Mathf.Abs(angle)<90.0f)
		{
			ErrorDirectionTimmer=0.0f;
			Tishi.enabled = false;
		}
		if(OutTimmer >=3.0f || ErrorDirectionTimmer>=3.0f)
		{
			ResetPlayer ();
		}
	}
	void ResetPlayer()
	{
		transform.position = PathPoint[PathNum].position;
		if(Physics.Raycast(transform.position+Vector3.up*50.0f,-Vector3.up,out hit,100.0f))
		{
			transform.position = hit.point;
		}
		transform.eulerAngles = new Vector3(PathPoint[PathNum].eulerAngles.x,PathPoint[PathNum].eulerAngles.y + 180.0f,PathPoint[PathNum].eulerAngles.z);
		OutTimmer = 0.0f;
		ErrorDirectionTimmer = 0.0f;
		IsReset = true;
	}
	void MoveByPlayer()
	{
		if(myUI.CountTime <= 0.0f && !myUI.IsGameOver && !myUI.m_IsXubi)
		{
			if (pcvr.bIsHardWare)
			{
				if(pcvr.GetInstance().getJiasu())
				{
					OnClickJiasuBt();
				}
				else if(!IsEnterZhangai)
				{
					OnJiasuUp();
				}
				if(pcvr.GetInstance().getTurnLeft())
				{
					transform.Rotate(new Vector3(0.0f,-Myangle*Time.deltaTime,0.0f));				
				}
				if(pcvr.GetInstance().getTurnRight())
				{
					transform.Rotate(new Vector3(0.0f,Myangle*Time.deltaTime,0.0f));
				}
			}
			else
			{
				if(Input.GetKey(KeyCode.W))
				{
					OnClickJiasuBt();
				}
				else if(!IsEnterZhangai)
				{
					OnJiasuUp();
				}
				if(Input.GetKey(KeyCode.A))
				{
					transform.Rotate(new Vector3(0.0f,-Myangle*Time.deltaTime,0.0f));				
				}
				if(Input.GetKey(KeyCode.D))
				{
					transform.Rotate(new Vector3(0.0f,Myangle*Time.deltaTime,0.0f));
				}
				if(Input.GetKey(KeyCode.X))
				{
					OnClickShacheBt();
				}
			}

			transform.position += transform.forward * speed *Time.deltaTime;
			if(Physics.Raycast(transform.position+Vector3.up*50.0f,-Vector3.up,out hit,100.0f,mask.value))
			{
				transform.position = hit.point;
			}
			//float yrotate = transform.eulerAngles.y;			 
			CubeForCamera.localEulerAngles = new Vector3(0.0f,transform.localEulerAngles.y,0.0f);
			CubeForCamera.position = transform.position;
			xiaoma.localPosition = new Vector3(0.0f,0.0f,2.32f);
			if(!IsReset)
				transform.LookAt(cube.transform.position);
			else
			{
				IsReset = false;
			}
			if(!IsReset)
			{
				Vector3 testR = Vector3.zero;
				Vector3 testL = Vector3.zero;
				if(Physics.Raycast(testPositionR.position,-Vector3.up,out hit,100.0f,mask.value))
				{
					testR = hit.point;
				}
				if(Physics.Raycast(testPositionL.position,-Vector3.up,out hit,100.0f,mask.value))
				{
					testL = hit.point;
				}
				Vector3 testDirection = Vector3.Normalize (testR - testL);
				float angle = Vector3.Angle (testDirection,transform.right); //cefan

				if(testR.y > testL.y)
				{
					if (angle > 1.5f)
					{
						pcvr.qinangStateRight = 1;
					}
					else if (pcvr.qinangStateRight != 11)
					{
						pcvr.qinangStateRight = 2;
					}

					transform.localEulerAngles = new Vector3 ( transform.localEulerAngles.x, transform.localEulerAngles.y, angle);
				}
				else if(testR.y < testL.y)
				{
					if (angle > 1.5f)
					{
						pcvr.qinangStateLeft = 1;
					}
					else if (pcvr.qinangStateLeft != 11)
					{
						pcvr.qinangStateLeft = 2;
					}

					transform.localEulerAngles = new Vector3 ( transform.localEulerAngles.x, transform.localEulerAngles.y,-angle);
				}
				else if(testR.y == testL.y)
				{
					if (pcvr.qinangStateRight != 11 && pcvr.qinangStateLeft != 11)
					{
						pcvr.qinangStateRight = 2;
						pcvr.qinangStateLeft = 2;
					}
					transform.localEulerAngles = new Vector3 ( transform.localEulerAngles.x, transform.localEulerAngles.y,-angle);
				}
			}
			else
			{
				IsReset = false;
			}
		}
	}
	void MoveBySelf()
	{
		if(myUI.CountTime <= 0.0f && !myUI.IsGameOver)
		{
			if(PathNum < PathPoint.Length-1)
			{
				NextPathNum = PathNum + 1;
			}
			else
			{
				NextPathNum = 0;
			}
			Transform NextPoint = PathPoint[NextPathNum];
			if(NextPathNum != PathNumIndexRecord)
			{
				PathNumIndexRecord = NextPathNum;
				Vector3 Direction = NextPoint.position - transform.position;
				YangleRecord = Vector3.Angle(transform.forward,Direction);
				float dir = (Vector3.Dot (Vector3.up, Vector3.Cross (transform.forward, Direction)) < 0 ? -1 : 1);
				YangleRecord *= dir;
				IsRotate = true;
			}
			if(IsRotate)
			{
				if(YangleRecord > 0)
				{
					transform.Rotate(new Vector3(0.0f,80.0f*Time.deltaTime,0.0f));
					YangleTotal+=80.0f*Time.deltaTime;
				}
				else
				{
					transform.Rotate(new Vector3(0.0f,-80.0f*Time.deltaTime,0.0f));
					YangleTotal+=-80.0f*Time.deltaTime;
				}
				if(Mathf.Abs(YangleTotal)>=Mathf.Abs(YangleRecord))
				{
					IsRotate = false;
					YangleTotal = 0.0f;
				}
			}
			speed = 10.0f;
			transform.position+=transform.forward*Time.deltaTime*15.0f;
			/*if(Physics.Raycast(transform.position+Vector3.up*50.0f,-Vector3.up,out hit,100.0f))
			{
				transform.position = hit.point;
			}*/
			float yAngle = transform.eulerAngles.y;
			CubeForCamera.eulerAngles = new Vector3 (0.0f, yAngle, 0.0f);
			CubeForCamera.position =  transform.position /*+ CubeForCamera.forward*10.0f*/;
			myPlayer = transform;
		}
	}
	public void OnClickLeftBt()
	{
		if(myUI.CountTime <= 0.0f && !myUI.IsGameOver && !myUI.m_IsXubi && !pcvr.m_IsShache)
		{
			transform.Rotate(new Vector3(0.0f,-Myangle*Time.deltaTime,0.0f));
		}
		ZhujuemaController.GetInstance().OnClickLeftBt();
	}
	public void OnClickRightBt()
	{
		if(myUI.CountTime <= 0.0f && !myUI.IsGameOver && !myUI.m_IsXubi && !pcvr.m_IsShache)
		{
			transform.Rotate(new Vector3(0.0f,Myangle*Time.deltaTime,0.0f));
		}
		ZhujuemaController.GetInstance().OnClickRightBt();
	}
	public void OnClickShacheBt()
	{
		ZhujuemaController.GetInstance().OnClickShacheBt();
		if(myUI.CountTime <= 0.0f && !myUI.IsGameOver && !myUI.m_IsXubi)
		{
			if(speed>0.0f)
			{
				speed-=3.0f*Time.deltaTime;
			}
			else
			{
				speed = 0.0f;
			}
		}
	}
	void OnClickJiasuBt()
	{
		if(myUI.CountTime <= 0.0f && !myUI.IsGameOver && !myUI.m_IsXubi)
		{
			if(!IsEnterZhangai)
			{
				IsMove = true;
				if(speed<21.7f)
					speed+=2.7f*Time.deltaTime;
				else
					speed = 21.7f;
			}
		}
	}
	void OnJiasuUp()
	{
		if(!IsEnterZhangai)
		{
			IsMove = false;
			if(speed>0.0f)
			{
				speed-=1.67f*Time.deltaTime;
			}
			else
			{
				speed = 0.0f;
			}
		}
	}
	public void OnMoveShootCursor(float x,float y)
	{
		if(IsKaiqiang)
		{
			zhunxin.gameObject.SetActive(true);
			zhunxin.localPosition = new Vector3((x-0.5f)*Screen.width, (y-0.5f)*Screen.height, 0.0f);
			m_ShootPoint.x = x * Screen.width;
			m_ShootPoint.y = y * Screen.height;
			m_ShootPoint.z = 0.0f;
		}
		//Vector3 ScreenPoint = new Vector3(Input.mousePosition.x - Screen.width/2.0f,Input.mousePosition.y-Screen.height/2.0f,0.0f);
	}

	void CheckPlayerIsIntoZhangAi()
	{
		if (!IsEnterZhangai) {
			return;
		}

		if (Time.frameCount % 25 != 0) {
			return;
		}

		Transform pathPoint = Path.GetChild(PathNum);
		Vector3 vecA = pathPoint.right;
		Vector3 vecB = myPlayer.position - pathPoint.position;
		vecA.y = vecB.y = 0f;
		float speedRot = 5f;
		float speedMv = 5f * Time.deltaTime;
		if (Vector3.Dot(vecA, vecB) > 0) {
			//Debug.Log("make player turn right!");
			myPlayer.Rotate(new Vector3(0f, speedRot, 0f));
			myPlayer.position += (myPlayer.right * speedMv);
		}
		
		if (Vector3.Dot(vecA, vecB) < 0) {
			//Debug.Log("make player turn left!");
			myPlayer.Rotate(new Vector3(0f, -speedRot, 0f));
			myPlayer.position -= (myPlayer.right * speedMv);
		}
	}
}