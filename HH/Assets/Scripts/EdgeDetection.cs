using Godot;
using System;

public partial class EdgeDetection : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//GD.Print("TEST!");
		BodyEntered += PlayerDetection;
	}

	private void PlayerDetection(Node body) {
		if (body is Player player) {
			GD.Print("Player Detected!");
		}
	}
}
