namespace JuegoBolillero;

public class GenerarAleatorio : IBolillero
{
    int IBolillero.GenerarAleatorio(int mini, int maxi)
    {
        Random r = new Random();
        var num = r.Next(mini, maxi);

        return num;
    }
}

public class GeneradorPrimero : IBolillero
{
    public int GenerarAleatorio(int mini, int maxi)
    {
        return mini;
    }

}
