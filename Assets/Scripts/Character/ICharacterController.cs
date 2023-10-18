    public interface ICharacterController<Input, State>
    {
        Input PlayerInput { get; }

        State PlayerState { get; }
    }