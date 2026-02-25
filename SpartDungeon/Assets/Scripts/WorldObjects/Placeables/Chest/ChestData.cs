using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestData
{
	public ItemData item;
	public int index;
	public int quantity;


	public void Set(ItemData data,int _index,int _quantity)
	{
		item = data;
		index = _index;
		quantity = _quantity;
	}
}
