using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
	[Serializable]
	public class ObjectPoolEntry
	{
		[SerializeField]
		public GameObject Prefab;

		[SerializeField]
		public int Count;

		[HideInInspector]
		public GameObject[] pool;

		[HideInInspector]
		public int objectsInPool;
	}

	private class PoolRow
	{
		public int rowID = -1;

		public int columnStartID = -1;

		public int columnEndID = -1;

		public Dictionary<int, GameObject> rowDict = new Dictionary<int, GameObject>();

		public GameObject parentObj;

		public bool isActive = true;

		public void SetActive(bool state)
		{
			isActive = state;
			parentObj.SetActive(state);
		}

		public void AddObj(int id, GameObject obj)
		{
			try
			{
				rowDict[id] = obj;
				obj.transform.parent = parentObj.transform;
			}
			catch (Exception)
			{
			}
		}

		public void removeObj(int id, GameObject obj)
		{
			rowDict.Remove(id);
		}
	}

	public static ObjectPool instance;

	private const int CHUNKS_IN_ROW = 21;

	private const int CHUNK_ROW_SIZE = 20;

	public int loadedprefabs;

	private ObjPoolEntry[,] Entries = new ObjPoolEntry[2, 1];

	protected GameObject ContainerAllFreeObjs;

	protected GameObject ContainerAllUsedObjs;

	protected GameObject[] ContainerFreeObjs = new GameObject[2];

	protected GameObject[] ContainerUsedObjs = new GameObject[2];

	private PoolRow[] ContainerRows = new PoolRow[8505];

	private prefabEntry[,] prefabList = new prefabEntry[2, 1];

	public static bool poolsCreated;

	public static int getContainerID(int row, int column)
	{
		return column / 20 + row * 21;
	}

	public bool IsContainerActive(int row, int column)
	{
		int containerID = getContainerID(row, column);
		return ContainerRows[containerID].isActive;
	}

	private void Awake()
	{
		instance = this;
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private void setupGeneralPrefabList()
	{
		prefabList[0, 0].name = "Prefabs/simpleSprite";
		prefabList[0, 0].max = 16000;
	}

	private void setupOrgPrefabList()
	{
		prefabList[1, 0].name = "Prefabs/simpleSprite";
		prefabList[1, 0].max = 4000;
	}

	private void setupPoolRows()
	{
		int num = ContainerRows.Length / 21;
		for (int i = 0; i < num; i++)
		{
			for (int j = 0; j < 21; j++)
			{
				ContainerRows[i * 21 + j] = new PoolRow();
				ContainerRows[i * 21 + j].parentObj = new GameObject();
				ContainerRows[i * 21 + j].parentObj.name = "Pool Row " + i + " Chunk " + j;
				ContainerRows[i * 21 + j].parentObj.transform.parent = ContainerAllUsedObjs.transform;
				ContainerRows[i * 21 + j].rowID = i;
				ContainerRows[i * 21 + j].columnStartID = j * 20;
				ContainerRows[i * 21 + j].columnEndID = (j + 1) * 20 - 1;
			}
		}
	}

	private void Start()
	{
		setupGeneralPrefabList();
		setupOrgPrefabList();
		loadPrefabListIntoObjectPoolStructures();
		createObjectPools();
		fillObjectPools();
		groupContainersUnderOneMaster();
		setupPoolRows();
		poolsCreated = true;
	}

	private void groupContainersUnderOneMaster()
	{
		for (int i = 0; i < 2; i++)
		{
			ContainerUsedObjs[i].transform.parent = ContainerAllUsedObjs.transform;
			ContainerFreeObjs[i].transform.parent = ContainerAllFreeObjs.transform;
		}
	}

	private void loadPrefabListIntoObjectPoolStructures()
	{
		loadedprefabs = 0;
		for (int i = 0; i < 2; i++)
		{
			for (int j = 0; j < 1; j++)
			{
				Entries[i, j].objectsInPool = 0;
				Entries[i, j].Count = prefabList[i, j].max;
				if (prefabList[i, j].max != 0)
				{
					UnityEngine.Object @object = Resources.Load(prefabList[i, j].name);
					if (!(@object == null))
					{
						Entries[i, j].Prefab = @object;
						loadedprefabs++;
					}
				}
			}
		}
	}

	private void createObjectPools()
	{
		ContainerAllFreeObjs = new GameObject("ObjectPool");
		ContainerAllUsedObjs = new GameObject("ObjectPoolActives");
		ContainerFreeObjs[0] = new GameObject("GeneralPool");
		ContainerUsedObjs[0] = new GameObject("GeneralPool");
		ContainerFreeObjs[1] = new GameObject("OrgPool");
		ContainerUsedObjs[1] = new GameObject("OrgPool");
	}

	private void fillObjectPools()
	{
		for (int i = 0; i < 2; i++)
		{
			for (int j = 0; j < 1; j++)
			{
				ObjPoolEntry objPoolEntry = Entries[i, j];
				if (objPoolEntry.Count == 0)
				{
					break;
				}
				Entries[i, j].pool = new GameObject[objPoolEntry.Count];
				for (int k = 0; k < objPoolEntry.Count; k++)
				{
					if (!(objPoolEntry.Prefab == null))
					{
						GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(objPoolEntry.Prefab);
						gameObject.name = objPoolEntry.Prefab.name;
						poolThisObject(i, gameObject, j);
						spriteLoader.instance.setDefaultMaterial(gameObject);
					}
				}
			}
		}
	}

	public GameObject GetObjectForType(int poolNo, string objectType, int containerID = -1, int objID = -1)
	{
		for (int i = 0; i < 1; i++)
		{
			UnityEngine.Object prefab = Entries[poolNo, i].Prefab;
			if (!(prefab == null) && !(prefab.name != objectType) && Entries[poolNo, i].objectsInPool > 0)
			{
				GameObject gameObject = Entries[poolNo, i].pool[--Entries[poolNo, i].objectsInPool];
				if (containerID < 0 || objID == -1)
				{
					gameObject.transform.parent = ContainerUsedObjs[poolNo].transform;
				}
				else if (containerID < ContainerRows.Length)
				{
					ContainerRows[containerID].AddObj(objID, gameObject);
				}
				else
				{
					Debug.Log("Illegal container " + containerID);
				}
				gameObject.SetActive(value: true);
				gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
				gameObject.GetComponent<SpriteRenderer>().material = spriteLoader.instance.defaultMaterial;
				return gameObject;
			}
		}
		Debug.Log("Pool Empty");
		return null;
	}

	public int getPoolCount(int poolNo, bool visibleOnly)
	{
		int num = 0;
		if (ContainerUsedObjs != null && ContainerUsedObjs.Length > poolNo && ContainerUsedObjs[poolNo] != null)
		{
			num += ContainerUsedObjs[poolNo].transform.childCount;
		}
		if (ContainerRows != null)
		{
			PoolRow[] containerRows = ContainerRows;
			foreach (PoolRow poolRow in containerRows)
			{
				if (poolRow != null && (!visibleOnly || poolRow.parentObj.activeSelf))
				{
					num += poolRow.rowDict.Count;
				}
			}
		}
		return num;
	}

	public void PoolObject(int poolNo, GameObject obj, int row = -1, int objID = -1)
	{
		for (int i = 0; i < 1; i++)
		{
			if (!(Entries[poolNo, i].Prefab == null) && !(Entries[poolNo, i].Prefab.name != obj.name))
			{
				poolThisObject(poolNo, obj, i, row, objID);
				break;
			}
		}
	}

	public void moveRow(int poolNo, GameObject obj, int oldContainerID, int newContainerID, int objID)
	{
		if (oldContainerID == newContainerID)
		{
			return;
		}
		for (int i = 0; i < 1; i++)
		{
			if (!(Entries[poolNo, i].Prefab == null) && !(Entries[poolNo, i].Prefab.name != obj.name))
			{
				if (oldContainerID >= 0)
				{
					ContainerRows[oldContainerID].removeObj(objID, obj);
					ContainerRows[newContainerID].AddObj(objID, obj);
				}
				break;
			}
		}
	}

	public void filterRows(int topRow, int bottowRow, int leftColumn, int rightColumn)
	{
		for (int i = 1; i < GameMap.tilemapSize + 1; i++)
		{
			if (i < topRow || i > bottowRow)
			{
				for (int j = 0; j < 21; j++)
				{
					if (ContainerRows[i * 21 + j].isActive)
					{
						ContainerRows[i * 21 + j].SetActive(state: false);
					}
				}
				continue;
			}
			for (int k = 0; k < 21; k++)
			{
				int num = i * 21 + k;
				if (leftColumn > ContainerRows[num].columnEndID || rightColumn < ContainerRows[num].columnStartID)
				{
					if (ContainerRows[num].isActive)
					{
						ContainerRows[num].SetActive(state: false);
					}
				}
				else if (!ContainerRows[num].isActive)
				{
					ContainerRows[num].SetActive(state: true);
				}
			}
		}
	}

	private void poolThisObject(int poolNo, GameObject obj, int entry, int row = -1, int objID = -1)
	{
		if (Entries[poolNo, entry].objectsInPool >= Entries[poolNo, entry].Count)
		{
			Debug.Log("OP error! ->" + Entries[poolNo, entry].objectsInPool);
			return;
		}
		obj.SetActive(value: false);
		if (row >= 0)
		{
			ContainerRows[row].removeObj(objID, obj);
		}
		obj.transform.parent = ContainerFreeObjs[poolNo].transform;
		Entries[poolNo, entry].pool[Entries[poolNo, entry].objectsInPool++] = obj;
	}
}
