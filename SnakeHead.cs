using Godot;
using System.Collections.Generic;

public partial class SnakeHead : Area2D{
    private Vector2 _direction;
    private Vector2 _newPosition;
    private TextureRect _gameArea;
    private static int _headSize;

    public List<Area2D> BodySegments = new List<Area2D>();

    //private int _appendBody = 0;
    private static int _delay = 15;
    private static int _timer = _delay;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready() {
        _headSize = Base.HeadSize;
        _newPosition = new Vector2(0, 0);
        _direction = new Vector2(0, 0);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) {
        if (!Base.GameRun || Base.GameOver)
            return;

        if (_timer <= 0 && (_direction.X != 0 || _direction.Y != 0)) {
            _moveDirection();
            Position = _newPosition;
            _timer = _delay;
        }
        else {
            _timer--;
        }
    }

    public override void _Input(InputEvent @event) {
        if (@event is InputEventKey eventKey) {
            if (eventKey.IsActionPressed("ui_up"))
                _direction = new Vector2(0, -1);
            else if (eventKey.IsActionPressed("ui_down"))
                _direction = new Vector2(0, 1);
            else if (eventKey.IsActionPressed("ui_left"))
                _direction = new Vector2(-1, 0);
            else if (eventKey.IsActionPressed("ui_right"))
                _direction = new Vector2(1, 0);
        }
    }

    private void Move() {
        if (BodySegments.Count > 0) {
            Vector2 previousPosition = Position; // Pozice hlavy hada
            foreach (Area2D segment in BodySegments) {
                Vector2 tempPosition = segment.Position;
                // !!!! important we must movee head, before spavn new segment
                Position = _newPosition;
                segment.Position = previousPosition;
                previousPosition = tempPosition;
            }
        }
    }

    private void _moveDirection() {
        if (Position.X >= 0 && Position.Y < Base.GetPlayGround().Size.Y - _headSize &&
            Position.Y >= 0 && Position.X < Base.GetPlayGround().Size.X - _headSize) {
            _newPosition = Position + _direction * _headSize;
            Move();
        }
        else {
            GD.Print("Game Over!");
            Base.GameOver = true;
        }
    }

    private void _on_area_entered(Area2D area) {
        if (area is Apple apple) {
            Base.IncScore();

            Area2D newSegment = this.GetNode<Area2D>("../SnakeBody").Duplicate() as Area2D;

            if (BodySegments.Count > 0) {
                newSegment.Position = BodySegments[BodySegments.Count - 1].Position + _direction * -_headSize;
            }
            else {
                newSegment.Position = Position + _direction * -_headSize;
            }

            GetParent().AddChild(newSegment);
            BodySegments.Add(newSegment);

            Base.randonApplePosition();
        }

        if (area is SnakeBody snakeBody) {
            Base.GameOver = true;
        }
    }
}