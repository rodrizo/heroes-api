using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {

        //private static List<SuperHero> heroes = new List<SuperHero>
        //{
        //        new SuperHero {
        //            Id = 1,
        //            Name = "Spider-Man",
        //            FirstName = "Peter",
        //            LastName = "Parker",
        //            Place = "Queens"
        //        },
        //        new SuperHero {
        //            Id = 2,
        //            Name = "IronMan",
        //            FirstName = "Tony",
        //            LastName = "Stark",
        //            Place = "Long Island"
        //        }
        //};
        private readonly DataContext dataContext;
        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            //it returns status code 200
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Get(int id)
        {
            //var hero = heroes.Find(h => h.Id == id);
            var hero = await _context.SuperHeroes.FindAsync(id);

            if (hero == null) return BadRequest("Hero not found");

            return Ok(hero);


        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            //heroes.Add(hero);
            
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            var hero = await _context.SuperHeroes.FindAsync(request.Id);

            if (hero == null) return BadRequest("Hero not found");

            //setting correct properties
            hero.Name = request.Name;
            hero.FirstName = request.FirstName;
            hero.LastName = request.LastName;
            hero.Place = request.Place;

            await _context.SaveChangesAsync();
            return Ok(hero);
        }

        [HttpDelete]

        public async Task<ActionResult<List<SuperHero>>> Delete(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);

            if (hero == null) return BadRequest("Hero not found");

            _context.SuperHeroes.Remove(hero);

            await _context.SaveChangesAsync();

            return Ok(hero);


        }

    }
}
