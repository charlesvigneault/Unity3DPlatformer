using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpState : BaseState
{
	//Animator hashs
	private int _isWallJumpHash;

	public WallJumpState(CharacterMovements currentContext, StateFactory stateFactory) : base(currentContext, stateFactory)
	{
		_isWallJumpHash = Animator.StringToHash("isWallJump");
	}
	public override void EnterState()
	{
		Debug.Log("ENTER WALL JUMP STATE");
		_context.Animator.SetBool(_isWallJumpHash, true);
	}

	protected override void UpdateState()
	{
	}

	protected override void FixedUpdateState() { }
	public override void ExitState()
	{
		Debug.Log("LEAVE WALL JUMP STATE");
		_context.Animator.SetBool(_isWallJumpHash, true);
	}
	protected override void InitializeSubState()
	{
	}
	public override void CheckSwitchStates()
	{
	}
}
