using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public event Action PausePressed;

    private PlayerInputActions _playerInputActions;

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
    }

    private void OnDestroy()
    {
        _playerInputActions.Player.Disable();
        _playerInputActions.Dispose();
    }

    private void OnEnable()
    {
        _playerInputActions.Player.Pause.performed += Pause_performed;
    }

    private void OnDisable()
    {
        _playerInputActions.Player.Pause.performed -= Pause_performed;
    }

    private void Pause_performed(InputAction.CallbackContext obj)
    {
        PausePressed?.Invoke();
    }

    /// <summary>
    /// Méthode qui retourne un vecteur 2 pour le déplacement du joueur
    /// </summary>
    /// <returns></returns>

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;  // Normaliser la valeur du vecteur

        return inputVector;
    }
}
