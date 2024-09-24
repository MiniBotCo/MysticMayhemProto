using Godot;
using System;

public partial class Player : CharacterBody2D
{
	private Camera2D _camera;

	public const float Speed = 175.0f;
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

		// Disables the mask for the second collision layer, allowing the player to drop through certain platforms.
		if (Input.IsActionPressed("down"))
		{
			SetCollisionMaskValue(2, false);
		}
		else
		{
			SetCollisionMaskValue(2, true);
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
