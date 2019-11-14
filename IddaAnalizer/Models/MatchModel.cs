namespace IddaAnalyser
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public class MatchModel : DbContext
    {
        public MatchModel(string connectionString)//<add name="MatchModel" connectionString="Data Source=DESKTOP-0ITDCMB\SQLEXPRESS;AttachDbFilename=D:\ATA\RecoverMatches.mdf;Initial Catalog=Matches1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False" providerName="System.Data.SqlClient" />
            : base(connectionString)
        {
            Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = true;
        }
        public MatchModel():base("MatchModel1")
        {
            Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = true;
        }
        // Temporary
       
        public DbSet<Test> Tests { get; set; }

        public DbSet<OddCombination> OddCombinations { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<PartialOdd> PartialOdds { get; set; }
        public DbSet<PartialOddPerm> PartialOddPerms { get; set; }
        public DbSet<AnalyseLimit> AnalyseLimits { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Time> Times { get; set; }
        public DbSet<Date> Dates { get; set; }

        public DbSet<FullOdd> FullOdds { get; set; }

        public DbSet<ExcelColumn> ExcelColumns { get; set; }

        public DbSet<AnalysedMatch> AnalysedMatches { get; set; }


        public DbSet<MatchResult> MatchResults { get; set; }
        public DbSet<GeneralResult> GeneralResults { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<MatchModel>(null);
            base.OnModelCreating(modelBuilder);
            this.Configuration.LazyLoadingEnabled = true;
            modelBuilder.Entity<MatchResult>().HasKey(m => m.MatchResultId);
            modelBuilder.Entity<Test>().HasKey(m => m.Id);


            // GeneralResult Relations
            modelBuilder.Entity<GeneralResult>()
               .HasMany<MatchResult>(m => m.MatchResults)
               .WithRequired(m => m.GeneralResult)
               .HasForeignKey(m => m.GeneralResultId)
               .WillCascadeOnDelete(false);

            // GeneralResult Relations


            modelBuilder.Entity<Date>()
               .HasMany<Match>(m => m.Matches)
               .WithRequired(m => m.Date)
               .HasForeignKey(m => m.DateId)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<Time>()
               .HasMany<Match>(m => m.Matches)
               .WithRequired(m => m.Time)
               .HasForeignKey(m => m.TimeId)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<Team>()
               .HasMany<Match>(m => m.HomeMatches)
               .WithRequired(m => m.HomeTeam)
               .HasForeignKey(m => m.HomeTeamId)
               .WillCascadeOnDelete(false);

           modelBuilder.Entity<FullOdd>()
              .HasMany<Match>(m => m.Matches)
              .WithRequired(m => m.FullOdd)
              .HasForeignKey(m => m.FullOddId)
              .WillCascadeOnDelete(false);

            modelBuilder.Entity<Team>()
               .HasMany<Match>(m => m.AwayMatches)
               .WithRequired(m => m.AwayTeam)
               .HasForeignKey(m => m.AwayTeamId)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<League>()
               .HasMany<Match>(m => m.Matches)
               .WithRequired(m => m.League)
               .HasForeignKey(m => m.LeagueId)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<MatchResult>()
               .HasMany<Match>(m => m.Matches)
               .WithRequired(m => m.MatchResult)
               .HasForeignKey(m => m.MatchResultId)
               .WillCascadeOnDelete(false);

        }
    }
}
