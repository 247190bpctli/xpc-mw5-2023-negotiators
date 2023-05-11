using eshopBackend.DAL.DTOs;
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

    public Guid CategoryAdd(CategoryDto categoryDto)
    {
        //assemble the row
        CategoryEntity newCategory = new()
        {
            Name = categoryDto.Name,
            ImageUrl = categoryDto.ImageUrl,
            Description = categoryDto.Description
        };

        //add row to db
        DbSet<CategoryEntity> categoryUpdate = _db.Set<CategoryEntity>();

        categoryUpdate.Add(newCategory);
        _db.SaveChanges();

        return newCategory.Id;
    }

    public void CategoryEdit(Guid id, CategoryDto categoryDto)
    {
        CategoryEntity categoryToEdit = _db.Categories.SingleOrDefault(category => category.Id == id)!;

        categoryToEdit.Name = categoryDto.Name;
        categoryToEdit.ImageUrl = categoryDto.ImageUrl;
        categoryToEdit.Description = categoryDto.Description;

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