using System.Collections.Generic;
using System.Linq;
using DevTrackr.API.Entities;
using DevTrackr.API.Persistance;
using Microsoft.EntityFrameworkCore;

namespace DevTrackr.API.Persistence.Repository
{
    public class PackageRepository : IPackageRepository
    {
        private readonly DevTrackRContext _context;
        public PackageRepository(DevTrackRContext context)
        {
            _context = context;
        }

        public void Add(Package package)
        {
            _context.Packages.Add(package);
            _context.SaveChanges();
        }

        public List<Package> GetAll()
        {
            return _context.Packages.ToList();
        }

        public Package GetByCode(string code)
        {
            return _context.Packages
            .Include(p => p.Updates)
            .SingleOrDefault(p => p.Code == code);
        }

        public void Update(Package package)
        {
            _context.Entry(package).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}