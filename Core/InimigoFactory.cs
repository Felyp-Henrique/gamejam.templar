using Godot;

public class InimigoFactory : Node
{
    [Export]
    public NodePath AlvoPath;

    [Export]
    public PackedScene[] Inimigos = {};

    public Inimigo GetInimigo()
    {
        Inimigo inimigo = null;
        if (Inimigos != null && Inimigos.Length > 0)
        {
            int index = (int) GD.RandRange(0, Inimigos.Length - 1);
            inimigo = Inimigos[index].Instance<Inimigo>();
            inimigo.AlvoPath = AlvoPath;
        }
        return inimigo;
    }

    public override void _Ready()
    {
        GD.Randomize();
    }
}