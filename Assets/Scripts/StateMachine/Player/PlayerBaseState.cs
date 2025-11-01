using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine StateMachine;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.StateMachine = stateMachine;
    }

    protected void Move(Vector3 motion, float deltaTime)
    {
        StateMachine.Controller.Move((motion + StateMachine.ForceReceiver.Movement) * deltaTime);
    }

    protected void Move(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
    }

    protected void ReturnToLocomotion()
    {
        StateMachine.SwitchState(new PlayerFreeLookState(StateMachine, false));
    }
    
}
