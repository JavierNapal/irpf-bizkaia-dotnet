namespace IrpfBizkaia.Domain.Tests;

public class BonificacionTrabajoTests
{
    // Fuente: Norma Foral 13/2013, art. 23 (Bizkaia), redacción de la NF 2/2025.
    // Parámetro: diferencia = rendimiento íntegro del trabajo - gastos deducibles.
    //   <= 14.800                  -> 8.000
    //   14.800,01 a 23.000         -> 8.000 - 0,6098 x (diferencia - 14.800)
    //   > 23.000                   -> 3.000
    // La bonificación nunca puede superar la diferencia (rendimiento neto no negativo).
    public static TheoryData<decimal, decimal> Casos => new()
    {
        { 0m, 0m },
        { 5_000m, 5_000m },
        { 10_000m, 8_000m },
        { 14_800m, 8_000m },
        { 16_830m, 6_762.11m },
        { 23_000m, 2_999.64m },
        { 23_000.01m, 3_000m },
        { 30_000m, 3_000m },
    };

    [Theory]
    [MemberData(nameof(Casos))]
    public void Calcular_devuelve_la_bonificacion_esperada(decimal diferencia, decimal esperada)
    {
        var bonificacion = BonificacionTrabajo2026.Calcular(diferencia);

        Assert.Equal(esperada, bonificacion);
    }

    [Fact]
    public void Calcular_rechaza_una_diferencia_negativa()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => BonificacionTrabajo2026.Calcular(-1m));
    }
}
