using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "InputReaderSO", menuName = "ScriptableObjects/InputReader")]

public class InputReader : ScriptableObject, GameInput.IGameplayActions, GameInput.IMenusActions
{
    public event UnityAction<Vector2> MoveEvent = delegate { };
    public event UnityAction PauseEvent = delegate { };
    public event UnityAction UnpauseEvent = delegate { };

    // Only referenced for testing purposes
    internal bool isMenuInputEnabled;
    internal bool isGameplayInputEnabled;

    private GameInput _gameInput;

    private void OnEnable()
	{
		if (_gameInput == null)
		{
			_gameInput = new GameInput();

			_gameInput.Gameplay.SetCallbacks(this);
            _gameInput.Menus.SetCallbacks(this);
		}
    }

    public void EnableGameplayInput()
    {
        _gameInput.Gameplay.Enable();
        _gameInput.Menus.Disable();
        isGameplayInputEnabled = true;
    }

    public void EnableMenuInput()
    {
        _gameInput.Gameplay.Disable();
        _gameInput.Menus.Enable();
        isMenuInputEnabled = true;
    }

    // --- Event Listeners ---
    public void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent.Invoke(context.ReadValue<Vector2>());
    }

    public void OnPause(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Performed)
			PauseEvent.Invoke();
	}

    public void OnUnpause(InputAction.CallbackContext context)
	{
		if (context.phase == InputActionPhase.Performed)
			UnpauseEvent.Invoke();
	}
}
