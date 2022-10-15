using Godot;

public class Inimigo : KinematicBody2D
{
    [Export]
    public int Velocidade = 55;

    [Export]
    public double Vida = 100d;

    [Export]
    public string SpritePath = "./Sprite";

    [Export]
    public string AlcancePath = "./Alcance";

    private AnimatedSprite _Sprite;
    private Alcance _Alcance;
    private State _State;

    public override void _Ready()
    {
        _State = State.GetInstance();
        _Alcance = GetNode<Alcance>(AlcancePath);
        _Sprite = GetNode<AnimatedSprite>(SpritePath);
    }

    public override void _PhysicsProcess(float delta)
    {
        if (_State.GetHeroi() != null && _Alcance.OverlapsBody(_State.GetHeroi()))
        {
            Vector2 direcao = GlobalPosition.DirectionTo(_State.GetHeroi().GlobalPosition);

            MoveAndSlide(direcao * Velocidade);

            _Sprite.FlipH = direcao.x < 0;
            _Sprite.Play("walk");
        }
        else
        {
            _Sprite.Play("idle");
        }
    }
}