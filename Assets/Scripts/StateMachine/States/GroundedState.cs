using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedState : BaseState
{
	//Private constants
	private const float GROUNDED_GRAVITY = -0.1f;
	private const float FALLING_BUFFER = 0.12f;

	//Falling variables
	private float _fallingSince = 0;

	public GroundedState(CharacterMovements currentContext, StateFactory stateFactory) : base(currentContext, stateFactory) { }
	public override void EnterState()
	{
		Debug.Log("ENTER GROUNDED STATE");
		_context.CharacterAppliedYMovement = GROUNDED_GRAVITY;
	}

	protected override void UpdateState()
	{
		_context.CharacterAppliedYMovement = GROUNDED_GRAVITY;

		if (_context.CharacterController.isGrounded)
		{
			_fallingSince = 0;
		}
		else
		{
			Vector3 position = _context.CharacterController.transform.position;
			Ray rayDown = new Ray(position, Vector3.down);
			Physics.Raycast(rayDown, out RaycastHit hitDownInfo, 1);
			if (hitDownInfo.distance > 0 && hitDownInfo.distance < 0.4f)
			{
				position.y = position.y - hitDownInfo.distance;
				_context.CharacterController.Move(new Vector3(0, -hitDownInfo.distance, 0));
			}
			else
			{
				_fallingSince += Time.deltaTime;
			}
		}
	}

	protected override void FixedUpdateState() { }

	public override void ExitState()
	{
		Debug.Log("LEAVE GROUNDED STATE");
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
		if (_context.IsJumpPressed && Time.time - _context.JUMP_BUFFER_TIMER < _context.JumpPressTimer)
		{
			_context.JumpPressTimer = -1000;
			SwitchState(_factory.Jump());
		}
		else
		{
			//Adding a buffer to let the player still be grounded for a while if he want to jump as close to the ledge but have input lag.
			if (_fallingSince > FALLING_BUFFER)
			{
				if (!_context.CharacterController.isGrounded)
				{
					SwitchState(_factory.Falling());
				}
			}
		}
	}
}
