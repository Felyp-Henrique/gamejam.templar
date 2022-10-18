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

    private Heroi _Heroi;
    private InimigoFactory _InimigoFactory;
    private PathFollow2D _SpawnerLocations;
    private State _State;
    private RandomNumberGenerator _Random;

    public override void _Ready()
    {
        _Random = new RandomNumberGenerator();
        _State = State.GetInstance();
        _InimigoFactory = GetNode<InimigoFactory>(InimigoFactoryPath);
        _SpawnerLocations = GetNode<PathFollow2D>(SpawnerLocationsPath);
        _Heroi = GetNode<Heroi>(HeroiPath);

        _State.SetHeroi(_Heroi);
    }

    public override void _Process(float delta)
    {
        if (_SpawnerLocations != null && _InimigoFactory != null)
        {
            if (InimigoLength > 0)
            {
                _SpawnerLocations.Offset = _Random.RandiRange(0, 999999999);

                Node2D inimigo = _InimigoFactory.GetInimigo<Node2D>();
                inimigo.Position = _SpawnerLocations.Position;

                AddChild(inimigo);

                InimigoLength -= 1;
            }
        }
    }
}
