using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingState : BaseState
{
	//Animator hashs
	private int _isFallingHash;

	public FallingState(CharacterMovements currentContext, StateFactory stateFactory) : base(currentContext, stateFactory)
	{
		_isFallingHash = Animator.StringToHash("isFalling");
	}
	public override void EnterState()
	{
		Debug.Log("ENTER FALLING STATE");
		_context.Animator.SetBool(_isFallingHash, true);
		_context.RawYValue = _context.CharacterAppliedYMovement;
	}

	protected override void UpdateState()
	{
		float currentGravity = _context.JumpGravities[0];
		float previousVelocity = _context.RawYValue;

		float currentYVelocity = previousVelocity + (currentGravity * Time.deltaTime);
		_context.CharacterAppliedYMovement = (previousVelocity + currentYVelocity) * 0.5f;
		_context.RawYValue = currentYVelocity;
	}

	protected override void FixedUpdateState() { }
	public override void ExitState()
	{
		Debug.Log("LEAVE FALLING STATE");
		_context.Animator.SetBool(_isFallingHash, false);
	}
	protected override void InitializeSubState()
	{
		//We could add conditions if we need to, in this case, the character will always spawn idle.
		if (_context.IsMovementPressed)
		{
			if (_context.IsRunning)
			{
				SetSubState(_factory.Run());
			}
			else
			{
				SetSubState(_factory.Walk());
			}
		}
		else
		{
			SetSubState(_factory.Idle());
		}
	}
	public override void CheckSwitchStates()
	{
		//You don't want to be able to buffer the jump button during other states more than JUMP_BUFFER_TIMER seconds
		if (_context.CharacterController.isGrounded)
		{
			SwitchState(_factory.Grounded());
		}
	}
}
