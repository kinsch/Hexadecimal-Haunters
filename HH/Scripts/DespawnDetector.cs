using Godot;
using System;

public partial class DespawnDetector : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		BodyEntered += DespawnCheck;
	}
	
	private void DespawnCheck(Node body) {
		if (body.IsInGroup("FallObject")) {
			body.QueueFree();
		}
	}
	
}
