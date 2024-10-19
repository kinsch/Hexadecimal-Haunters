using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public float moveSpeed = 150f; 
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta) {
		Vector2 vel = Velocity;
		//horizontal movment
		vel.X = 0;
		if (Input.IsKeyPressed(Key.Left)) {
			vel.X = -moveSpeed;
		} else if (Input.IsKeyPressed(Key.Right)) {
			vel.X = moveSpeed;
		}
		
		Velocity = vel;
		MoveAndSlide();
		for (int i = 0; i < GetSlideCollisionCount(); i++){
				var collision = GetSlideCollision(i);
				GD.Print("I collided with ", ((Node)collision.GetCollider()).Name);
		}
	}
}
