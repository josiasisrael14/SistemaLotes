using System.Linq;
using System.Threading.Tasks;
using System;

using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SistemaLotes.Models
{
    public class SistemaLotesDBContext:DbContext
    {

        public SistemaLotesDBContext(DbContextOptions<SistemaLotesDBContext> options): base(options)
        {



        }


        public DbSet<entidad> entidad { get; set; }





    }



}
