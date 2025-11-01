using UnityEngine;

public class PlayerReloadState : PlayerBaseState
{
    private readonly int RELOAD_HASH = Animator.StringToHash("Reload");
    private const float CROSS_FADE_DURATION = 0.1f;
    private const float RELOAD_DURATION = 2.0f;
    private float _reloadTime;

    public PlayerReloadState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        _reloadTime = RELOAD_DURATION;
        StateMachine.Animator.CrossFadeInFixedTime(RELOAD_HASH, CROSS_FADE_DURATION);
    }

    public override void Tick(float deltaTime)
    {
        _reloadTime -= deltaTime;

        if (_reloadTime <= 0f)
        {
            ReturnToLocomotion();
            return;
        }
    }

    public override void Exit()
    {
        
    }
}
