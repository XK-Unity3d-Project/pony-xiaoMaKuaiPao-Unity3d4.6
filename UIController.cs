using UnityEngine;
using System.Collections;
using System;


public class UIController : MonoBehaviour 
{
	public GameObject uiBack = null;
	private bool hadInit = false;
	public UISprite BeginDaojishi;
	public UISprite MiaoBaiwei;
	public UISprite Miaoshiwei;
	public UISprite Miaogewei;
	public UISprite Haomiaoshiwei;
	public UISprite Haomiaogewei;
	public UISprite MiaoBiaozhi;
	public UIAtlas myWarningAtlas;
	public UIAtlas myNormalAtlas;
	public UISprite DistanceWangwei;
	public UISprite DistanceQianwei;
	public UISprite Distancebaiwei;
	public UISprite Distanceshiwei;
	public UISprite Distancegewei;

	public UISprite ScoreQianwei;
	public UISprite Scorebaiwei;
	public UISprite Scoreshiwei;
	public UISprite Scoregewei;

	public UISprite Jindutiao;
	public Transform TouxiangPosition;
	private float GameTime;
	private float ActiveGameTime = 0.0f;
	private int TimeMiaoBaiwei = 0;
	private int TimeMiaoshiwei = 0;
	private int TimeMiaogewei = 0;
	private int TimeHaomiaoshiwei = 0;
	private int TimeHaomiaogewei = 0;
	private float BeginTotalTime = 5.20f;
	public float CountTime = 0.0f;
	private string TimetoSting;
	public AddTime addTimeCompents;
	public TweenPosition JiashiMove;
	public PlayerController myPlayer;
	public Transform AddTimeObject;
	public UISprite  AddTimeshiwei;
	public UISprite  AddTimegewei;
	public TweenPosition GameOver;
	public TweenPosition winner;
	public GameObject gameOverObj = null;
	public GameObject winnerObj = null;
	//public GameObject Win;
	public bool IsGameOver = false;
	private float DistanceForEnd = 0.0f;
	public Transform Distance;

	//wenzijishi
	public GameObject BigdaoJishi;
	public UISprite BigMiaoshiwei;
	public UISprite BigMiaogewei;
	public UISprite BigHaomiaoshiwei;
	public UISprite BigHaomiaogewei;

	public Transform[] m_BofangWenzi;
	private Vector3[] m_WenziPosRecord;
	private bool[] m_BofangMove;
	private float[] m_MoveTimmer;
	private float m_Fu1Timmer = 0.0f;
	private float m_Fu2Timmer = 0.0f;
	//private float m_Fu2Num = 1;
	private float m_DistancRecord = 0.0f;
	public float speed = 2000.0f;
	private float m_DistanceRecordForThree = 0.0f;

	private bool[] m_HasSelect;
	private float   m_QufenTimmer = 0.0f;

	public static int m_ShootNum = 0;
	public static int m_QuiverNum = 0;
	private float m_ShootTimmer = 0.0f;
	public static int m_Score = 0;

	public AudioSource m_Audio;
	public AudioSource m_AudioDaojishi;
	private bool m_IsBofang = false;
	public AudioSource m_AudioBeijing;

	private string CoinNumSet = "";
	public UISprite CoinNumSetTex;
	public UISprite m_InsertNumS;
	public UISprite m_InsertNumG;

	public AudioSource m_AudioWin;
	public AudioSource m_AudioLose;
	private bool m_AudioIsPlay = false;
	public AudioSource m_AudioToubi;
	public AudioSource m_AudioXiaonvhai;
	public UITexture m_Xiaonvhai;
	public Texture m_XiaonvhaiTex;

	public UITexture m_XiaonvhaiToubi;
	public UITexture m_XiaonuhaiKaishi;
	public AudioSource m_XiaoMaBengpaoNormal;
	public AudioSource m_XiaomaBengpaoQuick;
	public AudioSource m_XiaomaShache;
	private bool m_IsNormalPlay = false;
	private bool m_IsQuickPlay = false;
	private int biaoji = 0;

	public GameObject m_pMianfeiobject;
	public GameObject m_pToubiobject;
	public GameObject m_pShootObject;
	public GameObject m_pZhunxing;
	private string GameMode = "";
	private string GameSorD = "";

	public GameObject m_XubiDaojishi;
	public TweenScale m_XubiScale;
	public UISprite m_XubiSprite;
	private float m_TimmerForXubi = 0.0f;
	public GameObject m_BeiginTishi;
	public GameObject m_ToubiTishi;
	public UITexture m_Warn;

	private int GameState = 1;
	public AudioSource m_pXubiAudio;
	public AudioSource m_pBeginAudio;
	public AudioSource m_pWarnAudio;
	public bool m_IsXubi = false;
	//private float m_speedRecord = 0.0f;
	private int m_XubiCishu = 0;
	private int m_JindutiaoCishu = 0;
	private int m_JindutiaoIndex = 0;

	public static UIController Instance;
	public GameObject m_pDOpenObj;
	public GameObject m_pDCloseObj;
	
	//an quan dai
	public GameObject m_pP1AnquandaiObj = null;
	public GameObject m_pP2AnquandaiObj = null;

	void aaaa()
	{
		hadInit = true;
		uiBack.SetActive(false);
	}

