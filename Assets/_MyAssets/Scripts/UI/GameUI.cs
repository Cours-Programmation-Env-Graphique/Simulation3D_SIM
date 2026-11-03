using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _timeText;
    [SerializeField] private TMP_Text _hitText;
    [SerializeField] private TMP_Text _penaltyText;

    [SerializeField] private float _hitMessageDuration = 1f;

    private float _hitMessageEndTime;
    private float _previousLevelsTime;

    private void OnEnable()
    {
        Obstacle.Hit += HandleObstacleHit;
        GameManager.PenaltyChanged += HandlePenaltyChanged;
    }

    private void OnDisable()
    {
        Obstacle.Hit -= HandleObstacleHit;
        GameManager.PenaltyChanged -= HandlePenaltyChanged;
    }

    private void Start()
    {
        _previousLevelsTime = GameManager.Instance.TotalTime;
        _penaltyText.text = $"Pénalité : {GameManager.Instance.TotalPenalty}";
        _hitText.gameObject.SetActive(false);
    }

    private void HandlePenaltyChanged(int p_totalPenalty)
    {
        _penaltyText.text = $"Pénalité : {p_totalPenalty} s";
    }

    private void HandleObstacleHit(int p_penaltySeconds)
    {
        _hitText.text = $"+{p_penaltySeconds} s";
        _hitText.gameObject.SetActive(true);
        _hitMessageEndTime = Time.time + _hitMessageDuration;
    }

    private void Update()
    {
        // Le temps change à chaque frame
        float elapsedTime = _previousLevelsTime + Time.timeSinceLevelLoad;
        _timeText.text = $"Temps : {elapsedTime:F2}";

        if(_hitText.gameObject.activeSelf && Time.time >= _hitMessageEndTime)
        {
            _hitText.gameObject.SetActive(false);
        }
    }
}
