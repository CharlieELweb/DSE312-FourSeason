using Mono.Cecil.Cil;
using UnityEngine;

public class Object : Node2D
{

	// Update is called once per frame
	protected override void FixedUpdate()
	{
		if (IsOnFloor())
		{
			rigidbody2D.linearDamping = 10;
		}
		else
		{
			rigidbody2D.linearDamping = 1;
		}
	}
}