	void Start () 
	{Debug.Log ("timessssssssssss   " + Time.time);
		if (uiBack)
		{
			hadInit = false;
			uiBack.SetActive(true);
			Invoke ("aaaa", 1.15f);
		}
		else
		{
			hadInit = true;
		}

		OpenDonggan();

		UpdateInsertCoin();

		m_pP1AnquandaiObj.SetActive (false);
		m_pP2AnquandaiObj.SetActive (false);
		m_pZhunxing.SetActive(false);
		GameOver.enabled = false;
		winner.enabled = false;
		gameOverObj.SetActive (false);
		winnerObj.SetActive (false);

		GameMode = ReadGameInfo.GetInstance ().ReadGameStarMode();
		GameSorD = ReadGameInfo.GetInstance().ReadStarSingleorDouble();
		if(GameMode == "oper")
		{
			m_pMianfeiobject.SetActive(false);
			m_pToubiobject.SetActive(true);
			CoinNumSet = ReadGameInfo.GetInstance ().ReadStarCoinNumSet();			
			CoinNumSetTex.spriteName = CoinNumSet;
		}
		else
		{
			m_pMianfeiobject.SetActive(true);
			m_pToubiobject.SetActive(false);
		}
		if(GameSorD == "single")
		{
			m_pShootObject.SetActive(false);
			m_pZhunxing.SetActive(false);
		}
		else if(GameSorD == "double")
		{
			m_pShootObject.SetActive(true);
			//m_pZhunxing.SetActive(true);
		}
		int GameTimeRecord = Convert.ToInt32(ReadGameInfo.GetInstance().ReadGameTimeSet());
		GameTime = GameTimeRecord;
		TimeUpdate (GameTime);
		BeginTotalTime = BeginTotalTime + Time.time;
		GameTime = GameTime + Time.time;
		GameTime = GameTime + 4.0f;
		AddTimeObject.gameObject.SetActive(false);
		addTimeCompents.Fangda.enabled = false;
		addTimeCompents.suoxiao.enabled = false;
		JiashiMove.enabled = false;
		GameOver.enabled = false;
		Jindutiao.fillAmount = 0.0f;
		TouxiangPosition.localPosition = new Vector3(-1.52f,-0.47f,0.0f); 
		Distance.localPosition = new Vector3(-140.0f,331.322f,0.0f);
		BigdaoJishi.SetActive(false);
		m_DistancRecord = myPlayer.Distance;
		m_BofangMove = new bool[5];
		m_MoveTimmer = new float[5];
		m_HasSelect = new bool[8];
		for(int i=0;i<m_BofangMove.Length;i++)
		{
			m_BofangMove[i] = false;
			m_MoveTimmer[i] = 0.0f;
		}
		for(int i=0;i<8;i++)
		{
			m_HasSelect[i] = false;
		}
		m_WenziPosRecord = new Vector3[m_BofangWenzi.Length];
		for(int i=0;i<m_WenziPosRecord.Length;i++)
		{
			m_WenziPosRecord[i] = m_BofangWenzi[i].localPosition;
		}

		m_QufenTimmer+=Time.deltaTime;
		CountTime = BeginTotalTime - Time.time;
		TimetoSting = ((int)CountTime).ToString ();
		if(CountTime > 0)
		{
			if(CountTime > 1)
			{
				BeginDaojishi.spriteName = TimetoSting;
			}
			else
			{
				BeginDaojishi.spriteName = "go";
			}
		}
		m_Audio.Play ();
		m_AudioBeijing.PlayDelayed (3.2f);
		DistanceForEnd = ((int) (GameTimeRecord / 60)) * 1300.0f;
		m_XiaonuhaiKaishi.enabled = false;
		m_XiaonvhaiToubi.enabled = true;
		m_Score = 0;
		m_XubiDaojishi.SetActive (false);
		m_Warn.enabled = false;
		m_DistanceRecordForThree = myPlayer.Distance;
		Instance = this;
	}

	public static UIController GetInstance()
	{
		return Instance;
	}

