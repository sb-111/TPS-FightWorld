using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerKeyInput), typeof(Rigidbody), typeof(Animator))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float m_walkSpeed;
    [SerializeField] private float m_runSpeed;
    [SerializeField] private float m_jumpPower;
    [SerializeField] private LayerMask m_layerMask;
    public Transform cam;

    private PlayerKeyInput playerKeyInput;
    private Rigidbody rigid;
    private Animator animator;

    private Vector3 m_inputVec;
    

    void Awake()
    {
        playerKeyInput = GetComponent<PlayerKeyInput>();
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        m_inputVec = playerKeyInput.m_InputVec;
        UpdateAnimation();
        //Debug.DrawRay(transform.position, transform.forward * 10f, Color.blue);
        //Debug.DrawRay(transform.position, transform.right * 10f, Color.red);
    }
    void FixedUpdate()
    {
           
        Jump();
        Move();
    }
    // 점프 함수를 분리할 필요가 없을듯하다. 똑같은 메커니즘이다.
    private void Move()
    {
        if (playerKeyInput.CheckInputVector(m_inputVec))  // 키 입력이 되었는지 체크
        {
            // 입력된 인풋벡터가 카메라의 회전을 고려해야함
            float targetAngle = Mathf.Atan2(m_inputVec.x, m_inputVec.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            // 플레이어 회전
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

            // 플레이어의 이동 방향
            Vector3 moveVec = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            // 목적지
            Vector3 targetDestination = moveVec.normalized * (playerKeyInput.IsRunning ? m_runSpeed : m_walkSpeed) * Time.deltaTime;
            if(!CheckWallInFrontOfPlayer())
                // 플레이어 이동
                rigid.MovePosition(transform.position + targetDestination);
        }

    }
    private void Jump()
    {
        // 1. 점프키를 눌렀는지 체크 && 땅에 있는지 체크
        // 2. 월드좌표 y축 방향으로 힘 가함
        if(playerKeyInput.IsJumpKeyDown && CheckIsGrounded())
        {
            rigid.AddForce(Vector3.up * m_jumpPower, ForceMode.VelocityChange);
        }
    }
    private bool CheckIsGrounded()
    {
        RaycastHit hit;
        if (Physics.Raycast((transform.position + Vector3.up * .3f), Vector3.down, out hit, .4f, m_layerMask))
        {
            return true; // 땅에 있는거
        }
        return false; // 공중에 있는거
    }
    private bool CheckWallInFrontOfPlayer()
    {
        Debug.DrawRay(transform.position+Vector3.up*3f, transform.forward * .5f, Color.green);
        RaycastHit hit;
        if(Physics.Raycast(transform.position+Vector3.up*3f, transform.forward, out hit, .5f))
        {
            return true;
        }
        return false;
    }
    private void CalculateIncline()
    {

    }
    private void UpdateAnimation()
    {
        animator.SetBool("IsMoving", playerKeyInput.IsMoving);
        animator.SetFloat("Speed", playerKeyInput.IsRunning ? m_runSpeed : m_walkSpeed);
    }
}
