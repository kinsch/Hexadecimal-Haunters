using Godot;
using System;

public partial class GameManager : Node
{
	[Export] private Player PlayerNode;
	[Export] private FallObjectSpawner FOS; 
	[Export] private Label TimeLabel;
	
	[Export] private PackedScene NailObjScene;
	[Export] private PackedScene BroomObjScene;
	[Export] private PackedScene ToothObjScene;
	private PackedScene curFallObj;
	
	[Export] private float SpawnInterval = 1f;
	private float timerStartValue = 60f;
	
	private Timer spawnTimer;
	private Timer gameTimer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		curFallObj = ToothObjScene;
		FOS.OnObjectSpawn(curFallObj);
		
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
		
		//time checks
		if (gameTimer.TimeLeft < 50f && gameTimer.TimeLeft > 40f) {
			//swap to nails
			curFallObj = NailObjScene;
		}
		else if (gameTimer.TimeLeft > 30f && gameTimer.TimeLeft < 40f) {
			curFallObj = BroomObjScene;
		}
		else if (gameTimer.TimeLeft > 25f && gameTimer.TimeLeft < 30f) {
			curFallObj = NailObjScene;
		}
		else if (gameTimer.TimeLeft > 15f && gameTimer.TimeLeft < 25f) {
			curFallObj = ToothObjScene;
		}
	}

	private void CallObjectSpawn() {
		FOS.OnObjectSpawn(curFallObj);
		spawnTimer.WaitTime = SpawnInterval;
		spawnTimer.Start();
	}
	
	private void TimeUp() {
		GetTree().ChangeSceneToFile("res://Scenes/death.tscn");
	}
}
