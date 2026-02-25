using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface OnGather
{
	public void Gather(Vector3 hitPoint, Vector3 hitNormal)
    {

    }
}

public class Resource : MonoBehaviour,OnGather
{
    [SerializeField]
    public ItemData itemToGive;
	[SerializeField]
	public int quantityPerHit = 1;
	[SerializeField]
	public int capacity;
   
    public void Gather(Vector3 hitPoint,Vector3 hitNormal)
    {
        for(int i  = 0; i < quantityPerHit; i++)
        {
            if(capacity <= 0) break;
            capacity -= 1;
            Instantiate(itemToGive.dropPrefabs,hitPoint + Vector3.up,Quaternion.LookRotation(hitNormal,Vector3.up));
        }
        if(capacity <= 0)
        {
            Destroy(gameObject);
        }
    }
}
