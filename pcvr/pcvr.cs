using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using SLAB_HID_DEVICE;
using System;
using System.IO;

public class pcvr : MonoBehaviour {
	
	static public bool bIsHardWare = false;
	static public bool bIsAutoMoveSelf = false;
	private float jiaoyanTotalTime = 0.0f;
	private const float jiaoyanTime = 15.0f;
	private bool stopJiaoyan = false;
	public static bool IsSlowLoopCom = false;

	//the pos information
	private int posXMin = 0;
	private int posXMax = 0;
	private int posYMin = 0;
	private int posYMax = 0;
	
	public static int CoinCurGame = 0;	//CoinNumCurrent  
	bool IsSubPlayerCoin = false;

	static public uint gOldCoinNum = 0;
	static public uint mOldCoinNum = 0;

	private int subCoinNum = 0;
	public static uint CoinCurPcvr;

	static public bool openAanquandai = true;

	public static bool m_IsShache = false;
	public static bool bZhendong = false;
	private byte []bufferSendQinang;

	static public bool bPlayerStartKeyDownP1 = false;
	static public bool bPlayerStartKeyDownP2 = false;
	private bool bSetEnterKeyDown = false;
	static public bool bSetMoveKeyDown = false;
	static public bool bPlayerOnRocket = false;
	static public bool bPlayerOnShootP2 = false;
	static public bool bPlayerOnRocketP2 = false;
	static public bool bPlayerOnRocketP1 = false;
	public static bool bPlayerHitTaBan = false;
	private bool turnLeftPressDown = false;
	private bool turnRightPressDown = false;
	private bool huizhongCGQ = false;
	private bool stopUrgentY = false;
	static public bool stopUrgent = false;

	private int openPCVRFlag = 0;
	private System.IntPtr gTestHidPtr;

	static private pcvr Instance = null;

	public static int dianboState = 0;	//0 - stop; 1- huizhong; 2 - run
	public static int dianboFudu = 0;
	private int dianboValue = 0;
	public static int qinangStateLeft = 2;	//0 - ping; 1 - chongqi; 2 - fangqi
	public static int qinangStateRight = 2;	//0 - ping; 1 - chongqi; 2 - fangqi
	public static int qinangStateUp = 2;	//0 - ping; 1 - chongqi; 2 - fangqi
	public static int qinangStateDown = 2;	//0 - ping; 1 - chongqi; 2 - fangqi
	public static int qinangState5 = 2;	//0 - ping; 1 - chongqi; 2 - fangqi
	public static int qinangState6 = 2;	//0 - ping; 1 - chongqi; 2 - fangqi
	public static int gunShakeLevel = 0;
	public bool gunShakeOpen = false;

	private int IsOpenStartLightP1 = 2;	//2-close; 3-flash	0xaa 0xbb
	private int IsOpenStartLightP2 = 2;	//2-close; 3-flash	0xaa 0xbb

	public static bool jiasuChuanganqi = false;	//left and right jiasu chuanganqi
	private bool p1AnquandaiPress = false;
	private bool p2AnquandaiPress = false;
	public bool p1AnquandaiOpen = false;
	public bool p2AnquandaiOpen = false;

	private byte WriteHead_1 = 0x02;
	private byte WriteHead_2 = 0x55;
	private byte WriteEnd_1 = 0x0d;
	private byte WriteEnd_2 = 0x0a;
	
	static byte ReadHead_1 = 0x01;
	static byte ReadHead_2 = 0x55;
	
	private byte[]bufferSendTemp5;
	
	private string CHEN = "";		//0-ch	5-en
	static public int chenNum = 0;	//0-ch	5-en

	static public pcvr GetInstance()
	{
		if(Instance == null)
		{
			GameObject obj = new GameObject("_PCVR");
			DontDestroyOnLoad(obj);
			Instance = obj.AddComponent<pcvr>();
			Instance.InitPcvr();
		}
		return Instance;
	}

	void Start()
	{
		//InitJiaoYanMiMa();

		bufferSendQinang = new byte[4];

		for (int i=0; i<4; i++)
		{
			bufferSendQinang[i] = 0x00;
		}

		bufferSendTemp5 = new byte[8];

		readAfterGameset ();

		jiaoyanTotalTime = jiaoyanTime;
		stopJiaoyan = false;
	}

