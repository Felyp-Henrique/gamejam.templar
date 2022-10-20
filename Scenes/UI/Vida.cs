using Godot;
using System;

public class Vida : CenterContainer
{
    [Export]
    public NodePath AlvoPath;

    [Export]
    public Texture Icone;

    [Export]
    public int IconeQuantidade = 10;

    private IStatus _VidaStatus;
    private HBoxContainer _Bar;
    private double _VidaTotal = 0;
    private float _UpdateTime = 0;

    public override void _Ready()
    {
        if ((_VidaStatus = GetNode<IStatus>(AlvoPath)) != null)
        {
            _VidaTotal = _VidaStatus.GetVida();
        }

        _Bar = GetNode<HBoxContainer>("./Bar");

        for (int i = 0; i < IconeQuantidade; ++i)
        {
            TextureRect texture = new TextureRect();
            texture.Texture = Icone;
            _Bar.AddChild(texture);
        }
    }

    public override void _Process(float delta)
    {

        int fracao = (int)(_VidaTotal / IconeQuantidade);
        int quantidadeAtual = (int)(_VidaStatus.GetVida() / fracao);
        int diferenca = IconeQuantidade - quantidadeAtual;
        for (int i = 0; i < diferenca; ++i)
        {
            if (_Bar.GetChildren().Count > 0)
            {
                _Bar.GetChild(i)?.QueueFree();
            }
        }
        if (diferenca > 0)
        {
            IconeQuantidade = diferenca;
            _VidaTotal = _VidaStatus.GetVida();
        }
    }
}
