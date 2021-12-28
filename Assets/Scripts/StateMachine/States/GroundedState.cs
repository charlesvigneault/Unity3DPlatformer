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
		if (_context.CharacterController.isGrounded)
		{
			//TEMPORARY CODE, SEE METHOD COMMENT
			float slopeAngle = SlopeAngle();

			if (slopeAngle > 40)
			{
				_context.CharacterAppliedYMovement = -9f;
			}
			else if (slopeAngle > 35)
			{
				_context.CharacterAppliedYMovement = -8f;
			}
			else
			{
				_context.CharacterAppliedYMovement = -7f;
			}
		}
		else
		{
			_context.CharacterAppliedYMovement = GROUNDED_GRAVITY;
		}
	}

	//Temporary method. We should look for ground and keep character on ground level when leaving ground in slope.
	private float SlopeAngle()
	{
		RaycastHit hit;
		if (Physics.Raycast(_context.transform.position, Vector3.down, out hit, (_context.CharacterController.height / 4) * 3))
		{
			return Vector3.Angle(Vector3.up, hit.normal);
		}

		return 0;
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