	public void readAfterGameset()
	{
		//POS Info
		posXMin = PlayerPrefs.GetInt("posXMin");
		posXMax = PlayerPrefs.GetInt("posXMax");
		posYMin = PlayerPrefs.GetInt("posYMin");
		posYMax = PlayerPrefs.GetInt("posYMax");
		//int tempT = 0;

		if (posXMin == posXMax)
		{
			posXMin = 1;
			posXMax = 2;
		}
		
		if (posYMin == posYMax)
		{
			posYMin = 1;
			posYMax = 2;
		}

		/*if (posXMin > posXMax)
		{
			tempT = posXMin;
			posXMin = posXMax;
			posXMax = tempT;
		}

		tempT = 0;

		if (posYMin > posYMax)
		{
			tempT = posYMin;
			posYMin = posYMax;
			posYMax = tempT;
		}*/
		Debug.Log(posXMin + " "+ posXMax + " " + posYMin + " " + posYMax);
		string aa = "";
		aa = ReadGameInfo.GetInstance().ReadShake();
		gunShakeLevel = Convert.ToInt32 (aa);
		Debug.Log ("gunShakeLevel   = " + gunShakeLevel);

		if (gunShakeLevel <= 0)
		{
			gunShakeLevel = 0;
		}
		else
		{
			gunShakeLevel += 15;
		}
		
		string bb = "";
		bb = ReadGameInfo.GetInstance().ReadDianji();
		dianboFudu = Convert.ToInt32 (bb);
		Debug.Log ("dianboFudu   = " + dianboFudu);
		if (dianboFudu <= 0)
		{
			dianboFudu = 0;
		}

		stopUrgent = false;
		
		if (UIController.GetInstance()!=null)
		{
			UIController.GetInstance().OpenDonggan();
		}

		qinangStateLeft = 2;
		qinangStateRight = 2;
		qinangStateUp = 2;
		qinangStateDown = 2;
		qinangState5 = 2;
		qinangState6 = 2;
		p1AnquandaiPress = false;
		p2AnquandaiPress = false;
		p1AnquandaiOpen = false;
		p2AnquandaiOpen = false;
		gunShakeOpen = false;
		bZhendong = false;
		
		string cc = "";
		cc = ReadGameInfo.GetInstance().ReadAnquandai();
		if (cc == "close" || pcvr.bIsAutoMoveSelf)
		{
			openAanquandai = false;
			
			p1AnquandaiOpen = true;
			p2AnquandaiOpen = true;
		}
		else
		{
			openAanquandai = true;
		}

		CHEN = ReadGameInfo.GetInstance ().ReadCHEN();
		if (CHEN == "EN")
		{
			chenNum = 5;
		}
		else
		{
			chenNum = 0;
		}

		CancelInvoke("zhendongA");
		CancelInvoke("zhendongB");
		CancelInvoke("zhendongC");
		CancelInvoke("zhendongD");
	}

	void InitPcvr () {

		if(!bIsHardWare)
		{
			return;
		}
		createPCVR();
	}

	float lastUpTime;
	float dTime;
	// Update is called once per frame
	void Update()
	{
		if (!bIsHardWare || MyCOMDevice.ComThreadClass.IsLoadingLevel)
		{
			return;
		}

		/*if (!stopJiaoyan)
		{
			jiaoyanTotalTime -= Time.deltaTime;

		    if (jiaoyanTotalTime <= 0)
			{
				StartJiaoYanIO();
				jiaoyanTotalTime = jiaoyanTime + (float)UnityEngine.Random.Range(2, 10);
			}
		}*/
		
		dTime = Time.realtimeSinceStartup - lastUpTime;
		if (IsJiaoYanHid) {
			if (dTime < 0.1f) {
				return;
			}
		}
		else {
			if (dTime < 0.03f) {
				return;
			}
		}
		lastUpTime = Time.realtimeSinceStartup;

		sendMessage();
		GetMessage();
	}

	bool createPCVR()
	{
		MyCOMDevice.GetInstance ();
		openPCVRFlag = 1;
		return true;
	}

