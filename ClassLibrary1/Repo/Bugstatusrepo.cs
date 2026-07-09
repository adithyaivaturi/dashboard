using ClassLibrary1.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Repo
{

    public interface IBugstatusrepo
    {
        Task<string> Addstudent(Bugstatus bugstatus);
        Task<Bugstatus> Getstudent(int id);
    }

    public class Bugstatusrepo : IBugstatusrepo
    {
        private readonly DBcontext _context;
        public Bugstatusrepo(DBcontext context)
        {
            _context = context;
        }
        public async Task<string> Addstudent(Bugstatus bugstatus)
        {
            try
            {
                _context.Bugstatuses.Add(bugstatus);
                await _context.SaveChangesAsync();
                return "Bugstatus added successfully.";
            }
            catch (Exception ex)
            {
                return "Error adding bugstatus : " + ex.Message;
            }
        }
        public async Task<Bugstatus> Getstudent(int id)
        {
            return await _context.Bugstatuses.FindAsync(id);
        }
        public async Task<Bugstatus> UpdateBugstatus(Bugstatus bugstatus)
        {
            var existingBugstatus = await _context.Bugstatuses.FindAsync(bugstatus.Id);
            if (existingBugstatus != null)
            {
                existingBugstatus.Bug = bugstatus.Bug;
                existingBugstatus.Description = bugstatus.Description;
                existingBugstatus.Status = bugstatus.Status;
                existingBugstatus.Priority = bugstatus.Priority;
                existingBugstatus.UpdatedAt = DateTime.Now;
                await _context.SaveChangesAsync();
                return existingBugstatus;
            }
            return null;
        }
        public async Task<bool> DeleteBugstatus(int id)
        {
            var existingBugstatus = await _context.Bugstatuses.FindAsync(id);
            if (existingBugstatus != null)
            {
                _context.Bugstatuses.Remove(existingBugstatus);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
