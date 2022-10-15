using Godot;
using System;

public class Heroi : KinematicBody2D
{
    private static readonly string NODE_SPRITE = "./Sprite";

    private AnimatedSprite _Sprite;
    private float _Velocidade = 50;

    public override void _Ready()
    {
        _Sprite = GetNode<AnimatedSprite>(NODE_SPRITE);
    }

    public override void _PhysicsProcess(float delta)
    {
        Vector2 movimento = Vector2.Zero;

        movimento.x = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
        movimento.y = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");
        movimento = movimento.Normalized();

        MoveAndSlide(movimento * _Velocidade);

        if (movimento.x != 0 || movimento.y != 0)
        {
            _Sprite.FlipH = movimento.x < 0;

            if (Input.IsActionPressed("ui_attack"))
            {
                _Sprite.Play("attack_walk");
            }
            else
            {
                _Sprite.Play("walk");
            }
        }
        else
        {
            _Sprite.FlipV = false;

            if (Input.IsActionPressed("ui_attack"))
            {
                _Sprite.Play("attack_idle");
            }
            else
            {
                _Sprite.Play("idle");
            }
        }
    }
}
