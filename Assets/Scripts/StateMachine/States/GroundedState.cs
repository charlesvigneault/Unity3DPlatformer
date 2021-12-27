using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedState : BaseState
{
	private const float GROUNDED_GRAVITY = -0.1f;
	public GroundedState(CharacterMovements currentContext, StateFactory stateFactory) : base(currentContext, stateFactory) { }
	public override void EnterState()
	{
		Debug.Log("ENTER GROUNDED STATE");
		_context.CharacterAppliedYMovement = GROUNDED_GRAVITY;
	}
	public override void UpdateState() { CheckSwitchStates(); }
	public override void FixedUpdateState() { }
	public override void ExitState()
	{
		Debug.Log("LEAVE GROUNDED STATE");
	}
	public override void InitializeSubState() { }
	public override void CheckSwitchStates()
	{
		//You don't want to be able to buffer the jump button during other states more than JUMP_BUFFER_TIMER seconds
		if (_context.IsJumpPressed && Time.time - _context.JUMP_BUFFER_TIMER < _context.JumpPressTimer)
		{
			_context.SwitchState(_factory.Jump());
		}
	}
}
