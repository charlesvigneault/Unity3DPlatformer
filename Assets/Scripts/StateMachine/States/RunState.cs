using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : BaseState
{
	public RunState(CharacterMovements currentContext, StateFactory stateFactory) : base(currentContext, stateFactory) { }
	public override void EnterState() { }
	public override void UpdateState() { CheckSwitchStates(); }
	public override void FixedUpdateState() { }
	public override void ExitState() { }
	public override void InitializeSubState() { }
	public override void CheckSwitchStates() { }
}
