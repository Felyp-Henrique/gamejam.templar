using Godot;
using System;

public class Inimigo : KinematicBody2D
{
    [Export]
    public int Velocidade = 55;

    [Export]
    public double Vida = 100d;

    [Export]
    public string SpritePath = "./Sprite";

    [Export]
    public string RangePath = "./Range";

    private AnimatedSprite _Sprite;
    private Area2D _Range;
    private State _State;


    public override void _Ready()
    {
        _State = State.GetInstance();
        _Sprite = GetNode<AnimatedSprite>(SpritePath);
        _Range = GetNode<Area2D>(RangePath);
    }

    public override void _PhysicsProcess(float delta)
    {
        if (_State.GetHeroi() != null)
        {
            Vector2 direcao = GlobalPosition.DirectionTo(_State.GetHeroi().GlobalPosition);

            MoveAndSlide(direcao * Velocidade);

            if (direcao != Vector2.Zero)
            {
                _Sprite.FlipH = direcao.x < 0;
                _Sprite.Play("walk");
            }
            else
            {
                _Sprite.Play("idle");
            }
        }
    }
}