using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TextMeshProUGUI tmProReference;
    float totalTime = 0;
    public Canvas gameplayUI;
    public Canvas gameMenu;
    public static bool gamePaused;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Cursor.lockState = CursorLockMode.Locked;

        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (!gamePaused)
        {
            totalTime += Time.deltaTime;
            TimeSpan timer = TimeSpan.FromSeconds(totalTime);
            tmProReference.text = timer.ToString("mm':'ss':'ff");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMode(true);
        }

    }
    private void ChangeCursorMode(bool unlocked)
    {
        if (unlocked)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void PauseMode(bool paused)
    {
        gamePaused = paused;
        OnPause(paused);
        ChangeCursorMode(paused);
    }

    private void OnPause(bool paused)
    {
        if (paused)
        {
            gameMenu.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            gameplayUI.gameObject.SetActive(true);
            gameMenu.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

    public void ReduceTimerForBonus(float time)
    {
        totalTime -= time;
        totalTime = Mathf.Max(0, totalTime);
    }
}
