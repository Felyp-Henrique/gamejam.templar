using Godot;

public class Inimigo : KinematicBody2D
{
    [Export]
    public int Velocidade = 55;

    [Export]
    public double Vida = 100d;

    [Export]
    public NodePath SpritePath = "./Sprite";

    [Export]
    public NodePath AlcancePath = "./Alcance";

    [Export]
    public NodePath AlvoPath;

    private KinematicBody2D _Alvo;
    private AnimatedSprite _Sprite;
    private Area2D _Alcance;

    public override void _Ready()
    {
        _Alcance = GetNode<Area2D>(AlcancePath);
        _Sprite = GetNode<AnimatedSprite>(SpritePath);
        if (AlvoPath != null) {
            _Alvo = GetNode<KinematicBody2D>(AlvoPath);
        }
    }

    public override void _PhysicsProcess(float delta)
    {
        if (_Alvo != null && _Alcance.OverlapsBody(_Alvo))
        {
            Vector2 direcao = Position.DirectionTo(_Alvo.Position);

            MoveAndSlide(direcao * Velocidade);

            if (_Sprite != null)
            {
                _Sprite.FlipH = direcao.x < 0;
                _Sprite.Play("walk");
            }
        }
        else
        {
            if (_Sprite != null)
            {
                _Sprite.Play("idle");
            }
        }
    }
}