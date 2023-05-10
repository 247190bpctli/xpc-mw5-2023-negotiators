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

    public ManufacturerEntity ManufacturerDetails(Guid ManufacturerId)
    {
        return _db.Manufacturers.SingleOrDefault(manufacturer => manufacturer.Id == ManufacturerId)!;
    }

    public Guid ManufacturerAdd(AddManufacturerDto addManufacturerDto)
    {
        //assemble the row
        ManufacturerEntity newManufacturer = new()
        { 
            Name = addManufacturerDto.Name,
            Description = addManufacturerDto.Description,
            LogoUrl = addManufacturerDto.LogoUrl,
            Origin = addManufacturerDto.Origin
        };
        
        _db.Manufacturers.Add(newManufacturer);
        _db.SaveChanges();
        
        return newManufacturer.Id;
    }

    public void ManufacturerEdit(Guid ManufacturerId, EditManufacturerDto editManufacturerDto)
    {
        ManufacturerEntity manufacturerToEdit = _db.Manufacturers.SingleOrDefault(manufacturer => manufacturer.Id == ManufacturerId)!;

        manufacturerToEdit.Name = editManufacturerDto.Name;
        manufacturerToEdit.Description = editManufacturerDto.Description;
        manufacturerToEdit.LogoUrl = editManufacturerDto.LogoUrl;
        manufacturerToEdit.Origin = editManufacturerDto.Origin;

        _db.SaveChanges();
    }

    public void ManufacturerDelete(Guid ManufacturerId)
    {
        ManufacturerEntity manufacturerToDelete = _db.Manufacturers.SingleOrDefault(manufacturer => manufacturer.Id == ManufacturerId)!;

        _db.Manufacturers.Remove(manufacturerToDelete);
        _db.SaveChanges();
    }

    public List<ManufacturerEntity> SearchManufacturerByName(string searchTerm)
    {
        return _db.Manufacturers.Where(manufacturer => manufacturer.Name.Contains(searchTerm)).ToList();
    }
}