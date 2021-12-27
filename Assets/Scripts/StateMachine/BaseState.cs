public abstract class BaseState
{
	protected CharacterMovements _context;
	protected StateFactory _factory;
	public BaseState(CharacterMovements currentContext, StateFactory playerStatefactory)
	{
		_context = currentContext;
		_factory = playerStatefactory;
	}

	public abstract void EnterState();
	public abstract void UpdateState();
	public abstract void FixedUpdateState();
	public abstract void ExitState();
	public abstract void CheckSwitchStates();
	public abstract void InitializeSubState();

	private void UpdateStates() { }

	protected void SetSuperState() { }

	protected void SetSubState() { }

}