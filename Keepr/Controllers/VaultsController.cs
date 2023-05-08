namespace Keepr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VaultsController : ControllerBase
    {
        private readonly VaultsService _vaultsService;
        private readonly Auth0Provider _auth;

        public VaultsController(VaultsService vaultsService, Auth0Provider auth)
        {
            _vaultsService = vaultsService;
            _auth = auth;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault vaultData)
        {
            try
            {
                Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
                vaultData.CreatorId = userInfo.Id;
                Vault vault = _vaultsService.CreateVault(vaultData);
                return Ok(vault);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{vaultId}")]
        public async Task<ActionResult<Vault>> GetOne(int vaultId)
        {
            try
            {
                Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
                Vault vault = _vaultsService.GetOne(vaultId, userInfo?.Id);
                return Ok(vault);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{vaultId}")]
        [Authorize]
        public async Task<ActionResult<Vault>> EditVault([FromBody] Vault vaultData, int vaultId)
        {
            try
            {
                Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
                vaultData.Id = vaultId;
                Vault vault = _vaultsService.EditVault(vaultData, vaultId, userInfo?.Id);
                return Ok(vault);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{vaultId}")]
        [Authorize]
        public async Task<ActionResult<string>> Remove(int vaultId)
        {
            try
            {
                Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
                string message = _vaultsService.Remove(vaultId, userInfo?.Id);
                return Ok(message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{vaultId}/keeps")]
        public async Task<ActionResult<List<VaultKeep>>> GetVaultKeeps(int vaultId)
        {
            try
            {
                Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
                List<VaultKeep> vaultKeeps = _vaultsService.GetVaultKeeps(vaultId, userInfo?.Id);
                return Ok(vaultKeeps);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}