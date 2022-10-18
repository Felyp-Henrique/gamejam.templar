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
    public NodePath AtaquePath = "./Ataque";

    [Export]
    public float AtaqueDelay = 1f;

    [Export]
    public NodePath AlvoPath;

    private KinematicBody2D _Alvo;
    private AnimatedSprite _Sprite;
    private Area2D _Alcance;
    private Area2D _Ataque;
    private float _AtaqueTime;

    public override void _Ready()
    {
        _Alcance = GetNode<Area2D>(AlcancePath);
        _Ataque = GetNode<Area2D>(AtaquePath);
        _Sprite = GetNode<AnimatedSprite>(SpritePath);
        if (AlvoPath != null)
        {
            _Alvo = GetNode<KinematicBody2D>(AlvoPath);
        }
    }

    public override void _PhysicsProcess(float delta)
    {
        /**
         * Movimentação e Perseguição
         */

        if (_Alvo != null && _Alcance.OverlapsBody(_Alvo) && !_Ataque.OverlapsBody(_Alvo))
        {
            Vector2 direcao = Position.DirectionTo(_Alvo.Position);

            MoveAndSlide(direcao * Velocidade);

            if (_Sprite != null)
            {
                _Sprite.FlipH = direcao.x < 0;
                _Sprite.Play("walk");
            }

            _AtaqueTime = 0;
        }
        else
        {
            if (_Sprite != null)
            {
                _Sprite.Play("idle");
            }
        }

        /**
         * Ataque
         */

        if (_Alvo != null && _Ataque.OverlapsBody(_Alvo))
        {
            if (_AtaqueTime > AtaqueDelay)
            {
                /* programar dano no alvo */

                GD.Print("Recebeu dano o alvo!");

                _AtaqueTime = 0;
            }

            _AtaqueTime += delta;
        }
    }
}