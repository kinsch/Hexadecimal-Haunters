using Godot;
using System;

public partial class GameManager : Node
{
	[Export] private Player PlayerNode;
	[Export] private FallObjectSpawner FOS; 
	[Export] private float SpawnInterval = 1f;
	
	private Timer spawnTimer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		FOS.OnObjectSpawn();
		
		//Setup and start Timer
		spawnTimer = new Timer();
		spawnTimer.WaitTime = SpawnInterval;
		spawnTimer.Timeout += CallObjectSpawn;
		AddChild(spawnTimer);
		spawnTimer.Start();
		
		
		//FOS.OnObjectSpawn();
	}
	
	public override void _Process(double delta) {
		if (PlayerNode.Health < 1) {
			//GAME OVER
			GetTree().ChangeSceneToFile("res://Scenes/death.tscn");
		}
	}

	private void CallObjectSpawn() {
		FOS.OnObjectSpawn();
		spawnTimer.WaitTime = SpawnInterval;
		spawnTimer.Start();
	}
}
