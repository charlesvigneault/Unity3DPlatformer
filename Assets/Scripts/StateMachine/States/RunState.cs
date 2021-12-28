using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : BaseState
{
	public RunState(CharacterMovements currentContext, StateFactory stateFactory) : base(currentContext, stateFactory) { }
	public override void EnterState()
	{
		Debug.Log("ENTER RUN STATE");
		_context.Animator.SetBool(_context.IsWalkingHash, true);
		_context.Animator.SetBool(_context.IsRunningHash, true);
	}
	protected override void UpdateState()
	{
		_context.CharacterAppliedXMovement = _context.CurrentMovementsInput.x * _context.RunningSpeed;
		_context.CharacterAppliedZMovement = _context.CurrentMovementsInput.y * _context.RunningSpeed;
	}
	protected override void FixedUpdateState() { }
	public override void ExitState()
	{
		Debug.Log("LEAVE RUN STATE");
	}
	protected override void InitializeSubState() { }
	public override void CheckSwitchStates()
	{
		if (!_context.IsRunning)
		{
			if (_context.IsMovementPressed)
			{
				SwitchState(_factory.Walk());
			}
			else
			{
				SwitchState(_factory.Idle());
			}
		}
	}
}
