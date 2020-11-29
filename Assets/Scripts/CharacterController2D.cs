using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Hellmade.Sound;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float _moveSpeed = 5f;
	[SerializeField] private float _jumpForce = 400f;
	[Range(0, .3f)] [SerializeField] private float _movementSmoothing = .05f;
	[SerializeField] private bool _airControl = false;
	[SerializeField] private LayerMask _whatIsGround;
	[SerializeField] private Transform _groundCheck;
	[SerializeField] private Rigidbody2D _rigidbody2D;

	[SerializeField] private GameObject _damagedSprite = null;
	[SerializeField] private GameObject _normalSprite = null;

	public AudioClip runSound = null;
	public AudioClip jumpSound = null;
	public AudioClip damageSound = null;
	public AudioClip colectSound = null;

	private int runSoundId;

	const float _groundedRadius = .01f;
	private bool _grounded;
	private bool _facingRight = true;
	private Vector3 _velocity = Vector3.zero;

	private float _moveDirection = 0;
	private bool _isJumpPressed = false;

	public void DoFixedUpdate()
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
			Debug.Log(move + "   " + _facingRight);


			if (move > 0 && !_facingRight)
			{
				// ... flip the player.
				Flip();
			}
			if (move < 0 && _facingRight)
			{
				Flip();
			}
		}
		if (_grounded && _isJumpPressed && _rigidbody2D.velocity.y <=0.5f)
		{
			EazySoundManager.PlaySound(jumpSound);
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
		Audio a = EazySoundManager.GetSoundAudio(runSoundId);
		if (a == null)
		{
			runSoundId = EazySoundManager.PrepareSound(runSound,0.3f, true,null);
		}
		if (_moveDirection != 0)
		{
			EazySoundManager.GetSoundAudio(runSoundId).Play();
		}
		else
		{
			EazySoundManager.GetSoundAudio(runSoundId).Stop();
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
		
		if (collision.tag == "DamageObjet")
		{
			EazySoundManager.PlaySound(damageSound);
			LevelManager.Instance._currentLevel.heats--;
			if(LevelManager.Instance._currentLevel.heats < 0)
			{
				LevelManager.Instance._currentLevel.heats = 0;
			}
			_damagedSprite.SetActive(true);
			_normalSprite.SetActive(false);
		}
		else if (collision.tag == "BonusObject")
		{
			EazySoundManager.PlaySound(colectSound);
			LevelManager.Instance._currentLevel.CollectedLeafs++;
			collision.gameObject.SetActive(false);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == "DamageObjet")
		{
			_damagedSprite.SetActive(false);
			_normalSprite.SetActive(true);
		}
	}
}