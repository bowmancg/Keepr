namespace Keepr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KeepsController : ControllerBase
    {
        private readonly KeepsService _keepsService;
        private readonly Auth0Provider _auth;

        public KeepsController(KeepsService keepsService, Auth0Provider auth)
        {
            _keepsService = keepsService;
            _auth = auth;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Keep>> CreateKeep([FromBody] Keep keepData)
        {
            try
            {
                Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
                keepData.CreatorId = userInfo.Id;
                Keep keep = _keepsService.CreateKeep(keepData);
                return Ok(keep);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public ActionResult<List<Keep>> GetKeeps(string query)
        {
            try
            {
                List<Keep> keeps;
                if (query == null)
                {
                    keeps = _keepsService.GetKeeps();
                }
                else
                {
                    keeps = _keepsService.Get(query);
                }
                return Ok(keeps);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{keepId}")]
        public ActionResult<Keep> GetOne(int keepId)
        {
            try
            {
                Keep keep = _keepsService.GetOne(keepId);
                return Ok(keep);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{keepId}")]
        [Authorize]
        public async Task<ActionResult<Keep>> EditKeep([FromBody] Keep keepData, int keepId)
        {
            try
            {
                Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
                keepData.Id = keepId;
                Keep keep = _keepsService.EditKeep(keepData, keepId, userInfo.Id);
                return Ok(keep);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{keepId}")]
        [Authorize]
        public ActionResult<string> Remove(int keepId)
        {
            try
            {
                string message = _keepsService.Remove(keepId);
                return Ok(message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}