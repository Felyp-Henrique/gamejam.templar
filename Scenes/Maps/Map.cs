using Godot;
using System;

public class Map : Node2D
{
    [Export]
    public string HeroiPath = "./Heroi";

    private Heroi _Heroi;
    private State _State;

    public override void _Ready()
    {
        _State = State.GetInstance();
        _Heroi = GetNode<Heroi>(HeroiPath);

        _State.SetHeroi(_Heroi);
    }
}
