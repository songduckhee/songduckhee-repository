using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public ItemSlot[] slots;  //UI에는 이미 하나로 잘 구성된 슬롯들이랑

    public GameObject inventoryWindow;
    public Transform slotPanel;
    public Transform dropPosition;

    [Header("Select Item")]
    public TextMeshProUGUI selectedItemName;  //슬롯 버튼을 클릭했을때 그 슬롯에 있는 정보를 보여줄 큰 item ui창이 이렇게있음.
	public TextMeshProUGUI selectedItemDescription;
	public TextMeshProUGUI selectedStatName;
	public TextMeshProUGUI selectedStatValue;
    public GameObject useButton;
	public GameObject equipButton;
	public GameObject unequipButton;
	public GameObject dropButton;

    private PlayerController controller;
    private PlayerCondition condition;

    ItemData selectedItem;
    int selectedItemIndex = 0;
    
    int curEquipIndex;

	// Start is called before the first frame update
	void Start()
    {
        controller = CharacterManager.Instance.Player.controller;
        condition = CharacterManager.Instance.Player.condition;
        dropPosition = CharacterManager.Instance.Player.dropPosition;

        controller.inventory += Toggle;
        CharacterManager.Instance.Player.addItem += AddItem;

        inventoryWindow.SetActive(false); //인벤토리윈도우를 꺼줌
        slots = new ItemSlot[slotPanel.childCount]; //여기서 슬롯판넬에있는 슬롯들이 들어감 자료형이 참조형이라서 씬에있는 아이템들이 들어감.

        for(int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<ItemSlot>();
            slots[i].index = i;
            slots[i].inventory = this;
        }

		ClearSelectedItemWindow();

       
	}

    // Update is called once per frame
    void Update()
    {
        
    }
    void ClearSelectedItemWindow()
    {
        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;
		selectedStatName.text = string.Empty;
		selectedStatValue.text = string.Empty;

        useButton.SetActive(false);
		equipButton.SetActive(false);
		unequipButton.SetActive(false);
		dropButton.SetActive(false);
	}
    public void Toggle()
    {
        if (IsOpen())
        {
            inventoryWindow.SetActive(false);
        }
        else
        {
            inventoryWindow.SetActive(true);
        }
    }
    public bool IsOpen()
    {
        return inventoryWindow.activeInHierarchy;
    }
    public void AddItem()
    {
        ItemData data = CharacterManager.Instance.Player.itemData;

        // 아이템이 중복 가능한지 canStack
        if (data.canStack)
        {
            ItemSlot slot = GetItemStack(data);
            if(slot != null)
            {
                slot.quantity++;
                UpdateUI();
                CharacterManager.Instance.Player.itemData = null;
                return;
            }
        }
        //비어있는 슬롯을 가져온다.
        ItemSlot emptySlot = GetEmptySlot();

        //있다면
        if(emptySlot != null)
        {
            emptySlot.item = data;
            emptySlot.quantity = 1;
            UpdateUI();
            CharacterManager.Instance.Player.itemData = null;
            return;
        }

        //없다면 
        ThrowItem(data);
        CharacterManager.Instance.Player.itemData = null;
    }
	ItemSlot GetItemStack(ItemData data)
	{
		for (int i = 0; i < slots.Length; i++)
		{
			if (slots[i].item == data && slots[i].quantity < data.maxStackAmount)
			{
				return slots[i];
			}
		}
		return null;
	}

	void UpdateUI()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                slots[i].Set();
            }
            else
            {
                slots[i].Clear();
            }
        }
    }

    ItemSlot GetEmptySlot()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                return slots[i];
            }
        }
        return null;
    }
    
    void ThrowItem(ItemData data)
    {
        Instantiate(data.dropPrefabs,dropPosition.position,Quaternion.Euler(Vector3.one*Random.value*360)); //정확히는 새로생성하는코드이다
    }

    public void SelectItem(int index) // 버튼을 클릭하면 이게 실행됨
    {
        if (slots[index].item == null)return; //그 슬롯에 item데이터가 없으면 중단해줌)
                                              //선택된 아이템에 대한 정보를 잠깐 갖고있어준다
        selectedItem = slots[index].item;     //이 정보를 가지고있는 이유를 생각해봤는데 player의 item데이터는 사실이미 사라져버려서 없고 player가가진 아이템데이터가
                                              //사실 그 이미 인벤토리에 넣어놓게되는 그런 데이터인데다 이미인벤토리에 넣어놓은다음엔 그 데이터가 사라지고 그 빈 슬롯에만 그 데이터가 들어가있음
                                              //그래서 이걸 저장해줄 변수가필요함.
        selectedItemIndex = index;

		selectedItemName.text = selectedItem.displayName;
        selectedItemDescription.text = selectedItem.description;
        
        selectedStatName.text = string.Empty;
        selectedStatValue.text = string.Empty;

        if (slots[index].item.consumables != null)
        {
            for(int i = 0 ; i < slots[index].item.consumables.Length; i++)
            {
				ItemDataConsumable[] data = slots[index].item.consumables;

				selectedStatName.text += data[i].type.ToString() +"\n";
				selectedStatValue.text += data[i].value.ToString() + "\n";
			}

		}

        useButton.SetActive(selectedItem.type == ItemType.Consumable);
        equipButton.SetActive(selectedItem.type == ItemType.Equipable && slots[index].equipped == false);
		unequipButton.SetActive(selectedItem.type == ItemType.Equipable && slots[index].equipped == true);
		dropButton.SetActive(true);
    }

    public void OnUseButton()
    {
        if(selectedItem.type == ItemType.Consumable)
        {
            for(int i = 0; i <selectedItem.consumables.Length; i++)
            {
                switch (selectedItem.consumables[i].type)
                {
                    case ConsumableType.Health:
                        condition.Heal(selectedItem.consumables[i].value);

						break;
                    case ConsumableType.Hunger:
                        condition.Eat(selectedItem.consumables[i].value);
                        break;
                }
            }
			RemoveSelectedItem();


		}
    }

    public void OnDropButton()
    {
        ThrowItem(selectedItem);
        RemoveSelectedItem();


    }
    void RemoveSelectedItem()
    {
        slots[selectedItemIndex].quantity--;

        if (slots[selectedItemIndex].quantity <= 0)
        {
            selectedItem = null;
            slots[selectedItemIndex].item = null;
            selectedItemIndex = -1;
            ClearSelectedItemWindow();
        }
        UpdateUI();
    }

    public void OnEquipButton()
    {
        if (slots[curEquipIndex].equipped)
        {
            //UnEquip
            UnEquip(curEquipIndex);
        }
        slots[selectedItemIndex].equipped = true;
        curEquipIndex = selectedItemIndex;
        CharacterManager.Instance.Player.equip.EquipNew(selectedItem);
        UpdateUI();

        SelectItem(selectedItemIndex);
    }

    void UnEquip(int index)
    {
        slots[index].equipped = false;
        CharacterManager.Instance.Player.equip.UnEquip();
        UpdateUI();
        if(selectedItemIndex == index)
        {
            SelectItem(selectedItemIndex);
        }
    }

    public void OnUnEquipButton()
    {
        UnEquip(selectedItemIndex);
    }
}
