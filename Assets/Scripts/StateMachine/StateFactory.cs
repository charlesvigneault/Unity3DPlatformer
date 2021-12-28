public class StateFactory
{
	private CharacterMovements _context;
	private IdleState _idleState;
	private WalkState _walkState;
	private RunState _runState;
	private JumpState _jumpState;
	private GroundedState _groundedState;
	private FallingState _fallingState;

	private StateFactory() { }
	public StateFactory(CharacterMovements currentContext)
	{
		_context = currentContext;
		_idleState = new IdleState(_context, this);
		_walkState = new WalkState(_context, this);
		_runState = new RunState(_context, this);
		_jumpState = new JumpState(_context, this);
		_groundedState = new GroundedState(_context, this);
		_fallingState = new FallingState(_context, this);
	}

	public BaseState Idle() { return _idleState; }
	public BaseState Walk() { return _walkState; }
	public BaseState Run() { return _runState; }
	public BaseState Jump() { return _jumpState; }
	public BaseState Grounded() { return _groundedState; }
	public BaseState Falling() { return _fallingState; }
}
