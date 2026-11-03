using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private GameObject _instructionsPanel;

    [SerializeField] private GameObject _mainFirstSelected;
    [SerializeField] private GameObject _instructionsFirstSelected;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif 
    }

    public void ShowInstructions()
    {
        _instructionsPanel.SetActive(true);
        _mainPanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(_instructionsFirstSelected);
    }

    public void ShowMainPanel()
    {
        _instructionsPanel.SetActive(false);
        _mainPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(_mainFirstSelected);
    }
}
