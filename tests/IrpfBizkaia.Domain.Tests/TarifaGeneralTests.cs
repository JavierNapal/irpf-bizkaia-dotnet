namespace IrpfBizkaia.Domain.Tests;

public class TarifaGeneralTests
{
    // Fuente: Norma Foral 13/2013, art. 75, redacción de la NF 7/2025
    // (efectos 1/1/2026). Los umbrales usan la "cuota íntegra" acumulada
    // que publica la propia norma; el resto se calcula a mano con el tipo marginal.
    public static TheoryData<decimal, decimal> Casos => new()
    {
        { 0m, 0m },
        { 18_080m, 4_158.40m },
        { 25_000m, 6_096.00m },
        { 36_160m, 9_220.80m },
        { 54_240m, 15_548.80m },
        { 60_000m, 17_852.80m },
        { 77_450m, 24_832.80m },
        { 100_000m, 34_980.30m },
        { 107_260m, 38_247.30m },
        { 142_960m, 54_669.30m },
        { 208_390m, 85_421.40m },
        { 250_000m, 105_810.30m },
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
