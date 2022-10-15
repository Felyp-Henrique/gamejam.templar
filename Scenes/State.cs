using Godot;
using System;

public class State
{
    public static readonly State _Instance = new State();

    private Heroi _Heroi;

    public void SetHeroi(Heroi heroi)
    {
        _Heroi = heroi;
    }

    public Heroi GetHeroi()
    {
        return _Heroi;
    }

    public static State GetInstance()
    {
        return _Instance;
    }
}
