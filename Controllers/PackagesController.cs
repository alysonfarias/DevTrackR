using DevTrackr.API.Entities;
using DevTrackr.API.Models;
using DevTrackr.API.Persistance;
using DevTrackr.API.Persistence.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevTrackr.API.Controllers
{
    [ApiController]
    [Route("api/packages")]
    public class PackagesController : ControllerBase
    {
        private readonly IPackageRepository _repository;
        public PackagesController(IPackageRepository context)
        {
            _repository = context;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var packages = _repository.GetAll();
            return Ok(packages);
        }

        [HttpGet("{code}")]
        public IActionResult GetByCode(string code)
        {
            var package = _repository.GetByCode(code);

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


            _repository.Add(package);

            return CreatedAtAction(
                "GetByCode",
             new { code = package.Code },
             package);
        }

        // POST api/packages/1234-5678-9012-3456/updates
        [HttpPost("{code}/updates")]
        public IActionResult PostUpdate(string code, AddPackageUpdateInputModel model)
        {
            var package = _repository.GetByCode(code);

            if(package is null) return NotFound();

            package.AddUpdate(model.Status, model.Delivered);
            
            _repository.Update(package);

            return Ok();
        }
    }
}