using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
	public float gravity = 100;
	public float maxSpeed;
	public float jumpSpeed;

	[Header("Ground Momenet")]
	public float groundAccelaration;
	public float groundDecelaration;
	[Header("Air Momenet")]
	public float airAccelaration;
	public float airDecelaration;
	public float coyoteTime;
	public float jumpBufferTime;
}
