using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestData
{
	public ItemData item;
	public int index;
	public int quantity;


	public void Set(int _index,int _quantity,ItemData data = null )
	{
		item = data;
		index = _index;
		quantity = _quantity;
	}
}
