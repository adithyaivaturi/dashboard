using ClassLibrary1;
using ClassLibrary1.model;
using ClassLibrary1.Repo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace test.Controllers
{
    [Route("api/Dashboard")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly DBcontext _context;
        public DashboardController(DBcontext context)
        {
            _context = context;
        }
        [HttpGet("Hello")]
        public string get()
        {
            return "hello world v2";
        }

        [HttpPost("AddBugstatus")]
        public async Task<IActionResult> AddBugstatus([FromBody] Bugstatus bugstatus)
        {
            Bugstatusrepo repo = new Bugstatusrepo(_context);
            var result = await repo.Addstudent(bugstatus);
            return Ok(result);
        }
        [HttpGet("GetBugstatus/{id}")]
        public async Task<Bugstatus> GetBugstatus(int id)
        {
            Bugstatusrepo repo = new Bugstatusrepo(_context);
            var result = await repo.Getstudent(id);
            if (result == null)
            {
                return new Bugstatus();
            }
            return result;
        }

        [HttpPatch("updateBugstatus")]
        public async Task<Bugstatus> updateBugstatus([FromBody] Bugstatus bugstatus)
        {
            List<String> error = isvalid(bugstatus);
            var bugstatus1 = _context.Bugstatuses.FindAsync(bugstatus.Id);
            Bugstatusrepo repo = new Bugstatusrepo(_context);
            if (bugstatus1 != null && error.Count <= 0)
            {
                Bugstatus bs = bugstatus1.Result;
                bs.Bug = bugstatus.Bug;
                bs.Description = bugstatus.Description;
                bs.Status = bugstatus.Status;
                bs.Priority = bugstatus.Priority;
                await repo.UpdateBugstatus(bs);
                return bs;
            }
            return new Bugstatus();
        }

        [HttpDelete("deleteBugstatus/{id}")]
        public async Task<IActionResult> deleteBugstatus(int id)
        {
            Bugstatusrepo repo = new Bugstatusrepo(_context);
            var result = await repo.DeleteBugstatus(id);
            if (result)
            {
                return Ok("Bugstatus deleted successfully.");
            }
            else
            {
                return NotFound("Bugstatus not found.");
            }
        }

        public List<string> isvalid(Bugstatus bugstatus)
        {
            List<string> error = new List<string>();
            if (string.IsNullOrEmpty(bugstatus.Bug) || string.IsNullOrWhiteSpace(bugstatus.Bug)){
                error.Add("Bug is required");
            }
            if (string.IsNullOrEmpty(bugstatus.Description) || string.IsNullOrWhiteSpace(bugstatus.Description)){
                error.Add("Description is required");
            }
            if (string.IsNullOrEmpty(bugstatus.Status) || string.IsNullOrWhiteSpace(bugstatus.Status)){
                error.Add("Status is required");
            }
            if (string.IsNullOrEmpty(bugstatus.Priority) || string.IsNullOrWhiteSpace(bugstatus.Priority)){
                error.Add("Priority is required");
            }
            return error;
        }


    }

}
