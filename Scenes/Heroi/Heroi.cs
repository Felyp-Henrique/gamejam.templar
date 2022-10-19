using Godot;

public class Heroi : KinematicBody2D
{
    [Export]
    public double Vida = 100;

    [Export]
    public int Velocidade = 50;

    [Export]
    public double Forca = 5;

    [Export]
    public NodePath SpritePath =  "./Sprite";

    [Signal]
    public delegate void Ataque(double forca);

    private AnimatedSprite _Sprite;

    private float _AtaqueTime = 0;
    private bool _AtaquePausa = false;

    private float ExecDelay(float Delta, float TimerDelay)
    {
        return TimerDelay += 1 * Delta;
    }

    public void Ferir(double forca)
    {
        Vida -= forca;
    }

    public override void _Ready()
    {
        _Sprite = GetNode<AnimatedSprite>(SpritePath);
    }

    public override void _PhysicsProcess(float delta)
    {
        Vector2 movimento = Vector2.Zero;

        movimento.x = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
        movimento.y = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");
        movimento = movimento.Normalized();

        MoveAndSlide(movimento * Velocidade);

        if (movimento != Vector2.Zero)
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

            if (Input.IsActionPressed("ui_attack") && !_AtaquePausa)
            {
                _Sprite.Play("attack_idle");
                EmitSignal("Ataque", Forca);
                _AtaqueTime = 0;
                _AtaquePausa = true;
            }
            else
            {
                _Sprite.Play("idle");
            }

            if (_AtaquePausa)
            {
                _AtaqueTime += delta;
            }

            if (_AtaqueTime > 0.8)
            {
                _AtaqueTime = 0;
                _AtaquePausa = false;
            }
        }
    }
}
