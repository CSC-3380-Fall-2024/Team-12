using Godot;
using System;

public partial class Button : Godot.Button
{

    [Export(PropertyHint.ResourceType, "NodePath")]
    public NodePath progressBarPath;

    private ProgressBar progressBar;

    public override void _Ready()
    {
        if (progressBarPath != null)
        {
            progressBar = GetNode<ProgressBar>(progressBarPath);
        }
        this.Pressed += OnButtonPressed;

    }

    private void OnButtonPressed()
    {
        if (progressBar != null)
        {
            progressBar.hpGainHealth();
        }
    }
}
