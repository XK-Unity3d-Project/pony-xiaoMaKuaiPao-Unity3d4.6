using UnityEngine;
using System.Collections;
using System;

public class Loading : MonoBehaviour
{
	private string CoinNumSet = "";
	//private string InsertCoinNum = "";

	public UITexture m_BeginTex;
	public Texture   m_LoadingTex;
	public UITexture m_InsertTex;
	public UISprite CoinNumSetTex;
	public UISprite m_InsertNumS;
	public UISprite m_InsertNumG;
	private bool m_IsBeginOk = false;

	private float m_PressTimmer = 0.0f;
	private float m_InserTimmer = 0.0f;
	private float m_RetrunTimmer = 0.0f;
	private bool m_IsStartGame =false;
	public UITexture m_JhTishi;
	public AudioSource m_TbSource;
	public AudioSource m_BeginSource;
	public AudioSource m_JhSource;
	public UITexture m_Loading;

	//matou
	public UITexture m_pMatou;
	public Texture[] m_pMatouTex;
	private int m_pMatouIndex = 0;
	private float m_pMatouTimmer = 0.0f;

	//xuanguan
	public UITexture m_pFirstGuan;
	public Texture[] m_pFirstTex;
	public UITexture m_pSecondGuan;
	public Texture[] m_pSecondTex;

	public TweenRotation m_pFirstRotation;
	public TweenRotation m_pSecondRotation;
	private int m_pGuanqia = 2;
	public AudioSource m_pAudioXuanguan;


	//public TweenAlpha m_pAlpha;
	public GameObject m_pXuanguan;
	private bool m_IsSelectGuan = false;


	public GameObject m_pMianfeiobject;
	public GameObject m_pToubiobject;
	public string GameMode = "";
	private string ANQUANDAI = "";
	public static Loading Instance;

	private bool StartLe1p = false;

	//an quan dai
	public GameObject m_pP1AnquandaiObj = null;
	public GameObject m_pP2AnquandaiObj = null;

	public GameObject xuanguanObj = null;
	public GameObject toubiObj = null;
	public GameObject anquandaiObj = null;
	public GameObject mianfeiObj = null;

	void SetLoade()
	{
		MyCOMDevice.ComThreadClass.IsLoadingLevel = false;
	}

	void Start ()
	{
		int volumNum = Convert.ToInt32(ReadGameInfo.GetInstance ().ReadVolumeNum ());
		AudioListener.volume = volumNum * 0.1f;

		UpdateInsertCoin();

		if(pcvr.bIsHardWare)
		{
			Screen.showCursor = false;
			Screen.SetResolution(1360, 768, true);
			PlayerController.inGame = false;
			pcvr.bZhendong = false;
			pcvr.dianboState = 0;
			//MyCOMDevice.ComThreadClass.IsLoadingLevel = false;
			Invoke ("SetLoade", 0.1f);
			
			if (pcvr.GetInstance())
			{
				pcvr.GetInstance().gunShakeOpen = false;
			}
		}
		if (pcvr.bIsAutoMoveSelf)
		{
			GameMode = "free";
		}
		else
		{
			GameMode = ReadGameInfo.GetInstance ().ReadGameStarMode();
		}
		StartLe1p = false;

		m_pP1AnquandaiObj.SetActive (false);
		m_pP2AnquandaiObj.SetActive (false);

		if(GameMode == "oper")
		{
			m_pMianfeiobject.SetActive(false);
			m_pToubiobject.SetActive(true);
			CoinNumSet = ReadGameInfo.GetInstance ().ReadStarCoinNumSet();
			CoinNumSetTex.spriteName = CoinNumSet;
			UpdateTex();

			//xuanguan
			m_pXuanguan.SetActive(false);
			m_pMatou.mainTexture = m_pMatouTex [m_pMatouIndex];
			m_pFirstGuan.mainTexture = m_pFirstTex [0];
			m_pSecondGuan.mainTexture = m_pSecondTex [0];
			m_pFirstRotation.enabled = true;
			m_pSecondRotation.enabled = false;
			//m_pAlpha.enabled = false;
		}
		else
		{
			m_pMianfeiobject.SetActive(true);
			m_pToubiobject.SetActive(false);
			//xuanguan
			m_pXuanguan.SetActive(true);
			m_pMatou.mainTexture = m_pMatouTex [m_pMatouIndex];
			m_pFirstGuan.mainTexture = m_pFirstTex [0];
			m_pSecondGuan.mainTexture = m_pSecondTex [0];
			m_pFirstRotation.enabled = true;
			m_pSecondRotation.enabled = false;
			//m_pAlpha.enabled = true;
		}
		m_JhTishi.enabled = false;
		m_Loading.enabled = false;
		
		ANQUANDAI = ReadGameInfo.GetInstance ().ReadAnquandai();
		
		if (ANQUANDAI == "close" || pcvr.bIsAutoMoveSelf)
		{
			pcvr.openAanquandai = false;
			pcvr.GetInstance().p1AnquandaiOpen = true;
			pcvr.GetInstance().p2AnquandaiOpen = true;
		}
		else
		{
			pcvr.openAanquandai = true;
		}

		Instance = this;
	}
	public static Loading GetInstance()
	{
		return Instance;
	}

