using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;

public class PlayerController : MonoBehaviour
{
	[Header("Moverment")]
	public float moveSpeed;
	public float jumpPower;
	private Vector2 curMovementInput;
	public LayerMask groundLayerMask;

	[Header("Look")]
	public Transform cameraContainer;
	public float minXLook;
	public float maxXLook;
	private float camCurXRot;
	public float LookSensitivity;
	private Vector2 mouseDelta;
	public bool canLook = true;

	public Action inventory;
	private Rigidbody _rigidbody;
	public Animator ghostAnimator;
	public Animator playerAnimator;

	[Header("Collider")]
	public Collider playerCollider;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
		playerAnimator = GetComponent<Animator>();
		ghostAnimator = transform.Find("Ghost").GetComponent<Animator>();
		playerCollider = GetComponent<Collider>();
	}

	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
	}

	private void FixedUpdate()
	{
		Move();
	}
	private void LateUpdate()
	{
		if (canLook)
		{
			CameraLook();
		}

	}
	private void OnTriggerEnter(Collider other)
	{
		if (LayerMask.LayerToName(other.gameObject.layer) == "Water")
		{
			_rigidbody.drag = 2f;
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (LayerMask.LayerToName(other.gameObject.layer) == "Water")
		{
			_rigidbody.drag = 0f;
		}
	}

	void Move()
	{
		Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
		dir *= moveSpeed;
		dir.y = _rigidbody.velocity.y;

		_rigidbody.velocity = dir;
	}
	void CameraLook()
	{
		camCurXRot += mouseDelta.y * LookSensitivity;
		camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
		cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

		transform.eulerAngles += new Vector3(0, mouseDelta.x * LookSensitivity, 0);
	}

	public void OnMove(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Performed)
		{
			curMovementInput = context.ReadValue<Vector2>();
			ghostAnimator.SetBool("Moving", true);
		}
		else if (context.phase == InputActionPhase.Canceled)
		{
			curMovementInput = Vector2.zero;
			ghostAnimator.SetBool("Moving", false);
		}
	}
	public void OnLook(InputAction.CallbackContext context)
	{
		mouseDelta = context.ReadValue<Vector2>();
	}

	public void OnJump(InputAction.CallbackContext context)
	{
		LayerMask water;
		if (context.phase == InputActionPhase.Started && IsGrounded())
		{
			_rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
		}
	}

	bool IsGrounded()
	{
		Ray[] rays = new Ray[4]
		{
			new Ray(transform.position + (transform.forward *0.2f)+(transform.up * 0.01f), Vector3.down),
			new Ray(transform.position + (-transform.forward *0.2f)+(transform.up * 0.01f), Vector3.down),
			new Ray(transform.position + (transform.right *0.2f)+(transform.up * 0.01f), Vector3.down),
			new Ray(transform.position + (-transform.right *0.2f)+(transform.up * 0.01f), Vector3.down),
		};

		for (int i = 0; i < rays.Length; i++)
		{

			if (Physics.Raycast(rays[i],out RaycastHit hit, 0.1f, groundLayerMask, QueryTriggerInteraction.Collide))
			{
				return true;
			}
		}
		if (Physics.CheckSphere(transform.position, 0.2f, groundLayerMask, QueryTriggerInteraction.Collide))
		{
			return true;
		}
		return false;

	}

	public void OnInventory(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Started)
		{
			inventory?.Invoke();
			toggleCursor();
		}
	}

	public void toggleCursor()
	{
		bool toggle = Cursor.lockState == CursorLockMode.Locked;
		Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
		canLook = !toggle;
	}


}
