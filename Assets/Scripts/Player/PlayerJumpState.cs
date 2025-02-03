using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Jump");
        rb.gravityScale = (player.jumpHeight * 2) / (player.jumpTime * player.jumpTime * Physics2D.gravity.y * -1);
        float yVelocity = (player.jumpHeight * 2) / player.jumpTime;
        //rb.velocity = new Vector2(rb.velocity.x, player.jumpForce);
        rb.velocity = new Vector2(rb.velocity.x, yVelocity);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (rb.velocity.y <= 0)
        {
            stateMachine.ChangeState(player.airState);
        }
    }
}
