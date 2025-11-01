using UnityEngine;

public class PlayerDodgeState : PlayerBaseState
{
    private readonly int DODGE_HASH = Animator.StringToHash("Dodge");
    private const float CROSS_FADE_DURATION = 0.1f;
    private const float DODGE_DURATION = 0.8f;
    private const float DODGE_SPEED = 20f;

    private float _dodgeTime;
    private Vector3 _dodgeDirection;

    public PlayerDodgeState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        _dodgeTime = DODGE_DURATION;
        _dodgeDirection = StateMachine.transform.forward;
        StateMachine.Animator.CrossFadeInFixedTime(DODGE_HASH, CROSS_FADE_DURATION);
    }

    public override void Tick(float deltaTime)
    {
        _dodgeTime -= deltaTime;

        if (_dodgeTime <= 0f)
        {
            ReturnToLocomotion();
            return;
        }

        Move(_dodgeDirection * DODGE_SPEED, deltaTime);
    }
    
    public override void Exit()
    {
        
    }
}
