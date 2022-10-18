using Godot;

public class InimigoFactory : Node
{
    [Export]
    public PackedScene[] Inimigos = {};

    public T GetInimigo<T>() where T: Node
    {
        if (Inimigos != null && Inimigos.Length > 0)
        {
            int index = (int) GD.RandRange(0, Inimigos.Length - 1);
            return Inimigos[index].Instance<T>();
        }
        else
        {
            return null;
        }
    }

    public override void _Ready()
    {
        GD.Randomize();
    }
}