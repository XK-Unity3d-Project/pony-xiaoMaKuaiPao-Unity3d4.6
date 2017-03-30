using UnityEngine;
using System.Collections;
using System;

public class CtrlForHardWare : MonoBehaviour 
{
	public static CtrlForHardWare Instance;
//	public int m_SenceNum = 0;
//	public int m_InsertNum = 0;
//	public int m_CoinNunStar = 0;
//	public int m_GameMode = 1;
//	public int m_GameDifficulty = 2;
//	public int m_TicketSet = 5;
//	public int m_TicketRate = 2;
	//	public int m_IsOutTickets = 1;

	void Update () 
	{
//		int SenceNum = ReadloadedLevel();
//		if(SenceNum!=3)
//		{
//			pcvr.GetInstance().setFengShanInfo(0,0);
//			pcvr.GetInstance().setFengShanInfo(0,1);
//		}

		if(pcvr.m_IsShache)
		{
			OnClickShacheBt();
		}
	}
	public static CtrlForHardWare GetInstance()
	{
		if(Instance == null)
		{
			GameObject obj = new GameObject("_CtrlForHardWare");
			DontDestroyOnLoad(obj);
			Instance = obj.AddComponent<CtrlForHardWare>();
		}
		return Instance;
	}	


	//全局响应投币按键消息
	public void OnClickInsertBt()
	{
		int SenceNum = Application.loadedLevel - pcvr.chenNum;
		switch(SenceNum)
		{
		case 0:   //循环动画
		{
			OnClickInsertBtInMoview();
		}
			break;
		case 1:  //投币界面
		{
			OnClickInsertBtInToubi();
		}
			break;
		case 2:  //游戏界面
		{
			OnClickInsertBtInGame();
		}
			break;
		case 3:  //设置界面
		{
			SetPanel.GetInstance().OnClickInsertBt();
		}
			break;
		case 4:  //游戏界面
		{
			OnClickInsertBtInGame();
		}
			break;
		}
	}

	//全局响应1P开始按键消息
	public void OnClickBeginBt1P()
	{
		int SenceNum = Application.loadedLevel - pcvr.chenNum;
		switch(SenceNum)
		{
		case 0:   //循环动画
		{
			OnClickBeginBtInMoview();
		}
			break;
		case 1:   //投币界面
		{
			OnClickBeginBt1PInToubi();
		}
			break;
		case 2:   //游戏1界面
		{
			OnClickBeginBt1PInGame();
		}
			break;
		case 3:   //设置界面
		{
			OnClickStartButtonP1InPanel();
		}
			break;
		case 4:   //游戏2界面
		{
			OnClickBeginBt1PInGame();
		}
			break;
		}
	}

	//全局响应2P开始按键消息
	public void OnClickBeginBt2P()
	{
		int SenceNum = Application.loadedLevel - pcvr.chenNum;
		switch(SenceNum)
		{
		case 1:   //投币界面
		{
			OnClickBeginBt2PInToubi();
		}
			break;
		case 2:   //游戏界面
		{
			OnClickBeginBt2PInGame();
		}
			break;
		case 3:   //设置界面
		{
			OnClickStartButtonP2InPanel();
		}
			break;
		case 4:   //游戏界面
		{
			OnClickBeginBt2PInGame();
		}
			break;
		}
	}

	//全局响应设置按键
	public void OnClickSetBt()
	{
		int SenceNum = Application.loadedLevel - pcvr.chenNum;
		switch(SenceNum)
		{
		case 3: //设置界面
			OnClickSetBtInSetPanel();
			break;
		default:
			Application.LoadLevel(3 + pcvr.chenNum);
			break;
		}
	}

	//全局响应移动按键
	public void OnClickMoveBt()
	{
		int SenceNum = Application.loadedLevel - pcvr.chenNum;
		switch(SenceNum)
		{
		case 3: //设置界面
			OnClickMoveBtSetPanel();
			break;
		default:
			break;
		}
	}

	public void OnClickMoveLeft()
	{
		int SenceNum = Application.loadedLevel - pcvr.chenNum;
		switch(SenceNum)
		{
		case 1: //投币界面
			OnClickLeftBtInToubi();
			break;
		case 2: //游戏1界面
			OnClickMoveLeftBtInGame();
			break;
		case 3: //设置界面
			SetPanel.GetInstance().OnMoveLeftBt();
			break;
		case 4: //游戏2界面
			OnClickMoveLeftBtInGame();
			break;
		default:
			break;
		}
	}