	void Update ()
	{
		if (pcvr.bIsAutoMoveSelf)
		{
			OnClickBeginBt1P();
			return;
		}

		if(GameMode == "free" || pcvr.CoinCurGame >= Convert.ToInt32(CoinNumSet))
		{
			//m_pAlpha.enabled = true;
			m_IsSelectGuan = true;
		}
		if(m_IsSelectGuan)
		{
			m_pXuanguan.SetActive(true);
			MatouTexSet();
			SelectGuan ();
		}
		if(GameMode == "oper")
		{
			m_RetrunTimmer+=Time.deltaTime;
			if(!m_IsStartGame)
			{
				UpdateTex();
			}
		}
		else if(GameMode == "free")
		{
			m_RetrunTimmer+=Time.deltaTime;
		}
		if(m_RetrunTimmer >= 10.0f  && !m_IsStartGame && (GameMode == "free" || pcvr.CoinCurGame < Convert.ToInt32(CoinNumSet)))
		{
			Application.LoadLevel(0 + pcvr.chenNum);
		}
		if(!pcvr.bIsHardWare)
		{
			if(Input.GetKeyDown(KeyCode.T))
			{
				m_TbSource.Play();
				m_RetrunTimmer = 0.0f;
				pcvr.CoinCurGame ++;
				UpdateInsertCoin();
			}
			if(Input.GetKeyDown(KeyCode.K))
			{
				OnClickBeginBt1P();
			}
			if(Input.GetKeyDown(KeyCode.O) )
			{
				OnClickBeginBt2P();
			}
			if(Input.GetKeyDown(KeyCode.F5))
			{
				Application.LoadLevel(3 + pcvr.chenNum);
			}
		}
	}

