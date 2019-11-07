using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Prosjekt_3.Models
{
    public class DBSpørsmål
    {
        [Key]
        public int Id { get; set; }
        public string spørmål { get; set; }
        public string svar { get; set; }
        public int rating { get; set; }
        public int stemmer { get; set; }
        public virtual DBType type { get; set; }

    }
    public class DBType
    {
        [Key]
        public int TypeId { get; set; }
        public string type { get; set; }
        public virtual List<DBSpørsmål> spørmål { get; set; }
    }


    public class SpørsmålContext : DbContext
    {
        public SpørsmålContext(DbContextOptions<SpørsmålContext> options) : base(options)
        {





        }


        /*
                protected override void OnModelCreating(DbModelBuilder modelBuilder)
                {
                    modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
                }*/


        public DbSet<DBType> TypeSpørsmåls { get; set; }
        public DbSet<DBSpørsmål> spørsmåls { get; set; }

    }
}
