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
		if (_context.CharacterController.isGrounded && OnSlope())
		{
			//To change, now I just apply a big gravity so the player stay stick to the ground
			_context.CharacterAppliedYMovement = -5f;
		}
		else
		{
			_context.CharacterAppliedYMovement = GROUNDED_GRAVITY;
		}
	}

	private bool OnSlope()
	{
		RaycastHit hit;
		if (Physics.Raycast(_context.transform.position, Vector3.down, out hit, (_context.CharacterController.height / 4) * 3))
		{
			if (hit.normal != Vector3.up)
			{
				return true;
			}
		}

		return false;
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
			if (!_context.CharacterController.isGrounded)
			{
				//Adding a buffer to let the player still be grounded for a while if he want to jump as close to the ledge but have input lag.
				if (_fallingSince > FALLING_BUFFER)
				{
					SwitchState(_factory.Falling());
				}
				else
				{
					_fallingSince += Time.deltaTime;
				}

			}
			else
			{
				_fallingSince = 0;
			}
		}
	}
}
