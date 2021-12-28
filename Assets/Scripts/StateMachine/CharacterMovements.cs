using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class CharacterMovements : BaseStateMachine
{
	/*********************
	** Controllers
	*********************/
	private CharacterController _characterController;
	private Animator _animator;
	private CharacterInputsController _characterInput;
	//Getters and setters
	public Animator Animator { get { return _animator; } }
	public CharacterController CharacterController { get { return _characterController; } }

	/*********************
	** Constants
	*********************/
	//Privates
	private float _rocationFactorPerFrame = 25.0f;
	//Public
	public float ZERO = 0.0f;
	public float FALLING_VELOCITY_CAP = -20f;
	public float JUMP_BUFFER_TIMER = 0.25f;

	/*********************
	** Character movements
	*********************/
	private float _movementSpeed = 4.0f;
	private float _runningSpeed = 8.0f;
	private Vector2 _currentMovementsInput;
	private Vector3 _characterAppliedMovement;
	private bool _isMovementPressed = false;
	//	Getters and setters
	public float MovementSpeed { get { return _movementSpeed; } }
	public float RunningSpeed { get { return _runningSpeed; } }
	public bool IsMovementPressed { get { return _isMovementPressed; } }
	public Vector2 CurrentMovementsInput { get { return _currentMovementsInput; } }
	public float CharacterAppliedXMovement { get { return _characterAppliedMovement.x; } set { _characterAppliedMovement.x = value; } }
	public float CharacterAppliedZMovement { get { return _characterAppliedMovement.z; } set { _characterAppliedMovement.z = value; } }
	public bool IsRunning { get { return _currentMovementsInput.magnitude >= 0.9; } }

	/*********************
	** State variables
	*********************/
	private StateFactory _states;

	/*********************
	** Jump variables
	*********************/
	private bool _isJumpPressed = false;
	private float _jumpPressTimer = -1000;
	private float _maxJumpHeight = 2.0f;
	private float _maxJumpTime = 0.5f;
	private float _rawYValue;
	private Dictionary<int, float> _jumpVelocities = new Dictionary<int, float>();
	private Dictionary<int, float> _jumpGravities = new Dictionary<int, float>();
	//	Jump button get and set
	public bool IsJumpPressed { get { return _isJumpPressed; } set { _isJumpPressed = value; } }
	public float JumpPressTimer { get { return _jumpPressTimer; } set { _jumpPressTimer = value; } }
	//	Jump arc get and set
	public Dictionary<int, float> JumpVelocities { get { return _jumpVelocities; } }
	public Dictionary<int, float> JumpGravities { get { return _jumpGravities; } }
	//	Y values get and set
	public float RawYValue { get { return _rawYValue; } set { _rawYValue = value; } }
	public float CharacterAppliedYMovement { get { return _characterAppliedMovement.y; } set { _characterAppliedMovement.y = Mathf.Max(value, FALLING_VELOCITY_CAP); } }

	/*********************
	** Animator hashs
	*********************/
	private int _isWalkingHash;
	private int _isRunningHash;
	public int IsWalkingHash { get { return _isWalkingHash; } }
	public int IsRunningHash { get { return _isRunningHash; } }

	private void Awake()
	{
		// VARIABLES FIRST

		//Initialize characters controllers
		_characterInput = new CharacterInputsController();
		_characterController = GetComponent<CharacterController>();
		_animator = GetComponent<Animator>();

		//Initialize animator hashs
		_isWalkingHash = Animator.StringToHash("isWalking");
		_isRunningHash = Animator.StringToHash("isRunning");

		SetupCharacterInputsCallbacks();
		SetupJumpVariables();

		// SETUP STATE AFTER

		//Initialize states
		_states = new StateFactory(this);
		SwitchState(_states.Grounded());
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
		_currentMovementsInput = context.ReadValue<Vector2>();
		_isMovementPressed = _currentMovementsInput.magnitude != 0;
	}

	private void OnJumpInput(InputAction.CallbackContext context)
	{
		bool isButtonPressed = context.ReadValueAsButton();
		if (isButtonPressed)
		{
			_jumpPressTimer = Time.time;
		}
		_isJumpPressed = isButtonPressed;
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
		positionToLookAt.x = _currentMovementsInput.x;
		positionToLookAt.y = ZERO;
		positionToLookAt.z = _currentMovementsInput.y;

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