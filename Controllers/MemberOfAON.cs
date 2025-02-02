using Microsoft.AspNetCore.Mvc;
using ArulOliNagar.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using ArulOliNagar.Services;

namespace ArulOliNagar.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MemberOfAON : ControllerBase
    {
        private readonly MembersService _membersService;

        public MemberOfAON(MembersService _MemberService)
        {
            _membersService = _MemberService;
        }

        [HttpPost("PostMemberDetails")]
        public async Task<IActionResult> InsertDataAsync([FromBody] List<Members> memberspostdata)
        {
            if (memberspostdata == null || !memberspostdata.Any())
            {
                return BadRequest("Member  data cannot be empty.");
            }
            else


                await _membersService.InsertMemberDataAsync(memberspostdata);
            return Ok(new { Message = "Member Data Saved successfully!" });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMembers()
        {
            var members = await _membersService.GetAllMembersAsync();
            return Ok(members);
        }

       
     
    }
}
