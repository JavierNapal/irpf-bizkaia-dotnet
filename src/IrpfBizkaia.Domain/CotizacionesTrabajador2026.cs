namespace IrpfBizkaia.Domain;

/// <summary>
/// Cotizaciones del trabajador en el régimen general durante 2026
/// (contrato indefinido, jornada completa).
/// Fuera de alcance en esta versión: base mínima, cuota de solidaridad
/// y contratos temporales.
/// </summary>
public static class CotizacionesTrabajador2026
{
    public const decimal BaseMaximaAnual = 61_214.40m;

    private const decimal TipoContingenciasComunes = 0.0470m;
    private const decimal TipoMei = 0.0015m;
    private const decimal TipoDesempleo = 0.0155m;
    private const decimal TipoFormacionProfesional = 0.0010m;

    public static CotizacionesTrabajador Calcular(decimal salarioBrutoAnual)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(salarioBrutoAnual);

        decimal baseCotizacion = Math.Min(salarioBrutoAnual, BaseMaximaAnual);

        return new CotizacionesTrabajador(
            ContingenciasComunes: Redondear(baseCotizacion * TipoContingenciasComunes),
            Mei: Redondear(baseCotizacion * TipoMei),
            Desempleo: Redondear(baseCotizacion * TipoDesempleo),
            FormacionProfesional: Redondear(baseCotizacion * TipoFormacionProfesional));
    }

    private static decimal Redondear(decimal importe) =>
        Math.Round(importe, 2, MidpointRounding.AwayFromZero);
}
