using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenuUI : MonoBehaviour
{
    [Header("Affichage")]
    [Tooltip("Texte qui reçoit le bilan de la partie")]
    [SerializeField] private TMP_Text _summaryText;

    private void Start()
    {
        // Scène lancée seule dans l'éditeur : aucune partie n'a été jouée
        if (GameManager.Instance == null)
        {
            _summaryText.text = "Aucune partie jouée.";
            return;
        }

        GameManager manager = GameManager.Instance;

        // Le résultat final ajoute les pénalités au temps
        float finalResult = manager.TotalTime + manager.TotalPenalty;

        _summaryText.text = $"Temps total : {manager.TotalTime:F2} s\n"
            + $"Collisions : {manager.TotalHits}\n"
            + $"Pénalités : {manager.TotalPenalty} s\n"
            + $"Résultat final : {finalResult:F2} s";
    }

    /// <summary>
    /// Termine la partie et retourne au menu principal.
    /// </summary>
    public void ReturnToMainMenu()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ReturnToMainMenu();
        }
        else
        {
            // Aucun gestionnaire à détruire : le menu occupe l'index 0
            SceneManager.LoadScene(0);
        }
    }

    /// <summary>
    /// Ferme le jeu, ou arrête le mode Play dans l'éditeur.
    /// </summary>
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
