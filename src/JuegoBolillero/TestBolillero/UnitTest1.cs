using JuegoBolillero;

namespace TestBolillero;

public class BolilleroTest
{
    private readonly Bolillero _bolillero;
    public BolilleroTest()
    {
        var generarPrimero = new Primero();
        _bolillero = new Bolillero(10, generarPrimero);
    }

    [Fact]
    public void SacarBolilla()
    {
        var bolilla = _bolillero.SacarBolilla();

        Assert.Equal(0, bolilla);
        Assert.Equal(9, _bolillero.BolillasAdentro.Count);
        Assert.Single(_bolillero.BolillasFuera);
    }

    [Fact]
    public void ReIngresar()
    {
        _bolillero.SacarBolilla();
        _bolillero.Revocar();

        Assert.Equal(10, _bolillero.BolillasAdentro.Count);
        Assert.Empty(_bolillero.BolillasFuera);
    }

    [Fact]
    public void JugarGana()
    {
        var jugada = new List<int> { 0, 1, 2, 3 };

        Assert.True(_bolillero.Jugar(jugada));
    }

    [Fact]
    public void JugarPierde()
    {
        var jugada = new List<int> { 4, 2, 1 };

        Assert.False(_bolillero.Jugar(jugada));
    }

    [Fact]
    public void GanarNVeces()
    {
        var jugada = new List<int> { 0, 1 };

        var resultado = _bolillero.JugarNVeces(jugada, 1);

        Assert.Equal(1, resultado);
    }

    [Fact]
    public void SimularConHilos()
    {
        var clon = _bolillero.Clonar() as Bolillero;
        var bolillasIniciales = clon.BolillasAdentro.Count;

        long ganadas = SimulacionBolillero.SimularConHilos(_bolillero, new List<int> { 0, 1 }, 1000, 4);

        Assert.Equal(bolillasIniciales, _bolillero.BolillasAdentro.Count);
        Assert.Empty(_bolillero.BolillasFuera);
    }
}