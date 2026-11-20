namespace IrpfBizkaia.Domain;

/// <summary>
/// Bonificación del rendimiento del trabajo (Norma Foral 13/2013, art. 23, Bizkaia).
/// Se calcula sobre la diferencia entre el rendimiento íntegro del trabajo
/// y los gastos deducibles. Fuera de alcance en esta versión: el caso de
/// más de 7.500 EUR de rentas que no son del trabajo (bonificación fija de 3.000).
/// </summary>
public static class BonificacionTrabajo2026
{
    private const decimal LimiteTramoPlano = 14_800m;
    private const decimal LimiteTramoDecreciente = 23_000m;
    private const decimal BonificacionMaxima = 8_000m;
    private const decimal BonificacionMinima = 3_000m;
    private const decimal FactorDeReduccion = 0.6098m;

    public static decimal Calcular(decimal diferencia)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(diferencia);

        decimal bonificacion;

        if (diferencia <= LimiteTramoPlano)
        {
            bonificacion = BonificacionMaxima;
        }
        else if (diferencia <= LimiteTramoDecreciente)
        {
            bonificacion = BonificacionMaxima - FactorDeReduccion * (diferencia - LimiteTramoPlano);
        }
        else
        {
            bonificacion = BonificacionMinima;
        }

        // La bonificación no puede dejar el rendimiento neto en negativo.
        decimal limitada = Math.Min(bonificacion, diferencia);

        return Math.Round(limitada, 2, MidpointRounding.AwayFromZero);
    }
}
