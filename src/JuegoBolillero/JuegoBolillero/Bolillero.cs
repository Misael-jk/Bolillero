namespace JuegoBolillero;

public class Bolillero
{
    private List<int> _bolillasAdentro = new();
    public IReadOnlyCollection<int> BolillasAdentro => _bolillasAdentro.AsReadOnly();

    private List<int> _bolillasFuera = new();
    public IReadOnlyCollection<int> BolillasFuera => _bolillasFuera.AsReadOnly();

    private IBolillero _generarAleatorio;

    public Bolillero(int cantidadBolillas, IBolillero generarAleatorio)
    {
        _bolillasAdentro = Enumerable.Range(0, cantidadBolillas).ToList();
        _generarAleatorio = generarAleatorio;
    }

    public int SacarBolilla()
    {
        var numero = _generarAleatorio.GenerarAleatorio(0, _bolillasAdentro.Count);
        var bolilla = _bolillasAdentro[numero];
        _bolillasAdentro.RemoveAt(bolilla);
        _bolillasFuera.Add(bolilla);

        return bolilla;
    }

    public void Revocar()
    {
        _bolillasAdentro.AddRange(_bolillasFuera);
        _bolillasFuera.Clear();
    }

    public bool Jugar(List<int> jugada)
    {
        if (_bolillasAdentro.Count == 0)
            return true;

        foreach(var bolillas in jugada)
        {
            int sacada = SacarBolilla();

            if(sacada != bolillas)
            {
                Revocar();
                return false;
            }
        }

        Revocar();
        return true;
    }

    public int JugarNVeces(List<int> jugada, int cantidadVeces)
    {
        int acierto = 0;

        for(int i = 0; i < cantidadVeces; i++)
        {
            if(Jugar(jugada))
            {
                return acierto++;
            }
        }

        return acierto;
    }
}