	void sendMessage()
	{
		if (openPCVRFlag != 1 || !MyCOMDevice.IsFindDeviceDt)
		{
			return;
		}

		if (openPCVRFlag == 1)
		{
			byte []bufferSend = new byte[MyCOMDevice.ComThreadClass.BufLenWrite];
			bufferSend[0] = WriteHead_1;
			bufferSend[1] = WriteHead_2;
			bufferSend[2] = 0;
			bufferSend[3] = 0;
			bufferSend[MyCOMDevice.ComThreadClass.BufLenWrite - 2] = WriteEnd_1;
			bufferSend[MyCOMDevice.ComThreadClass.BufLenWrite - 1] = WriteEnd_2;

			for(int i = 4; i < MyCOMDevice.ComThreadClass.BufLenWrite - 2; i++)
			{
				bufferSend[i] = (byte)UnityEngine.Random.Range(0x00, 0xff);
			}

			if (IsSubPlayerCoin)
			{
				bufferSend[2] = 0xaa;
				bufferSend[3] = (byte)subCoinNum;
			}
			else
			{
				bufferSend[2] = 0x00;
			}

			//4 --- not use
			bufferSend[4] = 0;

			for (int i=0; i<8; i++)
			{
				bufferSendTemp5[i] = 0;
			}

			if (!bZhendong)
			{
				if (!stopUrgent
				    && p1AnquandaiOpen
				    && (!PlayerController.IsKaiqiang || (PlayerController.IsKaiqiang && p2AnquandaiOpen)))
				{
					//7 - left qinang
					//qinangStateLeft = 0;	//0 - ping; 1 - chongqi; 2 - fangqi
					if (!PlayerController.inGame)
					{
						qinangStateLeft = 2;
						qinangStateRight = 2;
					}

					if (qinangStateLeft == 1 || qinangStateLeft == 11)
					{
						bufferSendTemp5[0] = 1;
						bufferSendTemp5[3] = 1;
					}
					else
					{
						bufferSendTemp5[0] = 0;
						bufferSendTemp5[3] = 0;
					}
					
					//8 - right qinang
					//qinangStateRight = 0;	//0 - ping; 1 - chongqi; 2 - fangqi
					if (qinangStateRight == 1 || qinangStateRight == 11)
					{
						bufferSendTemp5[1] = 1;
						bufferSendTemp5[2] = 1;
					}
					else
					{
						bufferSendTemp5[1] = 0;
						bufferSendTemp5[2] = 0;
					}
				}
				else
				{
					bufferSendTemp5[0] = 0;
					bufferSendTemp5[1] = 0;
					bufferSendTemp5[2] = 0;
					bufferSendTemp5[3] = 0;
				}
			}
			else
			{
				if (PlayerController.inGame && !stopUrgent
				    && p1AnquandaiOpen
				    && (!PlayerController.IsKaiqiang || (PlayerController.IsKaiqiang && p2AnquandaiOpen)))
				{
					bufferSendTemp5[0] = bufferSendQinang[0];
					bufferSendTemp5[1] = bufferSendQinang[1];
					bufferSendTemp5[2] = bufferSendQinang[2];
					bufferSendTemp5[3] = bufferSendQinang[3];
				}
				else
				{
					bufferSendTemp5[0] = 0;
					bufferSendTemp5[1] = 0;
					bufferSendTemp5[2] = 0;
					bufferSendTemp5[3] = 0;
				}
			}
			
			//6-5 - 1P start light
			// 2-close; 3-flash	0xaa 0xbb
			if(IsOpenStartLightP1 == 3)
			{
				bufferSendTemp5[6] = 1;
			}
			else if(IsOpenStartLightP1 == 2)
			{
				bufferSendTemp5[6] = 0;
			}
			
			//6-6 -2P start light
			if(IsOpenStartLightP2 == 3)
			{
				bufferSendTemp5[7] = 1;
			}
			else if(IsOpenStartLightP2 == 2)
			{
				bufferSendTemp5[7] = 0;
			}
			
			bufferSend[5] = (byte)(bufferSendTemp5[0] + bufferSendTemp5[1] * 2 + bufferSendTemp5[2] * 4 + bufferSendTemp5[3] * 8
			                       + bufferSendTemp5[4] * 16 + bufferSendTemp5[5] * 32 +bufferSendTemp5[6] * 64 + bufferSendTemp5[7] * 128);
			
			//7 - random value

			//8 -- gun shake level ------------ can record first and set value here
			if (gunShakeOpen)
			{
				bufferSend[8] = (byte) gunShakeLevel;
			}
			else
			{
				bufferSend[8] = 0x00;
			}
			//PlayerController.speed = 5.0f;
			//change           fffffffffffffffffffff lxy
			//9- dianboState -- 0 - stop; 1- huizhong; 2 - run
			
			if (dianboState == 3)
			{
				bufferSend[9] = (byte)(dianboFudu);
			}
			else if (!stopUrgent
			    && p1AnquandaiOpen
			    && (!PlayerController.IsKaiqiang || (PlayerController.IsKaiqiang && p2AnquandaiOpen)))
			{//Debug.Log("speeeeeeeeeeeeeeeeee "+ dianboState + " " + PlayerController.speed + " " + dianboFudu);
				
				if (PlayerController.speed > 0 && dianboFudu > 0)
				{
					dianboState = 2;
					
					if (dianboFudu < 4)
					{
						dianboValue = (int)((PlayerController.speed / 21.3f) * 6.0f);
					}
					else if(dianboFudu < 7)
					{
						dianboValue = (int)((PlayerController.speed / 21.3f) * 6.0f + 5);
					}
					else
					{
						dianboValue = (int)((PlayerController.speed / 21.3f) * 6.0f + 9);
					}
					
					if (dianboValue > 15)
					{
						dianboValue = 15;
					}
				}
				else
				{
					dianboState = 0;
				}
				
				//9 - dianbo value
				if (dianboState == 0)
				{
					bufferSend[9] = 0x0;
				}
				else if (dianboState == 1)
				{
					bufferSend[9] = 0xf1;
				}
				else if (dianboState == 2)
				{
					bufferSend[9] = (byte)(dianboValue);
				}
			}
			else
			{
				bufferSend[9] = 0x00;
			}
			//Debug.Log("speeeeeeeeeeeeeeeeee "+ dianboState + " " + bufferSend[9] + " " + dianboFudu);
			if (IsJiaoYanHid) {
				for (int i = 0; i < 4; i++) {
					bufferSend[i + 10] = JiaoYanMiMa[i];
				}
				
				for (int i = 0; i < 4; i++) {
					bufferSend[i + 14] = JiaoYanDt[i];
				}
			}
			else {
				RandomJiaoYanMiMaVal();
				for (int i = 0; i < 4; i++) {
					bufferSend[i + 10] = JiaoYanMiMaRand[i];
				}
				
				//0x41 0x42 0x43 0x44
				for (int i = 15; i < 18; i++) {
					bufferSend[i] = (byte)UnityEngine.Random.Range(0x00, 0x40);
				}
				bufferSend[14] = 0x00;
				
				for (int i = 15; i < 18; i++) {
					bufferSend[14] ^= bufferSend[i];
				}
			}
			
			//6 - 2~11 ^ value
			bufferSend[6] = 0x00;

			for (int i=2; i<12; i++)
			{
				bufferSend[6] ^= bufferSend[i];
			}

			//18 20 - random value

			//19 ^(2- 18, 20)
			bufferSend[19] = 0x00;

			for (int i = 2; i < (MyCOMDevice.ComThreadClass.BufLenWrite - 2); i++)
			{
				if (i == 19) 
				{
					continue;
				}

				bufferSend[19] ^= bufferSend[i];
			}

			MyCOMDevice.ComThreadClass.WriteByteMsg = bufferSend;
		}
	}

