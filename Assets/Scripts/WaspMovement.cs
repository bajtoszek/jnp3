using UnityEngine;

public class WaspMovement : APooledObject
{
    [SerializeField]
    private Vector2 m_MovementSpeed;

    [SerializeField]
    private float m_SideMovementAmplitude = 1f;

    [SerializeField]
    private Rigidbody m_Rigidbody = null;

    [SerializeField]
    private DestructibleObject m_DestructibleObject = null;

    private void FixedUpdate()
    {
        Vector3 direction = transform.forward * m_MovementSpeed.y + 
            new Vector3(Mathf.Sin(Time.time * m_MovementSpeed.x) * m_SideMovementAmplitude, 0f, 0f);

        Vector3 targetPosition = m_Rigidbody.position + direction * Time.fixedDeltaTime;
        m_Rigidbody.MovePosition(targetPosition);
    }

    public void HandleDestroyed()
    {
        GameplayManager.Instance.HandlePoitnsAdded(999);
        ReturnToPool();
    }

    public void Reset(Vector3 worldPosition)
    {
        transform.position = worldPosition;
        gameObject.SetActive(true);

        m_DestructibleObject.Reset();
    }
}