	void Update () 
	{
		if (!hadInit)
		{
			return;
		}

		if(!pcvr.bIsHardWare)
		{
			if(Input.GetKeyDown(KeyCode.T))
			{
				pcvr.CoinCurGame ++;
				OnClickInsertBt();
			}
			if(GameState == 1
			   && Input.GetKeyDown(KeyCode.O)
			   && GameSorD == "double"
			   && !PlayerController.IsKaiqiang
			   && (GameMode=="free" || pcvr.CoinCurGame >= Convert.ToInt32(CoinNumSet)))
			{
				OnClickBeginBt2P();
			}
			if(Input.GetKeyDown(KeyCode.F5))
			{
				Application.LoadLevel(3 + pcvr.chenNum);
			}
		}
		if(GameSorD == "double" && !PlayerController.IsKaiqiang)
		{
			XiaoNvhaiSelect ();
		}
		m_QufenTimmer+=Time.deltaTime;
		CountTime = BeginTotalTime - Time.time;
		TimetoSting = ((int)CountTime).ToString ();

		if(CountTime > 0)
		{
			if(CountTime > 1)
			{
				BeginDaojishi.spriteName = TimetoSting;
			}
			else
			{
				BeginDaojishi.spriteName = "go";
			}
		}
		else
		{
			BeginDaojishi.enabled = false;
			ActiveGameTime = GameTime - Time.time + 1.2f;
			if(ActiveGameTime > 10.0f)
			{
				m_IsBofang = false;
				BigdaoJishi.SetActive(false);
				m_AudioDaojishi.Stop();
			}
			if(ActiveGameTime >= 0.0f)
			{
				TimeUpdate(ActiveGameTime);
			}
			else
			{
				XubiSelect();
			}
		}
		if(PlayerController.IsAddTime)
		{
			AddTimeObject.gameObject.SetActive(true);
			ResetAddTime(myPlayer.AddTime);
			EventDelegate.Add(JiashiMove.onFinished,OnMoveFinished);
			addTimeCompents.Fangda.enabled = true;
			addTimeCompents.suoxiao.enabled = true;
			JiashiMove.enabled = true;
			addTimeCompents.Fangda.ResetToBeginning();
			addTimeCompents.suoxiao.ResetToBeginning();
			JiashiMove.ResetToBeginning();
			addTimeCompents.Fangda.PlayForward();
			addTimeCompents.suoxiao.PlayForward();
			JiashiMove.PlayForward();
			PlayerController.IsAddTime = false;
		}
		ResetDistance();
		ResetJindutiao();
		ResetScore();
		WenZiSet();
		XiaomaBengpaoAudio ();
	}
	void TimeUpdate(float currenttime)
	{
		TimeMiaoBaiwei = (int)(currenttime / 100);
		TimeMiaoshiwei = (int)((currenttime - TimeMiaoBaiwei*100)/10);
		TimeMiaogewei = (int)(currenttime - TimeMiaoBaiwei*100 - TimeMiaoshiwei*10);
		float temp = (currenttime - (int)currenttime)*100;
		TimeHaomiaoshiwei = (int)(temp/10);
		TimeHaomiaogewei = (int)(temp - TimeHaomiaoshiwei*10);
		if(currenttime < 10.0f)
		{
			MiaoBaiwei.atlas = myWarningAtlas;
			Miaoshiwei.atlas = myWarningAtlas;
			Miaogewei.atlas = myWarningAtlas;
			MiaoBiaozhi.atlas = myWarningAtlas;
			Haomiaoshiwei.atlas = myWarningAtlas;
			Haomiaogewei.atlas = myWarningAtlas;
		}
		else
		{
			MiaoBaiwei.atlas = myNormalAtlas;
			Miaoshiwei.atlas = myNormalAtlas;
			Miaogewei.atlas = myNormalAtlas;
			MiaoBiaozhi.atlas = myNormalAtlas;
			Haomiaoshiwei.atlas = myNormalAtlas;
			Haomiaogewei.atlas = myNormalAtlas;
		}
		MiaoBaiwei.spriteName = TimeMiaoBaiwei.ToString();
		Miaoshiwei.spriteName = TimeMiaoshiwei.ToString();
		Miaogewei.spriteName = TimeMiaogewei.ToString();
		Haomiaoshiwei.spriteName = TimeHaomiaoshiwei.ToString();
		Haomiaogewei.spriteName = TimeHaomiaogewei.ToString();
		if(currenttime <= 10.0f)
		{
			if(!m_IsBofang)
			{
				m_IsBofang = true;
				m_AudioDaojishi.Play();	
			}

			BigdaoJishi.SetActive(true);
			BigMiaoshiwei.spriteName = Miaoshiwei.spriteName;
			BigMiaogewei.spriteName = Miaogewei.spriteName;
			BigHaomiaoshiwei.spriteName = Haomiaoshiwei.spriteName;
			BigHaomiaogewei.spriteName = Haomiaogewei.spriteName;
		}
	}
	void OnMoveFinished()
	{
		if(!IsGameOver && !m_IsXubi)
		{
			GameTime += myPlayer.AddTime;
		}
		addTimeCompents.Fangda.enabled = false;
		addTimeCompents.suoxiao.enabled = false;
		JiashiMove.enabled = false;
		EventDelegate.Remove(JiashiMove.onFinished,OnMoveFinished);
		AddTimeObject.gameObject.SetActive (false);
	}
	void ResetAddTime(float AddTime)
	{
		string TempShiwei = ((int)(AddTime / 10)).ToString ();
		string TempGewei = ((int)(AddTime - (int)(AddTime / 10) * 10)).ToString ();
		AddTimeshiwei.spriteName = TempShiwei;
		AddTimegewei.spriteName = TempGewei;
	}
	void ResetDistance()
	{
		int n = 1;
		int num = (int)myPlayer.Distance;
		int temp = num;
		while(num > 9)
		{
			num /= 10;
			n++;
		}
		if(n==5)
		{
			int wangwei = (int)(temp / 10000);
			Debug.Log("wangwei wangwei wangwei" + wangwei);
			int qianwei = (int)((temp-wangwei*10000) / 1000);
			int baiwei = (int)((temp -wangwei*10000 - qianwei * 1000)/100);
			int shiwei = (int)((temp -wangwei*10000  - qianwei*1000 - baiwei*100)/10);
			int gewei = (int)(temp-wangwei*10000  - qianwei*1000 - baiwei*100-shiwei*10);
			DistanceWangwei.enabled = true;
			DistanceWangwei.spriteName = wangwei.ToString();
			DistanceQianwei.spriteName = qianwei.ToString();
			Distancebaiwei.spriteName = baiwei.ToString();
			Distanceshiwei.spriteName = shiwei.ToString();
			Distancegewei.spriteName = gewei.ToString();
			Distance.localPosition = new Vector3(-10.0f,331.322f,0.0f);
		}
		if(n==4)
		{
			int qianwei = (int)(temp / 1000);
			int baiwei = (int)((temp - qianwei * 1000)/100);
			int shiwei = (int)((temp - qianwei*1000 - baiwei*100)/10);
			int gewei = (int)(temp - qianwei*1000 - baiwei*100-shiwei*10);
			DistanceWangwei.enabled = false;
			DistanceQianwei.enabled =true;
			DistanceQianwei.spriteName = qianwei.ToString();
			Distancebaiwei.spriteName = baiwei.ToString();
			Distanceshiwei.spriteName = shiwei.ToString();
			Distancegewei.spriteName = gewei.ToString();
			Distance.localPosition = new Vector3(-29.0f,331.322f,0.0f);
		}
		else if(n==3)
		{
			int baiwei = (int)(temp/100);
			int shiwei = (int)((temp  - baiwei*100)/10);
			int gewei = (int)(temp  - baiwei*100-shiwei*10);
			DistanceWangwei.enabled = false;
			DistanceQianwei.enabled =false;
			Distancebaiwei.enabled =true;
			Distancebaiwei.spriteName = baiwei.ToString();
			Distanceshiwei.spriteName = shiwei.ToString();
			Distancegewei.spriteName = gewei.ToString();
			Distance.localPosition = new Vector3(-71.0f,331.322f,0.0f);
		}
		else if(n==2)
		{
			int shiwei = (int)(temp/10);
			int gewei = (int)(temp-shiwei*10);
			DistanceWangwei.enabled = false;
			DistanceQianwei.enabled =false;
			Distancebaiwei.enabled =false;
			Distanceshiwei.enabled = true;
			Distanceshiwei.spriteName = shiwei.ToString();
			Distancegewei.spriteName = gewei.ToString();
			Distance.localPosition = new Vector3(-102.0f,331.322f,0.0f);
		}
		else if(n == 1)
		{
			DistanceWangwei.enabled = false;
			DistanceQianwei.enabled =false;
			Distancebaiwei.enabled =false;
			Distanceshiwei.enabled = false;
			Distancegewei.spriteName = temp.ToString();
			Distance.localPosition = new Vector3(-140.0f,331.322f,0.0f);
		}
	}

