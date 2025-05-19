namespace Domain.Products;
public class EditProductDTO : CreateProductDTO
{
    
    public int ProductId { get; set; }

    public string ImageName { get; set; }

}