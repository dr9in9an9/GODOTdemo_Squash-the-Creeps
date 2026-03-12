using Godot;
using System;

public partial class Main : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	[Export]
	public PackedScene MobScene { get; set; }
	
	private void OnMobTimerTimeout()
	{
		Mob mobster = MobScene.Instantiate<Mob>();
		
		var mobSpawnLocation = GetNode<PathFollow3D>("spawnPath/spawnLocation");
		mobSpawnLocation.ProgressRatio = GD.Randf();
		
		Vector3 playerPosition = GetNode<Player>("player").Position;
		mobster.Initialize(mobSpawnLocation.Position, playerPosition);
		
		AddChild(mobster);
		
		mobster.Squashed += GetNode<ScoreLabel>("UserInterface/ScoreLabel").OnMobSquashed;
	}
	
	private void OnPlayerHit()
	{
		GetNode<Timer>("mobTimer").Stop();
	}
}
