using Godot;
using System;

public partial class BossLevel : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.ChildEnteredTree += _on_child_entered_tree;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
	private void _on_child_entered_tree(Node child){
    if (child.Name == "placeholderforboss")
    {
		GetTree().ChangeSceneToFile("res://Assets/Levels/BossLevel/ending.tscn");
    }
	}

	
}
