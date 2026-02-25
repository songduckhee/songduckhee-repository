
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class SpawnGrass : MonoBehaviour
{
	public Transform grassParent;
	public GameObject[] grassPrefabs;
	public int grassCount;
	public LayerMask groundLayer;

	public float clumpRadius = 20f;

	[Header("Spawn Settings")]
	[Range(0f, 1f)]
	public float baseChance = 0.05f;
	[Range(0f, 1f)]
	public float clumpMaxChance = 0.8f;
	// Start is called before the first frame update
	void Start()
	{
		grassParent = transform;
		SpawnGrassClumps();

	}

	// Update is called once per frame
	void Update()
	{

	}


	void SpawnGrassClumps()
	{
		int spawnLocationCount = Random.Range(1, 5);
		Vector3[] spawnLocation = new Vector3[spawnLocationCount];


		for (int i = 0; i < spawnLocationCount; i++)
		{
			Vector3 location = GetRandomWorldPos();
			spawnLocation[i] = location;
		}

		for (int i = 0; i < grassCount; i++)
		{
			float currentChance = baseChance;

			Vector3 spawnPos = GetRandomWorldPos();
			if (spawnPos == Vector3.zero) continue;
			for (int j = 0; j < spawnLocationCount; j++)
			{
				if (spawnLocation[j] == Vector3.zero) continue;
				float distance = Vector3.Distance(spawnLocation[j], spawnPos);
				if (distance < clumpRadius)
				{
					float proximityChance = (1f - (distance / clumpRadius)) * clumpMaxChance;

					// 기존 확률보다 이 구역 확률이 더 높으면 덮어쓰기
					if (proximityChance > currentChance)
						currentChance = proximityChance;
				}

			}
			if (Random.value < currentChance)
			{
				int index = Random.Range(0, grassPrefabs.Length);
				GameObject _grassPrefab = grassPrefabs[index];
				Vector3 yOffset = new Vector3(0f, _grassPrefab.transform.position.y, 0f);

				Instantiate(_grassPrefab, spawnPos + yOffset, Quaternion.Euler(0, Random.Range(0f, 360f), 0f), grassParent);
			}

		}
	}

	Vector3 GetRandomWorldPos()
	{
		for (int i = 0; i < 10; i++)
		{
			float x = Random.Range(-50, 51);
			float z = Random.Range(-50, 50.2f);
			Vector3 rayOrigin = new Vector3(x, 20f, z);

			RaycastHit hit;

			if (Physics.Raycast(rayOrigin, Vector3.down, out hit, 40f, groundLayer))
			{
				if (Vector3.Angle(hit.normal, Vector3.up) < 30f)
				{
					return hit.point;
				}
			}
		}
		return Vector3.zero;

	}
}

