namespace Keepr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly ProfilesService _profilesService;
        private readonly Auth0Provider _auth;

        public ProfilesController(ProfilesService profilesService, Auth0Provider auth)
        {
            _profilesService = profilesService;
            _auth = auth;
        }

        [HttpGet("{profileId}")]
        public ActionResult<Profile> GetProfile(string profileId)
        {
            try
            {
                Profile profile = _profilesService.GetProfile(profileId);
                return Ok(profile);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{profileId}/keeps")]
        public ActionResult<List<Keep>> GetKeeps(string profileId)
        {
            try
            {
                List<Keep> keeps = _profilesService.GetKeeps(profileId);
                return Ok(keeps);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}