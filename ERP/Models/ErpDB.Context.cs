﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ERP.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ErpDB : DbContext
    {
        public ErpDB()
            : base("name=ErpDB")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Depts> Depts { get; set; }
        public virtual DbSet<EmpHis> EmpHis { get; set; }
        public virtual DbSet<EmpInfo> EmpInfo { get; set; }
        public virtual DbSet<EmpShft> EmpShft { get; set; }
        public virtual DbSet<Jop> Jop { get; set; }
        public virtual DbSet<Loan> Loan { get; set; }
        public virtual DbSet<Mors> Mors { get; set; }
        public virtual DbSet<Punsh> Punsh { get; set; }
        public virtual DbSet<Sal> Sal { get; set; }
        public virtual DbSet<Vac> Vac { get; set; }
        public virtual DbSet<BanksList> BanksList { get; set; }
        public virtual DbSet<DegreeList> DegreeList { get; set; }
        public virtual DbSet<EmpDegree> EmpDegree { get; set; }
        public virtual DbSet<EmpFamily> EmpFamily { get; set; }
        public virtual DbSet<EmpFin> EmpFin { get; set; }
        public virtual DbSet<EmpForm> EmpForm { get; set; }
        public virtual DbSet<EmpGender> EmpGender { get; set; }
        public virtual DbSet<emploan> emploan { get; set; }
        public virtual DbSet<emppunish> emppunish { get; set; }
        public virtual DbSet<EmpVications> EmpVications { get; set; }
        public virtual DbSet<FamilyList> FamilyList { get; set; }
        public virtual DbSet<Hr_dept> Hr_dept { get; set; }
        public virtual DbSet<HR_EmpReform> HR_EmpReform { get; set; }
        public virtual DbSet<HR_SpecificGroup> HR_SpecificGroup { get; set; }
        public virtual DbSet<JobCategory> JobCategory { get; set; }
        public virtual DbSet<JobGarde> JobGarde { get; set; }
        public virtual DbSet<JobKader> JobKader { get; set; }
        public virtual DbSet<JobStatus> JobStatus { get; set; }
        public virtual DbSet<LoanList> LoanList { get; set; }
        public virtual DbSet<PunishList> PunishList { get; set; }
        public virtual DbSet<SOCIALSTATUSES> SOCIALSTATUSES { get; set; }
        public virtual DbSet<townlist> townlist { get; set; }
        public virtual DbSet<Ag> Ag { get; set; }
        public virtual DbSet<AgentsList> AgentsList { get; set; }
        public virtual DbSet<AgMove> AgMove { get; set; }
        public virtual DbSet<digtab> digtab { get; set; }
        public virtual DbSet<LevelList> LevelList { get; set; }
        public virtual DbSet<PartMount> PartMount { get; set; }
        public virtual DbSet<partsname> partsname { get; set; }
        public virtual DbSet<projectlist> projectlist { get; set; }
        public virtual DbSet<SysList> SysList { get; set; }
        public virtual DbSet<Syslisto> Syslisto { get; set; }
        public virtual DbSet<townlist1> townlist1 { get; set; }
        public virtual DbSet<YearsList1> YearsList1 { get; set; }
        public virtual DbSet<CActivity> CActivity { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<KPro> KPro { get; set; }
        public virtual DbSet<PostList> PostList { get; set; }
        public virtual DbSet<PurType> PurType { get; set; }
        public virtual DbSet<TPro> TPro { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<Usr> Usr { get; set; }
        public virtual DbSet<AccDocSub> AccDocSub { get; set; }
        public virtual DbSet<AccDocSubSubSub> AccDocSubSubSub { get; set; }
        public virtual DbSet<AcckindList> AcckindList { get; set; }
        public virtual DbSet<AccTree> AccTree { get; set; }
        public virtual DbSet<DocAcc> DocAcc { get; set; }
        public virtual DbSet<DocKindList> DocKindList { get; set; }
        public virtual DbSet<TrialBalance> TrialBalance { get; set; }
        public virtual DbSet<YearsList> YearsList { get; set; }
        public virtual DbSet<VicationList> VicationList { get; set; }
        public virtual DbSet<AgentIDFind> AgentIDFind { get; set; }
        public virtual DbSet<BeginList> BeginList { get; set; }
        public virtual DbSet<fig> fig { get; set; }
        public virtual DbSet<mval> mval { get; set; }
        public virtual DbSet<vDepts> vDepts { get; set; }
        public virtual DbSet<vEDJ> vEDJ { get; set; }
        public virtual DbSet<vEmpHis> vEmpHis { get; set; }
        public virtual DbSet<vEmpl> vEmpl { get; set; }
        public virtual DbSet<vEmplon> vEmplon { get; set; }
        public virtual DbSet<vJops> vJops { get; set; }
        public virtual DbSet<vLoanE> vLoanE { get; set; }
        public virtual DbSet<vMorsE> vMorsE { get; set; }
        public virtual DbSet<vPunE> vPunE { get; set; }
        public virtual DbSet<vSalary> vSalary { get; set; }
        public virtual DbSet<vSalEntry> vSalEntry { get; set; }
        public virtual DbSet<vTSal> vTSal { get; set; }
        public virtual DbSet<vUsrEmp> vUsrEmp { get; set; }
        public virtual DbSet<vVacE> vVacE { get; set; }
        public virtual DbSet<vURoles> vURoles { get; set; }
        public virtual DbSet<ExpPro> ExpPro { get; set; }
        public virtual DbSet<ProState> ProState { get; set; }
        public virtual DbSet<Gallary> Gallary { get; set; }
        public virtual DbSet<DurList> DurList { get; set; }
        public virtual DbSet<ProList> ProList { get; set; }
        public virtual DbSet<ArchPro> ArchPro { get; set; }
    
        [DbFunction("ErpDB", "Sysrep")]
        public virtual IQueryable<Sysrep_Result> Sysrep()
        {
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<Sysrep_Result>("[ErpDB].[Sysrep]()");
        }
    }
}
