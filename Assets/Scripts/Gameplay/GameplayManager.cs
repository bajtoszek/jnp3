using UnityEngine;

public class GameplayManager : ASingleton<GameplayManager>
{
    [SerializeField]
    private UIGamePanel m_UIGamePanel = null;

    private int m_Points = 0;

    private bool m_Paused = false;

    public void HandlePoitnsAdded(int addedPoints)
    {
        m_Points += addedPoints;
        m_UIGamePanel.SetPoints(m_Points);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            m_Paused = !m_Paused;
            Time.timeScale = m_Paused ? 0f : 1f;

            m_UIGamePanel.SetPauseMenuVisible(m_Paused);
        }   
    }

#if UNITY_EDITOR
    [ContextMenu(nameof(AddPoints_DEBUG))]
    private void AddPoints_DEBUG()
    {
        HandlePoitnsAdded(999);
    }
#endif
}