	public void OnClickMoveRight()
	{
		int SenceNum = Application.loadedLevel - pcvr.chenNum;
		switch(SenceNum)
		{
		case 1: //投币界面
			OnClickRightBtInToubi();
			break;
		case 2: //游戏1界面
			OnClickMoveRightBtInGame();
			break;
		case 3: //设置界面
			SetPanel.GetInstance().OnMoveRightBt();
			break;
		case 4: //游戏2界面
			OnClickMoveRightBtInGame();
			break;
		default:
			break;
		}
	}

	public void OnClickShacheBt()
	{
		int SenceNum = Application.loadedLevel - pcvr.chenNum;
		switch(SenceNum)
		{
		case 2: //游戏1界面
			OnClickShacheBtInGame();
			break;
//		case 3: //设置界面
//			OnClickLeftBtInToubi();
//			break;
		case 4: //游戏2界面
			OnClickShacheBtInGame();
			break;
		default:
			break;
		}
	}

	void OnClickInsertBtInMoview() //循环动画界面响应投币按键
	{
		if(MovieTexturePlay.GetInstance().GameMode == "oper")
		{
			MovieTexturePlay.GetInstance().OnClickInsertBt();
		}
	}
	void OnClickBeginBtInMoview() //循环动画界面响应开始按键1P
	{
		if(MovieTexturePlay.GetInstance().GameMode == "free")
		{
			Application.LoadLevel(1 + pcvr.chenNum);
		}
	}


	void OnClickInsertBtInToubi() //投币界面响应投币按键
	{
		if(Loading.GetInstance().GameMode == "oper")
		{
			Loading.GetInstance().OnClickInsertBt();
		}
	}
	void OnClickBeginBt1PInToubi() //投币界面响应开始按键1P
	{
		Loading.GetInstance().OnClickBeginBt1P();
	}
	void OnClickBeginBt2PInToubi()//投币界面响应开始按键2P
	{
		Loading.GetInstance().OnClickBeginBt2P();
	}
	void OnClickLeftBtInToubi()
	{
		Loading.GetInstance().OnClickLeftBt();
	}
	void OnClickRightBtInToubi()
	{
		Loading.GetInstance().OnClickRightBt();
	}

	void OnClickInsertBtInGame() //游戏界面响应投币按键
	{
		UIController.GetInstance().OnClickInsertBt();
	}
	void OnClickBeginBt1PInGame() //游戏界面响应1P开始按键
	{
		UIController.GetInstance().OnClickBeginBt1P();
	}
	void OnClickBeginBt2PInGame() //游戏界面响应2P开始按键
	{
		UIController.GetInstance().OnClickBeginBt2P();
	}
	void OnClickMoveLeftBtInGame()
	{
		PlayerController.GetInstance().OnClickLeftBt();
	}
	void OnClickMoveRightBtInGame()
	{
		PlayerController.GetInstance().OnClickRightBt();
	}
	void OnClickShacheBtInGame()
	{
		PlayerController.GetInstance().OnClickShacheBt();
	}
	void OnClickSetBtInSetPanel()//设置界面响应设置按键
	{
		SetPanel.GetInstance().OnClickSelectBt();
	}

	void OnClickMoveBtSetPanel() //设置界面响应移动按键
	{
		SetPanel.GetInstance().OnClickMoveBt();
	}
//	public int GetShiWeiNum()
//	{
//		ReadInsertNum();
//		int num = 0;
//		if(m_InsertNum>=10)
//		{
//			num = m_InsertNum/10;
//		}
//		return num;
//	}
//	public int GetGeWeiNum()
//	{
//		int num = GetShiWeiNum();
//		if(num>0)
//		{
//			num = m_InsertNum -num*10;
//		}
//		else
//		{
//			num = m_InsertNum;
//		}
//		return num;
//	}
	void OnPushTabanInPanel(uint TabanNum)
	{
//		if(SetPanel.GetInstance().IsTest)
//		{
//			SetPanel.GetInstance().SetAjustBt(TabanNum);
//		}
	}
	void OnClickStartButtonP1InPanel()
	{
		SetPanel.GetInstance().OnClickStartButtonP1InPanel();
	}
	void OnClickStartButtonP2InPanel()
	{
		SetPanel.GetInstance().OnClickStartButtonP2InPanel();
	}
	void OnPushTabanInGame(float percent)
	{
		//Debug.Log("percent percent percent percent percent percent" + percent);
//		Controller.GetInstance().DoAnimator(percent);
	}

	public void OnMoveShootCursor(float x,float y)
	{
		if(PlayerController.GetInstance()!=null)
		{
			PlayerController.GetInstance().OnMoveShootCursor(x,y);
		}

		if (SetPanel.GetInstance() != null)
		{
			SetPanel.GetInstance().OnMoveShootCursor(x,y);
		}
	}
}
