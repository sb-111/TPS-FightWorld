using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof(PlayerKeyInput), typeof(PlayerMove) )]
public class Player : MonoBehaviour
{
    private PlayerKeyInput playerKeyInput;
    private PlayerMove playerMove;
    private PlayerStateMachine playerStateMachine;

    void Awake()
    {
        playerKeyInput = GetComponent<PlayerKeyInput>();
        playerMove = GetComponent<PlayerMove>();

        // 플레이어 FSM 생성
        playerStateMachine = new PlayerStateMachine(this);
    }
    void Start()
    {
        playerStateMachine.InitState(playerStateMachine.m_idleState);
    }
    // Update is called once per frame
    void Update()
    {
        playerStateMachine.Execute();

      
    }
}
