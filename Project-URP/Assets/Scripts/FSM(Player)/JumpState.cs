using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : IState
{
    Player player;
    public JumpState(Player player)
    {
        this.player = player;
    }

    public void Enter()
    {
        throw new System.NotImplementedException();
    }

    public void Execute()
    {
        throw new System.NotImplementedException();
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }

}
