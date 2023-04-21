using eshopBackend.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace eshopBackend.DAL.Repositories;

public class CategoryRepository
{
    private readonly AppDbContext _db;

    public CategoryRepository(AppDbContext db) => _db = db;

    public List<CategoryEntity> CategoriesOverview(uint page = 1)
    {
        page = page is <= 255 and > 0 ? page : 255; //limit pages to 255 without zero
        uint skipRange = (page - 1) * 25;
        List<CategoryEntity> categories = _db.Categories.Skip((int)skipRange).Take(25).ToList();

        return categories;
    }

    public CategoryEntity CategoryDetails(Guid id)
    {
        return _db.Categories.SingleOrDefault(category => category.Id == id)!;
    }

    public Guid CategoryAdd(string name, string imageUrl, string description)
    {
        //assemble the row
        CategoryEntity newCategory = new()
        {
            Name = name,
            ImageUrl = imageUrl,
            Description = description
        };

        //add row to db
        DbSet<CategoryEntity> categoryUpdate = _db.Set<CategoryEntity>();

        categoryUpdate.Add(newCategory);
        _db.SaveChanges();

        return newCategory.Id;
    }

    public void CategoryEdit(Guid id, string name, string imageUrl, string description)
    {
        CategoryEntity categoryToEdit = _db.Categories.SingleOrDefault(category => category.Id == id)!;

        categoryToEdit.Name = name;
        categoryToEdit.ImageUrl = imageUrl;
        categoryToEdit.Description = description;

        _db.SaveChanges();
    }

    public void CategoryDelete(Guid id)
    {
        CategoryEntity categoryToDelete = _db.Categories.SingleOrDefault(category => category.Id == id)!;

        _db.Categories.Remove(categoryToDelete);
        _db.SaveChanges();
    }

    public List<CategoryEntity> SearchCategoryByName(string searchTerm)
    {
        return _db.Categories.Where(category => category.Name.Contains(searchTerm)).ToList();
    }
}