	void ResetScore()
	{
		int n = 1;
		if(m_Score>=9999)
		{
			m_Score = 9999;
		}
		int num = m_Score;
		int temp = num;
		while(num > 9)
		{
			num /= 10;
			n++;
		}
		if(n==4)
		{
			int qianwei = (int)(temp / 1000);
			int baiwei = (int)((temp - qianwei * 1000)/100);
			int shiwei = (int)((temp - qianwei*1000 - baiwei*100)/10);
			int gewei = (int)(temp - qianwei*1000 - baiwei*100-shiwei*10);
//			ScoreQianwei.enabled =true;
			ScoreQianwei.spriteName = qianwei.ToString();
			Scorebaiwei.spriteName = baiwei.ToString();
			Scoreshiwei.spriteName = shiwei.ToString();
			Scoregewei.spriteName = gewei.ToString();
			//Distance.localPosition = new Vector3(-29.0f,331.322f,0.0f);
		}
		else if(n==3)
		{
			int baiwei = (int)(temp/100);
			int shiwei = (int)((temp  - baiwei*100)/10);
			int gewei = (int)(temp  - baiwei*100-shiwei*10);
//			ScoreQianwei.enabled =false;
//			Scorebaiwei.enabled =true;
			ScoreQianwei.spriteName = "0";
			Scorebaiwei.spriteName = baiwei.ToString();
			Scoreshiwei.spriteName = shiwei.ToString();
			Scoregewei.spriteName = gewei.ToString();
			//Distance.localPosition = new Vector3(-71.0f,331.322f,0.0f);
		}
		else if(n==2)
		{
			int shiwei = (int)(temp/10);
			int gewei = (int)(temp-shiwei*10);
//			ScoreQianwei.enabled =false;
//			Scorebaiwei.enabled =false;
//			Scoreshiwei.enabled = true;
			ScoreQianwei.spriteName = "0";
			Scorebaiwei.spriteName = "0";
			Scoreshiwei.spriteName = shiwei.ToString();
			Scoregewei.spriteName = gewei.ToString();
			//Distance.localPosition = new Vector3(-102.0f,331.322f,0.0f);
		}
		else if(n == 1)
		{
//			ScoreQianwei.enabled =false;
//			Scorebaiwei.enabled =false;
//			Scoreshiwei.enabled = false;

			ScoreQianwei.spriteName = "0";
			Scorebaiwei.spriteName = "0";
			Scoreshiwei.spriteName = "0";
			Scoregewei.spriteName = temp.ToString();
			//Distance.localPosition = new Vector3(-140.0f,331.322f,0.0f);
		}
	}

