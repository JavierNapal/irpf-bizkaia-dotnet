namespace IrpfBizkaia.Domain.Tests;

public class CotizacionesTrabajadorTests
{
    // Tipos del trabajador 2026 (régimen general, contrato indefinido):
    // contingencias comunes 4,70 % + MEI 0,15 % + desempleo 1,55 % + formación 0,10 % = 6,50 %.
    // Base máxima anual: 61.214,40 EUR (5.101,20 EUR x 12).
    public static TheoryData<decimal, decimal> CasosTotal => new()
    {
        { 0m, 0m },
        { 20_000m, 1_300.00m },
        { 30_000m, 1_950.00m },
        { 61_214.40m, 3_978.93m },
        { 100_000m, 3_978.93m },
    };

    [Theory]
    [MemberData(nameof(CasosTotal))]
    public void Calcular_devuelve_el_total_esperado(decimal salarioBrutoAnual, decimal totalEsperado)
    {
        var cotizaciones = CotizacionesTrabajador2026.Calcular(salarioBrutoAnual);

        Assert.Equal(totalEsperado, cotizaciones.Total);
    }

    [Fact]
    public void Calcular_devuelve_el_desglose_por_concepto()
    {
        var cotizaciones = CotizacionesTrabajador2026.Calcular(30_000m);

        Assert.Equal(1_410.00m, cotizaciones.ContingenciasComunes);
        Assert.Equal(45.00m, cotizaciones.Mei);
        Assert.Equal(465.00m, cotizaciones.Desempleo);
        Assert.Equal(30.00m, cotizaciones.FormacionProfesional);
    }

    [Fact]
    public void Calcular_rechaza_un_salario_negativo()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => CotizacionesTrabajador2026.Calcular(-1m));
    }
}
