using Godot;
using System;

public partial class ButtonNew : Button{
	private void _on_pressed() {
		Base.RestartGame();
	}
}
