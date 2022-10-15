using Godot;

public class Heroi : KinematicBody2D
{
    [Export]
    public int Velocidade = 50;

    [Export]
    public string SpritePath =  "./Sprite";

    private AnimatedSprite _Sprite;

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
