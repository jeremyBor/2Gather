using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float _moveSpeed = 5f;
	[SerializeField] private float _jumpForce = 400f;
	[Range(0, .3f)] [SerializeField] private float _movementSmoothing = .05f;
	[SerializeField] private bool _airControl = false;
	[SerializeField] private LayerMask _whatIsGround;
	[SerializeField] private Transform _groundCheck;
	[SerializeField] private Rigidbody2D _rigidbody2D;

	const float _groundedRadius = .01f;
	private bool _grounded;
	private bool _facingRight = true;
	private Vector3 _velocity = Vector3.zero;

	private float _moveDirection = 0;
	private bool _isJumpPressed = false;

	private void FixedUpdate()
	{
		bool wasGrounded = _grounded;
		_grounded = false;

		Collider2D[] colliders = Physics2D.OverlapCircleAll(_groundCheck.position, _groundedRadius, _whatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				_grounded = true;
			}
		}
		Move(_moveDirection);
	}

	public void Move(float move)
	{
		if (_grounded || _airControl)
		{
			Vector3 targetVelocity = new Vector2(move * _moveSpeed, _rigidbody2D.velocity.y);
			_rigidbody2D.velocity = Vector3.SmoothDamp(_rigidbody2D.velocity, targetVelocity, ref _velocity, _movementSmoothing);

			if (move > 0 && !_facingRight)
			{
				// ... flip the player.
				Flip();
			}

			else if (move < 0 && _facingRight)
			{
				Flip();
			}
		}
		if (_grounded && _isJumpPressed && _rigidbody2D.velocity.y <=0.5f)
		{
			_isJumpPressed = false;
			_grounded = false;
			_rigidbody2D.AddForce(new Vector2(0f, _jumpForce));
		}
	}

	private void Flip()
	{
		_facingRight = !_facingRight;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public void OnMove (InputAction.CallbackContext a_context)
	{
		_moveDirection = a_context.ReadValue<float>();

		if(_moveDirection >= 0)
		{
			_facingRight = true;
		}
		else
		{
			_facingRight = false;
		}
	}

	public void OnJump (InputAction.CallbackContext a_context)
	{
		if (a_context.ReadValue<float>() >=1)
		{
			_isJumpPressed = true;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "DamageObject")
		{
			Level.Instance.heats--;
			if(Level.Instance.heats < 0)
			{
				Level.Instance.heats = 0;
			}
		}
		else if (collision.tag == "BonusObject")
		{
			Level.Instance.CollectedLeafs++;
			collision.gameObject.SetActive(false);
		}
	}
}