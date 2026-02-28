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


public class ChestInventory : MonoBehaviour

{

	[SerializeField] private UIInventory uiInventory;

	public static ChestInventory instance;
	public GameObject chestUiPanel;


	[Header("ChestInventory")]
	public Transform chestSlotPanel;
	public ItemSlot[] chestslots;
	public Chest currentChest;

	[Header("UIInventory")]
	public Transform inventorySlotPanel;
	public ItemSlot[] inventorySlots;

	[Header("SelectItem")]
	public ItemSlot selectItem;
	int SelectIndex;


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
		inventorySlots = new ItemSlot[inventorySlotPanel.childCount];

		for (int i = 0; i < chestslots.Length; i++)
		{
			chestslots[i] = chestSlotPanel.GetChild(i).GetComponent<ItemSlot>();
			chestslots[i].index = i;
			chestslots[i].chestInventory = this;
			chestslots[i].type = InventoryType.Chest;
			chestslots[i].Clear();
		}
		for (int i = 0; i < inventorySlots.Length; i++)
		{
			inventorySlots[i] = inventorySlotPanel.GetChild(i).GetComponent<ItemSlot>();
			inventorySlots[i].index = i;
			inventorySlots[i].chestInventory = this;
			inventorySlots[i].type = InventoryType.Player;
			inventorySlots[i].Clear();
		}

		UpdateChestSlotUI();
		UpdateInventorySlotUI();
		chestUiPanel.SetActive(false);
	}

	public void OnChestInteract(Chest _chest)
	{
		currentChest = _chest;

		if (Opend == false)
		{
			OnChestOpen();

		}
		else
		{
			OnChestClose();

		}
		CharacterManager.Instance.player.controller.toggleCursor();
	}


	public void OnChestOpen()
	{
		UpdateChestSlotUI();
		UpdateInventorySlotUI();
		chestUiPanel.SetActive(true);
		Opend = true;
	}

	public void OnChestClose()
	{
		UpdateChestSlotUI();
		UpdateInventorySlotUI();
		chestUiPanel.SetActive(false);
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
					chestslots[i].item = currentChest.slot[i].item;
					chestslots[i].index = currentChest.slot[i].index;
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
				currentChest.slot[i].Set(chestslots[i].index, chestslots[i].quantity, chestslots[i].item);
			}
		}

	}

	public void UpdateInventorySlotUI() // 오픈될 때 인벤토리 정보를 상자UI에 옮겨오는 함수
	{
		if (inventorySlots.Length < uiInventory.slots.Length)
		{
			// 기존 배열 백업 및 새 배열 생성
			ItemSlot[] oldSlots = inventorySlots;
			inventorySlots = new ItemSlot[uiInventory.slots.Length];

			// 기존 슬롯 복사
			for (int i = 0; i < oldSlots.Length; i++)
			{
				inventorySlots[i] = oldSlots[i];
			}

			// 부족한 만큼 새로 생성
			for (int i = oldSlots.Length; i < uiInventory.slots.Length; i++)
			{
				inventorySlots[i] = Instantiate(slotPrefabs, inventorySlotPanel).GetComponent<ItemSlot>();
			}
		}

		int min = Mathf.Min(inventorySlots.Length, uiInventory.slots.Length);
		int max = Mathf.Max(inventorySlots.Length, uiInventory.slots.Length);

		for (int i = 0; i < min; i++)
		{
			if (uiInventory.slots[i] == null)
			{
				inventorySlots[i].gameObject.SetActive(false);
				continue;
			}
			if (uiInventory.slots[i].item == null)
			{
				inventorySlots[i].Set();
				continue;
			}
			if (Opend == false)
			{
				inventorySlots[i].item = uiInventory.slots[i].item;
				inventorySlots[i].Set();
				inventorySlots[i].gameObject.SetActive(true);
			}
			else
			{
				uiInventory.slots[i].item = inventorySlots[i].item;
				uiInventory.slots[i].Set();
			}

		}
		for (int i = min; i < max; i++)
		{
			inventorySlots[i].gameObject.SetActive(false);
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
		return inventorySlots[index];
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