	void ResetJindutiao()
	{
		float percent = (int)(myPlayer.Distance)/DistanceForEnd - m_JindutiaoCishu;
		if(percent <= 1.0f)
		{
			Jindutiao.fillAmount = percent;
			float yPos = 1.07f*percent - 0.47f;
			TouxiangPosition.localPosition = new Vector3(-1.52f,yPos,0.0f);
		}
		else
		{
			m_JindutiaoCishu++;
			m_JindutiaoIndex++;
			if(m_JindutiaoIndex >=4)
			{
				m_JindutiaoIndex = 0;
			}
			if(m_JindutiaoIndex == 0)
			{
				Jindutiao.spriteName = "zhaose";
			}
			else if(m_JindutiaoIndex == 1)
			{
				Jindutiao.spriteName = "lvse";
			}
			else if(m_JindutiaoIndex == 2)
			{
				Jindutiao.spriteName = "lanse";
			}
			else if(m_JindutiaoIndex == 3)
			{
				Jindutiao.spriteName = "huangse";
			}
		}
	}
	void WenZiSet()
	{
		m_Fu1Timmer += Time.deltaTime;
		if(m_QufenTimmer>3.5f)
		{
			m_Fu2Timmer+=Time.deltaTime;
		}
		if(m_QufenTimmer>7.0f)
		{
			m_ShootTimmer+=Time.deltaTime;
		}
		if(m_Fu1Timmer > 10.0f)
		{
			if((myPlayer.Distance - m_DistancRecord)/10.0f < 10.0f)
			{
				m_BofangMove[0] = true;
			}
			else
			{
				m_BofangMove[2] = true;
			}
			m_DistancRecord = myPlayer.Distance;
			m_Fu1Timmer = 0.0f;
		}
		if(m_Fu2Timmer >10.0f)
		{
//			if(m_Fu2Num*500.0f > myPlayer.Distance)
//			{
//				m_BofangMove[1] = true;
//			}
//			m_Fu2Timmer = 0.0f;
//			m_Fu2Num++;
			if(myPlayer.Distance - m_DistanceRecordForThree <100.0f)
			{
				m_BofangMove[1] = true;
			}
			m_DistanceRecordForThree = myPlayer.Distance;
			m_Fu2Timmer = 0.0f;
		}
		if(m_ShootTimmer>10.0f)
		{
			if(m_ShootNum > 0) //有开枪记录
			{
				if(m_QuiverNum/m_ShootNum >0.70f)
				{
					m_BofangMove[3] = true;
				}
				else if(m_QuiverNum/m_ShootNum <0.30f)
				{
					m_BofangMove[4] = true;
				}
			}
			m_ShootTimmer = 0.0f;
			m_QuiverNum = 0;
			m_ShootNum = 0;
		}

		if(m_BofangMove[0]) //平均速度小于10
		{
			m_MoveTimmer[0] += Time.deltaTime;
			if(!m_HasSelect[0] && !m_HasSelect[1])
			{
				int num = UnityEngine.Random.Range(1,1000);
				if(num%2 == 0)
				{
					m_HasSelect[0]  = true;
				}
				else
				{
					m_HasSelect[1]  = true;
				}
			}
			if(m_HasSelect[0])
			{
				if(m_MoveTimmer[0] < 2.5f)
				{
					m_BofangWenzi[0].localPosition = Vector3.MoveTowards(m_BofangWenzi[0].localPosition,new Vector3(0.0f,m_BofangWenzi[0].localPosition.y,0.0f),speed*Time.deltaTime);
				}
				else
				{
					m_BofangWenzi[0].localPosition = Vector3.MoveTowards(m_BofangWenzi[0].localPosition,new Vector3(1125.37f,m_BofangWenzi[0].localPosition.y,0.0f),speed*Time.deltaTime);
				}
				if(Vector3.Distance(m_BofangWenzi[0].localPosition,new Vector3(1125.37f,m_BofangWenzi[0].localPosition.y,0.0f)) < 1.0f)
				{
					m_MoveTimmer[0] = 0.0f;
					m_BofangWenzi[0].localPosition = m_WenziPosRecord[0];
					m_HasSelect[0] =false;
					m_HasSelect[1]  = true;
					m_BofangMove[0] = false;
				}
			}
			else if(m_HasSelect[1])
			{
				if(m_MoveTimmer[0] < 2.5f)
				{
					m_BofangWenzi[1].localPosition = Vector3.MoveTowards(m_BofangWenzi[1].localPosition,new Vector3(0.0f,m_BofangWenzi[1].localPosition.y,0.0f),speed*Time.deltaTime);
				}
				else
				{
					m_BofangWenzi[1].localPosition = Vector3.MoveTowards(m_BofangWenzi[1].localPosition,new Vector3(1125.37f,m_BofangWenzi[1].localPosition.y,0.0f),speed*Time.deltaTime);
				}
				if(Vector3.Distance(m_BofangWenzi[1].localPosition,new Vector3(1125.37f,m_BofangWenzi[0].localPosition.y,0.0f)) < 1.0f)
				{
					m_MoveTimmer[0] = 0.0f;
					m_BofangWenzi[1].localPosition = m_WenziPosRecord[1];
					m_HasSelect[0] =true;
					m_HasSelect[1]  = false;
					m_BofangMove[0] = false;
				}
			}
		}

		if(m_BofangMove[1])//15秒 玩家距离小于500m
		{
			m_MoveTimmer[1] += Time.deltaTime;
			if(m_MoveTimmer[1] < 2.5f)
			{
				m_BofangWenzi[2].localPosition = Vector3.MoveTowards(m_BofangWenzi[2].localPosition,new Vector3(0.0f,m_BofangWenzi[2].localPosition.y,0.0f),speed*Time.deltaTime);
			}
			else
			{
				m_BofangWenzi[2].localPosition = Vector3.MoveTowards(m_BofangWenzi[2].localPosition,new Vector3(1125.37f,m_BofangWenzi[2].localPosition.y,0.0f),speed*Time.deltaTime);
			}
			if(Vector3.Distance(m_BofangWenzi[2].localPosition,new Vector3(1125.37f,m_BofangWenzi[2].localPosition.y,0.0f)) < 1.0f)
			{
				m_MoveTimmer[1] = 0.0f;
				m_BofangWenzi[2].localPosition = m_WenziPosRecord[2];
				m_BofangMove[1] = false;
			}
		}

		if(m_BofangMove[2]) //平均速度da于10
		{
			m_MoveTimmer[2] += Time.deltaTime;
			if(!m_HasSelect[2] && !m_HasSelect[3])
			{
				int num = UnityEngine.Random.Range(1,1000);
				if(num%2 == 0)
				{
					m_HasSelect[2]  = true;
				}
				else
				{
					m_HasSelect[3]  = true;
				}
			}
			if(m_HasSelect[2])
			{
				if(m_MoveTimmer[2] < 2.5f)
				{
					m_BofangWenzi[3].localPosition = Vector3.MoveTowards(m_BofangWenzi[3].localPosition,new Vector3(0.0f,m_BofangWenzi[3].localPosition.y,0.0f),speed*Time.deltaTime);
				}
				else
				{
					m_BofangWenzi[3].localPosition = Vector3.MoveTowards(m_BofangWenzi[3].localPosition,new Vector3(1125.37f,m_BofangWenzi[3].localPosition.y,0.0f),speed*Time.deltaTime);
				}
				if(Vector3.Distance(m_BofangWenzi[3].localPosition,new Vector3(1125.37f,m_BofangWenzi[3].localPosition.y,0.0f)) < 1.0f)
				{
					m_MoveTimmer[2] = 0.0f;
					m_BofangWenzi[3].localPosition = m_WenziPosRecord[3];
					m_HasSelect[2] =false;
					m_HasSelect[3]  = true;
					m_BofangMove[2] = false;
				}
			}
			else if(m_HasSelect[3])
			{
				if(m_MoveTimmer[2] < 2.5f)
				{
					m_BofangWenzi[4].localPosition = Vector3.MoveTowards(m_BofangWenzi[4].localPosition,new Vector3(0.0f,m_BofangWenzi[4].localPosition.y,0.0f),speed*Time.deltaTime);
				}
				else
				{
					m_BofangWenzi[4].localPosition = Vector3.MoveTowards(m_BofangWenzi[4].localPosition,new Vector3(1125.37f,m_BofangWenzi[4].localPosition.y,0.0f),speed*Time.deltaTime);
				}
				if(Vector3.Distance(m_BofangWenzi[4].localPosition,new Vector3(1125.37f,m_BofangWenzi[4].localPosition.y,0.0f)) < 1.0f)
				{
					m_MoveTimmer[2] = 0.0f;
					m_BofangWenzi[4].localPosition = m_WenziPosRecord[4];
					m_HasSelect[2] =true;
					m_HasSelect[3]  = false;
					m_BofangMove[2] = false;
				}
			}
		}
		if(m_BofangMove[3]) // 命中率大于0.7
		{
			m_MoveTimmer[3] += Time.deltaTime;
			if(!m_HasSelect[4] && !m_HasSelect[5])
			{
				int num = UnityEngine.Random.Range(1,1000);
				if(num%2 == 0)
				{
					m_HasSelect[4]  = true;
				}
				else
				{
					m_HasSelect[5]  = true;
				}
			}
			if(m_HasSelect[4])
			{
				if(m_MoveTimmer[3] < 2.5f)
				{
					m_BofangWenzi[5].localPosition = Vector3.MoveTowards(m_BofangWenzi[5].localPosition,new Vector3(0.0f,m_BofangWenzi[5].localPosition.y,0.0f),speed*Time.deltaTime);
				}
				else
				{
					m_BofangWenzi[5].localPosition = Vector3.MoveTowards(m_BofangWenzi[5].localPosition,new Vector3(1125.37f,m_BofangWenzi[3].localPosition.y,0.0f),speed*Time.deltaTime);
				}
				if(Vector3.Distance(m_BofangWenzi[5].localPosition,new Vector3(1125.37f,m_BofangWenzi[5].localPosition.y,0.0f)) < 1.0f)
				{
					m_MoveTimmer[3] = 0.0f;
					m_BofangWenzi[5].localPosition = m_WenziPosRecord[5];
					m_HasSelect[4] =false;
					m_HasSelect[5]  = true;
					m_BofangMove[3] = false;
				}
			}
			else if(m_HasSelect[5])
			{
				if(m_MoveTimmer[3] < 2.5f)
				{
					m_BofangWenzi[6].localPosition = Vector3.MoveTowards(m_BofangWenzi[6].localPosition,new Vector3(0.0f,m_BofangWenzi[6].localPosition.y,0.0f),speed*Time.deltaTime);
				}
				else
				{
					m_BofangWenzi[6].localPosition = Vector3.MoveTowards(m_BofangWenzi[6].localPosition,new Vector3(1125.37f,m_BofangWenzi[6].localPosition.y,0.0f),speed*Time.deltaTime);
				}
				if(Vector3.Distance(m_BofangWenzi[6].localPosition,new Vector3(1125.37f,m_BofangWenzi[6].localPosition.y,0.0f)) < 1.0f)
				{
					m_MoveTimmer[3] = 0.0f;
					m_BofangWenzi[6].localPosition = m_WenziPosRecord[6];
					m_HasSelect[4] =true;
					m_HasSelect[5]  = false;
					m_BofangMove[3] = false;
				}
			}
		}
		if(m_BofangMove[4])//命中率小于0.3
		{
			m_MoveTimmer[4] += Time.deltaTime;
			if(!m_HasSelect[6] && !m_HasSelect[7])
			{
				int num = UnityEngine.Random.Range(1,1000);
				if(num%2 == 0)
				{
					m_HasSelect[6]  = true;
				}
				else
				{
					m_HasSelect[7]  = true;
				}
			}
			if(m_HasSelect[6])
			{
				if(m_MoveTimmer[4] < 2.5f)
				{
					m_BofangWenzi[7].localPosition = Vector3.MoveTowards(m_BofangWenzi[7].localPosition,new Vector3(0.0f,m_BofangWenzi[7].localPosition.y,0.0f),speed*Time.deltaTime);
				}
				else
				{
					m_BofangWenzi[7].localPosition = Vector3.MoveTowards(m_BofangWenzi[7].localPosition,new Vector3(1125.37f,m_BofangWenzi[7].localPosition.y,0.0f),speed*Time.deltaTime);
				}
				if(Vector3.Distance(m_BofangWenzi[7].localPosition,new Vector3(1125.37f,m_BofangWenzi[7].localPosition.y,0.0f)) < 1.0f)
				{
					m_MoveTimmer[4] = 0.0f;
					m_BofangWenzi[7].localPosition = m_WenziPosRecord[7];
					m_HasSelect[6] =false;
					m_HasSelect[7]  = true;
					m_BofangMove[4] = false;
				}
			}
			else if(m_HasSelect[7])
			{
				if(m_MoveTimmer[4] < 2.5f)
				{
					m_BofangWenzi[8].localPosition = Vector3.MoveTowards(m_BofangWenzi[8].localPosition,new Vector3(0.0f,m_BofangWenzi[8].localPosition.y,0.0f),speed*Time.deltaTime);
				}
				else
				{
					m_BofangWenzi[8].localPosition = Vector3.MoveTowards(m_BofangWenzi[8].localPosition,new Vector3(1125.37f,m_BofangWenzi[8].localPosition.y,0.0f),speed*Time.deltaTime);
				}
				if(Vector3.Distance(m_BofangWenzi[8].localPosition,new Vector3(1125.37f,m_BofangWenzi[8].localPosition.y,0.0f)) < 1.0f)
				{
					m_MoveTimmer[4] = 0.0f;
					m_BofangWenzi[8].localPosition = m_WenziPosRecord[8];
					m_HasSelect[6] =true;
					m_HasSelect[7]  = false;
					m_BofangMove[4] = false;
				}
			}
		}
	}
	void GameOverSelect()
	{
		float num = myPlayer.Distance / DistanceForEnd-m_XubiCishu;
		m_AudioBeijing.Stop ();
		if(num < 0.8f)
		{
			gameOverObj.SetActive (true);
			GameOver.enabled = true;
			if(!m_AudioIsPlay)
			{
				m_AudioIsPlay = true;
				m_AudioLose.Play ();
			}
		}
		else 
		{
			winnerObj.SetActive (true);
			winner.enabled = true;
			if(!m_AudioIsPlay)
			{
				m_AudioIsPlay = true;
				m_AudioWin.Play();
			}
		}
	}

