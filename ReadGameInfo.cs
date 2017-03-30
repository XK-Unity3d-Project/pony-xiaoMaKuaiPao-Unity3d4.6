using UnityEngine;
using System.Collections;

public class ReadGameInfo : MonoBehaviour 
{
	static private ReadGameInfo Instance = null;
	private HandleJson handleJsonObj;
	private string fileName = "xiaoMaConfig.xml";
	public string m_pSingleorDouble = "";
	public string m_pStarCoinNum = "";
	public string m_pGameMode = "";
	public string m_pGameTime = "";
	public string m_pInsertCoinNum = "";
	public string m_pAnquandai = "";
	public string m_pCHEN = "";
	public string m_pVolumSet = "";
	public string m_pVolumNum = "";
	public string m_pShake = "";
	public string m_pDianji = "";

	static public ReadGameInfo GetInstance()
	{
		if(Instance == null)
		{
			GameObject obj = new GameObject("_Read");
			DontDestroyOnLoad(obj);
			Instance = obj.AddComponent<ReadGameInfo>();
			Instance.InitGameInfo();
		}
		return Instance;
	}
	
	void InitGameInfo()
	{
		handleJsonObj = HandleJson.GetInstance ();

		//m_pSingleorDouble = handleJsonObj.ReadFromFileXml (fileName, "START_SORD");
		m_pSingleorDouble = PlayerPrefs.GetString("START_SORD");
		if(m_pSingleorDouble == "" || m_pSingleorDouble == null)
		{
			m_pSingleorDouble = "double";
			WriteStarSingleorDouble ("double");
		}
		
		//m_pStarCoinNum = handleJsonObj.ReadFromFileXml (fileName, "START_COIN");
		m_pStarCoinNum = PlayerPrefs.GetString("START_COIN");
		if(m_pStarCoinNum == "" || m_pStarCoinNum == null)
		{
			m_pStarCoinNum = "1";
			WriteStarCoinNumSet("1");
		}
		
		//m_pGameMode = handleJsonObj.ReadFromFileXml (fileName, "START_MODE");
		m_pGameMode = PlayerPrefs.GetString("START_MODE");
		if(m_pGameMode == "" || m_pGameMode == null)
		{
			m_pGameMode = "oper";
			WriteGameStarMode("oper");
		}
		
		//m_pGameTime = handleJsonObj.ReadFromFileXml (fileName, "START_TIME");
		m_pGameTime = PlayerPrefs.GetString("START_TIME");
		if(m_pGameTime == "" || m_pGameTime == null)
		{
			m_pGameTime = "120";
			WriteGameTimeSet("120");
		}
		
		m_pInsertCoinNum = "0";
		//m_pInsertCoinNum = handleJsonObj.ReadFromFileXml (fileName, "INSERT_COIN");
		/*m_pInsertCoinNum = PlayerPrefs.GetString("INSERT_COIN");
		if(m_pInsertCoinNum == "" || m_pInsertCoinNum == null)
		{
			m_pInsertCoinNum = "0";
			WriteInsertCoinNum("0");
		}*/
		
		//m_pAnquandai = handleJsonObj.ReadFromFileXml (fileName, "START_ANQUANDAI");
		m_pAnquandai = PlayerPrefs.GetString("START_ANQUANDAI");
		if(m_pAnquandai == "" || m_pAnquandai == null)
		{
			m_pAnquandai = "open";
			WriteAnquandai("open");
		}
		
		//m_pCHEN = handleJsonObj.ReadFromFileXml (fileName, "START_CHEN");
		m_pCHEN = PlayerPrefs.GetString("START_CHEN");
		if(m_pCHEN == "" || m_pCHEN == null)
		{
			m_pCHEN = "CH";
			WriteCHEN("CH");
		}

		//m_pVolumSet = handleJsonObj.ReadFromFileXml (fileName, "START_VOLUMESET");
		m_pVolumSet = PlayerPrefs.GetString("START_VOLUMESET");
		if(m_pVolumSet == "" || m_pVolumSet == null)
		{
			m_pVolumSet = "on";
			WriteVolumeSet("on");
		}
		
		//m_pVolumNum = handleJsonObj.ReadFromFileXml (fileName, "START_VOLUMENUM");
		m_pVolumNum = PlayerPrefs.GetString("START_VOLUMENUM");
		if(m_pVolumNum == "" || m_pVolumNum == null)
		{
			m_pVolumNum = "7";
			WriteVolumeNum("7");
		}
		
		//m_pShake = handleJsonObj.ReadFromFileXml (fileName, "START_SHAKE");
		m_pShake = PlayerPrefs.GetString("START_SHAKE");
		if(m_pShake == "" || m_pShake == null)
		{
			m_pShake = "0";
			WriteShake("0");
		}
		
		//m_pDianji = handleJsonObj.ReadFromFileXml (fileName, "START_DIANJI");
		m_pDianji = PlayerPrefs.GetString("START_DIANJI");
		if(m_pDianji == "" || m_pDianji == null)
		{
			m_pDianji = "0";
			WriteDianji("0");
		}
	}

