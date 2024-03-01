using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Caching.Memory;
using TvLefisc.DAO;

namespace TvLefisc.Controllers
{
    public class TvLefiscController : ControllerBase
    {
        private readonly DaoLefisc daoLefisc;
        private readonly IMemoryCache cache;

        public TvLefiscController(IConfiguration configuration, IMemoryCache memoryCache)
        {
            daoLefisc = new DaoLefisc();
            cache = memoryCache;
        }

        [HttpPost]
        [Authorize]
        [EnableRateLimiting("fixed")]
        [Route("Add1visualizacao")]
        public IActionResult Add1Visualizacao(int visualizacoes, int idTV)
        {
            var result = daoLefisc.Add1Visualizacao(visualizacoes, idTV);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Erro ao adicionar visualização.");
            }
        }

        [HttpGet]
        [Authorize]
        [EnableRateLimiting("fixed")]
        [Route("TodasCategorias")]
        public IActionResult GetTodasCategorias([FromQuery(Name = "token")] string token)
        {
            string cacheKey = $"TodasCategorias-{token}";

            if (cache.TryGetValue(cacheKey, out var cachedValue))
            {
                return Ok(cachedValue);
            }

            var categorias = daoLefisc.GetTodasCategorias(token);
            if (categorias != null)
            {
                cache.Set(cacheKey, categorias, TimeSpan.FromMinutes(5));
                return Ok(categorias);
            }
            else
            {
                return BadRequest("Erro ao obter categorias.");
            }
        }

        [HttpGet]
        [Authorize]
        [EnableRateLimiting("fixed")]
        [Route("PorCategoriaUltimasPorPalavra")]
        public IActionResult GetCategoriaUltimasPorPalavra(int categoria, string palavra)
        {
            string cacheKey = $"PorCategoriaUltimasPorPalavra-{categoria}-{palavra}";

            if (cache.TryGetValue(cacheKey, out var cachedValue))
            {
                return Ok(cachedValue);
            }

            var result = daoLefisc.GetPorCategoriaUltimasPorPalavra(categoria, palavra);
            if (result != null)
            {
                cache.Set(cacheKey, result, TimeSpan.FromMinutes(5));
                return Ok(result);
            }
            else
            {
                return BadRequest("Erro ao obter categoria últimas por palavra.");
            }
        }

        [HttpGet]
        [Authorize]
        [EnableRateLimiting("fixed")]
        [Route("PorCategoriaMaisAcessadasPorPalavra")]
        public IActionResult GetCategoriaMaisAcessadasPorPalavra(int categoria, string palavra)
        {
            string cacheKey = $"PorCategoriaMaisAcessadasPorPalavra-{categoria}-{palavra}";

            if (cache.TryGetValue(cacheKey, out var cachedValue))
            {
                return Ok(cachedValue);
            }

            var result = daoLefisc.GetPorCategoriaMaisAcessadasPorPalavra(categoria, palavra);
            if (result != null)
            {
                cache.Set(cacheKey, result, TimeSpan.FromMinutes(5));
                return Ok(result);
            }
            else
            {
                return BadRequest("Erro ao obter categoria mais acessadas por palavra.");
            }
        }

