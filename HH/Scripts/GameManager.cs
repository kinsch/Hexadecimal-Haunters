using Godot;
using System;

public partial class GameManager : Node
{
	[Export] private Player PlayerNode;
	[Export] private FallObjectSpawner FOS; 
	[Export] private Label TimeLabel;
	[Export] private Label ScoreLabel;
	
	[Export] private PackedScene NailObjScene;
	[Export] private PackedScene BroomObjScene;
	[Export] private PackedScene ToothObjScene;
	
	[Export] private float SpawnInterval = 1f;
	private float timerStartValue = 10f;
	
	private Timer spawnTimer;
	private Timer gameTimer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		FOS.OnObjectSpawn(ToothObjScene);
		
		//Setup and start Timer
		spawnTimer = new Timer();
		spawnTimer.WaitTime = SpawnInterval;
		spawnTimer.Timeout += CallObjectSpawn;
		AddChild(spawnTimer);
		spawnTimer.Start();
		
		gameTimer = new Timer();
		gameTimer.WaitTime = timerStartValue;
		gameTimer.Timeout += TimeUp;
		AddChild(gameTimer);
		gameTimer.Start();
		
		
		//FOS.OnObjectSpawn();
	}
	
	public override void _Process(double delta) {
		TimeLabel.Text = gameTimer.TimeLeft.ToString("00:00");
		if (PlayerNode.Health < 1) {
			//GAME OVER
			GetTree().ChangeSceneToFile("res://Scenes/death.tscn");
		}
	}

	private void CallObjectSpawn() {
		FOS.OnObjectSpawn(ToothObjScene);
		spawnTimer.WaitTime = SpawnInterval;
		spawnTimer.Start();
	}
	
	private void TimeUp() {
		GetTree().ChangeSceneToFile("res://Scenes/death.tscn");
	}
}
