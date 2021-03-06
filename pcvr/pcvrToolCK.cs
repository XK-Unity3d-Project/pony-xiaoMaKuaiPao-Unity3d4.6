using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using SLAB_HID_DEVICE;
using System;
using System.IO;

public class pcvrToolCK : MonoBehaviour {
	
	static public bool bIsHardWare = true;
	private string fileName = "xiaoMaConfig.xml";
	private HandleJson handleJsonObj;
	private float jiaoyanTotalTime = 0.0f;
	private const float jiaoyanTime = 5.0f;
	public bool stopJiaoyan = false;

	//private float mTimeOpenPCVR = 0f;

	static public uint gOldCoinNum = 0;
	private uint mOldCoinNum = 0;
	private int SubCoinNum = 0;
	public static uint CoinCurPcvr;
	private bool IsCleanHidCoin;
	static public bool bIsTouBiBtDown = false;

	void ResetIsTouBiBtDown()
	{
		if(!bIsTouBiBtDown)
		{
			return;
		}
		bIsTouBiBtDown = false;
	}

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

	static private pcvrToolCK Instance = null;

	//private int dianboState = 0;	//0 - stop; 1- huizhong; 2 - run
	public static int dianboFudu = 0;
	public static int qinangStateLeft = 2;	//0 - ping; 1 - chongqi; 2 - fangqi
	public static int qinangStateRight = 2;	//0 - ping; 1 - chongqi; 2 - fangqi
	public static int qinangStateUp = 2;	//0 - ping; 1 - chongqi; 2 - fangqi
	public static int qinangStateDown = 2;	//0 - ping; 1 - chongqi; 2 - fangqi
	public static int qinangState5 = 2;	//0 - ping; 1 - chongqi; 2 - fangqi
	public static int qinangState6 = 2;	//0 - ping; 1 - chongqi; 2 - fangqi
	public static int qinangStateLeftTwo = 2;	//0 - ping; 1 - chongqi; 2 - fangqi
	public static int qinangStateRightTwo = 2;	//0 - ping; 1 - chongqi; 2 - fangqi
	public static int qinangStateShake = 2;	//0 - ping; 1 - chongqi; 2 - fangqi
	public static int gunShakeLevel = 0;

	private int IsOpenStartLightP1 = 2;	//2-close; 3-flash	0xaa 0xbb
	private int IsOpenStartLightP2 = 2;	//2-close; 3-flash	0xaa 0xbb

	private bool jiasuChuanganqi = false;	//left and right jiasu chuanganqi
	private bool p1AnquandaiPress = false;
	private bool p2AnquandaiPress = false;

	private byte WriteHead_1 = 0x02;
	private byte WriteHead_2 = 0x55;
	private byte WriteEnd_1 = 0x0d;
	private byte WriteEnd_2 = 0x0a;
	
	//private byte []bufferSendTest;
	private byte[]bufferSendTemp5;
	private byte []bufferSendQinang;
	public static bool bZhendong = false;

	static public pcvrToolCK GetInstance()
	{
		if(Instance == null)
		{
			GameObject obj = new GameObject("_PCVRTOOLCK");
			DontDestroyOnLoad(obj);
			Instance = obj.AddComponent<pcvrToolCK>();
			Instance.InitPcvr();
		}
		return Instance;
	}

	void Start()
	{
		//InitJiaoYanMiMa();
		handleJsonObj = HandleJson.GetInstance ();
		//bufferSendTest = new byte[MyCOMDevice.ComThreadClass.BufLenWrite];
		bufferSendTemp5 = new byte[8];
		
		bufferSendQinang = new byte[4];
		
		for (int i=0; i<4; i++)
		{
			bufferSendQinang[i] = 0x00;
		}

		readAfterGameset ();
		createPCVR();
		
		initAllInforFirst ();
		
		jiaoyanTotalTime = jiaoyanTime;
		stopJiaoyan = false;
	}

