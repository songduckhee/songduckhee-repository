using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public ItemData item;

    public Button button;
    public Image icon;
    public TextMeshProUGUI quantityText;
    private Outline outline;

    public UIInventory inventory;

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


    public void Set()
    {
        icon.gameObject.SetActive(true);
        icon.sprite = item.icon;
        quantityText.text = quantity >1 ? quantity.ToString():string.Empty;
        if(outline != null)
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
        inventory.SelectItem(index);
    }
}