	void UpdateInsertCoin()
	{//Debug.Log ("pcvr.CoinCurGame ============ " + pcvr.CoinCurGame);
		int n = 1;
		int num = pcvr.CoinCurGame;
		int temp = num;
		while(num > 9)
		{
			num /= 10;
			n++;
		}
		if(n > 2)
		{
			m_InsertNumS.spriteName = "9";
			m_InsertNumG.spriteName = "9";
		}
		else if(n==2)
		{
			int shiwei = (int)(temp/10);
			int gewei = (int)(temp-shiwei*10);
			m_InsertNumS.spriteName = shiwei.ToString();
			m_InsertNumG.spriteName = gewei.ToString();
		}
		else if(n == 1)
		{
			m_InsertNumS.spriteName = "0";
			m_InsertNumG.spriteName = temp.ToString();
		}
	}
	void XiaoNvhaiSelect()
	{
		if((GameMode == "free" || pcvr.CoinCurGame >= Convert.ToInt32(CoinNumSet)))
		{
			m_XiaonuhaiKaishi.enabled = true;
			m_XiaonvhaiToubi.enabled = false;

			//open
			pcvr.GetInstance().setLightStateP2(3);
		}
		else
		{
			m_XiaonuhaiKaishi.enabled = false;
			m_XiaonvhaiToubi.enabled = true;
		}
	}
	void XiaomaBengpaoAudio()
	{
		if(biaoji >= 1)
		{
			if(!m_XiaomaShache.isPlaying)
			{
				ZhujuemaController.m_IsBrake = false;
				biaoji = 0;
			}
		}
		if(PlayerController.speed >0.0f && PlayerController.speed<=12.0f && !m_IsNormalPlay && !IsGameOver && !ZhujuemaController.m_IsBrake)
		{
			m_IsNormalPlay = true;
			m_IsQuickPlay = false;
			m_XiaomaBengpaoQuick.Stop();
			m_XiaoMaBengpaoNormal.Play();
//			Debug.Log("m_XiaoMaBengpaoNormalm_XiaoMaBengpaoNormalm_XiaoMaBengpaoNormalm_XiaoMaBengpaoNormalm_XiaoMaBengpaoNormal");
		}
		else if(PlayerController.speed >12.0f && PlayerController.speed<=16.7f && !m_IsQuickPlay && !IsGameOver && !ZhujuemaController.m_IsBrake)
		{
			m_IsNormalPlay = false;
			m_IsQuickPlay = true;
			m_XiaoMaBengpaoNormal.Stop();
			m_XiaomaBengpaoQuick.Play();
//			Debug.Log("m_XiaoMaBengpaoquick m_XiaoMaBengpaoquick");
		}
		else if(PlayerController.speed <= 0.0f || IsGameOver || ZhujuemaController.m_IsBrake)
		{
			m_XiaoMaBengpaoNormal.Stop();
			m_XiaomaBengpaoQuick.Stop();
			if(ZhujuemaController.m_IsBrake && biaoji == 0)
			{
				m_XiaomaShache.Play();
				biaoji++;
			}
			m_IsNormalPlay = false;
			m_IsQuickPlay = false;
		}
	}