	void UpdateInsertCoin()
	{
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

	void UpdateTex()
	{
		if(pcvr.CoinCurGame >= Convert.ToInt32(CoinNumSet))
		{
			m_InserTimmer = 0.0f;
			m_IsBeginOk = true;
			m_InsertTex.enabled = false;
			m_BeginTex.enabled =true;
			m_PressTimmer+=Time.deltaTime;

			//open light
			pcvr.GetInstance().setLightStateP1(3);

			if(m_PressTimmer >= 0.0f && m_PressTimmer <= 0.5f)
			{
				m_BeginTex.enabled =true;
			}
			else if(m_PressTimmer > 0.5f && m_PressTimmer <= 1.0f)
			{
				m_BeginTex.enabled =false;
			}
			else
			{
				m_PressTimmer = 0.0f;
			}
		}
		else
		{
			m_InserTimmer+=Time.deltaTime;
			m_IsBeginOk = false;
			m_InsertTex.enabled = true;
			m_BeginTex.enabled =false;
			m_PressTimmer = 0.0f;
			if(m_InserTimmer >= 0.0f && m_InserTimmer <= 0.4f)
			{
				m_InsertTex.enabled = true;
			}
			else if(m_InserTimmer > 0.4f && m_InserTimmer <= 0.8f)
			{
				m_InsertTex.enabled = false;
			}
			else
			{
				m_InserTimmer = 0.0f;
			}
		}
	}

	void beginLoadScence()
	{
		StartCoroutine (loadScene(m_pGuanqia + pcvr.chenNum));
	}

	IEnumerator loadScene(int num)   
	{
		AsyncOperation async = Application.LoadLevelAsync(num);   
		yield return async;		
	}

	void MatouTexSet()
	{
		m_pMatouTimmer += Time.deltaTime;
		if(m_pMatouTimmer>0.3f && !m_IsStartGame)
		{
			m_pMatouIndex++;
			if(m_pMatouIndex>=4)
			{
				m_pMatouIndex = 0;
			}
			m_pMatou.mainTexture = m_pMatouTex[m_pMatouIndex];
			m_pMatouTimmer = 0.0f;
		}
	}
	void SelectGuan()
	{
		if(Input.GetKeyDown(KeyCode.A))
		{
			OnClickLeftBt();
		}
		if(Input.GetKeyDown(KeyCode.D))
		{
			OnClickRightBt();
		}
	}


	public void OnClickInsertBt()
	{
		m_TbSource.Play();
		m_RetrunTimmer = 0.0f;
		UpdateInsertCoin();
	}

	public void OnClickBeginBt1P()
	{
		if(!StartLe1p && (m_IsBeginOk || GameMode == "free"))
		{
			if (pcvr.GetInstance() && !pcvr.GetInstance().p1AnquandaiOpen)
			{
				m_pP1AnquandaiObj.SetActive (true);

				//return;
			}

			StartLe1p = true;
			m_pFirstRotation.enabled = false;
			m_pSecondRotation.enabled = false;
			m_BeginSource.Play();
			m_IsStartGame = true;

			//close --- light
			pcvr.GetInstance().setLightStateP1(2);

			if(GameMode == "oper")
			{
				m_InsertTex.enabled = false;
				m_BeginTex.enabled =false;
				
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

			if (pcvr.bIsAutoMoveSelf && UnityEngine.Random.Range(0, 2) == 1)
			{
				m_pGuanqia = 2;
			}
			else if (pcvr.bIsAutoMoveSelf)
			{
				m_pGuanqia = 4;
			}

			if(m_pGuanqia == 2)
			{
				m_pMatou.mainTexture = m_pMatouTex[0];
			}
			else
			{
				m_pMatou.mainTexture = m_pMatouTex[2];
			}
			m_Loading.enabled = true;

			if (xuanguanObj)
			{
				xuanguanObj.transform.position = new Vector3(1000, 0, 0);
			}
			
			if (toubiObj)
			{
				toubiObj.transform.position = new Vector3(1000, 0, 0);
			}
			
			if (anquandaiObj)
			{
				anquandaiObj.transform.position = new Vector3(1000, 0, 0);
			}
			
			if (mianfeiObj)
			{
				mianfeiObj.transform.position = new Vector3(1000, 0, 0);
			}

			MyCOMDevice.ComThreadClass.IsLoadingLevel = true;
			Invoke("beginLoadScence", 0.1f);
		}
	}
	public void OnClickBeginBt2P()
	{
		if(m_IsBeginOk)
		{
			m_JhSource.Play();
			m_JhTishi.enabled = true;
		}
	}
	public void OnClickLeftBt()
	{
		if(m_IsSelectGuan)
		{
			m_pFirstGuan.mainTexture = m_pFirstTex [0];
			m_pSecondGuan.mainTexture = m_pSecondTex [0];
			m_pFirstRotation.enabled = true;
			m_pSecondRotation.enabled = false;
			m_pGuanqia = 2;
			m_pAudioXuanguan.Play();
		}
	}
	public void OnClickRightBt()
	{
		if(m_IsSelectGuan)
		{
			m_pFirstGuan.mainTexture = m_pFirstTex [1];
			m_pSecondGuan.mainTexture = m_pSecondTex [1];
			m_pFirstRotation.enabled = false;
			m_pSecondRotation.enabled = true;
			m_pGuanqia = 4;
			m_pAudioXuanguan.Play();
		}
	}

	public void openLeAnquandaiP1()
	{
		m_pP1AnquandaiObj.SetActive (false);
	}
	
	public void openLeAnquandaiP2()
	{
		m_pP2AnquandaiObj.SetActive (false);
	}
}