	public void FactoryReset()
	{
		WriteStarSingleorDouble ("double");
		WriteStarCoinNumSet("1");
		WriteGameStarMode("oper");
		WriteGameTimeSet("120");
		WriteInsertCoinNum("0");
		WriteAnquandai("open");
		WriteCHEN("CH");
		WriteVolumeSet ("on");
		WriteVolumeNum ("7");
		WriteShake ("0");
		WriteDianji ("1");
	}

	//read
	public string ReadStarSingleorDouble()
	{
		return m_pSingleorDouble;
	}

	public string ReadStarCoinNumSet()
	{
		return m_pStarCoinNum;
	}

	public string ReadGameStarMode()
	{
		if (pcvr.bIsAutoMoveSelf)
		{
			return "free";;
		}

		return m_pGameMode;
	}

	public string ReadGameTimeSet()
	{
		return m_pGameTime;
	}

	public string ReadInsertCoinNum()
	{
		return m_pInsertCoinNum;
	}

	public string ReadAnquandai()
	{
		return m_pAnquandai;
	}
	
	public string ReadCHEN()
	{
		return m_pCHEN;
	}

	public string ReadVolumeSet()
	{
		return m_pVolumSet;
	}

	public string ReadVolumeNum()
	{
		return m_pVolumNum;
	}
	
	public string ReadShake()
	{
		return m_pShake;
	}
	
	public string ReadDianji()
	{
		return m_pDianji;
	}

	//write
	public void WriteStarSingleorDouble(string value)
	{
		//handleJsonObj.WriteToFileXml(fileName,"START_SORD",value);
		PlayerPrefs.SetString("START_SORD", value);
		m_pSingleorDouble = value;
	}

	public void WriteStarCoinNumSet(string value)
	{
		//handleJsonObj.WriteToFileXml(fileName,"START_COIN",value);
		PlayerPrefs.SetString("START_COIN", value);
		m_pStarCoinNum = value;
	}

	public void WriteGameStarMode(string value)
	{
		//handleJsonObj.WriteToFileXml(fileName,"START_MODE",value);
		PlayerPrefs.SetString("START_MODE", value);
		m_pGameMode = value;
	}

	public void WriteGameTimeSet(string value)
	{
		//handleJsonObj.WriteToFileXml(fileName,"START_TIME",value);
		PlayerPrefs.SetString("START_TIME", value);
		m_pGameTime = value;
	}

	public void WriteInsertCoinNum(string value)
	{
		//handleJsonObj.WriteToFileXml(fileName,"INSERT_COIN",value);
		PlayerPrefs.SetString("INSERT_COIN", value);
		m_pInsertCoinNum = value;
	}

	public void WriteAnquandai(string value)
	{
		//handleJsonObj.WriteToFileXml(fileName,"START_ANQUANDAI",value);
		PlayerPrefs.SetString("START_ANQUANDAI", value);
		m_pAnquandai = value;
	}

	public void WriteCHEN(string value)
	{
		//handleJsonObj.WriteToFileXml(fileName,"START_CHEN",value);
		PlayerPrefs.SetString("START_CHEN", value);
		m_pCHEN = value;
	}

	public void WriteVolumeSet(string value)
	{
		//handleJsonObj.WriteToFileXml(fileName,"START_VOLUMESET",value);
		PlayerPrefs.SetString("START_VOLUMESET", value);
		m_pVolumSet = value;
	}

	public void WriteVolumeNum(string value)
	{
		//handleJsonObj.WriteToFileXml(fileName,"START_VOLUMENUM",value);
		PlayerPrefs.SetString("START_VOLUMENUM", value);
		m_pVolumNum = value;
	}

	public void WriteShake(string value)
	{
		//handleJsonObj.WriteToFileXml(fileName,"START_SHAKE",value);
		PlayerPrefs.SetString("START_SHAKE", value);
		m_pShake = value;
	}

	public void WriteDianji(string value)
	{
		//handleJsonObj.WriteToFileXml(fileName,"START_DIANJI",value);
		PlayerPrefs.SetString("START_DIANJI", value);
		m_pDianji = value;
	}
}
