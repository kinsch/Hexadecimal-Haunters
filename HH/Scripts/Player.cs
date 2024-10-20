using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export] public float MoveSpeed = 250f; 
	[Export] public int Health = 3; //3 lives before gameover
	[Export] public int Score = 0;
	[Export] private Label ScoreLabel;
	[Export] private Label LifeLabel;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		LifeLabel.Text = Health.ToString();
		foreach(Node child in GetChildren()) {
			if (child is Area2D area) {
				area.BodyEntered += CollisionCheck;
			}
		}
		
		
	}
	
	public override void _Process(double delta) {
		if (Score >= 1000) {
			GetTree().ChangeSceneToFile("res://Scenes/win.tscn");
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta) {
		Vector2 vel = Velocity;
		//horizontal movment
		vel.X = 0;
		if (Input.IsKeyPressed(Key.Left)) {
			vel.X = -MoveSpeed;
		} else if (Input.IsKeyPressed(Key.Right)) {
			vel.X = MoveSpeed;
		}
		
		Velocity = vel;
		Position = new Vector2(Position.X, 230f);
		MoveAndSlide();
		
	}
	
	public void EdgeWrap() {
		Vector2 curPosition = Position;
		curPosition.X = -curPosition.X;
		//Check if wrapping around (Left->Right)
		if (curPosition.X > 0) {
			Position = curPosition + new Vector2(-6,0);
		} else { //wrapping around (right->Left)
			Position = curPosition + new Vector2(5, 0);
		}
		
		//GD.Print("Position: "+curPosition.Y);
	}
	
	public void CollisionCheck(Node body) {
		//check if nail, tooth, etc...
		if (body.IsInGroup("Nail")) {
			Health--;
			GD.Print("CurrentHealth= "+Health);
			//update health value UI
			LifeLabel.Text = Health.ToString();
		} else if (body.IsInGroup("Collect")) {
			Score += 100;
			ScoreLabel.Text = Score.ToString();
			body.QueueFree();
		}
	}
}
