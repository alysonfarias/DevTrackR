using DevTrackr.API.Entities;
using DevTrackr.API.Models;
using DevTrackr.API.Persistance;
using Microsoft.AspNetCore.Mvc;

namespace DevTrackr.API.Controllers
{
    [ApiController]
    [Route("api/packages")]
    public class PackagesController : ControllerBase
    {
        private readonly DevTrackRContext _context;
        public PackagesController(DevTrackRContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var packages = _context.Packages;
            return Ok(packages);
        }

        [HttpGet("{code}")]
        public IActionResult GetByCode(string code)
        {
            var package = _context
            .Packages
            .SingleOrDefault(p => p.Code == code);

            if (package is null) return (NotFound());

            return Ok(package);

        }

        [HttpPost]
        public IActionResult Post(AddPackageInputModel model)
        {
            // FluentValidation
            if(model.Title.Length < 10){
                return BadRequest("Title must be at least 10 characters long");
            }
            var package = new Package(model.Title, model.Weight);

            _context.Packages.Add(package);
            return CreatedAtAction(
                "GetByCode",
             new { code = package.Code },
             package);
        }

        // POST api/packages/1234-5678-9012-3456/updates
        [HttpPost("{code}/updates")]
        public IActionResult PostUpdate(string code, AddPackageUpdateInputModel model)
        {
            var package = _context.Packages.SingleOrDefault(p => p.Code == code);

            if(package is null) return NotFound();

            
            package.AddUpdate(model.Status, model.Delivered);
            return Ok();
        }
    }
}