using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TEST_API.Back;
using TEST_API_Database.Back;
using TEST_API_Database.Database;

namespace Test1_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonneController : ControllerBase
    {
        

        private readonly ILogger<PersonneController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public PersonneController(ILogger<PersonneController> logger , IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(Personne personne)
        {
            if (personne.dateDeNaissance <= DateTime.Now.AddYears(-150))
                return Problem("Date de naissance invalide");

            if (ModelState.IsValid)
            {
                //user.Id = Guid.NewGuid();

                await _unitOfWork.Personnes.Add(personne);
                await _unitOfWork.CompleteAsync();

                return CreatedAtAction("GetItem", new { personne.PersonneID }, personne);
            }

            return new JsonResult("Something went wrong") { StatusCode = 500 };
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(int id)
        {
            var user = await _unitOfWork.Personnes.GetById(id);

            if (user == null)
                return NotFound(); // 404 http status code 

            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _unitOfWork.Personnes.All() ;

            return Ok(users);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateItem(int id, Personne personne)
        {
            if (id != personne.PersonneID)
                return BadRequest();

            await _unitOfWork.Personnes.Upsert(personne);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = await _unitOfWork.Personnes.GetById(id);

            if (item == null)
                return BadRequest();

            await _unitOfWork.Personnes.Delete(id);
            await _unitOfWork.CompleteAsync();

            return Ok(item);
        }
    }
}
