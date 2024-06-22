using Godot;
using System;

public partial class Base : Node2D{
	public static bool GameRun = false;
	public static bool GameOver = false;
	public static int HeadSize = 32;

	private static ColorRect _playGround;
	private static Label _appleCount;
	private static Label _labelGameOver;
	private static Area2D _apple;
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		_playGround = GetNode<ColorRect>("GameArea/PlayGround");
		_labelGameOver = GetNode<Label>("GameArea/LabelGameOver");
		_appleCount = GetNode<Label>("ScoreArea/Score");
		_apple = GetNode<Area2D>("GameArea/PlayGround/Apple");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.

	public void RestartGame() {
		GameRun = true;
		GameOver = false;
		_labelGameOver.Visible = false;
		_appleCount.Text = "0";
	}
	public static ColorRect GetPlayGround() {
		return _playGround;
	}

	public static void IncScore() {
		int score = _appleCount.Text.ToInt() + 1;

		_appleCount.Text = $"{score}";
	}

	public static void randonApplePosition() {
		Random rand = new Random();
		Vector2 pos = new Vector2(rand.Next(0, 30) * HeadSize, rand.Next(0, 24) * HeadSize);
		_apple.Position = pos;
	}
}
