using System;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Tooltip("Couleur de l'obstacle une fois touché")]
    [SerializeField] private Material _hitMaterial;

    [SerializeField] private int _penaltySeconds = 1;

    private Renderer _renderer;
    private bool _wasHit;

    /// <summary>
    /// Déclenché quand le joueur touche un obstable,
    /// pénalité en int tranmis
    /// </summary>
    public static event Action<int> Hit;
    
    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.TryGetComponent<Player>(out _) || _wasHit)
        {
            return;
        }

        _wasHit = true;
        _renderer.material = _hitMaterial;
        // Augmenter le hitcount dans le gameManager
        Hit?.Invoke(_penaltySeconds);
    }
}
