using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSlideState : BaseState
{
	//Constants
	private const float WALL_SLIDE_SLOW_DIVIDER = 2.0f;
	//Animator hashs
	private int _isWallSlideHash;

	public WallSlideState(CharacterMovements currentContext, StateFactory stateFactory) : base(currentContext, stateFactory)
	{
		_isWallSlideHash = Animator.StringToHash("isWallSlide");
	}
	public override void EnterState()
	{
		Debug.Log("ENTER WALL SLIDE STATE");
		_context.Animator.SetBool(_isWallSlideHash, true);
	}

	protected override void UpdateState()
	{
		float currentGravity = _context.JumpGravities[0] / 2;

		float currentYVelocity = (currentGravity * 2 * Time.deltaTime);
		_context.CharacterAppliedYMovement = currentYVelocity;
	}

	protected override void FixedUpdateState() { }
	public override void ExitState()
	{
		Debug.Log("LEAVE WALL SLIDE STATE");
		_context.Animator.SetBool(_isWallSlideHash, false);
	}
	protected override void InitializeSubState()
	{
	}
	public override void CheckSwitchStates()
	{
		//You don't want to be able to buffer the jump button during other states more than JUMP_BUFFER_TIMER seconds
		if (_context.CharacterController.isGrounded)
		{
			SwitchState(_factory.Grounded());
		}
		else if (_context.IsJumpPressed && Time.time - _context.JUMP_BUFFER_TIMER < _context.JumpPressTimer)
		{
			_context.JumpPressTimer = -1000;
			SwitchState(_factory.WallJump());
		}
	}
}
