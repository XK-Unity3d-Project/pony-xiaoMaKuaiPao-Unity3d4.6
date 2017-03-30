using UnityEngine;
using System.Collections;
using System.Threading;

public enum NpcType
{
	NormalNpc,
	AddTimeNpc
}
public enum NpcKinds
{
	normal,
	yeren,
	mixed
}
public enum NpcState
{
	idle,
	run,
	run1,
	fire
}
public class NpcController : MonoBehaviour 
{
	public GameObject myEmptyObject;
	public GameObject myAniamlParent = null;
	public NpcType npcType = NpcType.NormalNpc;
	public NpcKinds npcKinds = NpcKinds.normal;
	public NpcState npcState = NpcState.idle;
	public int NpcNum = 0;
	public Transform NormalPathPoint = null;
	public GameObject AddTimeObject;
	public GameObject[] NormalNpcObject;
	public GameObject NormalAttackObject;
	public float NpcSpeed;
	public int PathNumIndex = 0;
	private Animator[] NormalNpcAnimator;
	private Animator NormalAttackAnimator;
	public bool IsEnterTrigger = false;
	private Transform[] CreatPoint;
	private int[] PointRecord;

	void Start () 
	{
		if (NpcNum > 3)
		{
			NpcNum = 2;
		}
		else
		{
			NpcNum = 1;
		}

		if(npcType == NpcType.NormalNpc)
		{
			if(transform.childCount > 0)
			{
				CreatPoint = new Transform[transform.childCount];
				for(int i=0;i<transform.childCount;i++)
				{
					CreatPoint[i] = transform.FindChild((i+1).ToString());
				}
			}
			myAniamlParent = Instantiate(myEmptyObject,transform.position,transform.rotation) as GameObject;
			PointRecord = new int[transform.childCount];
			if(npcKinds == NpcKinds.normal || npcKinds == NpcKinds.yeren)
			{
				int index = Random.Range(0,transform.childCount-1);
				NormalNpcAnimator = new Animator[NpcNum];
				for(int i=0;i<NpcNum;i++)
				{
					if(index >= transform.childCount-1)
					{
						index = -1;
					}
					index++;
					PointRecord[i] = index;
					int Normalindex = 0;
					if(NormalNpcObject.Length > 1)
					{
						Normalindex = Random.Range(0,NormalNpcObject.Length);
					}
					GameObject temp = Instantiate(NormalNpcObject[Normalindex],CreatPoint[PointRecord[i]].position,CreatPoint[PointRecord[i]].transform.rotation) as GameObject;
					temp.transform.parent = myAniamlParent.transform;
					NormalNpcAnimator[i] = temp.GetComponent<Animator>();
					if(npcState == NpcState.idle)
					{
						if(i ==1)
						{
							NormalNpcAnimator[i].SetBool("Isroot2",true);
						}
						else if(i ==2)
						{
							NormalNpcAnimator[i].SetBool("Isroot3",true);
						}
						else
						{
							int k = Random.Range(0,3);
							if(k==1)
							{
								NormalNpcAnimator[i].SetBool("Isroot2",true);
							}
							else if(k==2)
							{
								NormalNpcAnimator[i].SetBool("Isroot3",true);
							}
						}
						float randnum = Random.Range(0.6f,1.5f);
						NormalNpcAnimator[i].speed =  randnum;
					}
					else if(npcState == NpcState.run)
					{
						if(i == 0)
						{
							NormalNpcAnimator[i].SetBool("Isrun",true);
						}
						else if(i ==1)
						{
							NormalNpcAnimator[i].SetBool("Isrun1",true);
						}
						else if(i ==2)
						{
							NormalNpcAnimator[i].SetBool("Isrun2",true);
						}
						else if (i ==3 && npcKinds == NpcKinds.normal)
						{
							NormalNpcAnimator[i].SetBool("Isrun3",true);
						}
						else
						{
							int k = Random.Range(0,4);
							if(k==0)
							{
								NormalNpcAnimator[i].SetBool("Isrun",true);
							}
							else if(k==1)
							{
								NormalNpcAnimator[i].SetBool("Isrun1",true);
							}
							else if(k==2)
							{
								NormalNpcAnimator[i].SetBool("Isrun2",true);
							}
							else if(k==3 && npcKinds == NpcKinds.normal)
							{
								NormalNpcAnimator[i].SetBool("Isrun3",true);
							}
							else
							{
								NormalNpcAnimator[i].SetBool("Isrun",true);
							}
						}
						if(NormalPathPoint!=null)
						{
							NormalWaringCollier warn =  myAniamlParent.GetComponent<NormalWaringCollier>();
							warn.PathPoint = NormalPathPoint;
							warn.speed = NpcSpeed;
							float randnum = Random.Range(0.6f,1.5f);
							NormalNpcAnimator[i].speed =  randnum;
						}	
						else
						{
							Debug.Log("No PathPoint!");
						}
					}
					else if(npcState == NpcState.fire)
					{
						if(i == 0)
						{
							NormalNpcAnimator[i].SetBool("IsFire",true);
						}
						if(i ==1)
						{
							NormalNpcAnimator[i].SetBool("IsFire2",true);
						}
						else
						{
							int k = Random.Range(0,2);
							if(k == 0)
							{
								NormalNpcAnimator[i].SetBool("IsFire",true);
							}
							else if(k==1)
							{
								NormalNpcAnimator[i].SetBool("IsFire2",true);
							}
							else
							{
								NormalNpcAnimator[i].SetBool("IsFire",true);
							}
						float randnum = Random.Range(1.0f,1.5f);
						NormalNpcAnimator[i].speed =  randnum;
						}
					}
				}					
			}
			else if(npcKinds == NpcKinds.mixed)
			{
				NormalNpcAnimator = new Animator[2];
				for(int i=0;i<2;i++)
				{
					if(i == 0)
					{
						GameObject temp = Instantiate(NormalNpcObject[0],CreatPoint[i].position,CreatPoint[i].transform.rotation) as GameObject;
						temp.transform.parent = myAniamlParent.transform;
						NormalNpcAnimator[i] = temp.GetComponent<Animator>();
					}
					else
					{
						GameObject temp = Instantiate(NormalAttackObject,CreatPoint[i].position,CreatPoint[i].transform.rotation) as GameObject;
						temp.transform.parent = myAniamlParent.transform;
						NormalNpcAnimator[i] = temp.GetComponent<Animator>();
					}
					NormalNpcAnimator[i].SetBool("Isrun3",true);
					if(NormalPathPoint!=null)
					{
						NormalWaringCollier warn =  myAniamlParent.GetComponent<NormalWaringCollier>();
						warn.PathPoint = NormalPathPoint;
						warn.speed = NpcSpeed;
					}	
				}
			}
		}
		else if(npcType == NpcType.AddTimeNpc)
		{
			//Debug.Log("jiashi jiashi jiashi jiashi jiashi");
			GameObject temp = Instantiate(AddTimeObject,transform.position,transform.rotation) as GameObject;
			AddTimmerNpc npc = temp.GetComponent<AddTimmerNpc>();
			npc.PathNumIndex = PathNumIndex;
		}
	}

