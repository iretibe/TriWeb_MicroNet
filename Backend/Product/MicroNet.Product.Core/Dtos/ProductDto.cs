namespace MicroNet.Product.Core.Dtos
{
    public record ProductDto(Guid Id, string ProductCode, string ProductName, 
        string Description, string? Note);
}
