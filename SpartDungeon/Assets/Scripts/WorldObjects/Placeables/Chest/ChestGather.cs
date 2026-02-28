using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class ChestGather : MonoBehaviour,OnGather
{
	[SerializeField]
	public ItemData itemToGive;
	[SerializeField]
	public int capacity = 5;
	[SerializeField]
	public Chest chest;
	// Start is called before the first frame update
	void Start()
    {
        chest = GetComponent<Chest>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void Gather(Vector3 hitPoint, Vector3 hitNormal)
	{
		if (chest.IsSlotEmpty())
		{
			capacity -= 1;
			if(capacity == 0)
			{
				Vector3 dir = this.transform.position - CharacterManager.Instance.player.transform.position;
				Instantiate(itemToGive.dropPrefabs,new Vector3(this.transform.position.x,this.transform.position.y+5f, this.transform.position.z), Quaternion.LookRotation(dir));
				Destroy(this);
			}
		}
		else
		{
			capacity = 5;
		}
	}
}