	public void GetMessage()
	{
		if (!MyCOMDevice.ComThreadClass.IsReadComMsg) {
			return;
		}

		if (MyCOMDevice.ComThreadClass.IsReadMsgComTimeOut) {
			return;
		}
		
		if (MyCOMDevice.ComThreadClass.ReadByteMsg.Length < (MyCOMDevice.ComThreadClass.BufLenRead - MyCOMDevice.ComThreadClass.BufLenReadEnd)) {
			Debug.Log("ReadBufLen was wrong! len is "+MyCOMDevice.ComThreadClass.ReadByteMsg.Length);
			return;
		}
		
		/*if ((MyCOMDevice.ComThreadClass.ReadByteMsg[22]&0x01) == 0x01) {
			JiOuJiaoYanCount++;
			if (JiOuJiaoYanCount >= JiOuJiaoYanMax && !IsJiOuJiaoYanFailed) {
				IsJiOuJiaoYanFailed = true;
				//JiOuJiaoYanFailed
				Debug.Log("j o fail edddddddddddddd");
				jiaoyanFailDOSTH(1);
			}
		}*/
		
		/*byte tmpVal = 0x00;
		string testA = "";
		for (int i = 2; i < (MyCOMDevice.ComThreadClass.BufLenRead - 4); i++) {
			if (i == 8 || i == 21) {
				continue;
			}
			testA += MyCOMDevice.ComThreadClass.ReadByteMsg[i].ToString("X2") + " ";
			tmpVal ^= MyCOMDevice.ComThreadClass.ReadByteMsg[i];
		}
		tmpVal ^= EndRead_1;
		tmpVal ^= EndRead_2;
		testA += EndRead_1 + " ";
		testA += EndRead_2 + " ";
		
		if (tmpVal != MyCOMDevice.ComThreadClass.ReadByteMsg[21]) {
			if (MyCOMDevice.ComThreadClass.IsStopComTX) {
				Debug.Log("jiaoyan3 fail");
				return;
			}
			MyCOMDevice.ComThreadClass.IsStopComTX = true;
			//ScreenLog.Log("testA: "+testA);
			//ScreenLog.LogError("tmpVal: "+tmpVal.ToString("X2")+", byte[21] "+MyCOMDevice.ComThreadClass.ReadByteMsg[21].ToString("X2"));
			//ScreenLog.Log("byte21 was wrong!");
			return;
		}
		
		if (IsJiaoYanHid) {
			tmpVal = 0x00;
			for (int i = 11; i < 14; i++) {
				tmpVal ^= MyCOMDevice.ComThreadClass.ReadByteMsg[i];
			}
			
			if (tmpVal == MyCOMDevice.ComThreadClass.ReadByteMsg[10]) {
				bool isJiaoYanDtSucceed = false;
				Debug.Log("jiaoyan1 suce");
				tmpVal = 0x00;
				for (int i = 15; i < 18; i++) {
					tmpVal ^= MyCOMDevice.ComThreadClass.ReadByteMsg[i];
				}
				Debug.Log("jiaoyan val  " + tmpVal + " " + MyCOMDevice.ComThreadClass.ReadByteMsg[14] + " " + (JiaoYanDt[1]&0xef) + " " + MyCOMDevice.ComThreadClass.ReadByteMsg[15]);
				Debug.Log("jiaoyan val2  " + (JiaoYanDt[2]&0xfe) + " " + MyCOMDevice.ComThreadClass.ReadByteMsg[16]);
				Debug.Log("jiaoyan val3  " + (JiaoYanDt[3]|0x28) + " " + MyCOMDevice.ComThreadClass.ReadByteMsg[17]);
				//校验2...
				if ( tmpVal == MyCOMDevice.ComThreadClass.ReadByteMsg[14]
				    && (JiaoYanDt[1]&0xef) == MyCOMDevice.ComThreadClass.ReadByteMsg[15]
				    && (JiaoYanDt[2]&0xfe) == MyCOMDevice.ComThreadClass.ReadByteMsg[16]
				    && (JiaoYanDt[3]|0x28) == MyCOMDevice.ComThreadClass.ReadByteMsg[17] ) {
					isJiaoYanDtSucceed = true;Debug.Log("jiaoyan2 suc");
				}
				
				if (isJiaoYanDtSucceed) {
					//JiaMiJiaoYanSucceed
					OnEndJiaoYanIO(JIAOYANENUM.SUCCEED);
				}
			}
		}
		*/
		int len = MyCOMDevice.ComThreadClass.ReadByteMsg.Length;
		uint[] readMsg = new uint[len];
		for (int i = 0; i < len; i++) {
			readMsg[i] = MyCOMDevice.ComThreadClass.ReadByteMsg[i];
		}
		keyProcess( readMsg );
		MyCOMDevice.ComThreadClass.IsReadComMsg = false;
	}

	//FanXiangJiaoZhun
	public void InitFangXiangJiaoZhun(int index)
	{
		switch(index)
		{
		case 0:
			posXMin = gunX;
			PlayerPrefs.SetInt("posXMin", posXMin);
			break;
		case 1:
			posYMax = gunY;
			PlayerPrefs.SetInt("posYMax", posYMax);
			break;
		case 2:
			posXMax = gunX;
			PlayerPrefs.SetInt("posXMax", posXMax);
			break;
		case 3:
			posYMin = gunY;
			PlayerPrefs.SetInt("posYMin", posYMin);
			break;
		}
		
		//int tempT = 0;

		if (index == 3)
		{
			if (posXMin == posXMax)
			{
				posXMin = 1;
				posXMax = 2;
			}
			
			if (posYMin == posYMax)
			{
				posYMin = 1;
				posYMax = 2;
			}
			
			/*if (posXMin > posXMax)
			{
				tempT = posXMin;
				posXMin = posXMax;
				posXMax = tempT;
			}
			
			tempT = 0;
			
			if (posYMin > posYMax)
			{
				tempT = posYMin;
				posYMin = posYMax;
				posYMax = tempT;
			}*/
			Debug.Log("jiaozhun " + posXMin + " "+ posXMax + " " + posYMin + " " + posYMax);
		}
	}

	uint CongJiState;
	uint CongJiStateCount;
	int gunX = 0;
	int gunY = 0;
	int gunXT = 0;
	int gunYT = 0;

