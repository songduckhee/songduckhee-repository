using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestGather : MonoBehaviour,OnGather
{
	[SerializeField]
	public ItemData itemToGive;
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void Gather(Vector3 hitPoint, Vector3 hitNormal)
	{

	}
}
