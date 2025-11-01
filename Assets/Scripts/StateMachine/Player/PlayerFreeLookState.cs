using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    private readonly int FREE_LOOK_BLEND_TREE_HASH = Animator.StringToHash("FreeLookBlendTree");
    private readonly int FREE_LOOK_SPEED_HASH = Animator.StringToHash("FreeLookSpeed");
    private const float ANIMATOR_DAMP_TIME = 0.1f;
    private const float CROSS_FADE_DURATION = 0.1f; 
    private bool _shouldFade;
    public PlayerFreeLookState(PlayerStateMachine stateMachine, bool shouldFade = true) : base(stateMachine)
    {
        _shouldFade = shouldFade;
    }

    public override void Enter()
    {
        StateMachine.InputReader.DodgeEvent += OnDodge;
        StateMachine.InputReader.ReloadEvent += OnReload;

        StateMachine.Animator.SetFloat(FREE_LOOK_SPEED_HASH, 0f);
        if (_shouldFade)
        {
            StateMachine.Animator.CrossFadeInFixedTime(FREE_LOOK_BLEND_TREE_HASH, CROSS_FADE_DURATION);
        }
        else
        {
            StateMachine.Animator.Play(FREE_LOOK_BLEND_TREE_HASH);
        }
    }

    public override void Tick(float deltaTime)
    {
        if (StateMachine.InputReader.IsShooting)
        {
            StateMachine.SwitchState(new PlayerShootState(StateMachine));
            return;
        }
        
        Vector3 movement = CalculateMovement();
        Move(movement * StateMachine.FreeLookMoveSpeed, deltaTime);
        if (StateMachine.InputReader.MovementValue == Vector2.zero)
        {
            StateMachine.Animator.SetFloat(FREE_LOOK_SPEED_HASH, 0, ANIMATOR_DAMP_TIME, deltaTime);
            return;
        }
        StateMachine.Animator.SetFloat(FREE_LOOK_SPEED_HASH, 1, ANIMATOR_DAMP_TIME, deltaTime);

        FaceMovementDirection(movement, deltaTime);
    }

    public override void Exit()
    {
        StateMachine.InputReader.DodgeEvent -= OnDodge;
    }

    private void OnDodge()
    {
        StateMachine.SwitchState(new PlayerDodgeState(StateMachine));
    }

    private void OnReload()
    {
        StateMachine.SwitchState(new PlayerReloadState(StateMachine));
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
    
    private void FaceMovementDirection(Vector3 movement, float deltaTime)
    {
        StateMachine.transform.rotation = Quaternion.Slerp(
            StateMachine.transform.rotation,
            Quaternion.LookRotation(movement),
            deltaTime * StateMachine.RotationDamping
        );
    }
}
