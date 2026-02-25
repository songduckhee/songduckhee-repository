using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour,IInteractable
{
    public ChestData[] slot;
	public string displayName;
	public string description;
	public int slotCount;
	
	public bool Open;

	public string GetInteractPrompt()
	{
		string str = $"{displayName}\n{description}";
		return str;
	}

	public void OnInteract()
	{
	     ChestInventory.instance.OnChestInteract(this);
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

	public void Init()
	{
		slot = new ChestData[slotCount];

		for ( int i = 0; i < slotCount; i++)
		{
			slot[i].index = i;
			slot[i].quantity = 0;
		}
	}
}
