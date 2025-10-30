using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        StateMachine.InputReader.DodgeEvent += OnDodge;
    }

    public override void Tick(float deltaTime)
    {
        Vector3 movement = CalculateMovement();
        Move(movement * StateMachine.FreeLookMoveSpeed, deltaTime);
    }

    public override void Exit()
    {
        StateMachine.InputReader.DodgeEvent -= OnDodge;
    }

    private void OnDodge()
    {

    }
    
    private Vector3 CalculateMovement()
    {
        Vector2 inputMovement = StateMachine.InputReader.MovementValue;
        Transform cameraTransform = StateMachine.MainCameraTransform;

        Vector3 forward = cameraTransform.forward;
        forward.y = 0f;
        forward.Normalize();

        Vector3 right = cameraTransform.right;
        right.y = 0f;
        right.Normalize();

        Vector3 movement = forward * inputMovement.y + right * inputMovement.x;
        return movement.normalized;
    }
}
