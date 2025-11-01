using UnityEngine;

public class PlayerShootState : PlayerBaseState
{
    private readonly int SHOOT_HASH = Animator.StringToHash("Shoot");
    private const float CROSS_FADE_DURATION = 0.1f;

    public PlayerShootState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        StateMachine.Animator.CrossFadeInFixedTime(SHOOT_HASH, CROSS_FADE_DURATION);
        StateMachine.Weapon.Shoot();
    }

    public override void Tick(float deltaTime)
    {
        if (StateMachine.InputReader.IsShooting == false)
        {
            ReturnToLocomotion();
            return;
        }
    }
    
    public override void Exit()
    {

    }
}