	public void keyProcess(uint []bufferKey)
	{
		if (!MyCOMDevice.IsFindDeviceDt) {
			return;
		}
		
		if (bufferKey[0] != ReadHead_1 || bufferKey[1] != ReadHead_2) {
			return;
		}

		if (openPCVRFlag != 1)
		{
			return;
		}

		//41 - 42 - 19 - 20 gun - post
		gunX = (int)(( (bufferKey[2] & 0x0f) << 8 ) + bufferKey[3]); //gun x - high and low
		gunY = (int)(( (bufferKey[4] & 0x0f) << 8 ) + bufferKey[5]); //gun y - high and low

		gunXT = gunX;
		gunYT = gunY;

		if (gunXT > posXMax && gunXT > posXMin)
		{
			if (posXMax > posXMin)
			{
				gunXT = posXMax;
			}
			else
			{
				gunXT = posXMin;
			}
		}
		else if (gunXT < posXMax && gunXT < posXMin)
		{
			if (posXMax > posXMin)
			{
				gunXT = posXMin;
			}
			else
			{
				gunXT = posXMax;
			}
		}

		if (posXMin == posXMax)
		{
			posXMax = posXMin + 1;
		}
		
		if (posYMin == posYMax)
		{
			posYMax = posYMin + 1;
		}

		if (gunYT > posYMax && gunYT > posYMin)
		{
			if (posYMax > posYMin)
			{
				gunYT = posYMax;
			}
			else
			{
				gunYT = posYMin;
			}
		}
		else if (gunYT < posYMax && gunYT < posYMin)
		{
			if (posYMax > posYMin)
			{
				gunYT = posYMin;
			}
			else
			{
				gunYT = posYMax;
			}
		}

		if (CtrlForHardWare.GetInstance()!=null)
		{
			CtrlForHardWare.GetInstance().OnMoveShootCursor((float)Mathf.Abs(gunXT - posXMin) / Mathf.Abs(posXMax - posXMin), (float)Mathf.Abs(gunYT - posYMin) / Mathf.Abs(posYMax - posYMin));
		}

		CoinCurPcvr = bufferKey[6];
		if (IsSubPlayerCoin) {
			IsSubPlayerCoin = false;
		}
		else {
			if (CoinCurPcvr > 0)
			{
				mOldCoinNum += CoinCurPcvr;
				CoinCurGame = (int)mOldCoinNum;
				
				SubPcvrCoin((int)CoinCurPcvr);
				
				if (CtrlForHardWare.GetInstance()!=null)
				{
					CtrlForHardWare.GetInstance().OnClickInsertBt();
				}
			}
		}

		//3 - setPanel selectBt
		if( !bSetEnterKeyDown && (bufferKey[9] & 0x02) == 0x02 )
		{
			bSetEnterKeyDown = true;
		}
		else if ( bSetEnterKeyDown && (bufferKey[9] & 0x02) == 0x00 )
		{
			bSetEnterKeyDown = false;

			if (CtrlForHardWare.GetInstance()!=null)
			{
				CtrlForHardWare.GetInstance().OnClickSetBt();
			}
		}
		
		//4 - setPanel moveBt
		if ( !bSetMoveKeyDown && (bufferKey[9] & 0x04) == 0x04 )
		{
			bSetMoveKeyDown = true;
		}
		else if( bSetMoveKeyDown && (bufferKey[9] & 0x04) == 0x00 )
		{
			bSetMoveKeyDown = false;
			if (CtrlForHardWare.GetInstance()!=null)
			{
				CtrlForHardWare.GetInstance().OnClickMoveBt();
			}
		}

		//8-7 - huizhong chuanganqi
		if ( !huizhongCGQ && (bufferKey[8] & 0x80) == 0x80 )
		{
			huizhongCGQ = true;
		}
		else if( huizhongCGQ && (bufferKey[8] & 0x80) == 0x00 )
		{
			huizhongCGQ = false;

			//add infor here -- lxy
			
			if (SetPanel.GetInstance())
			{
				SetPanel.GetInstance().OnClickOtherButtonInPanel();
			}
		}
		
		//8 - stop urgent
		if (!stopUrgentY && (bufferKey[9] & 0x01) == 0x01 )
		{
			stopUrgentY = true;

			if (!stopUrgent && (!p1AnquandaiOpen || (PlayerController.IsKaiqiang && !p2AnquandaiOpen)))
			{
				Debug.Log("stop urgent do nothing!");
			}
			else
			{
				stopUrgent = !stopUrgent;
				
				if (stopUrgent && UIController.GetInstance()!=null)
				{
					UIController.GetInstance().CloseDonggan();
					UIController.GetInstance().openLeAnquandaiP1(false);
					UIController.GetInstance().openLeAnquandaiP2(false);
				}
				else if(!stopUrgent && UIController.GetInstance()!=null)
				{
					if (!p1AnquandaiOpen)
					{
						UIController.GetInstance().closeLeAnquandaiP1();
					}
					
					if (PlayerController.IsKaiqiang && !p2AnquandaiOpen)
					{
						UIController.GetInstance().closeLeAnquandaiP2();
					}
				}
				
				if (!stopUrgent && UIController.GetInstance()!=null)
				{
					UIController.GetInstance().OpenDonggan();
				}
			}
		}
		else if( stopUrgentY && (bufferKey[9] & 0x01) == 0x00 )
		{
			stopUrgentY = false;
			
			if (SetPanel.GetInstance())
			{
				SetPanel.GetInstance().OnClickOtherButtonInPanel();
			}
		}
		//ffffffffffffffffff lxy change
		if (!p1AnquandaiPress && (bufferKey[8] & 0x01) == 0x01)
		{
			p1AnquandaiPress = true;
			p1AnquandaiOpen = true;
			
			if (Loading.GetInstance())
			{
				Loading.GetInstance().openLeAnquandaiP1();
			}
			
			if (!stopUrgent && UIController.GetInstance())
			{
				if (!PlayerController.IsKaiqiang || (PlayerController.IsKaiqiang && p2AnquandaiOpen))
				{
					UIController.GetInstance().openLeAnquandaiP1(true);
				}
				else
				{
					UIController.GetInstance().openLeAnquandaiP1(false);
				}
			}
		}
		else if (p1AnquandaiPress && (bufferKey[8] & 0x01) == 0x00)
		{
			p1AnquandaiPress = false;
			p1AnquandaiOpen = false;

			if (!openAanquandai)
			{
				p1AnquandaiOpen = true;
			}

			if (SetPanel.GetInstance())
			{
				SetPanel.GetInstance().OnClickOtherButtonInPanel();
			}
		}
		
		if (!stopUrgent && !p1AnquandaiOpen && UIController.GetInstance())
		{
			UIController.GetInstance().closeLeAnquandaiP1();
		}
		//fffffffffffffffffffffff lxy change
		if (!p2AnquandaiPress  && (bufferKey[8] & 0x02) == 0x02)
		{
			p2AnquandaiPress = true;
			p2AnquandaiOpen = true;
			
			if (Loading.GetInstance())
			{
				Loading.GetInstance().openLeAnquandaiP2();
			}
			
			if (!stopUrgent && UIController.GetInstance())
			{
				if (p1AnquandaiOpen)
				{
					UIController.GetInstance().openLeAnquandaiP2(true);
				}
				else
				{
					UIController.GetInstance().openLeAnquandaiP2(false);
				}
			}
		}
		else if (p2AnquandaiPress && (bufferKey[8] & 0x02) == 0x00)
		{
			p2AnquandaiPress = false;
			p2AnquandaiOpen = false;
			
			if (!openAanquandai)
			{
				p2AnquandaiOpen = true;
			}

			if (SetPanel.GetInstance())
			{
				SetPanel.GetInstance().OnClickOtherButtonInPanel();
			}
		}
		
		if (!stopUrgent && !p2AnquandaiOpen && UIController.GetInstance() && PlayerController.IsKaiqiang)
		{
			UIController.GetInstance().closeLeAnquandaiP2();
		}
		
		//30 - check congJi move state ?????????????? lxy ---- bian pin qi tingzhi biaoji
		if((bufferKey[9] & 0x08) == 0x08)
		{
			//moving
		}
		else if((bufferKey[9] & 0x08) == 0x00)
		{
			//stop moving
		}
		
		//37 - turn left
		if (!turnLeftPressDown && (bufferKey[8] & 0x04) == 0x00)
		{
			turnLeftPressDown = true;
			if (CtrlForHardWare.GetInstance()!=null)
			{
				CtrlForHardWare.GetInstance().OnClickMoveLeft();
			}
		}
		else if (turnLeftPressDown && (bufferKey[8] & 0x04) == 0x00)
		{
			if ((Application.loadedLevel - chenNum == 2 || Application.loadedLevel - chenNum == 4) && !bZhendong)
			{
				qinangStateRight = 11;
			}
		}
		else if (turnLeftPressDown && (bufferKey[8] & 0x04) == 0x04)
		{
			turnLeftPressDown = false;
			qinangStateRight = 12;
		}
		
		//38 - turn right
		if (!turnRightPressDown && (bufferKey[8] & 0x08) == 0x00)
		{
			turnRightPressDown = true;
			if (CtrlForHardWare.GetInstance()!=null)
			{
				CtrlForHardWare.GetInstance().OnClickMoveRight();
			}
			
			if ((Application.loadedLevel - chenNum == 2 || Application.loadedLevel - chenNum == 4) && !bZhendong)
			{
				qinangStateLeft = 11;
			}
		}
		else if (turnRightPressDown && (bufferKey[8] & 0x08) == 0x00)
		{
			if ((Application.loadedLevel - chenNum == 2 || Application.loadedLevel - chenNum == 4) && !bZhendong)
			{
				qinangStateLeft = 11;
			}
		}
		else if (turnRightPressDown && (bufferKey[8] & 0x08) == 0x08)
		{
			turnRightPressDown = false;
			qinangStateLeft = 12;
		}
		
		//shache
		if (turnLeftPressDown && turnRightPressDown)
		{
			//shache here
			m_IsShache = true;
			if (ZhujuemaController.GetInstance()!=null && PlayerController.GetInstance()!=null)
			{
				ZhujuemaController.GetInstance().OnClickShacheBt();
				PlayerController.GetInstance().OnClickShacheBt();
			}
			if (CtrlForHardWare.GetInstance()!=null)
			{
				CtrlForHardWare.GetInstance().OnClickShacheBt();
			}
		}
		else
		{
			m_IsShache = false;
		}

		if (!bPlayerStartKeyDownP1 && (bufferKey[8] & 0x10) == 0x10)
		{
			bPlayerStartKeyDownP1 = true;
		}
		else if (bPlayerStartKeyDownP1 && (bufferKey[8] & 0x10) == 0x00)
		{
			bPlayerStartKeyDownP1 = false;
			if (CtrlForHardWare.GetInstance()!=null)
			{
				CtrlForHardWare.GetInstance().OnClickBeginBt1P();
			}
		}

		//40 - shooting
		if( (bufferKey[8] & 0x20) == 0x20 )
		{
			if(PlayerShoot.GetInstance() != null)
			{
				PlayerShoot.GetInstance().OnClickFire();
			}
			if(SetPanel.GetInstance()!=null && !bPlayerOnRocket)
			{
				bPlayerOnRocket = true;
				SetPanel.GetInstance().OnClickFireBt();
			}

			//2p start button chang ffffffffffffffffffffffffffffffffffffff lxy
			if( !bPlayerStartKeyDownP2 )
			{
				bPlayerStartKeyDownP2 = true;
			}
		}
		else if( (bufferKey[8] & 0x20) == 0x00 )
		{
			if(PlayerShoot.GetInstance() != null)
			{
				PlayerShoot.GetInstance().m_IsFire = false;
			}
			if(SetPanel.GetInstance()!=null)
			{
				bPlayerOnRocket = false;
			}

			//2p start button chang ffffffffffffffffffffffffffffffffffffff lxy
			if ( bPlayerStartKeyDownP2 )
			{
				bPlayerStartKeyDownP2 = false;
				if (CtrlForHardWare.GetInstance()!=null)
				{
					CtrlForHardWare.GetInstance().OnClickBeginBt2P();
				}
			}
		}

		//9-6 - chuan gan
		if( (bufferKey[9] & 0x40) == 0x40)
		{
			//speed
			jiasuChuanganqi = true;
		}
		else
		{
			jiasuChuanganqi = false;
		}
		
		//9-4 - 2pshooting 
		if(!bPlayerOnShootP2 && (bufferKey[9] & 0x10) == 0x10)
		{
			bPlayerOnShootP2 = true;
		}
		else if (bPlayerOnShootP2 && (bufferKey[9] & 0x10) == 0x00)
		{
			bPlayerOnShootP2 = false;

			if (SetPanel.GetInstance())
			{
				SetPanel.GetInstance().OnClickOtherButtonInPanel();
			}
		}
		
		//9-5 - 2p daodan 
		if(!bPlayerOnRocketP2 && (bufferKey[9] & 0x20) == 0x20)
		{
			bPlayerOnRocketP2 = true;
		}
		else if (bPlayerOnRocketP2 && (bufferKey[9] & 0x20) == 0x00)
		{
			bPlayerOnRocketP2 = false;
			
			if (SetPanel.GetInstance())
			{
				SetPanel.GetInstance().OnClickOtherButtonInPanel();
			}
		}
		
		//8-6 - 1p daodan 
		if(!bPlayerOnRocketP1 && (bufferKey[8] & 0x40) == 0x40)
		{
			bPlayerOnRocketP1 = true;
		}
		else if (bPlayerOnRocketP1 && (bufferKey[8] & 0x40) == 0x00)
		{
			bPlayerOnRocketP1 = false;
			
			if (SetPanel.GetInstance())
			{
				SetPanel.GetInstance().OnClickOtherButtonInPanel();
			}
		}
	}
	
