namespace MicroNet.Revenue.Core.ValueObjects
{
    public record FeeStructure(string Type, decimal RateOrAmount, string Frequency);
}
