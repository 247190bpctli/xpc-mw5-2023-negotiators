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

    public ProductEntity ProductDetails(Guid id)
    {
        return _db.Products
            .Include(x => x.Category)
            .Include(x => x.Manufacturer)
            .Include(x => x.Reviews)
            .SingleOrDefault(product => product.Id == id)!;
    }

    public Guid ProductAdd(ProductAddDto productToAdd)
    {
        //assemble the row
        ProductEntity newProduct = new()
        {
            Name = productToAdd.Name,
            ImageUrl = productToAdd.ImageUrl,
            Description = productToAdd.Description,
            Price = productToAdd.Price,
            Weight = productToAdd.Weight,
            Stock = productToAdd.Stock,
            Category = _db.Categories.SingleOrDefault(category => category.Id == productToAdd.CategoryId),
            Manufacturer = _db.Manufacturers.SingleOrDefault(manufacturer => manufacturer.Id == productToAdd.ManufacturerId),
            Reviews = new List<ReviewEntity>()
        };

        _db.Products.Add(newProduct);
        _db.SaveChanges();

        return newProduct.Id; 
    }

    public void ProductEdit(Guid id, string name, string imageUrl, string description, double price, double weight, int stock, Guid categoryId, Guid manufacturerId)
    {

        ProductEntity productToEdit = _db.Products.SingleOrDefault(product => product.Id == id)!;

        productToEdit.Name = name;
        productToEdit.ImageUrl = imageUrl;
        productToEdit.Description = description;
        productToEdit.Price = price;
        productToEdit.Weight = weight;
        productToEdit.Stock = stock;
        productToEdit.Category = _db.Categories.SingleOrDefault(category => category.Id == categoryId);
        productToEdit.Manufacturer = _db.Manufacturers.SingleOrDefault(manufacturer => manufacturer.Id == manufacturerId);

        _db.SaveChanges();
    }

    public void ProductDelete(Guid id)
    {
        ProductEntity productToDelete = _db.Products.SingleOrDefault(product => product.Id == id)!;

        _db.Products.Remove(productToDelete); 
        _db.SaveChanges();
    }

    public void ReviewAdd(ReviewAddDto r)
    {
        r.Stars = (r.Stars <= 5) ? r.Stars : 5; //limit stars to 5

        //assemble the row
        ReviewEntity @new = new()
        { 
            Stars = r.Stars,
            User = r.User,
            Description = r.Description
        };

        _db.Products
            .Include(x => x.Reviews)
            .SingleOrDefault(product => product.Id == r.ProductId)!.Reviews.Add(@new);
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