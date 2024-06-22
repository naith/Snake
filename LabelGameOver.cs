using Godot;
using System;

public partial class LabelGameOver : Label
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Visible = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Base.GameOver)
		{
			Visible = true;
		}
	}
}
