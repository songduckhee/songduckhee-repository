using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public enum InventoryType
{
	UIInventory,
	ChestInventory
}


public class ChestInventory : MonoBehaviour

{

	[SerializeField] private UIInventory inventory;

	public static ChestInventory instance;

	[Header("ChestInventory")]
	public Transform ChestSlotPanel;
	public ItemSlot[] Chestslots;
	public Chest CurrentChest;

	[Header("UIInventory")]
	public Transform InventorySlotPanel;
	public ItemSlot[] UIslots;

	[Header("SelectItem")]
	public ItemSlot SelectItem;
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
		Chestslots = new ItemSlot[ChestSlotPanel.childCount];
		UIslots = new ItemSlot[InventorySlotPanel.childCount];

		for (int i = 0; i < Chestslots.Length; i++)
		{
			Chestslots[i] = ChestSlotPanel.GetChild(i).GetComponent<ItemSlot>();
			Chestslots[i].index = i;
			Chestslots[i].chestInventory = this;
			Chestslots[i].type = InventoryType.ChestInventory;
		}
		for (int i = 0; i < UIslots.Length; i++)
		{
			UIslots[i] = InventorySlotPanel.GetChild(i).GetComponent<ItemSlot>();
			UIslots[i].index = i;
			UIslots[i].chestInventory = this;
			Chestslots[i].type = InventoryType.ChestInventory;
		}

		UpdateChestSlotUI();
		UpdateInventorySlotUI();
	}

	public void OnChestInteract(Chest _chest)
	{
		CurrentChest = _chest;

		if (Opend == false)
		{
			OnChestOpen();

		}
		else
		{
			OnChestClose();

		}
	}


	public void OnChestOpen()
	{
		UpdateChestSlotUI();
		UpdateInventorySlotUI();
		ChestSlotPanel.gameObject.SetActive(true);
		InventorySlotPanel.gameObject.SetActive(true);
		Opend = true;
		CurrentChest.Open = true;
	}

	public void OnChestClose()
	{
		UpdateChestSlotUI();
		UpdateInventorySlotUI();
		ChestSlotPanel.gameObject.SetActive(false);
		InventorySlotPanel.gameObject.SetActive(false);
		Opend = false;
		CurrentChest.Open = false;
	}

	public void UpdateChestSlotUI()
	{
		if (Opend == false)
		{
			for (int i = 0; i < Chestslots.Length; i++)
			{
				if (i < CurrentChest.slotCount)
				{
					Chestslots[i].gameObject.SetActive(true);
					Chestslots[i].item = CurrentChest.slot[i].item;
					Chestslots[i].index = CurrentChest.slot[i].index;
					Chestslots[i].Set();
				}
				else
				{
					Chestslots[i].gameObject.SetActive(false);
				}
			}
		}
		else
		{
			for (int i = 0; i < CurrentChest.slotCount; i++)
			{
				CurrentChest.slot[i].Set(Chestslots[i].item, Chestslots[i].index, Chestslots[i].quantity);
			}
		}
		
	}

	public void UpdateInventorySlotUI()
	{
		if (Opend == false)
		{
			for (int i = 0; i < UIslots.Length; i++)
			{
				UIslots[i].item = inventory.slots[i].item;
				UIslots[i].Set();
			}
		}
		else
		{
			for (int i = 0; i < inventory.slots.Length; i++)
			{
				inventory.slots[i].item = UIslots[i].item;
				inventory.slots[i].Set();
			}
		}
	}

	public void SelectChestItem(ItemSlot itemSlot)
	{
		if (SelectItem != null)
		{
			SelectItem = itemSlot;
			SelectIndex = itemSlot.index;
		}
		else
		{
			ItemSlot item = itemSlot;
			ItemSlot select = SelectItem;
			SelectItem = item;
			itemSlot = select;

			SelectItem.Set();
			itemSlot.Set();

			SelectItem = null;
			SelectIndex = -1;
		}
	}
}