	void XubiSelect()
	{
		if (pcvr.GetInstance())
		{
			pcvr.GetInstance().gunShakeOpen = false;
		}

		if(!m_IsXubi)
		{
			//m_speedRecord = PlayerController.speed;
			PlayerController.speed = 0.0f;
		}

		PlayerController.inGame = false;
		pcvr.bZhendong = false;
		m_IsXubi = true;
		//GameState = 2;
		BigdaoJishi.SetActive(false);
		m_AudioDaojishi.Stop();
		Haomiaoshiwei.spriteName = "0";
		Haomiaogewei.spriteName = "0";
		m_XubiDaojishi.SetActive (true);
		m_TimmerForXubi += Time.deltaTime;
		if(m_TimmerForXubi<=9.0f)
		{
			if(!m_pXubiAudio.isPlaying)
			{
				m_pXubiAudio.Play();
			}
			m_AudioBeijing.Pause();
			m_XubiSprite.spriteName = ((int)(10 - m_TimmerForXubi)).ToString ();
			XubiTishi();
			if(!pcvr.bIsHardWare)
			{
				if(Input.GetKeyDown(KeyCode.O) && GameSorD == "double")
				{
					OnClickBeginBt2P();
				}
				if(Input.GetKeyDown(KeyCode.K))
				{
					OnClickBeginBt1P();
				}
			}
		}
		else
		{
			m_IsXubi = false;
			m_pXubiAudio.Stop();
			m_Warn.enabled = false;
			m_XubiDaojishi.SetActive (false);
			IsGameOver = true;
			GameOverSelect();
		}
		if(ActiveGameTime < -14.0f)
		{
			if(GameMode == "free"  || pcvr.CoinCurGame < Convert.ToInt32(CoinNumSet))
				Application.LoadLevel(0 + pcvr.chenNum);
			else
				Application.LoadLevel(1 + pcvr.chenNum);
		}
	}
	void XubiTishi()
	{
		if(GameMode == "free")
		{
			m_BeiginTishi.SetActive(true);
			m_ToubiTishi.SetActive(false);
		}
		else
		{
			if(pcvr.CoinCurGame >= Convert.ToInt32(CoinNumSet))
			{
				m_BeiginTishi.SetActive(true);
				m_ToubiTishi.SetActive(false);
			}
			else
			{
				m_BeiginTishi.SetActive(false);
				m_ToubiTishi.SetActive(true);
			}
		}
	}
	public void OnClickInsertBt()
	{
		m_AudioToubi.Play();
		UpdateInsertCoin();
	}
	public void OnClickBeginBt2P()
	{
		if(GameState == 1
		   && GameSorD == "double"
		   && !PlayerController.IsKaiqiang
		   && !m_IsXubi
		   && (GameMode=="free" || pcvr.CoinCurGame >= Convert.ToInt32(CoinNumSet)))
		{
			if (pcvr.GetInstance() && !pcvr.stopUrgent && !pcvr.GetInstance().p2AnquandaiOpen)
			{
				m_pP2AnquandaiObj.SetActive (true);
			}

			if(!PlayerController.IsKaiqiang)
			{
				m_AudioXiaonvhai.Play();
				m_Xiaonvhai.mainTexture = m_XiaonvhaiTex;
				m_XiaonuhaiKaishi.enabled = false;
				m_XiaonvhaiToubi.enabled = false;

				//close light
				pcvr.GetInstance().setLightStateP2(2);
			}
			int GameTimeRecord = Convert.ToInt32 (ReadGameInfo.GetInstance().ReadGameTimeSet());
			GameTime = GameTimeRecord + Time.time;
			PlayerController.IsKaiqiang = true;
			m_pZhunxing.SetActive(true);

			if (pcvr.GetInstance())
			{
				pcvr.GetInstance().gunShakeOpen = true;
			}

			if(GameMode=="oper")
			{
				if(!pcvr.bIsHardWare)
				{
					pcvr.CoinCurGame -= Convert.ToInt32(CoinNumSet);
				}
				else
				{
					pcvr.GetInstance().SubPlayerCoin(Convert.ToInt32(CoinNumSet));
				}

				UpdateInsertCoin();
			}
		}
		else if(m_IsXubi)
		{
			m_Warn.enabled = true;
			m_pWarnAudio.Play();
		}
	}
	public void OnClickBeginBt1P()
	{
		if(m_IsXubi && (GameMode=="free" || pcvr.CoinCurGame >= Convert.ToInt32(CoinNumSet)))
		{
			if (pcvr.GetInstance() && !pcvr.stopUrgent && !pcvr.GetInstance().p1AnquandaiOpen)
			{
				m_pP1AnquandaiObj.SetActive (true);
				
				//return;
			}

			if (pcvr.GetInstance() && PlayerController.IsKaiqiang)
			{
				pcvr.GetInstance().gunShakeOpen = true;
			}

			PlayerController.inGame = true;
			m_XubiCishu++;
			m_IsXubi = false;
			m_pBeginAudio.Play();
			m_AudioBeijing.Play();
			m_Warn.enabled = false;
			m_XubiDaojishi.SetActive (false);
			int GameTimeRecord = Convert.ToInt32 (ReadGameInfo.GetInstance().ReadGameTimeSet());
			GameTime = GameTimeRecord + Time.time;
			if(GameMode=="oper")
			{
				GameState = 1;

				if(!pcvr.bIsHardWare)
				{
					pcvr.CoinCurGame -= Convert.ToInt32(CoinNumSet);
				}
				else
				{
					pcvr.GetInstance().SubPlayerCoin(Convert.ToInt32(CoinNumSet));
				}
				UpdateInsertCoin();
			}
			m_TimmerForXubi = 0.0f;
			m_pXubiAudio.Stop();
		}
	}