	public void readAfterGameset()
	{
		string aa = "";
		aa = handleJsonObj.ReadFromFileXml(fileName, "START_SHAKE");
		if (aa == null || aa == "")
		{
			handleJsonObj.WriteToFileXml(fileName,"START_SHAKE","0");
			aa = handleJsonObj.ReadFromFileXml(fileName, "START_SHAKE");
		}
		gunShakeLevel = Convert.ToInt32 (aa);

		if (gunShakeLevel <= 0)
		{
			gunShakeLevel = 0;
		}
		
		string bb = "";
		bb = handleJsonObj.ReadFromFileXml(fileName, "START_DIANJI");
		if (bb == null || bb == "")
		{
			handleJsonObj.WriteToFileXml(fileName,"START_DIANJI","0");
			bb = handleJsonObj.ReadFromFileXml(fileName, "START_DIANJI");
		}
		dianboFudu = Convert.ToInt32 (bb);
		
		if (dianboFudu <= 0)
		{
			dianboFudu = 0;
		}

		stopUrgent = false;

		qinangStateLeft = 2;
		qinangStateRight = 2;
		qinangStateUp = 2;
		qinangStateDown = 2;
		qinangState5 = 2;
		qinangState6 = 2;
		qinangStateLeftTwo = 2;
		qinangStateRightTwo = 2;
		qinangStateShake = 2;
		p1AnquandaiPress = false;
		p2AnquandaiPress = false;
	}

	void InitPcvr () {

		if(!bIsHardWare)
		{
			return;
		}
		createPCVR();
	}

	//change        ffffffffffffffff           lxy
	public void SubPlayerCoin(int subNum)
	{
		//Debug.Log("SubPlayerCoin ---- num "+subNum);
		if (gOldCoinNum >= subNum) {
			gOldCoinNum = (uint)(gOldCoinNum - subNum);
		}
		else {
			SubCoinNum = (int)(subNum - gOldCoinNum);
			if (mOldCoinNum < SubCoinNum) {
				return;
			}
			//Debug.Log("mOldCoinNum "+mOldCoinNum+", SubCoinNum "+SubCoinNum);
			mOldCoinNum -= (uint)SubCoinNum;
			//GlobalData.CoinCur = (int)mOldCoinNum;
			gOldCoinNum = 0;
			setBizhi((int)mOldCoinNum);
		}
	}

	/*bool IsPlayerHit = false;

	void ResetPlayerHitState()
	{
		if(!IsPlayerHit)
		{
			return;
		}
		IsPlayerHit = false;
	}*/

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

		//if (openPCVRFlag != 0)
		//{
			sendMessage();
			GetMessage();
		//}
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

			if (IsCleanHidCoin)
			{
				bufferSend[2] = 0xaa;
				bufferSend[3] = 0x01;

				if (CoinCurPcvr == 0)
				{
					IsCleanHidCoin = false;
				}
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
			
			if (qinangStateLeft == 1)
			{
				bufferSendTemp5[3] = 1;
			}
			else
			{
				bufferSendTemp5[3] = 0;
			}

			if (qinangStateRight == 1)
			{
				bufferSendTemp5[1] = 1;
			}
			else
			{
				bufferSendTemp5[1] = 0;
			}
			
			if (qinangStateUp == 1)
			{
				bufferSendTemp5[0] = 1;
			}
			else
			{
				bufferSendTemp5[0] = 0;
			}
			
			if (qinangStateDown == 1)
			{
				bufferSendTemp5[2] = 1;
			}
			else
			{
				bufferSendTemp5[2] = 0;
			}
			
			if (qinangState5 == 1)
			{
				bufferSendTemp5[4] = 1;
			}
			else
			{
				bufferSendTemp5[4] = 0;
			}
			
			if (qinangState6 == 1)
			{
				bufferSendTemp5[5] = 1;
			}
			else
			{
				bufferSendTemp5[5] = 0;
			}

			if (qinangStateLeftTwo == 1)
			{
				bufferSendTemp5[0] = 1;
				bufferSendTemp5[3] = 1;
			}
			
			if (qinangStateRightTwo == 1)
			{
				bufferSendTemp5[1] = 1;
				bufferSendTemp5[2] = 1;
			}

			if (bZhendong)
			{
				bufferSendTemp5[0] = bufferSendQinang[0];
				bufferSendTemp5[1] = bufferSendQinang[1];
				bufferSendTemp5[2] = bufferSendQinang[2];
				bufferSendTemp5[3] = bufferSendQinang[3];
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
			bufferSend[8] = (byte) gunShakeLevel;
			
			//change           fffffffffffffffffffff lxy
			//9- dianboState -- 0 - stop; 1- huizhong; 2 - run
			if (dianboFudu < 16)
			{
				bufferSend[9] = (byte) dianboFudu;
			}
			else if (dianboFudu > 240)
			{
				bufferSend[9] = (byte) (dianboFudu - 240);
			}
			else
			{
				bufferSend[9] = 0x00;
			}
			
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
			}
		}
		//IsJiOuJiaoYanFailed = true; //test
		
