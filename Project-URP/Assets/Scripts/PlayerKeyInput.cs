using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeyInput : MonoBehaviour
{
    private Vector3 m_inputVec;

    private float m_inputHorizontal;
    private float m_inputVertical;

    private bool isMoving;
    private bool isRunning;
    private bool isJumpKeyDown;

    // 프로퍼티
    public Vector3 m_InputVec{ get { return m_inputVec; }}
    public bool IsMoving { get { return isMoving; } }
    public bool IsRunning { get { return isRunning; } }
    public bool IsJumpKeyDown {  get { return isJumpKeyDown; } set { isJumpKeyDown = value; } }
    void Update()
    {
        m_inputHorizontal = Input.GetAxis("Horizontal");
        m_inputVertical = Input.GetAxis("Vertical");
        m_inputVec = new Vector3(m_inputHorizontal, 0f, m_inputVertical);

        if (CheckInputVector(m_inputVec))
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumpKeyDown = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            IsJumpKeyDown = false;
        }



    }
    /// <summary>
    /// 인풋벡터가 크기를 가지는지 체크
    /// </summary>
    public bool CheckInputVector(Vector3 vec)
    {
        return (vec.magnitude >=.1f) ? true : false; 
    }
}
