using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    private int _totalPenalty;
    public int TotalPenalty => _totalPenalty;

    private float _totalTime;
    public float TotalTime => _totalTime;

    private int _totalHits;
    public int TotalHits => _totalHits;

    public static event Action<int> PenaltyChanged;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        Obstacle.Hit += HandleObstacleHit;
    }

    private void OnDisable()
    {
        Obstacle.Hit -= HandleObstacleHit;
    }

    private void HandleObstacleHit(int p_penaltySeconds)
    {
        _totalHits++;
        _totalPenalty += p_penaltySeconds;
        PenaltyChanged?.Invoke(_totalPenalty);
    }

    /// <summary>
    /// Affiche le temps et le résultats du niveau qui vient de se terminer
    /// </summary>
    public void CompleteLevel()
    {

        _totalTime += Time.timeSinceLevelLoad;

        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
   
        if (nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextIndex);
        }
    }

    public void ReturnToMainMenu()
    {
        // S'assurer que le jeu est pas en pause pour la nouvelle partie;
        Time.timeScale = 1.0f;
        
        // Détruit le gameManager pour en faire un neuf à la partie suivante
        Destroy(gameObject);
        SceneManager.LoadScene(0);
    }
}
