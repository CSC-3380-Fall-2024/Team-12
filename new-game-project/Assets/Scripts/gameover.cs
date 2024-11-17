using Godot;
using System;

public partial class gameover : CanvasLayer
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
	
	void _on_respawn_pressed(){
		GD.Print("Respawning");
		changeScene("res://Assets/Levels/game.tscn");
	}
	
	void _on_quit_pressed(){
		GD.Print("Quitting");
		changeScene("Main Menu");
	}
	
	public void changeScene(string pathToScene){
		PackedScene newScene = (PackedScene)ResourceLoader.Load(pathToScene);
		if(newScene == null){
			GD.Print("Error loading scene: " + pathToScene);
			return;
		}
		Node newSceneInstance = newScene.Instantiate();
		Node root = GetTree().Root;
		Node currentScene = root.GetChild(0);
		currentScene.QueueFree();
		root.RemoveChild(currentScene);
		root.AddChild(newSceneInstance);
		GetTree().CurrentScene = newSceneInstance;
	}
}
