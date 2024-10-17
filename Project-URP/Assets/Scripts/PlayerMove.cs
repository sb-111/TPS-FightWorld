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
    // ���� �Լ��� �и��� �ʿ䰡 �������ϴ�. �Ȱ��� ��Ŀ�����̴�.
    private void Move()
    {
        if (playerKeyInput.CheckInputVector(m_inputVec))  // Ű �Է��� �Ǿ����� üũ
        {
            // �Էµ� ��ǲ���Ͱ� ī�޶��� ȸ���� ����ؾ���
            float targetAngle = Mathf.Atan2(m_inputVec.x, m_inputVec.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            // �÷��̾� ȸ��
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

            // �÷��̾��� �̵� ����
            Vector3 moveVec = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            // ������
            Vector3 targetDestination = moveVec.normalized * (playerKeyInput.IsRunning ? m_runSpeed : m_walkSpeed) * Time.deltaTime;
            if(!CheckWallInFrontOfPlayer())
                // �÷��̾� �̵�
                rigid.MovePosition(transform.position + targetDestination);
        }

    }
    private void Jump()
    {
        // 1. ����Ű�� �������� üũ && ���� �ִ��� üũ
        // 2. ������ǥ y�� �������� �� ����
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
            return true; // ���� �ִ°�
        }
        return false; // ���߿� �ִ°�
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
