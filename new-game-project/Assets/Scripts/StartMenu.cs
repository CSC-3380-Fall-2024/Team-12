using Godot;
using System;

public partial class StartMenu : Control
{
	private Button startGame;

	public override void _Ready() {
		startGame = GetNode<Button>("StartGame");
		startGame.Connect("button_down", new Callable(this, nameof(_on_start_game_button_down)));
		GD.Print("SDFDS");
	}


	public override void _Process(double delta) {

	}

	public void _on_start_game_button_down() {
		GetNode<Button>("StartGame").Hide();
		GetNode<Label>("GameName").Hide();
		var loading = GetNode<LoadingScreen>("Loading");
		GD.Print("SDFDSF ", loading.GetType());
		loading.LoadScene("res://Assets/Nodes/topdown.tscn");
	}
}
