using SoferiProprietari.Domain;
using SoferiProprietari.Repository;

namespace SoferiProprietari.Service;

public class Service
{
    private IRepository<string, Person> personRepo;
    private IRepository<string, Driver> driverRepo;
    private IRepository<string, Vehicle> vehicleRepo;

    public Service(IRepository<string, Person> personRepo, IRepository<string, Driver> driverRepo, IRepository<string, Vehicle> vehicleRepo)
    {
        this.personRepo = personRepo;
        this.driverRepo = driverRepo;
        this.vehicleRepo = vehicleRepo;
    }

    public List<String> PeopleDescByName()
    {
        return
            (from person in personRepo.FindAll()
                orderby person.Name descending
                select person.Name + " " + person.Age)
            .ToList();
    }
    
    public List<string> DriversWithExpiredLicensesIn2019()
    {
        return
            (from sofer in driverRepo.FindAll()
                where sofer.ExpirationDate.Year == 2019
                select sofer.Name + " " + sofer.LicenseCat)
            .ToList();
    }
    
    public List<string> DriversWhoDontOwn()
    {
        var x = this.driverRepo.FindAll()
            .Where(sofer => sofer.Vehicles.Count == 0)
            .Select(sofer=>sofer.Name+" "+sofer.Age)
            .ToList();
        return x;
    }
    
    public List<string> OwnersButNotDrivers()
    {
        return (from persoana in personRepo.FindAll()
                where persoana.Vehicles.Count > 0 && driverRepo.FindAll().All(x => x.Id != persoana.Id)
                select persoana.Name + " " + persoana.Age)
            .ToList();
    }
    
    public List<string> VehiclesWithDriverOwnersWrongCat()
    {
        return (from vehicul in vehicleRepo.FindAll()
            where driverRepo.FindAll().Any(x => x.Id == vehicul.Owner && x.LicenseCat != vehicul.LicenseCat)
            select vehicul.ToString()).ToList();
    }
    
}



























/*
 Exercitiul 1
return 
    (from person in personRepo.FindAll()
        orderby person.Name descending
        select person.Name + " " + person.Age)
    .ToList();
    */


/*
 Exercitiul 2
 int expYear = 2019;

return (from driver in driverRepo.FindAll()
    where driver.ExpirationDate.Year == expYear
    select driver.Name + " "+ driver.LicenseCat).ToList();*/


/*
 Exercitiul 3
 return
    (from driver in driverRepo.FindAll()
        where driver.Vehicles.Count == 0
        select driver.Name + " " + driver.Age)
    .ToList();*/


/*
 Exercitiul 4
 return ( from person in personRepo.FindAll()
    where person.Vehicles.Any() && !driverRepo.FindAll().Any(sofer => sofer.Id == person.Id)
    select person.Name + " " + person.Age).ToList();*/

/*
 Exercitiul 5
 return
            (from vehicle in vehicleRepo.FindAll()
                where driverRepo.FindAll().Any(dr => dr.Id == vehicle.Owner && dr.LicenseCat != vehicle.LicenseCat)
                select vehicle.ToString())
            .ToList();*/
    
    