using JuegoBolillero;

namespace TestProject
{
    public class UnitTest1
    {
        private readonly Bolillero _bolillero;

        public UnitTest1()
        {
            var generadorPrimero = new GeneradorPrimero();
            _bolillero = new Bolillero(10, generadorPrimero);
        }

        [Fact]
        public void SacarBolilla()
        {
            // Tendria que devolver la bola 0
            int bolilla = _bolillero.SacarBolilla();
            Assert.Equal(0, bolilla);

            // Tendria que ver 9 bolitas en total
            Assert.Equal(9, _bolillero.BolillasAdentro.Count);

            // Verifica que afuera del bolillero hay una bolilla
            Assert.Single(_bolillero.BolillasFuera);
        }

        [Fact]
        public void ReIngresar()
        {
            _bolillero.SacarBolilla();
            _bolillero.Revocar();

            // En este punto deberia haber 10 bolillas dentro
            Assert.Equal(10, _bolillero.BolillasAdentro.Count);

            // Aca no tiene que haber 0 bolitas afuera
            Assert.Equal(0, _bolillero.BolillasFuera.Count);
        }

        [Fact]
        public void JugarGana()
        {
            bool resultado = _bolillero.Jugar(new List<int> { 0, 1, 2, 3 });
            Assert.True(resultado);
        }

        [Fact]
        public void JugarPierde()
        {
            bool resultado = _bolillero.Jugar(new List<int> { 4, 2, 1 });
            Assert.False(resultado);
        }

        [Fact]
        public void GanarNVeces()
        {
            int ganadas = _bolillero.JugarNVeces(new List<int> { 0, 1 }, 1);
            Assert.Equal(1, ganadas);
        }
    }
}