	public void OpenDonggan()
	{
		pcvr.stopUrgent = false;

		m_pDOpenObj.SetActive (true);
		m_pDCloseObj.SetActive (false);

		CancelInvoke ("HideOpenTishi");
		Invoke("HideOpenTishi", 3.0f);
	}

	void HideOpenTishi()
	{
		m_pDOpenObj.SetActive (false);
		m_pDCloseObj.SetActive (false);
	}
	
	public void CloseDonggan()
	{
		CancelInvoke ("HideOpenTishi");
		m_pDOpenObj.SetActive (false);
		m_pDCloseObj.SetActive (true);
	}
	
	public void openLeAnquandaiP1(bool flag)
	{
		m_pP1AnquandaiObj.SetActive (false);
		
		if (flag)
		{
			OpenDonggan ();
		}
	}
	
	public void openLeAnquandaiP2(bool flag)
	{
		m_pP2AnquandaiObj.SetActive (false);

		if (flag)
		{
			OpenDonggan ();
		}
	}
	
	public void closeLeAnquandaiP1()
	{
		m_pP1AnquandaiObj.SetActive (true);

		CloseDonggan ();
	}
	
	public void closeLeAnquandaiP2()
	{
		m_pP2AnquandaiObj.SetActive (true);

		CloseDonggan ();
	}
}
