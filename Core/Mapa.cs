using Godot;

public class Mapa : Node2D
{
    [Export]
    public NodePath HeroiPath = "./Heroi";

    [Export]
    public NodePath InimigoFactoryPath = "./InimigoFactory";

    [Export]
    public int InimigoLength = 5;

    [Export]
    public NodePath SpawnerLocationsPath = "./Spawner/Locations";

    [Export]
    public PackedScene GameOverScene;

    private Heroi _Heroi;
    private InimigoFactory _InimigoFactory;
    private PathFollow2D _SpawnerLocations;
    private RandomNumberGenerator _Random;

    public override void _Ready()
    {
        _Random = new RandomNumberGenerator();
        _InimigoFactory = GetNode<InimigoFactory>(InimigoFactoryPath);
        _SpawnerLocations = GetNode<PathFollow2D>(SpawnerLocationsPath);
        _Heroi = GetNode<Heroi>(HeroiPath);
    }

    public override void _Process(float delta)
    {
        if (_SpawnerLocations != null && _InimigoFactory != null)
        {
            if (InimigoLength > 0)
            {
                _SpawnerLocations.Offset = _Random.RandiRange(0, 999999999);

                Node2D inimigo = _InimigoFactory.GetInimigo();
                inimigo.Position = _SpawnerLocations.Position;

                AddChild(inimigo);

                InimigoLength -= 1;
            }
        }

        if (_Heroi != null && _Heroi.GetVida() <= 0)
        {
            GetTree().ChangeScene(GameOverScene.ResourcePath);
        }
    }
}
