using Godot;

public class Zumbi : Inimigo
{
    public override void _Ready()
    {
        base._Ready();
        if (_Alvo != null)
        {
            _Alvo.Connect("Ataque", this, "OnHeroiAtaque");
        }
    }

    private void OnHeroiAtaque(double forca)
    {
        this.Ferir(forca);
    }
}
