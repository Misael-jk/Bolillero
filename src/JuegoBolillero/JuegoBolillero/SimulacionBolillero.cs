using JuegoBolillero;

namespace JuegoBolillero;

public class SimulacionBolillero
{
    public static long SimularSinHilos(Bolillero bolillero, List<int> jugada, int cantidad)
    {
        var clon = bolillero.Clonar() as Bolillero;

        return clon.JugarNVeces(jugada, cantidad);
    }

    public long SimularConHilos(Bolillero bolillero, List<int> jugada, int cantSim, int cantHilos)
    {
        long ganadas = 0;

        int restante = cantSim % cantHilos;
        int simularPorHilo = cantSim / cantHilos;

        Task<long>[] tareas = new Task<long>[cantHilos];

        for (int i = 0; i < cantHilos; i++)
        {
            int simsPorHilo = simularPorHilo + (i < restante ? 1 : 0);

            tareas[i] = Task.Run(() =>
            {
                var clon = bolillero.Clonar() as Bolillero;
                return SimularSinHilos(clon, jugada, simsPorHilo);
            });
        }

        Task.WaitAll(tareas);

        foreach (Task<long> tarea in tareas)
            ganadas += tarea.Result;

        return ganadas;
    }
}
