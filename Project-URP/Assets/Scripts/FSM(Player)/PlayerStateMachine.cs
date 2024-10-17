using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerStateMachine
{
    public IState CurrentState { get; private set; }

    public IdleState m_idleState;
    public WalkState m_walkState;
    public RunState m_runState;
    public JumpState m_jumpState;

    public PlayerStateMachine(Player player)
    {
        m_idleState = new IdleState(player);
        m_walkState = new WalkState(player);
        m_runState = new RunState(player);
        m_jumpState = new JumpState(player);
    }
    /// <summary>처음 상태 초기화 </summary>
    public void InitState(IState initialState)
    {
        CurrentState = initialState;
        if(CurrentState != null)
        {
            CurrentState.Enter();
        }
    }
    /// <summary>상태 전환</summary>
    public void TransitionState(IState nextState)
    {
        CurrentState.Exit();
        CurrentState = nextState;
        CurrentState.Enter();
    }
    // Player.cs 가 호출
    public void Execute()
    {
        if(CurrentState != null)
        {
            CurrentState.Execute();
        }
    }
}
