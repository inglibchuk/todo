using Microsoft.AspNetCore.Mvc;
using ToDo.Core;
using ToDo.Data;

namespace ToDo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaxonomyController : ControllerBase
    {
        private readonly IRepository<Taxonomy> _taxonomyRepository;

        public TaxonomyController (IRepository<Taxonomy> taxonomyRepository)
        {
            _taxonomyRepository = taxonomyRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Taxonomy>> Get()
        {
            return await _taxonomyRepository.GetAllAsync();
        }
    }
}