using Godot;
using System;

public partial class FallObjectSpawner : Area2D
{
	//[Export] private PackedScene objScene;
	private Rect2 spawnerArea;
	
	private RandomNumberGenerator rng = new RandomNumberGenerator();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		rng.Randomize();
		
		//Get rect from this area
		foreach(Node child in GetChildren()) {
			if (child is CollisionShape2D collisionShape) {
				spawnerArea = collisionShape.Shape.GetRect();
			}
		}
		
		//OnObjectSpawn();
	}
	
	//Spawn a single Instance at a random position within 2d area
	public void OnObjectSpawn(PackedScene curObj) {
		if (curObj != null) {
			Node2D newObj = (Node2D)curObj.Instantiate();
			
			//gen random position from this 2d area
			Vector2 randPosition = new Vector2(
				rng.RandfRange(spawnerArea.Position.X, spawnerArea.Position.X + spawnerArea.Size.X),
				-400);
			
			newObj.Position = randPosition;
			AddChild(newObj);
			//GD.Print("FOS.cs: Objects Spawned!"+newObj.Position.X);
		} else {
			GD.Print("FallObjectSpawner.cs: Not a valid packedscene!");
		}
	}

	
}
