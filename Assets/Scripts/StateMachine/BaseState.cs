public abstract class BaseState
{
	//Monobehavior object
	protected CharacterMovements _context;

	//State variables
	protected StateFactory _factory;
	protected BaseState _currentSubState;
	protected BaseState _currentSuperState;

	public BaseState(CharacterMovements currentContext, StateFactory playerStatefactory)
	{
		_context = currentContext;
		_factory = playerStatefactory;
	}

	//Enter leave state(s)
	public abstract void EnterState();
	public abstract void ExitState();
	public void EnterStates()
	{
		InitializeSubState();
		EnterState();
		if (_currentSubState != null)
		{
			_currentSubState.EnterState();
		}
	}
	public void ExitStates()
	{
		if (_currentSubState != null)
		{
			_currentSubState.ExitState();
		}
		ExitState();
	}

	//Switch state / sub state
	public abstract void CheckSwitchStates();
	public void SwitchState(BaseState newState)
	{
		if (_currentSuperState != null)
		{
			_currentSuperState.SwitchSubState(newState);
		}
		else
		{
			_context.SwitchState(newState);
		}
	}

	//Update state(s)
	protected abstract void UpdateState();
	protected abstract void FixedUpdateState();
	public void UpdateStates()
	{
		//Look at the child first because if the child switch state on the same frame as the parent the parent will just override it.
		//It was causing a bug where the state was switching to jump and after the animation was switching to run
		if (_currentSubState != null)
		{
			_currentSubState.UpdateStates();
		}
		UpdateState();
		CheckSwitchStates();
	}
	public void FixedUpdateStates()
	{
		//Look at the child first because if the child switch state on the same frame as the parent the parent will just override it.
		//It was causing a bug where the state was switching to jump and after the animation was switching to run
		if (_currentSubState != null)
		{
			_currentSubState.FixedUpdateStates();
		}
		FixedUpdateState();
	}

	//Sub state methods
	protected abstract void InitializeSubState();
	protected void SetSuperState(BaseState newSuperState)
	{
		_currentSuperState = newSuperState;
	}
	public void SwitchSubState(BaseState newSubState)
	{
		if (newSubState != null && _currentSubState != newSubState)
		{
			if (_currentSubState != null)
			{
				_currentSubState.ExitState();
			}

			SetSubState(newSubState);

			newSubState.EnterStates();

		}
	}
	protected void SetSubState(BaseState newSubState)
	{
		newSubState.SetSuperState(this);
		_currentSubState = newSubState;
	}
}