using System;

namespace JuegoBolillero;

public class Bolillero : IClonable
{
    private List<int> _bolillasAdentro = new();
    public IReadOnlyCollection<int> BolillasAdentro => _bolillasAdentro.AsReadOnly();

    private List<int> _bolillasFuera = new();
    public IReadOnlyCollection<int> BolillasFuera => _bolillasFuera.AsReadOnly();

    private IBolillero _generarAleatorio;

    public Bolillero(int n, IBolillero generarAleatorio)
    {
        if (n < 0)
            throw new ArgumentException("N debe ser mayor o igual a 0.");

        _bolillasAdentro = Enumerable.Range(0, n).ToList();
        _generarAleatorio = generarAleatorio;
    }

    public int SacarBolilla()
    {
        var numero = _generarAleatorio.GenerarAleatorio(0, _bolillasAdentro.Count);
        var bolilla = _bolillasAdentro[numero];
        _bolillasAdentro.RemoveAt(numero);
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

    public long JugarNVeces(List<int> jugada, int cantidadVeces)
    {
        long acierto = 0;

        for(int i = 0; i < cantidadVeces; i++)
        {
            if(Jugar(jugada))
            {
               acierto++;
            }
        }

        return acierto;
    }

    public object Clonar()
    {
        Bolillero clon = new Bolillero(_bolillasAdentro.Count + _bolillasFuera.Count, _generarAleatorio);

        clon._bolillasFuera = new List<int>(_bolillasFuera);
        clon._bolillasAdentro = new List<int>(_bolillasAdentro);

        return clon;
    }
}
