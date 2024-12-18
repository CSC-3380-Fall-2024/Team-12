using Godot;
using System;

public partial class Game : Node2D
{
	 private ArmorGlobal cape;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		cape = (ArmorGlobal)GetNode("/root/ArmorGlobal");
        cape.Armor = 0;
		GD.Print("calling all");
				this.ChildEnteredTree += _on_child_entered_tree;
	}
	private void _on_child_entered_tree(Node child){
		GD.Print($"Node entered: {child.Name}");
		if(child.Name == "Sprite2D"){
			GD.Print("Remove This");
			 Node kid = GetNode<Node>("InteractionArea");
			 cape.Armor = 1;	
        RemoveChild(kid);

        kid.QueueFree();
		}
		if(child.Name == "cookieRemove"){
			GD.Print("Remove This");
			 Node kid = GetNode<Node>("InteractionAreaCookie");	
        RemoveChild(kid);
       
        kid.QueueFree();
		}
		if(child.Name == "potionramove"){
			GD.Print("Remove This");
			 Node kid = GetNode<Node>("InteractionPotion");	
        RemoveChild(kid);
        
        kid.QueueFree();
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	public void LoadBackToTopDown() {
		GetNode<LoadingScreen>("/root/LevelOne/CanvasLayer/Loading").LoadScene("res://Assets/Overworld/world_pt_1.tscn");
	}
}
