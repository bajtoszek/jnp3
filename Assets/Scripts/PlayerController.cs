using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Animator m_Animator = null;

    [SerializeField]
    private float m_Speed = 100f;

    private float m_Vertical = 0f;

    private float m_Horizontal = 0f;

    private readonly int m_HorizontalAnimatorHash = Animator.StringToHash("Horizontal");

    private readonly int m_VerticalAnimatorHash = Animator.StringToHash("Vertical");

    private void GetInput()
    {
        m_Horizontal = Input.GetAxis("Horizontal");
        m_Vertical = Input.GetAxis("Vertical");
    }

    private void UpdateAnimator()
    {
        m_Animator.SetFloat(m_HorizontalAnimatorHash, m_Horizontal);
        m_Animator.SetFloat(m_VerticalAnimatorHash, m_Vertical);
    }

    private void FixedUpdate()
    {
        Vector3 deltaPosition = new Vector3(m_Horizontal, 0f, m_Vertical);
        transform.position += deltaPosition * Time.deltaTime * m_Speed;
    }

    private void Update()
    {
        GetInput();
        UpdateAnimator();
    }
}
