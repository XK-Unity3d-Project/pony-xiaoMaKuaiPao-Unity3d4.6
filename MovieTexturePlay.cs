using UnityEngine;
using System.Collections;
using System;

public class MovieTexturePlay : MonoBehaviour 
{
	public MovieTexture m_MovieTex;
	private string CoinNumSet = "";

	public UISprite CoinNumSetTex;
	public UISprite m_InsertNumS;
	public UISprite m_InsertNumG;
	public UITexture m_InsertTex;
	public AudioSource m_Audio;
	public AudioSource m_Donghua;
	private float m_InsertTimmer = 0.0f;

	public GameObject m_pToubiobject;
	public GameObject m_pMianfeiobject;
	public string GameMode = "";
	private string ANQUANDAI = "";
	public static MovieTexturePlay Instance;
	private float autoTime = 6.0f;
	
	private string CHEN = "";	//0-ch	5-en
	private int chenNum = 0;	//0-ch	5-en

	void SetLoade()
	{
		MyCOMDevice.ComThreadClass.IsLoadingLevel = false;
	}

	void Start () 
	{
		int volumNum = Convert.ToInt32(ReadGameInfo.GetInstance ().ReadVolumeNum ());
		AudioListener.volume = volumNum * 0.1f;

		UpdateInsertCoin();

		CHEN = ReadGameInfo.GetInstance ().ReadCHEN();
		if (CHEN == "EN")
		{
			chenNum = 5;
			if (Application.loadedLevel == 0)
			{
				Application.LoadLevel(0 + chenNum);
			}
		}
		else
		{
			chenNum = 0;
		}

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

		//Screen.showCursor = false;
		pcvr.GetInstance();
		CtrlForHardWare.GetInstance();
		Debug.Log("pcvr.CoinCurGame" + ReadGameInfo.GetInstance ().ReadInsertCoinNum());
		m_MovieTex.loop = true;
		m_MovieTex.Play();

		if (m_Donghua)
		{
			m_Donghua.loop = true;
			m_Donghua.Play ();
		}

		if (pcvr.bIsAutoMoveSelf)
		{
			autoTime = 6.0f;
		}
		else
		{
			GameMode = ReadGameInfo.GetInstance ().ReadGameStarMode();
		}

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

	public static MovieTexturePlay GetInstance()
	{
		return Instance;
	}	

	void Update ()
	{
		if (pcvr.bIsAutoMoveSelf)
		{
			autoTime -= Time.deltaTime;

			if (autoTime < 0.0f)
			{
				Application.LoadLevel(1 + chenNum);
			}
			return;
		}
		if(GameMode == "oper")
		{
			m_InsertTimmer+=Time.deltaTime;
			if(m_InsertTimmer>=0.0f && m_InsertTimmer<= 0.4f)
			{
				m_InsertTex.enabled = true;
			}
			else if(m_InsertTimmer>0.4f && m_InsertTimmer<= 0.8f)
			{
				m_InsertTex.enabled = false;
			}
			else
			{
				m_InsertTimmer = 0.0f;
			}

			if(pcvr.CoinCurGame >= Convert.ToInt32(CoinNumSet))
			{
				Application.LoadLevel(1 + chenNum);
			}
		}
		if(!pcvr.bIsHardWare)
		{
			if(GameMode == "free" && Input.GetKeyDown(KeyCode.K))
			{
				Application.LoadLevel(1 + chenNum);
			}
			if(GameMode == "oper" && Input.GetKeyDown(KeyCode.T))
			{
				m_Audio.Play();
				pcvr.CoinCurGame ++;
				UpdateInsertCoin();
				Application.LoadLevel(1 + chenNum);
			}
			if(Input.GetKeyDown(KeyCode.F5))
			{
				Application.LoadLevel(3 + chenNum);
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

	public void OnClickInsertBt()
	{
		m_Audio.Play();
		UpdateInsertCoin ();
		Application.LoadLevel(1 + chenNum);
	}
}