	void SubPcvrCoin(int subNum)
	{
		if (!bIsHardWare) {
			return;
		}
		IsSubPlayerCoin = true;
		subCoinNum = subNum;
	}
	
	//change        ffffffffffffffff           lxy
	public void SubPlayerCoin(int subNum)
	{
		if (!bIsHardWare) {
			return;
		}
		
		if (gOldCoinNum >= subNum) {
			gOldCoinNum = (uint)(gOldCoinNum - subNum);
		}
		else {
			if (mOldCoinNum == 0) {
				return;
			}
			
			subCoinNum = (int)(subNum - gOldCoinNum);
			
			mOldCoinNum -= (uint)subCoinNum;
			CoinCurGame = (int)mOldCoinNum;
			gOldCoinNum = 0;
		}
	}

	public void setLightStateP1(int state)
	{
		IsOpenStartLightP1 = state;	//2-close; 3-flash	0xaa 0xbb
	}
	
	public void setLightStateP2(int state)
	{
		IsOpenStartLightP2 = state;	//2-close; 3-flash	0xaa 0xbb
	}

	public bool getJiasu()
	{//jiasuChuanganqi = true;
		return jiasuChuanganqi;
	}

	public bool getTurnLeft()
	{
		return turnLeftPressDown;
	}
	
