using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public struct SlotData
{
	public ItemData item;
	public int quantity;
	public bool equipped;
}
public class ItemSlot : MonoBehaviour
{

    
    public ItemData item;

    public Button button;
    public Image icon;
    public TextMeshProUGUI quantityText;
    private Outline outline;
    public InventoryType type;

    public UIInventory inventory;
    public ChestInventory chestInventory;

    public int index;
    public bool equipped;
    public int quantity;

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }

    private void OnEnable()
    {
        outline.enabled = equipped;
    }


    public void UpdateSlot()
    {
        if (item == null)
        {
            Clear();
        }
        else
        {
            Set();
        }
    }

    public void Set()
    {
        icon.gameObject.SetActive(true);
        icon.sprite = item.icon;
        quantityText.text = quantity > 1 ? quantity.ToString() : string.Empty;
        if (outline != null)
        {
            outline.enabled = equipped;
        }
    }

    public void Clear()
    {
        item = null;
        icon.gameObject.SetActive(false);
        quantityText.text = string.Empty;
    }
    public void OnClickButton() // 얘를 누르면 UIInventory에 있는 ui에 정보가 나타남(UIInventory에 아이템 슬롯에 대한 정보가있어서 그 아이템의 정보가 나타남)
    {
        if (type == InventoryType.Player)
        {
            inventory.SelectItem(index);
        }
        else if (type == InventoryType.Chest)
        {
            chestInventory.SelectChestItem(this);
        }
    }
    public void ChangeInfo(ItemSlot _item)
    {
        if (_item.item != null)
        {
            item = _item.item;
        }
        quantity = _item.quantity;
        UpdateSlot();
    }

    public SlotData GetData()
    {
        SlotData data = new SlotData();
        data.item = item;
        data.quantity = quantity;
        data.equipped = equipped;
        return data;
    }

	public void SetData(SlotData data)
	{
		item = data.item;
		quantity = data.quantity;
		equipped = data.equipped;
	}

}