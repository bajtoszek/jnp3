using System;
using System.Collections;
using UnityEngine;

public class GameplayManager : ASingleton<GameplayManager>
{
    [SerializeField]
    private UIGamePanel m_UIGamePanel = null;

    private int m_Points = 0;

    private bool m_Paused = false;
	
	private AudioManager audioManager;
	
	public void Start()
	{
		audioManager = FindObjectOfType<AudioManager>();
	}

    public void HandlePointsAdded(int addedPoints)
    {
		audioManager.Play("points");
        m_Points += addedPoints;
        m_UIGamePanel.SetPoints(m_Points);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();        
        }   
    }

    public void TogglePause()
    {
        SetPause(!m_Paused);
        m_UIGamePanel.SetPauseMenuVisible(m_Paused);
    }

    private void SetPause(bool paused)
    {
        m_Paused = paused;
        Time.timeScale = m_Paused ? 0f : 1f;
    }

    public void RestartLevel()
    {
        SceneLoader.LoadScene(SceneNamesConsts.SPACE_SCENE_NAME, null);
    }

    public void GoToMainMenu()
    {
        SceneLoader.LoadScene(SceneNamesConsts.MAIN_MENU_SCENE_NAME, null);
    }

    private void OnDestroy()
    {
        SetPause(false);
    }

    public void HandlePlayerDestroyed()
    {
        StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(2f);
        RestartLevel();
    }

#if UNITY_EDITOR
    [ContextMenu(nameof(AddPoints_DEBUG))]
    private void AddPoints_DEBUG()
    {
        HandlePointsAdded(999);
    }
#endif
}
