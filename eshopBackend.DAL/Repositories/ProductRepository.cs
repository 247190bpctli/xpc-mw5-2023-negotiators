using eshopBackend.DAL.DTOs;
using eshopBackend.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace eshopBackend.DAL.Repositories;

public class ProductRepository
{
    private readonly AppDbContext _db;

    public ProductRepository(AppDbContext db) => _db = db;

    public List<ProductEntity> ProductsOverview(uint page = 1)
    {
        page = page is <= 255 and > 0 ? page : 255; //limit pages to 255 without zero
        uint skipRange = (page - 1) * 25;
        List<ProductEntity> products = _db.Products.Skip((int)skipRange).Take(25)
            .Include(x => x.Category)
            .Include(x => x.Manufacturer)
            .Include(x => x.Reviews)
            .ToList();

        return products;
    }

    public ProductEntity? ProductDetails(Guid id)
    {
        return _db.Products
            .Include(x => x.Category)
            .Include(x => x.Manufacturer)
            .Include(x => x.Reviews)
            .SingleOrDefault(product => product.Id == id);
    }

    public Guid ProductAdd(ProductDto productDto)
    {
        //assemble the row
        ProductEntity newProduct = new()
        {
            Name = productDto.Name,
            ImageUrl = productDto.ImageUrl,
            Description = productDto.Description,
            Price = productDto.Price,
            Weight = productDto.Weight,
            Stock = productDto.Stock,
            Category = _db.Categories.SingleOrDefault(category => category.Id == productDto.CategoryId),
            Manufacturer = _db.Manufacturers.SingleOrDefault(manufacturer => manufacturer.Id == productDto.ManufacturerId),
            Reviews = new List<ReviewEntity>()
        };

        _db.Products.Add(newProduct);
        _db.SaveChanges();

        return newProduct.Id;
    }

    public void ProductEdit(Guid productId, ProductDto productDto)
    {
        ProductEntity productToEdit = _db.Products.SingleOrDefault(product => product.Id == productId)!;

        productToEdit.Name = productDto.Name;
        productToEdit.ImageUrl = productDto.ImageUrl;
        productToEdit.Description = productDto.Description;
        productToEdit.Price = productDto.Price;
        productToEdit.Weight = productDto.Weight;
        productToEdit.Stock = productDto.Stock;
        productToEdit.Category = _db.Categories.SingleOrDefault(category => category.Id == productDto.CategoryId);
        productToEdit.Manufacturer = _db.Manufacturers.SingleOrDefault(manufacturer => manufacturer.Id == productDto.ManufacturerId);

        _db.SaveChanges();
    }

    public void ProductDelete(Guid id)
    {
        ProductEntity productToDelete = _db.Products.SingleOrDefault(product => product.Id == id)!;

        _db.Products.Remove(productToDelete);
        _db.SaveChanges();
    }

    public void ReviewAdd(Guid id, AddReviewDto r)
    {
        r.Stars = r.Stars <= 5 ? r.Stars : 5; //limit stars to 5

        //assemble the row
        ReviewEntity @new = new()
        {
            Stars = r.Stars,
            User = r.User,
            Description = r.Description
        };

        _db.Products
            .Include(x => x.Reviews)
            .SingleOrDefault(product => product.Id == id)!.Reviews.Add(@new);
        _db.SaveChanges();
    }

    public List<ProductEntity> SearchProductByName(string searchTerm)
    {
        return _db.Products
            .Include(x => x.Category)
            .Include(x => x.Manufacturer)
            .Include(x => x.Reviews)
            .Where(product => product.Name.Contains(searchTerm)).ToList();
    }
}