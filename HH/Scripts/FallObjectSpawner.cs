using Godot;
using System;

public partial class FallObjectSpawner : Area2D
{
	private Area2D spawnerArea;
	[Export] PackedScene objScene;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		spawnerArea = this;
	}
	
	void OnObjectSpawn() {
		if (objScene != null) {
			Node2D newObj = (Node2D)objScene.Instance();
			
			//gen random position from this 2d area
			Vector2 randPosition = new Vector2(
				rng.RandfRange(spawnerArea.Position.X, spawnerArea.Position.X + spawnerArea.Size.X),
				rng.RandfRange(spawnerArea.Position.Y, spawnerArea.Position.Y + spawnerArea.Size.Y)
			);
			
			newObj.Position = randPosition;
		} else {
			GD.Print("FallObjectSpawner.cs: Not a valid object 'scene'!")
		}
	}

	
}
