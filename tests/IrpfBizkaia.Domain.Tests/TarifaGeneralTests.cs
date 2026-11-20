namespace IrpfBizkaia.Domain.Tests;

public class TarifaGeneralTests
{
    public static TheoryData<decimal, decimal> Casos => new()
    {
        { 0m, 0m },
        { 18_080m, 4_158.40m },
        { 25_000m, 6_096.00m },
        { 36_160m, 9_220.80m },
        { 54_240m, 15_548.80m },
        { 60_000m, 17_852.80m },
    };

    [Theory]
    [MemberData(nameof(Casos))]
    public void CalcularCuotaIntegra_devuelve_la_cuota_esperada(decimal baseLiquidable, decimal esperada)
    {
        var cuota = TarifaGeneral2026.CalcularCuotaIntegra(baseLiquidable);

        Assert.Equal(esperada, cuota);
    }

    [Fact]
    public void CalcularCuotaIntegra_rechaza_una_base_negativa()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => TarifaGeneral2026.CalcularCuotaIntegra(-1m));
    }
}
