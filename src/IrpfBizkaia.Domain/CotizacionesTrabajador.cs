namespace IrpfBizkaia.Domain;

/// <summary>
/// Desglose de las cotizaciones a la Seguridad Social a cargo del trabajador.
/// El total es la suma de los conceptos ya redondeados, para que el desglose siempre cuadre.
/// </summary>
public sealed record CotizacionesTrabajador(
    decimal ContingenciasComunes,
    decimal Mei,
    decimal Desempleo,
    decimal FormacionProfesional)
{
    public decimal Total => ContingenciasComunes + Mei + Desempleo + FormacionProfesional;
}
