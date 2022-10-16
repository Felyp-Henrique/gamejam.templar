using Godot;

public class Spawner : Position2D
{
    [Export]
    public NodePath MapaPath;

    [Export]
    public PackedScene InimigoScene;

    [Export]
    public int Quantidade = 3;

    [Export]
    public int Limite = 1;

    private Node _Mapa;

    private bool _CanSpawn;

    public override void _Ready()
    {
        GD.Randomize();

        _Mapa = GetNode(MapaPath);
        _CanSpawn = true;
    }

    public override void _Process(float delta)
    {
        if (_CanSpawn && Limite > 0)
        {
            for (int i = 0; i < Quantidade; i++)
            {
                int distancia = (int) GD.RandRange(40, 60);

                Node2D inimigo = InimigoScene.Instance<Node2D>();

                inimigo.Position = new Vector2(
                    (int) GD.RandRange(Position.x - distancia, Position.x + distancia),
                    (int) GD.RandRange(Position.y - distancia, Position.y + distancia)
                );

                _Mapa.AddChild(inimigo);
            }

            _CanSpawn = false;

            Limite -= 1;
        }
    }
}
