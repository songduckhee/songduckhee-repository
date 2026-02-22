using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnGrass : MonoBehaviour
{
    public Transform grassParent;
    public GameObject[] grassPrefabs;
    public int grassCount;
    public LayerMask groundLayer;
    // Start is called before the first frame update
    void Start()
    {
        grassParent = transform;
        SpawnGrassOnFlatMesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnGrassOnFlatMesh()
    {
        for (int i = 0; i < grassCount; i ++)
        {
            float x =  Random.Range(- 50 , 51);
            float z = Random.Range(- 50 , 50.2f);
            Vector3 rayOrigin = new Vector3 ( x,20f,z);

            RaycastHit hit;

            if (Physics.Raycast(rayOrigin,Vector3.down, out hit, 40f,groundLayer))
            {
                float angle = Vector3.Angle(hit.normal, Vector3.up);

                if (angle < 15f)
                {
                    int index =  Random.Range(0, grassPrefabs.Length);
                    GameObject _grassPrefab = grassPrefabs[index];
                    Vector3 yOffset = new Vector3(0f, _grassPrefab.transform.position.y, 0f);

					Instantiate(_grassPrefab,hit.point+yOffset, Quaternion.Euler(0,Random.Range(0f,360f),0f),grassParent);
                }
            }
        }
    }
}
