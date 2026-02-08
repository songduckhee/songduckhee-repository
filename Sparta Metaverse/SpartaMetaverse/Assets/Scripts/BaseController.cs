using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class BaseController : MonoBehaviour
{
  
  protected Rigidbody2D _rigidbody;
  
  
  [SerializeField] private SpriteRenderer characterRenderer;
  [SerializeField] private Transform weaponPivot;

  protected Vector2 movementDirection = Vector2.zero;
  public Vector2 MovementDirection {  get { return movementDirection; }}

  protected Vector2 lookDirection = Vector2.zero;
  public Vector2 LookDirection { get { return lookDirection; }}

  private Vector2 Knockback = Vector2.zero;
  private float KnockbackDuration = 0.0f;

  protected virtual void Awake()
  {
	_rigidbody = GetComponent<Rigidbody2D>(); 
  }

  protected virtual void Start()
  {

  }

  protected virtual void Update()
  {
	HandleAction();
	Rotate(lookDirection);
  }

  protected virtual void FixedUpdate()
  {
	Movment(movementDirection);
	if(KnockbackDuration > 0.0f)
	{
	  KnockbackDuration -= Time.fixedDeltaTime;
	}
  }

  protected virtual void HandleAction()
  {

  }

  private void Movment(Vector2 direction)
  {
	direction = direction * 5;
	if(KnockbackDuration > 0.0f)
	{
	  direction *= 0.2f;
	  direction += Knockback;
	}
	_rigidbody.velocity = direction;
  }

  private void Rotate(Vector2 direction)
  {
	float rotZ = Mathf.Atan2(direction.y,direction.x)*Mathf.Rad2Deg;
	//Atan2를 사용하면 radian의 각도값이 나옴 * Mathf.Rad2Deg를 곱해주면 라디안에 파이값같은걸 곱해주면서 라디안 값이 우리가 아는 360도의 디그리값이 됨
	bool isLeft =  Mathf.Abs(rotZ) > 90f;

	characterRenderer.flipX = isLeft;

	if(weaponPivot != null)
	{
	  weaponPivot.rotation = Quaternion.Euler(0,0,rotZ);
	}
  }

  public void Applyknockback(Transform other,float power, float duration)
  {

  }


}
