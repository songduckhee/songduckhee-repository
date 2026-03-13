using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


public enum InventoryType
{
	Player,
	Chest
}

public enum CurrentInputAction
{
	None,
	Player,
	Chest
}

public class ChestInventory : MonoBehaviour

{

	[SerializeField] private UIInventory uiInventory;

	public static ChestInventory instance;
	public GameObject chestInventoryPanel;


	[Header("ChestInventory")]
	public Transform chestSlotPanel;
	public ItemSlot[] chestslots;
	public Chest currentChest;

	[Header("PlayerInventory")]
	public Transform inventorySlotPanel;
	public ItemSlot[] chestInvenSlots;

	[Header("UiInventorySlotData")]
	public ItemSlot[] uiInventorySlots;

	[Header("SelectItem")]
	public ItemSlot selectItem;
	int SelectIndex;
	int quantity;


	[Header("SlotsPrefab")]
	public GameObject slotPrefabs;

	bool Opend;



	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(this.gameObject);
		}

	}
	// Start is called before the first frame update
	void Start()
	{
		Init();
	}

	// Update is called once per frame
	void Update()
	{

	}

	void Init()
	{
		chestslots = new ItemSlot[chestSlotPanel.childCount];
		chestInvenSlots = new ItemSlot[inventorySlotPanel.childCount];

		for (int i = 0; i < chestslots.Length; i++)
		{
			chestslots[i] = chestSlotPanel.GetChild(i).GetComponent<ItemSlot>();
			chestslots[i].index = i;
			chestslots[i].chestInventory = this;
			chestslots[i].type = InventoryType.Chest;
			chestslots[i].UpdateSlot();
		}
		for (int i = 0; i < chestInvenSlots.Length; i++)
		{
			chestInvenSlots[i] = inventorySlotPanel.GetChild(i).GetComponent<ItemSlot>();
			chestInvenSlots[i].index = i;
			chestInvenSlots[i].chestInventory = this;
			chestInvenSlots[i].type = InventoryType.Player;
			chestInvenSlots[i].inventory = uiInventory;
			chestInvenSlots[i].UpdateSlot();
		}

		UpdateChestSlotUI();
		UpdateInventorySlotUI();
		chestInventoryPanel.SetActive(false);
	}

	public void OnChestInteract(Chest _chest)
	{
		currentChest = _chest;

		if (Opend == false)
		{
			OnChestOpen();
			CharacterManager.Instance.Player.controller.ChangeInputAction(CurrentInputAction.Chest);

		}
		else
		{
			OnChestClose();
			CharacterManager.Instance.Player.controller.ChangeInputAction(CurrentInputAction.Player);
		}
	}


	public void OnChestOpen()
	{
		UpdateChestSlotUI();
		UpdateInventorySlotUI();
		chestInventoryPanel.SetActive(true);
		Opend = true;
	}

	public void OnChestClose()
	{
		UpdateChestSlotUI();
		UpdateInventorySlotUI();
		chestInventoryPanel.SetActive(false);
		Opend = false;
	}

	public void UpdateChestSlotUI()
	{
		if (currentChest == null)
			return;

		if (Opend == false)
		{
			for (int i = 0; i < chestslots.Length; i++)
			{
				if (i < currentChest.slotCount)
				{
					chestslots[i].gameObject.SetActive(true);
					chestslots[i].SetData(currentChest.slot[i].GetData());
					chestslots[i].UpdateSlot();
				}
				else
				{
					chestslots[i].gameObject.SetActive(false);
				}
			}
		}
		else
		{
			for (int i = 0; i < currentChest.slotCount; i++)
			{
				currentChest.slot[i].SetData(chestslots[i].GetData());
			}
		}

	}

	public void UpdateInventorySlotUI() // 오픈될 때 인벤토리 정보를 상자UI에 옮겨오는 함수
	{
		if (chestInvenSlots.Length < uiInventorySlots.Length)
		{
			// 기존 배열 백업 및 새 배열 생성
			ItemSlot[] oldSlots = chestInvenSlots;
			chestInvenSlots = new ItemSlot[uiInventory.slots.Length];

			// 기존 슬롯 복사
			for (int i = 0; i < oldSlots.Length; i++)
			{
				chestInvenSlots[i] = oldSlots[i];
			}

			// 부족한 만큼 새로 생성
			for (int i = oldSlots.Length; i < uiInventory.slots.Length; i++)
			{
				chestInvenSlots[i] = Instantiate(slotPrefabs, inventorySlotPanel).GetComponent<ItemSlot>();
			}
			Debug.Log("슬롯 갯수 부족 새로 생성");
		}


		int min = Mathf.Min(chestInvenSlots.Length, uiInventorySlots.Length);  ///예시 inventorySlots.Length = 6; uiInventorySlots.Length = 9 
		int max = Mathf.Max(chestInvenSlots.Length, uiInventorySlots.Length);

		for (int i = 0; i < min; i++) // 6번 반복
		{
			if (uiInventory.slots[i] == null)
			{
				Debug.Log($"슬롯{i}번째가 비었음!");
				continue;
			}
			if (Opend == false)
			{
				chestInvenSlots[i].SetData(uiInventory.slots[i].GetData());
				chestInvenSlots[i].gameObject.SetActive(true);
			}
			else
			{
				uiInventory.slots[i].SetData(chestInvenSlots[i].GetData());
			}

		}
		if (chestInvenSlots.Length > uiInventorySlots.Length)
		{
			for (int i = min; i < max; i++) // uiInventorySlots 갯수보다 많은건 꺼짐 // 만약 uiInventorySlot이 inventorySlots 보다 많다면 오류가 생김 -> if 문으로 비교를 통해 한쪽만 실행되도록 해결
			{
				chestInvenSlots[i].gameObject.SetActive(false);
			}
		}

	}

	public void SelectChestItem(ItemSlot itemSlot)
	{
		if (selectItem == null)
		{
			selectItem = itemSlot;
			SelectIndex = itemSlot.index;
			return;
		}
		if (selectItem == itemSlot)
		{
			selectItem = null;
			SelectIndex = -1;
			return;
		}

		ItemSlot a = GetSlot(selectItem.type, SelectIndex);
		ItemSlot b = GetSlot(itemSlot.type, itemSlot.index);
		SwapSlots(a, b);

		selectItem = null;
		SelectIndex = -1;
	}

	private ItemSlot GetSlot(InventoryType type, int index)
	{
		if (type == InventoryType.Chest)
		{
			return chestslots[index];
		}
		return chestInvenSlots[index];
	}

	private void SwapSlots(ItemSlot a, ItemSlot b)
	{
		SlotData temp = a.GetData();
		a.SetData(b.GetData());
		b.SetData(temp);

		a.UpdateSlot();
		b.UpdateSlot();
	}
}
