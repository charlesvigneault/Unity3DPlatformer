using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseStateMachine : MonoBehaviour
{
	//State variables
	private BaseState _currentState;
	private BaseState _previousState = null;

	//Getters and setters
	public BaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
	public BaseState PreviousState { get { return _previousState; } set { _previousState = value; } }

	private bool _inTransition = false;

	public void SwitchState(BaseState newState)
	{
		if (newState != null && _currentState != newState && !_inTransition)
		{
			_inTransition = true;
			//Call current child class state exit state
			if (_currentState != null)
			{
				_currentState.ExitStates();
				_previousState = _currentState;
			}

			//New state enter state
			newState.EnterStates();

			//Switch the state in the context
			_currentState = newState;

			_inTransition = false;
		}
	}

	public void RevertState()
	{
		//All the null verifications necessary are made in the SwitchState
		SwitchState(_previousState);
	}

	public virtual void Update()
	{
		if (_currentState != null && !_inTransition)
		{
			_currentState.UpdateStates();
		}
	}

	public virtual void FixedUpdate()
	{
		if (_currentState != null && !_inTransition)
		{
			_currentState.FixedUpdateStates();
		}
	}

}
