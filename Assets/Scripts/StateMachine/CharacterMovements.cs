using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class CharacterMovements : BaseStateMachine
{
	//Controllers
	private CharacterController _characterController;
	private Animator _animator;
	private CharacterInputsController _characterInput;
	//	Getters and setters
	public Animator Animator { get { return _animator; } }
	public CharacterController CharacterController { get { return _characterController; } }

	//Animator hashs
	private int _isWalkingHash;
	private int _isRunningHash;

	//Private constants
	private float _rocationFactorPerFrame = 15.0f;

	//Public constants
	public float ZERO = 0.0f;
	public float FALLING_VELOCITY_CAP = -20f;
	public float JUMP_BUFFER_TIMER = 0.25f;

	//Character movement values
	private Vector2 _currentMovementInput;
	private float _rawYValue;
	private Vector3 _characterAppliedMovement;
	private bool _isMovementPressed = false;
	//	Getters and setters
	public float RawYValue { get { return _rawYValue; } set { _rawYValue = value; } }
	public float CharacterAppliedYMovement { get { return _characterAppliedMovement.y; } set { _characterAppliedMovement.y = value; } }

	//States variables
	private StateFactory _states;


	//Jump variables
	private bool _isJumpPressed = false;
	private float _jumpPressTimer = 0;
	private bool _isJumping = false;
	private float _maxJumpHeight = 2.0f;
	private float _maxJumpTime = 0.5f;
	private Dictionary<int, float> _jumpVelocities = new Dictionary<int, float>();
	private Dictionary<int, float> _jumpGravities = new Dictionary<int, float>();
	//	Getters and setters
	public bool IsJumpPressed { get { return _isJumpPressed; } set { _isJumpPressed = value; } }
	public float JumpPressTimer { get { return _jumpPressTimer; } set { _jumpPressTimer = value; } }
	public Dictionary<int, float> JumpVelocities { get { return _jumpVelocities; } }
	public Dictionary<int, float> JumpGravities { get { return _jumpGravities; } }

	private void Awake()
	{
		//Initialize characters controllers
		_characterInput = new CharacterInputsController();
		_characterController = GetComponent<CharacterController>();
		_animator = GetComponent<Animator>();

		//Initialize states
		_states = new StateFactory(this);
		SwitchState(_states.Grounded());

		SetupAnimatorHashs();
		SetupCharacterInputsCallbacks();
		SetupJumpVariables();
	}

	private void SetupAnimatorHashs()
	{
		_isWalkingHash = Animator.StringToHash("isWalking");
		_isRunningHash = Animator.StringToHash("isRunning");
	}

	private void SetupCharacterInputsCallbacks()
	{
		_characterInput.CharacterMovements.Move.started += OnMovementInput;
		_characterInput.CharacterMovements.Move.performed += OnMovementInput;
		_characterInput.CharacterMovements.Move.canceled += OnMovementInput;

		_characterInput.CharacterMovements.Jump.started += OnJumpInput;
		_characterInput.CharacterMovements.Jump.canceled += OnJumpInput;
	}

	private void SetupJumpVariables()
	{
		float timeToApex = _maxJumpTime / 2;
		float initialJumpGravity = (-2 * _maxJumpHeight) / Mathf.Pow(timeToApex, 2);
		float initialJumpVelocity = (2 * _maxJumpHeight) / timeToApex;
		float secondJumpGravity = (-2 * (_maxJumpHeight * 1.5f)) / Mathf.Pow((timeToApex * 1.25f), 2);
		float secondJumpVelocity = (2 * (_maxJumpHeight * 1.5f)) / (timeToApex * 1.25f);
		float thirdJumpGravity = (-2 * (_maxJumpHeight * 2f)) / Mathf.Pow((timeToApex * 1.5f), 2);
		float thirdJumpVelocity = (2 * (_maxJumpHeight * 2f)) / (timeToApex * 1.5f);

		_jumpVelocities.Add(1, initialJumpVelocity);
		_jumpVelocities.Add(2, secondJumpVelocity);
		_jumpVelocities.Add(3, thirdJumpVelocity);

		_jumpGravities.Add(0, initialJumpGravity);
		_jumpGravities.Add(1, initialJumpGravity);
		_jumpGravities.Add(2, secondJumpGravity);
		_jumpGravities.Add(3, thirdJumpGravity);
	}

	private void OnMovementInput(InputAction.CallbackContext context)
	{
		_currentMovementInput = context.ReadValue<Vector2>();
		_isMovementPressed = _currentMovementInput.magnitude != 0;
	}

	private void OnJumpInput(InputAction.CallbackContext context)
	{
		_isJumpPressed = context.ReadValueAsButton();
		if (_isJumpPressed)
		{
			_jumpPressTimer = Time.time;
		}
	}

	// Update is called once per frame
	public override void Update()
	{
		base.Update();
		HandleRotation();
		_characterController.Move(_characterAppliedMovement * Time.deltaTime);
	}

	private void HandleRotation()
	{
		//Where the character should look based on his current movement vector
		Vector3 positionToLookAt = Vector3.zero;
		positionToLookAt.x = _currentMovementInput.x;
		positionToLookAt.y = ZERO;
		positionToLookAt.z = _currentMovementInput.y;

		//The current rotation of the character
		Quaternion currentRotation = transform.rotation;

		//if the character is currently moving
		if (_isMovementPressed)
		{
			//creates a new rotation based on twhere the player is currently pressing
			Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
			//rotate the character to face the positionToLookAt
			transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, _rocationFactorPerFrame * Time.deltaTime);
		}
	}

	private void OnEnable()
	{
		_characterInput.CharacterMovements.Enable();
	}

	private void OnDisable()
	{
		_characterInput.CharacterMovements.Disable();
	}
}
