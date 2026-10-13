using Mono.Cecil.Cil;
using UnityEngine;

public class Object : Node2D
{

	// Update is called once per frame
	void Update()
	{
		if (IsOnFloor())
		{
			rigidbody2D.linearDamping = 50;
		}
		else
		{
			rigidbody2D.linearDamping = 1;
		}
	}
}
