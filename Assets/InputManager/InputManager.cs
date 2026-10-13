using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
	public static PlayerInput playerInput;
	private void Awake()
	{
		playerInput = GetComponent<PlayerInput>();
	}
	public static Vector2 movement
	{
		get
		{
			Vector2 value = playerInput.actions["Move"].ReadValue<Vector2>();
			if (Mathf.Abs(value.x) < 0.1)
			{
				value.x = 0;
			}
			if (Mathf.Abs(value.y) < 0.1)
			{
				value.y = 0;
			}
			return value;
		}
	}

	public static Vector2 season
	{
		get
		{
			Vector2 value = playerInput.actions["Season"].ReadValue<Vector2>();
			if (Mathf.Abs(value.x) < 0.1)
			{
				value.x = 0;
			}
			if (Mathf.Abs(value.y) < 0.1)
			{
				value.y = 0;
			}
			return value;
		}
	}


	public static bool IsJumpPressed;
	public static bool IsJumpJustPressed;
	public static bool IsJumpJustReleased;
	public static bool IsChangePressed;

	private void Update()
	{
		IsChangePressed = playerInput.actions["Change"].IsPressed();
		IsJumpPressed = playerInput.actions["Jump"].IsPressed();
		IsJumpJustPressed = playerInput.actions["Jump"].WasPressedThisFrame();
		IsJumpJustReleased = playerInput.actions["Jump"].WasReleasedThisFrame();
	}
}