		byte tmpVal = 0x00;
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
				return;
			}Debug.Log("21 failed");
			MyCOMDevice.ComThreadClass.IsStopComTX = true;
			//ScreenLog.Log("testA: "+testA);
			//ScreenLog.LogError("tmpVal: "+tmpVal.ToString("X2")+", byte[21] "+MyCOMDevice.ComThreadClass.ReadByteMsg[21].ToString("X2"));
			//ScreenLog.Log("byte21 was wrong!");
			return;
		}
		
		if (IsJiaoYanHid) {Debug.Log("jjjjjs");
			tmpVal = 0x00;
			for (int i = 11; i < 14; i++) {
				tmpVal ^= MyCOMDevice.ComThreadClass.ReadByteMsg[i];
			}
			
			if (tmpVal == MyCOMDevice.ComThreadClass.ReadByteMsg[10]) {Debug.Log("jjjjjs2222");
				bool isJiaoYanDtSucceed = false;
				
				tmpVal = 0x00;
				for (int i = 15; i < 18; i++) {
					tmpVal ^= MyCOMDevice.ComThreadClass.ReadByteMsg[i];
				}
				
				//鏍￠獙2...
				if ( tmpVal == MyCOMDevice.ComThreadClass.ReadByteMsg[14]
				    && (JiaoYanDt[1]&0xef) == MyCOMDevice.ComThreadClass.ReadByteMsg[15]
				    && (JiaoYanDt[2]&0xfe) == MyCOMDevice.ComThreadClass.ReadByteMsg[16]
				    && (JiaoYanDt[3]|0x28) == MyCOMDevice.ComThreadClass.ReadByteMsg[17] ) {Debug.Log("jjjjjs444444");
					isJiaoYanDtSucceed = true;
				}
				
				if (isJiaoYanDtSucceed) {Debug.Log("jjjjjs6666666");
					//JiaMiJiaoYanSucceed
					OnEndJiaoYanIO(JIAOYANENUM.SUCCEED);
				}
			}
		}*/
		
		int len = MyCOMDevice.ComThreadClass.ReadByteMsg.Length;
		uint[] readMsg = new uint[len];
		for (int i = 0; i < len; i++) {
			readMsg[i] = MyCOMDevice.ComThreadClass.ReadByteMsg[i];
		}

		keyProcess( readMsg );
	}

	uint CongJiState;
	uint CongJiStateCount;
	int gunX = 0;
	int gunY = 0;

	public void keyProcess(uint []bufferKey)
	{
		if (openPCVRFlag != 1)
		{
			return;
		}

		//41 - 42 - 19 - 20 gun - post
		gunX = (int)(( (bufferKey[2] & 0x0f) << 8 ) + bufferKey[3]); //gun x - high and low
		gunY = (int)(( (bufferKey[4] & 0x0f) << 8 ) + bufferKey[5]); //gun y - high and low

		posXY (gunX, gunX, gunY, gunY);

		//2 - game coinInfo ------------------ fffffffffffffffff  lxy
		CoinCurPcvr = bufferKey[6];
		if (CoinCurPcvr > 0)
		{
			if (!IsCleanHidCoin)
			{
				IsCleanHidCoin = true;
				mOldCoinNum += CoinCurPcvr;
				//GlobalData.CoinCur = (int)mOldCoinNum;
				setBizhi((int)mOldCoinNum);
			}
		}
		/*uint coinNum = bufferKey[6] + gOldCoinNum;
		if( mOldCoinNum != coinNum && )
		{
			if(!bIsTouBiBtDown)
			{
				bIsTouBiBtDown = true;
				Invoke("ResetIsTouBiBtDown", 1.0f);
			}
			mOldCoinNum = coinNum;
			setBizhi((int)coinNum);
		}*/

		//3 - setPanel selectBt
		if( !bSetEnterKeyDown && (bufferKey[9] & 0x02) == 0x02 )
		{
			bSetEnterKeyDown = true;
			showKeyPressInfor(7);
		}
		else if ( bSetEnterKeyDown && (bufferKey[9] & 0x02) == 0x00 )
		{
			bSetEnterKeyDown = false;
			showKeyPressInfor(8);
		}
		
		//4 - setPanel moveBt
		if ( !bSetMoveKeyDown && (bufferKey[9] & 0x04) == 0x04 )
		{
			bSetMoveKeyDown = true;
			showKeyPressInfor(9);
		}
		else if( bSetMoveKeyDown && (bufferKey[9] & 0x04) == 0x00 )
		{
			bSetMoveKeyDown = false;
			showKeyPressInfor(10);
		}

		//8-7 - huizhong chuanganqi
		if ( !huizhongCGQ && (bufferKey[8] & 0x80) == 0x80 )
		{
			huizhongCGQ = true;
			showKeyPressInfor(15);
		}
		else if( huizhongCGQ && (bufferKey[8] & 0x80) == 0x00 )
		{
			huizhongCGQ = false;
			showKeyPressInfor(16);

			//add infor here -- lxy
		}
		
		//8 - stop urgent
		if (!stopUrgentY && (bufferKey[9] & 0x01) == 0x01 )
		{
			stopUrgentY = true;
			stopUrgent = !stopUrgent;
			showKeyPressInfor(17);
		}
		else if( stopUrgentY && (bufferKey[9] & 0x01) == 0x00 )
		{
			stopUrgentY = false;
			showKeyPressInfor(18);
		}

		if (!p1AnquandaiPress && (bufferKey[8] & 0x01) == 0x01)
		{
			p1AnquandaiPress = true;
			showKeyPressInfor(19);
		}
		else if (p1AnquandaiPress && (bufferKey[8] & 0x01) == 0x00)
		{
			p1AnquandaiPress = false;
			showKeyPressInfor(20);
		}

		if (!p2AnquandaiPress  && (bufferKey[8] & 0x02) == 0x02)
		{
			p2AnquandaiPress = true;
			showKeyPressInfor(21);
		}
		else if (p2AnquandaiPress && (bufferKey[8] & 0x02) == 0x00)
		{
			p2AnquandaiPress = false;
			showKeyPressInfor(22);
		}
		
		//30 - check congJi move state ?????????????? lxy ---- bian pin qi tingzhi biaoji
		if((bufferKey[9] & 0x08) == 0x08)
		{
			//moving
			congji (true);
		}
		else if((bufferKey[9] & 0x08) == 0x00)
		{
			//stop moving
			congji (false);
		}
		
		//8 - 2 - turn left
		if (!turnLeftPressDown && (bufferKey[8] & 0x04) == 0x00)
		{
			turnLeftPressDown = true;
			showKeyPressInfor(11);
		}
		else if (turnLeftPressDown && (bufferKey[8] & 0x04) == 0x04)
		{
			turnLeftPressDown = false;
			showKeyPressInfor(12);
		}
		
		//8 - 3 - turn right
		if (!turnRightPressDown && (bufferKey[8] & 0x08) == 0x00)
		{
			turnRightPressDown = true;
			showKeyPressInfor(13);
		}
		else if (turnRightPressDown && (bufferKey[8] & 0x08) == 0x08)
		{
			turnRightPressDown = false;
			showKeyPressInfor(14);
		}

		//8-4 - 1p start button
		if(!bPlayerStartKeyDownP1 && (bufferKey[8] & 0x10) == 0x10)
		{
			bPlayerStartKeyDownP1 = true;
			showKeyPressInfor(1);
		}
		else if (bPlayerStartKeyDownP1 && (bufferKey[8] & 0x10) == 0x00)
		{
			bPlayerStartKeyDownP1 = false;
			showKeyPressInfor(2);
		}

		//8-5 - shooting - 2p start button
		if( !bPlayerOnRocket && (bufferKey[8] & 0x20) == 0x20 )
		{
			bPlayerOnRocket = true;
			showKeyPressInfor(5);
			showKeyPressInfor(3);
		}
		else if( bPlayerOnRocket && (bufferKey[8] & 0x20) == 0x00 )
		{
			bPlayerOnRocket = false;
			showKeyPressInfor(6);
			showKeyPressInfor(4);
		}

		//9-6 - chuan gan
		if( (bufferKey[9] & 0x40) == 0x40)
		{
			//speed
			jiasuChuanganqi = true;
			chuanganqiYou(true);
		}
		else
		{
			if (jiasuChuanganqi)
			{
				jiasuChuanganqi = false;
				chuanganqiYou(false);
			}
		}
		
		//9-4 - 2pshooting 
		if(!bPlayerOnShootP2 && (bufferKey[9] & 0x10) == 0x10)
		{
			bPlayerOnShootP2 = true;
			showKeyPressInfor(23);
		}
		else if (bPlayerOnShootP2 && (bufferKey[9] & 0x10) == 0x00)
		{
			bPlayerOnShootP2 = false;
			showKeyPressInfor(24);
		}
		
		//9-5 - 2p daodan 
		if(!bPlayerOnRocketP2 && (bufferKey[9] & 0x20) == 0x20)
		{
			bPlayerOnRocketP2 = true;
			showKeyPressInfor(25);
		}
		else if (bPlayerOnRocketP2 && (bufferKey[9] & 0x20) == 0x00)
		{
			bPlayerOnRocketP2 = false;
			showKeyPressInfor(26);
		}
		
		//8-6 - 1p daodan 
		if(!bPlayerOnRocketP1 && (bufferKey[8] & 0x40) == 0x40)
		{
			bPlayerOnRocketP1 = true;
			showKeyPressInfor(27);
		}
		else if (bPlayerOnRocketP1 && (bufferKey[8] & 0x40) == 0x00)
		{
			bPlayerOnRocketP1 = false;
			showKeyPressInfor(28);
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
	{
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

	void closeDevice()
	{
		if (openPCVRFlag == 1)
		{
			openPCVRFlag = 2;
			
			//lianjie(false);
		}
	}
	
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
		Debug.Log ("jiaoyanhereddddddddddddddddddddd");
		if (/*!HardWareTest.IsTestHardWare*/true) {
			if (JiaoYanSucceedCount >= JiaoYanFailedMax) {
				stopJiaoyan = true;
				return;
			}
			
			if (JiaoYanState == JIAOYANENUM.FAILED && JiaoYanFailedCount >= JiaoYanFailedMax) {
				stopJiaoyan = true;
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
			}
			//ffffffffffffffffffff  lxy
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
	static byte JiaoYanFailedMax = 0x01;
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




	
	
	void initAllInforFirst()
	{
		for (int i=0; i<toolsLabels.Length; i++)
		{
			toolsLabels [i].text = "";
		}

		dianjizhuandong (dianboFudu);
		
		gunShakeLevel = 0;
		jiqiangzhendong (gunShakeLevel);
	}
	
	public UILabel[] toolsButtonLabels;
	//0 - jianbi
	//1 - dianbo
	//2 - dianji
	//3 - qinangzuo
	//4 - qinangyou
	//5 - dengP1
	//6 - dengP2
	//7 - qiangzhendong
	//8 - qinangUp
	//9 - qinangDown
	
	//some on-click functions
	public void subCoinFun()
	{
		//sub
		SubPlayerCoin (1);
	}
	
	public void dianjiZhuansuFun()
	{
		//add
		//0x00-0x0f
		if (dianboFudu < 15)
		{
			dianboFudu ++;
		}
		else if (dianboFudu == 15)
		{
			dianboFudu = 241;
		}
		else if (dianboFudu < 249)
		{
			dianboFudu ++;
		}
		
		dianjizhuandong (dianboFudu);
	}
	
	public void dianjiZhuansuFunSub()
	{
		//add
		if (dianboFudu == 241)
		{
			dianboFudu = 15;
		}
		else if (dianboFudu > 0)
		{
			dianboFudu --;
		}
		
		dianjizhuandong (dianboFudu);
	}
	
	public void gunShakeLevelFun()
	{
		//add
		//0 - stop
		//0x10-0x1f 16-31
		if (gunShakeLevel == 0)
		{
			gunShakeLevel = 16;
		}
		else if (gunShakeLevel < 31)
		{
			gunShakeLevel ++;
		}
		
		jiqiangzhendong (gunShakeLevel);
	}
	
	public void gunShakeLevelFunSub()
	{
		//add
		//0 - stop
		//0x10-0x1f 16-31
		if (gunShakeLevel > 16)
		{
			gunShakeLevel --;
		}
		else if (gunShakeLevel == 16)
		{
			gunShakeLevel = 0;
		}
		
		jiqiangzhendong (gunShakeLevel);
	}
	
	public void qinangLeftFun()
	{
		//open close
		if (qinangStateLeft == 0 || qinangStateLeft == 2)
		{
			qinangStateLeft = 1;
			toolsButtonLabels[3].text = "打 开";
		}
		else if (qinangStateLeft == 1)
		{
			qinangStateLeft = 2;
			toolsButtonLabels[3].text = "关 闭";
		}
	}
	
	public void qinangRightFun()
	{
		//open close
		if (qinangStateRight == 0 || qinangStateRight == 2)
		{
			qinangStateRight = 1;
			toolsButtonLabels[4].text = "打 开";
		}
		else if (qinangStateRight == 1)
		{
			qinangStateRight = 2;
			toolsButtonLabels[4].text = "关 闭";
		}
	}
	
	public void qinangUpFun()
	{
		//open close
		if (qinangStateUp == 0 || qinangStateUp == 2)
		{
			qinangStateUp = 1;
			toolsButtonLabels[8].text = "打 开";
		}
		else if (qinangStateUp == 1)
		{
			qinangStateUp = 2;
			toolsButtonLabels[8].text = "关 闭";
		}
	}
	
	public void qinangDownFun()
	{
		//open close
		if (qinangStateDown == 0 || qinangStateDown == 2)
		{
			qinangStateDown = 1;
			toolsButtonLabels[9].text = "打 开";
		}
		else if (qinangStateDown == 1)
		{
			qinangStateDown = 2;
			toolsButtonLabels[9].text = "关 闭";
		}
	}
	
	public void qinang5Fun()
	{
		//open close
		if (qinangState5 == 0 || qinangState5 == 2)
		{
			qinangState5 = 1;
			toolsButtonLabels[10].text = "打 开";
		}
		else if (qinangState5 == 1)
		{
			qinangState5 = 2;
			toolsButtonLabels[10].text = "关 闭";
		}
	}
	
	public void qinang6Fun()
	{
		//open close
		if (qinangState6 == 0 || qinangState6 == 2)
		{
			qinangState6 = 1;
			toolsButtonLabels[11].text = "打 开";
		}
		else if (qinangState6 == 1)
		{
			qinangState6 = 2;
			toolsButtonLabels[11].text = "关 闭";
		}
	}

	public void qinangLeftTwo()
	{
		//open close
		if (qinangStateLeftTwo == 0 || qinangStateLeftTwo == 2)
		{
			qinangStateLeftTwo = 1;
		}
		else if (qinangStateLeftTwo == 1)
		{
			qinangStateLeftTwo = 2;
		}
	}

	public void qinangRightTwo()
	{
		//open close
		if (qinangStateRightTwo == 0 || qinangStateRightTwo == 2)
		{
			qinangStateRightTwo = 1;
		}
		else if (qinangStateRightTwo == 1)
		{
			qinangStateRightTwo = 2;
		}
	}

	public void qinangShake()
	{
		//open close
		if (qinangStateShake == 0 || qinangStateShake == 2)
		{
			qinangStateShake = 1;
			zhendong();
		}
		else if (qinangStateShake == 1)
		{
			qinangStateShake = 2;
		}
	}
	
	public void startLightP1Fun()
	{
		//open close flash
		if (IsOpenStartLightP1 == 0 || IsOpenStartLightP1 == 2)
		{
			IsOpenStartLightP1 = 3;
			toolsButtonLabels[5].text = "闪";
		}
		else if (IsOpenStartLightP1 == 3)
		{
			IsOpenStartLightP1 = 2;
			toolsButtonLabels[5].text = "灭";
		}
	}
	
	public void startLightP2Fun()
	{
		//open close flash
		if (IsOpenStartLightP2 == 0 || IsOpenStartLightP2 == 2)
		{
			IsOpenStartLightP2 = 3;
			toolsButtonLabels[6].text = "闪";
		}
		else if (IsOpenStartLightP2 == 3)
		{
			IsOpenStartLightP2 = 2;
			toolsButtonLabels[6].text = "灭";
		}
	}
	
	//private int dianboFudu = 1;
	//private int qinangStateLeft = 0;	//0 - ping; 1 - left; 2 - right
	//private int qinangStateRight = 0;	//0 - ping; 1 - left; 2 - right
	//private int  = 0;
	//press button information
	void showKeyPressInfor(int index)
	{
		switch(index)
		{
		case 1:
			toolsLabels [0].text = "1P开始键按下";
			break;
		case 2:
			toolsLabels [0].text = "1P开始键弹起";
			break;
		case 3:
			toolsLabels [0].text = "2P开始键按下";
			break;
		case 4:
			toolsLabels [0].text = "2P开始键弹起";
			break;
		case 5:
			toolsLabels [0].text = "射击键按下";
			break;
		case 6:
			toolsLabels [0].text = "射击键弹起";
			break;
		case 7:
			toolsLabels [0].text = "设置按键按下";
			break;
		case 8:
			toolsLabels [0].text = "设置按键弹起";
			break;
		case 9:
			toolsLabels [0].text = "移动按键按下";
			break;
		case 10:
			toolsLabels [0].text = "移动按键弹起";
			break;
		case 11:
			toolsLabels [0].text = "左转按下";
			break;
		case 12:
			toolsLabels [0].text = "左转弹起";
			break;
		case 13:
			toolsLabels [0].text = "右转按下";
			break;
		case 14:
			toolsLabels [0].text = "右转弹起";
			break;
		case 15:
			toolsLabels [0].text = "回中传感器按下";
			break;
		case 16:
			toolsLabels [0].text = "回中传感器弹起";
			break;
		case 17:
			toolsLabels [0].text = "紧急停止按键按下";
			break;
		case 18:
			toolsLabels [0].text = "紧急停止按键弹起";
			break;
		case 19:
			toolsLabels [0].text = "P1安全带按下";
			break;
		case 20:
			toolsLabels [0].text = "P1安全带弹起";
			break;
		case 21:
			toolsLabels [0].text = "P2安全带按下";
			break;
		case 22:
			toolsLabels [0].text = "P2安全带弹起";
			break;
		case 23:
			toolsLabels [0].text = "P2射击按下";
			break;
		case 24:
			toolsLabels [0].text = "P2射击弹起";
			break;
		case 25:
			toolsLabels [0].text = "P2导弹按下";
			break;
		case 26:
			toolsLabels [0].text = "P2导弹弹起";
			break;
		case 27:
			toolsLabels [0].text = "P1导弹按下";
			break;
		case 28:
			toolsLabels [0].text = "P1导弹弹起";
			break;
		default :
			break;
		}
	}
	
	public UILabel[] toolsLabels;
	//0 - anjian
	//1 - lianjie
	//2 - bizhi
	//3 - congji
	//4 - gao X
	//5 - di X
	//6 - gao Y
	//7 - di Y
	//8 - chuanganqi you
	//9 - chuanganqi zuo
	//10 - dianjizhuansu 17-31
	//11 - qiangzhendong 16-31
	
	void lianjie(bool flag)
	{
		if (flag)
		{
			toolsLabels [1].text = "连接成功";
		}
		else
		{
			toolsLabels [1].text = "连接失败";
		}
	}
	
	void setBizhi(int num)
	{
		toolsLabels [2].text = num.ToString();
	}
	
	void congji(bool flag)
	{
		if (flag)
		{
			toolsLabels [3].text = "运行";
		}
		else 
		{
			toolsLabels [3].text = "停止";
		}
	}
	
	void posXY(int XH, int XL, int YH, int YL)
	{
		toolsLabels [4].text = XH.ToString();
		//toolsLabels [5].text = XL.ToString();
		toolsLabels [6].text = YH.ToString();
		//toolsLabels [7].text = YL.ToString();
	}
	
	void chuanganqiYou(bool flag)
	{
		if (flag)
		{
			toolsLabels [8].text = "加速";
		}
		else 
		{
			toolsLabels [8].text = "未加速";
		}
	}
	
	void chuanganqiZuo(bool flag)
	{
		if (flag)
		{
			toolsLabels [9].text = "加速";
		}
		else 
		{
			toolsLabels [9].text = "未加速";
		}
	}
	
	void dianjizhuandong(int num)
	{
		toolsLabels [10].text = num.ToString("x2");
	}
	
	void jiqiangzhendong(int num)
	{
		toolsLabels [11].text =num.ToString("x2");
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
}
