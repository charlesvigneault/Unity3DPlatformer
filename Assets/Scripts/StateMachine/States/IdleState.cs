using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
	public IdleState(CharacterMovements currentContext, StateFactory stateFactory) : base(currentContext, stateFactory) { }
	public override void EnterState()
	{
		Debug.Log("ENTER IDLE STATE");
		_context.Animator.SetBool(_context.IsWalkingHash, false);
		_context.Animator.SetBool(_context.IsRunningHash, false);
		_context.CharacterAppliedXMovement = 0;
		_context.CharacterAppliedZMovement = 0;
	}
	protected override void UpdateState() { CheckSwitchStates(); }
	protected override void FixedUpdateState() { }
	public override void ExitState()
	{
		Debug.Log("LEAVE IDLE STATE");
	}
	protected override void InitializeSubState() { }
	public override void CheckSwitchStates()
	{
		if (_context.IsMovementPressed)
		{
			if (_context.IsRunning)
			{
				SwitchState(_factory.Run());
			}
			else
			{
				SwitchState(_factory.Walk());
			}
		}
	}
}
