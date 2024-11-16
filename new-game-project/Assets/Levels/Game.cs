using Godot;
using System;

public partial class Game : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("calling all pussies");
				this.ChildEnteredTree += _on_child_entered_tree;
	}
	private void _on_child_entered_tree(Node child){
		GD.Print($"Node entered: {child.Name}");
		if(child.Name == "Sprite2D"){
			GD.Print("Remove This");
			 Node kid = GetNode<Node>("InteractionArea");	
        RemoveChild(kid);
        // Free the child node
        kid.QueueFree();
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
