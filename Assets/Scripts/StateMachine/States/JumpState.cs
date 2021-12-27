using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : BaseState
{
	//Constants
	private float RESET_JUMP_COUNT_TIME = 0.25f;
	private float FALL_MULTIPLIER = 1.5f;

	//Jump private variables
	private int _currentJumpCount = 0;
	private float _lastJumpTime = 0;
	private bool _isFalling = false;

	//Animator hashs
	private int _isJumpingHash;
	private int _jumpCountHash;
	public JumpState(CharacterMovements currentContext, StateFactory stateFactory) : base(currentContext, stateFactory)
	{
		_isJumpingHash = Animator.StringToHash("isJumping");
		_jumpCountHash = Animator.StringToHash("jumpCount");
	}
	public override void EnterState()
	{
		Debug.Log("ENTER JUMP STATE");

		//Reset variables
		_isFalling = false;
		_context.JumpPressTimer = 0;

		//Reset jump if it have been too long between two jumps
		if (_lastJumpTime + RESET_JUMP_COUNT_TIME < Time.time)
		{
			_currentJumpCount = 0;
		}

		//Change jump count
		if (_currentJumpCount >= 3)
		{
			_currentJumpCount = 0;
		}
		_currentJumpCount += 1;

		//Change animation values
		_context.Animator.SetInteger(_jumpCountHash, _currentJumpCount);
		_context.Animator.SetBool(_isJumpingHash, true);

		//Change Y velocity
		_context.RawYValue = _context.JumpVelocities[_currentJumpCount];
		_context.CharacterAppliedYMovement = _context.JumpVelocities[_currentJumpCount];
	}
	public override void UpdateState()
	{
		ModifyCharacterYVelocity();
		CheckSwitchStates();
	}
	public override void FixedUpdateState() { }
	public override void ExitState()
	{
		Debug.Log("LEAVE JUMP STATE");

		_context.Animator.SetBool(_isJumpingHash, false);

		_lastJumpTime = Time.time;

	}
	public override void InitializeSubState() { }
	public override void CheckSwitchStates()
	{
		if (_context.CharacterController.isGrounded)
		{
			_context.SwitchState(_factory.Grounded());
		}
	}

	public void ModifyCharacterYVelocity()
	{
		float currentGravity = _context.JumpGravities[_currentJumpCount];
		float previousVelocity = _context.RawYValue;

		//Keeping is falling in a variable so if _context.IsJumpPressed is released but re-pressed/buffered after for the next jump, the gravity will not stop being multiplied by 2.
		if (!_isFalling)
		{
			_isFalling = _context.CharacterAppliedYMovement <= 0.0f; //|| !_context.IsJumpPressed; -> If we want to be able to do smaller jump by tapping the button.
		}
		if (_isFalling)
		{
			currentGravity *= FALL_MULTIPLIER;
		}

		float currentYVelocity = previousVelocity + (currentGravity * Time.deltaTime);
		_context.CharacterAppliedYMovement = Mathf.Max((previousVelocity + currentYVelocity) * 0.5f, _context.FALLING_VELOCITY_CAP);
		_context.RawYValue = currentYVelocity;
	}
}
