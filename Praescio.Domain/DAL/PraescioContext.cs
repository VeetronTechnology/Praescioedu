using Praescio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praescio.Domain.DAL
{
    public class PraescioContext : DbContext
    {
        //private AuditTrailFactory _auditFactory;
        //private readonly List<AuditLog> _auditList = new List<AuditLog>();
        //private readonly List<DbEntityEntry> _objectList = new List<DbEntityEntry>();

        public PraescioContext()
            : base("PraescioContextConnectionString")
        {
            //this.Configuration.LazyLoadingEnabled = true;
            //this.Configuration.ProxyCreationEnabled = true;
            Database.SetInitializer<PraescioContext>(null);
        }

        #region Entities
        public DbSet<AssignmentStatus> AssignmentStatus { get; set; }
        public DbSet<UserAssessmentDetail> UserAssessmentDetail { get; set; }
        public DbSet<Mst_Activities> Activities { get; set; }
        public DbSet<Mst_Package> Package { get; set; }
        public DbSet<Mst_PackageHistory> PackageHistory { get; set; }
        public DbSet<Mst_CategoryType> CategoryType { get; set; }
        public DbSet<Mst_InstitutionAccount> OrganizationAccount { get; set; }
        public DbSet<Mst_AccountType> AccountType { get; set; }
        public DbSet<Mst_Account> Account { get; set; }
        public DbSet<Mst_StudentParentAccount> StudentParentAccount { get; set; }
        public DbSet<PasswordResetAccount> PasswordResetAccount { get; set; }
        public DbSet<Mst_Board> Board { get; set; }
        public DbSet<Mst_PrincipalDetail> PrincipalDetail { get; set; }
        public DbSet<Mst_State> State { get; set; }
        public DbSet<Mst_City> City { get; set; }
        public DbSet<Mst_AssignmentType> AssignmentType { get; set; }
        public DbSet<Assignment> Assignment { get; set; }
        public DbSet<AssignmentHKPMapping> AssignmentHKPMapping { get; set; }
        public DbSet<AssignmentHKPStudent> AssignmentHKPStudent { get; set; }
        public DbSet<AssignmentTeacherMapping> AssignmentTeacherMapping { get; set; }
        public DbSet<KnowledgeBank> KnowledgeBank { get; set; }
        public DbSet<QuestionType> QuestionType { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<QuestionOption> QuestionOption { get; set; }
        public DbSet<QuestionContent> QuestionContent { get; set; }
        public DbSet<UserAssessment> UserAssessment { get; set; }
        public DbSet<Standard> Standard { get; set; }
        public DbSet<Subject> Subject { get; set; }
        public DbSet<Medium> Medium { get; set; }
        public DbSet<TeacherStandardMapping> StandardMapping { get; set; }
        public DbSet<Mst_ExceptionLog> ExceptionLog { get; set; }
        public DbSet<Video> Video { get; set; }
        public DbSet<Creativity> Creativity { get; set; }
        public DbSet<CreativityComment> CreativityComment { get; set; }
        public DbSet<Notice> Notice { get; set; }
        public DbSet<PaymentTransaction> PaymentTransaction { get; set; }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //modelBuilder.Entity<Menu>().HasOptional(e => e.MainMenu).WithMany().HasForeignKey(m => m.MainMenuID);
            //modelBuilder.Entity<T_RequestActivities>().HasOptional(e => e.ParentActivity).WithMany().HasForeignKey(m => m.ParentId);
            //modelBuilder.Entity<M_DefaultActivites>().HasOptional(e => e.ParentActivity).WithMany().HasForeignKey(m => m.ParentId);
            //modelBuilder.Entity<T_ActivityMembers>().HasOptional(e => e.Status).WithMany().HasForeignKey(m => m.UserStatus);
            //modelBuilder.Entity<T_ActivityMembers>().HasOptional(e => e.Status).WithMany().HasForeignKey(m => m.InitiatorStatus);
        }

        //public override int SaveChanges()
        //{
        //    try
        //    {


        //        _auditList.Clear();
        //        _objectList.Clear();
        //        _auditFactory = new AuditTrailFactory(this);

        //        var entityList = ChangeTracker.Entries().Where(p => p.State == EntityState.Added || p.State == EntityState.Deleted || p.State == EntityState.Modified);
        //        foreach (var entity in entityList)
        //        {
        //            AuditLog audit = _auditFactory.GetAudit(entity);
        //            bool isValid = true;

        //            if (entity.State == EntityState.Modified && string.IsNullOrWhiteSpace(audit.NewData) && string.IsNullOrWhiteSpace(audit.OldData))
        //            {
        //                isValid = false;
        //            }
        //            if (isValid)
        //            {
        //                _auditList.Add(audit);
        //                _objectList.Add(entity);
        //            }
        //        }

        //        var retVal = base.SaveChanges();
        //        if (_auditList.Count > 0)
        //        {
        //            int i = 0;
        //            foreach (var audit in _auditList)
        //            {
        //                if (audit.Actions == AuditActions.I.ToString())
        //                    audit.TableIdValue = _auditFactory.GetKeyValue(_objectList[i]);
        //                AuditLog.Add(audit);
        //                i++;
        //            }

        //            base.SaveChanges();
        //        }

        //        return retVal;
        //    }
        //    catch (Exception)
        //    {

        //        return 1;
        //    }
        //}
        //public async override Task<int> SaveChangesAsync()
        //{
        //    _auditList.Clear();
        //    _objectList.Clear();
        //    _auditFactory = new AuditTrailFactory(this);

        //    var entityList = ChangeTracker.Entries().Where(p => p.State == EntityState.Added || p.State == EntityState.Deleted || p.State == EntityState.Modified);
        //    foreach (var entity in entityList)
        //    {
        //        AuditLog audit = _auditFactory.GetAudit(entity);
        //        bool isValid = true;

        //        if (entity.State == EntityState.Modified && string.IsNullOrWhiteSpace(audit.NewData) && string.IsNullOrWhiteSpace(audit.OldData))
        //        {
        //            isValid = false;
        //        }
        //        if (isValid)
        //        {
        //            _auditList.Add(audit);
        //            _objectList.Add(entity);
        //        }
        //    }

        //    var retVal = base.SaveChanges();
        //    if (_auditList.Count > 0)
        //    {
        //        int i = 0;
        //        foreach (var audit in _auditList)
        //        {
        //            if (audit.Actions == AuditActions.I.ToString())
        //                audit.TableIdValue = _auditFactory.GetKeyValue(_objectList[i]);
        //            AuditLog.Add(audit);
        //            i++;
        //        }

        //        await base.SaveChangesAsync();
        //    }

        //    return retVal;
        //}
    }
}
