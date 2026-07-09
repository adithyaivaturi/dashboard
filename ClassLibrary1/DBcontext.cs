using ClassLibrary1.model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class DBcontext: DbContext
    {
        public DBcontext(DbContextOptions<DBcontext> options) : base(options)
        {

        }

        public DbSet<Bugstatus> Bugstatuses { get; set; }

    }
}
