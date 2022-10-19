using Godot;

public class Heroi : KinematicBody2D
{
    [Export]
    public double Vida = 100;

    [Export]
    public int Velocidade = 50;

    [Export]
    public double Forca = 25;

    [Export]
    public NodePath SpritePath = "./Sprite";

    [Signal]
    public delegate void Ataque(double forca);

    private AnimatedSprite _Sprite;

    public void Ferir(double forca)
    {
        Vida -= forca;
    }

    public override void _Ready()
    {
        _Sprite = GetNode<AnimatedSprite>(SpritePath);
    }

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("ui_attack"))
        {
            EmitSignal("Ataque", Forca);
        }
    }

    public override void _PhysicsProcess(float delta)
    {
        Vector2 movimento = Vector2.Zero;

        movimento.x = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
        movimento.y = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");
        movimento = movimento.Normalized();

        MoveAndSlide(movimento * Velocidade);

        if (_Sprite != null) {
            if (movimento != Vector2.Zero)
            {
                _Sprite.FlipH = movimento.x < 0;
                _Sprite.Play("walk");
            }
            else
            {
                _Sprite.FlipV = false;
                _Sprite.Play("idle");
            }
        }
    }
}