	public bool getTurnRight()
	{
		return turnRightPressDown;
	}

	public void zhendong()
	{
		if (bZhendong || !bIsHardWare)
		{
			return;
		}

		bZhendong = true;

		CancelInvoke("zhendongA");
		CancelInvoke("zhendongB");
		CancelInvoke("zhendongC");
		CancelInvoke("zhendongD");

		bufferSendQinang [0] = 1;
		bufferSendQinang [1] = 0;
		bufferSendQinang [2] = 0;
		bufferSendQinang [3] = 0;

		CancelInvoke("zhendongA");
		Invoke("zhendongA", 0.18f);
	}

	void zhendongA()
	{
		bufferSendQinang [0] = 0;
		bufferSendQinang [1] = 0;
		bufferSendQinang [2] = 1;
		bufferSendQinang [3] = 0;
		
		CancelInvoke("zhendongB");
		Invoke("zhendongB", 0.18f);
	}
	
	void zhendongB()
	{
		bufferSendQinang [0] = 0;
		bufferSendQinang [1] = 1;
		bufferSendQinang [2] = 0;
		bufferSendQinang [3] = 0;
		
		CancelInvoke("zhendongC");
		Invoke("zhendongC", 0.18f);
	}
	
	void zhendongC()
	{
		bufferSendQinang [0] = 0;
		bufferSendQinang [1] = 0;
		bufferSendQinang [2] = 0;
		bufferSendQinang [3] = 1;
		
		CancelInvoke("zhendongD");
		Invoke("zhendongD", 0.18f);
	}
	
	void zhendongD()
	{
		bufferSendQinang [0] = 0;
		bufferSendQinang [1] = 0;
		bufferSendQinang [2] = 0;
		bufferSendQinang [3] = 0;

		bZhendong = false;
	}

