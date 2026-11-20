namespace IrpfBizkaia.Domain;

/// <summary>
/// Escala general del IRPF de Bizkaia para el ejercicio 2026
/// (Norma Foral 13/2013, art. 75, redacción dada por la NF 7/2025).
/// </summary>
public static class TarifaGeneral2026
{
    private readonly record struct Tramo(decimal Desde, decimal Tipo);

    private static readonly Tramo[] Tramos =
    [
        new(0m, 0.23m),
        new(18_080m, 0.28m),
        new(36_160m, 0.35m),
        new(54_240m, 0.40m),
        new(77_450m, 0.45m),
        new(107_260m, 0.46m),
        new(142_960m, 0.47m),
        new(208_390m, 0.49m),
    ];

    public static decimal CalcularCuotaIntegra(decimal baseLiquidable)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(baseLiquidable);

        decimal cuota = 0m;

        for (int i = 0; i < Tramos.Length; i++)
        {
            decimal desde = Tramos[i].Desde;
            if (baseLiquidable <= desde)
            {
                break;
            }

            decimal hasta = i + 1 < Tramos.Length ? Tramos[i + 1].Desde : decimal.MaxValue;
            decimal porcionEnTramo = Math.Min(baseLiquidable, hasta) - desde;
            cuota += porcionEnTramo * Tramos[i].Tipo;
        }

        return Math.Round(cuota, 2, MidpointRounding.AwayFromZero);
    }
}
