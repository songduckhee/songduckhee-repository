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
  public Vector2 LookDirection { get { return lookDirection; }};

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
	float rotZ = Mathf.Atan2(direction.y,direction.x)*Mathf.Rad2Deg; //Atan2�� ����ϸ� radian�� �������� ���� * Mathf.Rad2Deg�� �����ָ� ���ȿ� ���̰������� �����ָ鼭 ���� ���� �츮�� �ƴ� 360���� ��׸����� ��
  }

  public void Applyknockback(Transform other,float power, float duration)
  {

  }


}
