using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : BaseState
{
	public WalkState(CharacterMovements currentContext, StateFactory stateFactory) : base(currentContext, stateFactory) { }
	public override void EnterState()
	{
		Debug.Log("ENTER WALK STATE");
		_context.Animator.SetBool(_context.IsWalkingHash, true);
		_context.Animator.SetBool(_context.IsRunningHash, false);
	}
	protected override void UpdateState()
	{
		_context.CharacterAppliedXMovement = _context.CurrentMovementsInput.x * _context.MovementSpeed;
		_context.CharacterAppliedZMovement = _context.CurrentMovementsInput.y * _context.MovementSpeed;
	}
	protected override void FixedUpdateState() { }
	public override void ExitState()
	{
		Debug.Log("LEAVE WALK STATE");
	}
	protected override void InitializeSubState() { }
	public override void CheckSwitchStates()
	{
		if (_context.IsRunning)
		{
			SwitchState(_factory.Run());
		}
		else if (!_context.IsMovementPressed)
		{
			SwitchState(_factory.Idle());
		}
	}
}