	void LateUpdate()
	{
		if(IsEnterTrigger)
		{
			if(myAniamlParent == null)
			{
				if (NpcNum == 3)
				{
					NpcNum = 2;
				}
				else if (NpcNum > 3)
				{
					NpcNum = Mathf.CeilToInt(NpcNum / 2);
				}

				//Debug.Log("2ci");
				if(npcType == NpcType.NormalNpc)
				{
					if(transform.childCount > 0)
					{
						CreatPoint = new Transform[transform.childCount];
						for(int i=0;i<transform.childCount;i++)
						{
							CreatPoint[i] = transform.FindChild((i+1).ToString());
						}
					}
					myAniamlParent = Instantiate(myEmptyObject,transform.position,transform.rotation) as GameObject;
					PointRecord = new int[transform.childCount];
					if(npcKinds == NpcKinds.normal || npcKinds == NpcKinds.yeren)
					{
						int index = Random.Range(0,transform.childCount-1);
						NormalNpcAnimator = new Animator[NpcNum];
						for(int i=0;i<NpcNum;i++)
						{
							if(index >= transform.childCount-1)
							{
								index = -1;
							}
							index++;
							PointRecord[i] = index;
							int Normalindex = 0;
							if(NormalNpcObject.Length > 1)
							{
								Normalindex = Random.Range(0,NormalNpcObject.Length);
							}
							GameObject temp = Instantiate(NormalNpcObject[Normalindex],CreatPoint[PointRecord[i]].position,CreatPoint[PointRecord[i]].transform.rotation) as GameObject;
							temp.transform.parent = myAniamlParent.transform;
							NormalNpcAnimator[i] = temp.GetComponent<Animator>();
							if(npcState == NpcState.idle)
							{
								if(i ==1)
								{
									NormalNpcAnimator[i].SetBool("Isroot2",true);
								}
								else if(i ==2)
								{
									NormalNpcAnimator[i].SetBool("Isroot3",true);
								}
								else
								{
									int k = Random.Range(0,3);
									if(k==1)
									{
										NormalNpcAnimator[i].SetBool("Isroot2",true);
									}
									else if(k==2)
									{
										NormalNpcAnimator[i].SetBool("Isroot3",true);
									}
								}
								float randnum = Random.Range(0.6f,1.5f);
								NormalNpcAnimator[i].speed =  randnum;
							}
							else if(npcState == NpcState.run)
							{
								if(i == 0)
								{
									NormalNpcAnimator[i].SetBool("Isrun",true);
								}
								else if(i ==1)
								{
									NormalNpcAnimator[i].SetBool("Isrun1",true);
								}
								else if(i ==2)
								{
									NormalNpcAnimator[i].SetBool("Isrun2",true);
								}
								else if (i ==3)
								{
									NormalNpcAnimator[i].SetBool("Isrun3",true);
								}
								else
								{
									int k = Random.Range(0,4);
									if(k==0)
									{
										NormalNpcAnimator[i].SetBool("Isrun",true);
									}
									else if(k==1)
									{
										NormalNpcAnimator[i].SetBool("Isrun1",true);
									}
									else if(k==2)
									{
										NormalNpcAnimator[i].SetBool("Isrun2",true);
									}
									else if(k==3)
									{
										NormalNpcAnimator[i].SetBool("Isrun3",true);
									}
									else
									{
										NormalNpcAnimator[i].SetBool("Isrun",true);
									}
								}
								if(NormalPathPoint!=null)
								{
									NormalWaringCollier warn =  myAniamlParent.GetComponent<NormalWaringCollier>();
									warn.PathPoint = NormalPathPoint;
									warn.speed = NpcSpeed;
									float randnum = Random.Range(0.6f,1.5f);
									NormalNpcAnimator[i].speed =  randnum;
								}	
								else
								{
									Debug.Log("No PathPoint!");
								}
							}
							else if(npcState == NpcState.fire)
							{
								NormalNpcAnimator[i].SetBool("IsFire",true);
								float randnum = Random.Range(2.2f,2.5f);
								NormalNpcAnimator[i].speed =  randnum;
							}
						}					
					}
					else if(npcKinds == NpcKinds.mixed)
					{
						NormalNpcAnimator = new Animator[2];
						for(int i=0;i<2;i++)
						{
							if(i == 0)
							{
								GameObject temp = Instantiate(NormalNpcObject[0],CreatPoint[i].position,CreatPoint[i].transform.rotation) as GameObject;
								temp.transform.parent = myAniamlParent.transform;
								NormalNpcAnimator[i] = temp.GetComponent<Animator>();
							}
							else
							{
								GameObject temp = Instantiate(NormalAttackObject,CreatPoint[i].position,CreatPoint[i].transform.rotation) as GameObject;
								temp.transform.parent = myAniamlParent.transform;
								NormalNpcAnimator[i] = temp.GetComponent<Animator>();
							}
							NormalNpcAnimator[i].SetBool("Isrun3",true);
							if(NormalPathPoint!=null)
							{
								NormalWaringCollier warn =  myAniamlParent.GetComponent<NormalWaringCollier>();
								warn.PathPoint = NormalPathPoint;
								warn.speed = NpcSpeed;
							}	
						}
					}
				}
				else if(npcType == NpcType.AddTimeNpc)
				{
					GameObject temp = Instantiate(AddTimeObject,transform.position,transform.rotation) as GameObject;
					AddTimmerNpc npc = temp.GetComponent<AddTimmerNpc>();
					npc.PathNumIndex = PathNumIndex;
				}
			}
		}
		IsEnterTrigger = false;
	}
}
