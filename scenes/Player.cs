using Godot;
using System;

public partial class Player : CharacterBody2D
{
	private Camera2D _camera;

	public const float Speed = 215.0f;
	public const float JumpVelocity = -185.0f;

	public const int CoyoteTime = 8;

	private int _coyoteTimer = 0;

    public override void _Ready()
    {
        base._Ready();
		_camera = GetNode<Camera2D>("PlayerCamera");
    }

    public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta * 0.75f;
			_coyoteTimer += 1;
		}
		else
		{
			_coyoteTimer = 0;
		}

		// Handle Jump.
		if (Input.IsActionPressed("jump") && IsOnFloor() || Input.IsActionPressed("jump") && _coyoteTimer < CoyoteTime)
		{
			velocity.Y = JumpVelocity;
		}

		//TODO Make this work when more than than collision layers 1 and 2 are in use
		if (Input.IsActionPressed("down"))
		{
			CollisionMask = 0b00000000_00000000_00000000_00000001;
		}
		else
		{
			CollisionMask = 0b00000000_00000000_00000000_00000011;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("left", "right", "up", "down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
