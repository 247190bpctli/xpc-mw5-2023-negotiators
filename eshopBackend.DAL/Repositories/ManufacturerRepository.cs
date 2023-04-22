using eshopBackend.DAL.DTOs;
using eshopBackend.DAL.Entities;

namespace eshopBackend.DAL.Repositories;

public class ManufacturerRepository
{
    private readonly AppDbContext _db;

    public ManufacturerRepository(AppDbContext db) => _db = db;

    public List<ManufacturerEntity> ManufacturersOverview(uint page = 1)
    {
        page = page is <= 255 and > 0 ? page : 255; //limit pages to 255 without zero
        uint skipRange = (page - 1) * 25;
        List<ManufacturerEntity> manufacturers = _db.Manufacturers.Skip((int)skipRange).Take(25).ToList();

        return manufacturers;
    }

    public ManufacturerEntity ManufacturerDetails(Guid id)
    {
        return _db.Manufacturers.SingleOrDefault(manufacturer => manufacturer.Id == id)!;
    }

    public Guid ManufacturerAdd(ManufacturerAddDto m)
    {
        //assemble the row
        ManufacturerEntity newManufacturer = new()
        { 
            Name = m.Name,
            Description = m.Description,
            LogoUrl = m.LogoUrl,
            Origin = m.Origin
        };
        
        _db.Manufacturers.Add(newManufacturer);
        _db.SaveChanges();
        
        return newManufacturer.Id;
    }

    public void ManufacturerEdit(ManufacturerEditDto m)
    {
        ManufacturerEntity manufacturerToEdit = _db.Manufacturers.SingleOrDefault(manufacturer => manufacturer.Id == m.Id)!;

        manufacturerToEdit.Name = m.Name;
        manufacturerToEdit.Description = m.Description;
        manufacturerToEdit.LogoUrl = m.LogoUrl;
        manufacturerToEdit.Origin = m.Origin;

        _db.SaveChanges();
    }

    public void ManufacturerDelete(Guid id)
    {
        ManufacturerEntity manufacturerToDelete = _db.Manufacturers.SingleOrDefault(manufacturer => manufacturer.Id == id)!;

        _db.Manufacturers.Remove(manufacturerToDelete);
        _db.SaveChanges();
    }

    public List<ManufacturerEntity> SearchManufacturerByName(string searchTerm)
    {
        return _db.Manufacturers.Where(manufacturer => manufacturer.Name.Contains(searchTerm)).ToList();
    }
}