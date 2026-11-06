using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenuUI : MonoBehaviour
{
    [Header("Références")]
    [Tooltip("Panneau affiché pendant la pause")]
    [SerializeField] private GameObject _pausePanel;
    [Tooltip("Bouton sélectionné à l'ouverture du menu")]
    [SerializeField] private GameObject _firstSelected;

    private GameInput _gameInput;
    private bool _isPaused;

    private void Awake()
    {
        _gameInput = FindAnyObjectByType<GameInput>();
    }

    private void Start()
    {
        _pausePanel.SetActive(false);
    }

    private void OnEnable()
    {
        _gameInput.PausePressed += HandlePausePressed;
    }

    private void OnDisable()
    {
        _gameInput.PausePressed -= HandlePausePressed;
    }

    /// <summary>
    /// Ferme le menu de pause et remet le temps du jeu en marche.
    /// </summary>
    public void Resume()
    {
        _isPaused = false;
        _pausePanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        Time.timeScale = 1f;
    }

    /// <summary>
    /// Abandonne la partie en cours et retourne au menu principal.
    /// </summary>
    public void ReturnToMainMenu()
    {
        GameManager.Instance.ReturnToMainMenu();
    }

    private void Pause()
    {
        _isPaused = true;
        _pausePanel.SetActive(true);
        Time.timeScale = 0f;
        EventSystem.current.SetSelectedGameObject(_firstSelected);
    }

    public void HandlePausePressed()
    {
        // Le même bouton ouvre et ferme le menu
        if (_isPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }
}