	void closeDevice()
	{
		if (openPCVRFlag == 1)
		{
			openPCVRFlag = 2;
		}
	}

	//about jiaoyan
	static void RandomJiaoYanDt()
	{	
		for (int i = 1; i < 4; i++) {
			JiaoYanDt[i] = (byte)UnityEngine.Random.Range(0x00, 0x7b);
		}
		JiaoYanDt[0] = 0x00;
		for (int i = 1; i < 4; i++) {
			JiaoYanDt[0] ^= JiaoYanDt[i];
		}
	}
	
	public void StartJiaoYanIO()
	{
		if (IsJiaoYanHid) {
			return;
		}
		
		if (/*!HardWareTest.IsTestHardWare*/true) {
			if (JiaoYanSucceedCount >= JiaoYanFailedMax) {
				stopJiaoyan = true;
				jiaoyanFailDOSTH(5);
				return;
			}
			
			if (JiaoYanState == JIAOYANENUM.FAILED && JiaoYanFailedCount >= JiaoYanFailedMax) {
				stopJiaoyan = true;
				jiaoyanFailDOSTH(10);
				return;
			}
		}
		RandomJiaoYanDt();
		IsJiaoYanHid = true;
		CancelInvoke("CloseJiaoYanIO");
		Invoke("CloseJiaoYanIO", 5f);
	}
	
	void CloseJiaoYanIO()
	{
		if (!IsJiaoYanHid) {
			return;
		}
		IsJiaoYanHid = false;
		OnEndJiaoYanIO(JIAOYANENUM.FAILED);
		
		/*if (HardWareTest.IsTestHardWare) {
			HardWareTest.Instance.JiaMiJiaoYanFailed();
		}*/
	}
	
	void OnEndJiaoYanIO(JIAOYANENUM val)
	{
		IsJiaoYanHid = false;
		if (IsInvoking("CloseJiaoYanIO")) {
			CancelInvoke("CloseJiaoYanIO");
		}
		
		switch (val) {
		case JIAOYANENUM.FAILED:
			JiaoYanFailedCount++;
			
			jiaoyanFailDOSTH(2);
			break;
			
		case JIAOYANENUM.SUCCEED:
			JiaoYanSucceedCount++;
			JiaoYanFailedCount = 0;
			/*if (HardWareTest.IsTestHardWare) {
				HardWareTest.Instance.JiaMiJiaoYanSucceed();
			}*/

			if (JiaoYanSucceedCount >= JiaoYanFailedMax)
			{
				stopJiaoyan = true;
				jiaoyanFailDOSTH(5);
			}

			break;
		}
		JiaoYanState = val;
		//Debug.Log("*****JiaoYanState "+JiaoYanState);
		
		if (JiaoYanFailedCount >= JiaoYanFailedMax || IsJiOuJiaoYanFailed) {
			//JiaoYanFailed
			if (IsJiOuJiaoYanFailed) {
				//JiOuJiaoYanFailed
				//Debug.Log("JOJYSB...");
			}
			else {
				//JiaMiXinPianJiaoYanFailed
				//Debug.Log("JMXPJYSB...");
				IsJiaMiJiaoYanFailed = true;
			}
		}
	}
	public static bool IsJiaMiJiaoYanFailed;
	
	enum JIAOYANENUM
	{
		NULL,
		SUCCEED,
		FAILED,
	}
	static JIAOYANENUM JiaoYanState = JIAOYANENUM.NULL;
	static byte JiaoYanFailedMax = 0x03;
	static byte JiaoYanSucceedCount;
	static byte JiaoYanFailedCount;
	static byte[] JiaoYanDt = new byte[4];
	static byte[] JiaoYanMiMa = new byte[4];
	static byte[] JiaoYanMiMaRand = new byte[4];
	
	//#define First_pin			 	0xe5
	//#define Second_pin		 	0x5d
	//#define Third_pin		 		0x8c
	void InitJiaoYanMiMa()
	{
		JiaoYanMiMa[1] = 0xe5; //0xff;
		JiaoYanMiMa[2] = 0x5d; //0xff;
		JiaoYanMiMa[3] = 0x8c; //0xff;
		JiaoYanMiMa[0] = 0x00;
		for (int i = 1; i < 4; i++) {
			JiaoYanMiMa[0] ^= JiaoYanMiMa[i];
		}
	}
	
	void RandomJiaoYanMiMaVal()
	{
		for (int i = 0; i < 4; i++) {
			JiaoYanMiMaRand[i] = (byte)UnityEngine.Random.Range(0x00, (JiaoYanMiMa[i] - 1));
		}
		
		byte TmpVal = 0x00;
		for (int i = 1; i < 4; i++) {
			TmpVal ^= JiaoYanMiMaRand[i];
		}
		
		if (TmpVal == JiaoYanMiMaRand[0]) {
			JiaoYanMiMaRand[0] = JiaoYanMiMaRand[0] == 0x00 ?
				(byte)UnityEngine.Random.Range(0x01, 0xff) : (byte)(JiaoYanMiMaRand[0] + UnityEngine.Random.Range(0x01, 0xff));
		}
	}

	byte JiOuJiaoYanCount;
	byte JiOuJiaoYanMax = 5;
	public static bool IsJiOuJiaoYanFailed;
	byte EndRead_1 = 0x41;
	byte EndRead_2 = 0x42;
	public static bool IsJiaoYanHid;

	void jiaoyanFailDOSTH(int index)
	{
		Debug.Log("faillllll " + Time.time + " " + index);

		if (index == 10)
		{
			//do sth here
		}
	}
}
