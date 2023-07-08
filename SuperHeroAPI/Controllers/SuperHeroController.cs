using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Data;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _db;

        public SuperHeroController(DataContext db)
        {
            _db = db;
        }


        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetSuperHeroes()
        {
            return Ok(await _db.SuperHeroes.ToListAsync());
        }

        [HttpPost]

        public async Task<ActionResult<List<SuperHero>>> CreateSuperHeroes(SuperHero hero)
        {
            _db.SuperHeroes.Add(hero);
            await _db.SaveChangesAsync();

            return Ok(await _db.SuperHeroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateSuperHeroes(SuperHero hero)
        {
            var dbHero = await _db.SuperHeroes.FindAsync(hero.Id);
            if (dbHero == null)  
                return BadRequest("hero not found");

            dbHero.Name = hero.Name;
            dbHero.FirstName = hero.FirstName;
            dbHero.LastName = hero.LastName;
            dbHero.Place = hero.Place;

            await _db.SaveChangesAsync();

            return Ok(await _db.SuperHeroes.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> DeleteSuperHero(int id)
        {
            var dbHero = await _db.SuperHeroes.FindAsync(id);
            if (dbHero == null) 
                return BadRequest("hero not found");

            _db.SuperHeroes.Remove(dbHero);
            await _db.SaveChangesAsync();

            return Ok(await _db.SuperHeroes.ToListAsync());
        }

    }
}
