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

    public CategoryEntity CategoryDetails(Guid CategoryId)
    {
        return _db.Categories.SingleOrDefault(category => category.Id == CategoryId)!;
    }

    public Guid CategoryAdd(AddCategoryDto addCategoryDto)
    {
        //assemble the row
        CategoryEntity newCategory = new()
        {
            Name = addCategoryDto.Name,
            ImageUrl = addCategoryDto.ImageUrl,
            Description = addCategoryDto.Description
        };

        //add row to db
        DbSet<CategoryEntity> categoryUpdate = _db.Set<CategoryEntity>();

        categoryUpdate.Add(newCategory);
        _db.SaveChanges();

        return newCategory.Id;
    }

    public void CategoryEdit(Guid CategoryId, EditCategoryDto editCategoryDto)
    {
        CategoryEntity categoryToEdit = _db.Categories.SingleOrDefault(category => category.Id == CategoryId)!;

        categoryToEdit.Name = editCategoryDto.Name;
        categoryToEdit.ImageUrl = editCategoryDto.ImageUrl;
        categoryToEdit.Description = editCategoryDto.Description;

        _db.SaveChanges();
    }

    public void CategoryDelete(Guid CategoryId)
    {
        CategoryEntity categoryToDelete = _db.Categories.SingleOrDefault(category => category.Id == CategoryId)!;

        _db.Categories.Remove(categoryToDelete);
        _db.SaveChanges();
    }

    public List<CategoryEntity> SearchCategoryByName(string searchTerm)
    {
        return _db.Categories.Where(category => category.Name.Contains(searchTerm)).ToList();
    }
}