        [HttpGet]
        [Authorize]
        [EnableRateLimiting("fixed")]
        [Route("PorCategoriaUltimas")]
        public IActionResult GetCategoriaUltimas(int categoria)
        {
            var result = daoLefisc.GetPorCategoriaUltimas(categoria);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Erro ao obter categoria ultimas.");
            }
        }

        [HttpGet]
        [Authorize]
        [EnableRateLimiting("fixed")]
        [Route("PorCategoriaMaisAcessadas")]
        public IActionResult GetCategoriaMaisAcessadas(int categoria)
        {
            if (cache.TryGetValue($"PorCategoriaMaisAcessadas-{categoria}", out var cachedValue))
            {
                return Ok(cachedValue);
            }

            var result = daoLefisc.GetPorCategoriaMaisAcessadas(categoria);
            if (result != null)
            {
                cache.Set($"PorCategoriaMaisAcessadas-{categoria}", result, TimeSpan.FromMinutes(5));
                return Ok(result);
            }
            else
            {
                return BadRequest("Erro ao obter categoria mais acessadas.");
            }
        }

        [HttpGet]
        [Authorize]
        [EnableRateLimiting("fixed")]
        [Route("PorCategoria")]
        public IActionResult GetCategoria(int categoria)
        {
            if (cache.TryGetValue($"PorCategoria-{categoria}", out var cachedValue))
            {
                return Ok(cachedValue);
            }

            var result = daoLefisc.GetPorCategoria(categoria);
            if (result != null)
            {
                cache.Set($"PorCategoria-{categoria}", result, TimeSpan.FromMinutes(5));
                return Ok(result);
            }
            else
            {
                return NotFound("Categoria não encontrada.");
            }
        }

        [HttpGet]
        [EnableRateLimiting("fixed")]
        [Route("UltimasPorPalavra")]
        public IActionResult GetUltimasPalavra(string palavra)
        {
            if (cache.TryGetValue($"UltimasPorPalavra-{palavra}", out var cachedValue))
            {
                return Ok(cachedValue);
            }

            var result = daoLefisc.GetUltimasPorPalavra(palavra);
            if (result != null)
            {
                cache.Set($"UltimasPorPalavra-{palavra}", result, TimeSpan.FromMinutes(5));
                return Ok(result);
            }
            else
            {
                return BadRequest("Erro ao obter últimas por palavra.");
            }
        }

        [HttpGet]
        [Authorize]
        [EnableRateLimiting("fixed")]
        [Route("TodasMaisAcessadaPorPalavra")]
        public IActionResult GetTodasMaisPalavra(string palavra)
        {
            if (cache.TryGetValue($"TodasMaisAcessadaPorPalavra-{palavra}", out var cachedValue))
            {
                return Ok(cachedValue);
            }

            var result = daoLefisc.GetTodasMaisAcessadasPorPalavra(palavra);
            if (result != null)
            {
                cache.Set($"TodasMaisAcessadaPorPalavra-{palavra}", result, TimeSpan.FromMinutes(5));
                return Ok(result);
            }
            else
            {
                return BadRequest("Erro ao obter todas mais acessadas por palavra.");
            }
        }

        [HttpGet]
        [EnableRateLimiting("fixed")]
        [Route("UltimasCriadas")]
        public IActionResult GetUltimas()
        {
            if (cache.TryGetValue("UltimasCriadas", out var cachedValue))
            {
                return Ok(cachedValue);
            }

            var result = daoLefisc.GetUltimasCriadas();
            if (result != null)
            {
                cache.Set("UltimasCriadas", result, TimeSpan.FromMinutes(5));
                return Ok(result);
            }
            else
            {
                return BadRequest("Erro ao obter últimas criadas.");
            }
        }


        [HttpGet]
        [Authorize]
        [EnableRateLimiting("fixed")]
        [Route("MaisAcessadas")]
        public IActionResult GetMaisAcessadas()
        {
            if (cache.TryGetValue("MaisAcessadas", out var cachedValue))
            {
                return Ok(cachedValue);
            }

            var result = daoLefisc.GetMaisAcessada();
            if (result != null)
            {
                cache.Set("MaisAcessadas", result, TimeSpan.FromMinutes(5));
                return Ok(result);
            }
            else
            {
                return BadRequest("Erro ao obter mais acessadas.");
            }
        }

        [HttpGet]
        [Authorize]
        [EnableRateLimiting("fixed")]
        [Route("Todas")]
        public IActionResult GetTodas()
        {
            if (cache.TryGetValue("Todas", out var cachedValue))
            {
                return Ok(cachedValue);
            }

            var result = daoLefisc.GetTodas();
            if (result != null)
            {
                cache.Set("Todas", result, TimeSpan.FromMinutes(5));
                return Ok(result);
            }
            else
            {
                return BadRequest("Erro ao obter todas.");
            }
        }

        [HttpGet]
        [Authorize]
        [EnableRateLimiting("fixed")]
        [Route("TodasPorPalavra")]
        public IActionResult GetPorPalavra(string palavra)
        {
            if (cache.TryGetValue($"TodasPorPalavra-{palavra}", out var cachedValue))
            {
                return Ok(cachedValue);
            }

            var result = daoLefisc.GetPorPalavra(palavra);
            if (result != null)
            {
                cache.Set($"TodasPorPalavra-{palavra}", result, TimeSpan.FromMinutes(5));
                return Ok(result);
            }
            else
            {
                return BadRequest("Erro ao obter todas por palavra.");
            }
        }
    }
}