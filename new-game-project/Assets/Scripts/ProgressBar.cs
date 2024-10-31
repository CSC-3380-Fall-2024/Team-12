
using Godot;
using System;

public partial class ProgressBar : Godot.ProgressBar
{
    int hp = 5;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.Value = hp; // Optional, set the initial health value on ready
    }

    public void hpLoseHealth()
    {
        hp--;
        this.Value = hp;
    }

    public void hpGainHealth()
    {
        hp++;
        this.Value = hp;
    }
}
