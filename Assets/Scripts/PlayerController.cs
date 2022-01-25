using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Animator m_Animator = null;

    [SerializeField]
    private float m_Speed = 100f;

    [SerializeField]
    private WeaponList m_Weapons = null;

    [SerializeField]
    private Rigidbody m_Rigidbody = null;

    private float m_Vertical = 0f;

    private float m_Horizontal = 0f;

    private bool m_Shoot = false;

    private readonly int m_HorizontalAnimatorHash = Animator.StringToHash("Horizontal");

    private readonly int m_VerticalAnimatorHash = Animator.StringToHash("Vertical");

    private readonly int m_AttackingHash = Animator.StringToHash("Attacking");

    [Header("Staminabar")]
    [SerializeField]
    private Vector3 m_StaminabarOffset;

    [SerializeField]
    private Staminabar m_StaminabarPrefab = null;

    private Staminabar m_Staminabar = null;

    [SerializeField]
    private int m_TotalStamina = 100;

    [SerializeField]
    private float m_CurrentStamina = 100;

    [SerializeField]
    private int m_StaminaPerShot = 10;

    [SerializeField]
    private int m_StaminaRegenerationRate = 10;

    private void GetInput()
    {
        m_Horizontal = Input.GetAxis("Horizontal");
        m_Vertical = Input.GetAxis("Vertical");

        m_Shoot = Input.GetButton("Shoot");
    }

    private void UpdateAnimator()
    {
        //m_Animator.SetFloat(m_HorizontalAnimatorHash, m_Horizontal);
        //m_Animator.SetFloat(m_VerticalAnimatorHash, m_Vertical);
        m_Animator.SetBool(m_AttackingHash, m_Shoot);
    }

    private void FixedUpdate()
    {
        Vector3 newPosition = m_Rigidbody.position +  new Vector3(m_Horizontal, 0f, m_Vertical) * Time.deltaTime * m_Speed;
        m_Rigidbody.MovePosition(newPosition);

    }

    private void Update()
    {
        GetInput();
        UpdateAnimator();

        if (m_Staminabar == null)
        {
            PrefabPool<Staminabar> staminabarPool = PoolManager.Instance.GetPool(m_StaminabarPrefab);

            m_Staminabar = staminabarPool.Get();
            m_Staminabar.Reset(transform, m_StaminabarOffset);
        }
                
        m_CurrentStamina = Mathf.Min(m_CurrentStamina + Time.deltaTime * m_StaminaRegenerationRate, m_TotalStamina);

        if (m_Shoot && m_CurrentStamina >= m_StaminaPerShot)
        {
            m_Weapons.FireAll();
            
            m_CurrentStamina -= m_StaminaPerShot;
        }
        
        m_Staminabar.SetPercentage((float)m_CurrentStamina / m_TotalStamina);
    }

    public void HandleDestroyed()
    {
		FindObjectOfType<AudioManager>().Play("boom");
        GameplayManager.Instance.HandlePlayerDestroyed();
        Destroy(gameObject);
    }
}
