using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : APooledObject
{
    [SerializeField]
    private float m_MovementSpeed = 10f;

    [SerializeField]
    private Rigidbody m_Rigidbody = null;

    private void FixedUpdate()
    {
        Vector3 targetPosition = m_Rigidbody.position + transform.forward * m_MovementSpeed * Time.fixedDeltaTime;
        m_Rigidbody.MovePosition(targetPosition);
    }

    private void OnTriggerEnter(Collider other)
    {
        ReturnToPool();
    }
}
