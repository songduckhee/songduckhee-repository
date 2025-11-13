using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{

	Rigidbody playerRigidbody;
	float power = 80.0f;

	private void Start()
	{
		

	}


	private void OnCollisionEnter(Collision collision)
	{
		playerRigidbody = collision.rigidbody;

		playerRigidbody.AddForce(transform.up * power);
		playerRigidbody.AddForce(transform.up * power, ForceMode.Impulse);
	}
	private void OnCollisionExit(Collision collision)
	{
		playerRigidbody = collision.rigidbody;

		playerRigidbody.AddForce(transform.up * power);
		playerRigidbody.AddForce(transform.up * power, ForceMode.Impulse);
	}
}
