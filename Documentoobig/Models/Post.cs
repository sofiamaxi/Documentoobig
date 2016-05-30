namespace Documentoobig.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;

    public class Post : DbContext
    {
        public Post()
            : base("name=Post")
        {
        }

       
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();


            modelBuilder.Entity<Orders>()
                    .HasRequired(m => m.From)
                    .WithMany(t => t.From)
                    .HasForeignKey(m => m.FromCityID)
                    .WillCascadeOnDelete(false);

            modelBuilder.Entity<Orders>()
                    .HasRequired(m => m.To)
                    .WithMany(t => t.To)
                    .HasForeignKey(m => m.ToCityID)
                    .WillCascadeOnDelete(false);

            modelBuilder.Entity<Orders>()
                   .HasRequired(m => m.FromDep)
                   .WithMany(t => t.From)
                   .HasForeignKey(m => m.FromDepID)
                   .WillCascadeOnDelete(false);

            modelBuilder.Entity<Orders>()
                    .HasRequired(m => m.ToDep)
                    .WithMany(t => t.To)
                    .HasForeignKey(m => m.ToDepID)
                    .WillCascadeOnDelete(false);

            modelBuilder.Entity<Orders>()
                  .HasRequired(m => m.Client)
                  .WithMany(t => t.Orders)
                  .HasForeignKey(m => m.ClientID)
                  .WillCascadeOnDelete(false);

            modelBuilder.Entity<Orders>()
                  .HasRequired(m => m.Receiver)
                  .WithMany(t => t.Orders)
                  .HasForeignKey(m => m.ReceiverID)
                  .WillCascadeOnDelete(false);

            modelBuilder.Entity<Orders>()
                 .HasRequired(m => m.Staff)
                 .WithMany(t => t.Orders)
                 .HasForeignKey(m => m.StaffID)
                 .WillCascadeOnDelete(false);

            modelBuilder.Entity<Department>()
                 .HasRequired(m => m.City)
                 .WithMany(t => t.Departments)
                 .HasForeignKey(m => m.CityID)
                 .WillCascadeOnDelete(false);

            modelBuilder.Entity<Staff>()
                 .HasRequired(m => m.Company)
                 .WithMany(t => t.Stafs)
                 .HasForeignKey(m => m.CompanyId)
                 .WillCascadeOnDelete(false);

        }

        public System.Data.Entity.DbSet<Documentoobig.Models.Receiver> Receivers { get; set; }
        public System.Data.Entity.DbSet<Documentoobig.Models.Company> Companies { get; set; }
    }

    public class Orders
    {
        public int Id { get; set; }        
        public int FromCityID { get; set; }        
        public virtual City From { get; set; }
        public int FromDepID { get; set; }     
        public virtual Department FromDep  { get; set; }        
        public int ToCityID { get; set; }
        public virtual City To { get; set; }        
        public int ToDepID { get; set; }
        public virtual Department ToDep { get; set; }
        public int ClientID { get; set; }
        public virtual Client Client { get; set; }
        public int StaffID { get; set; }
        public virtual Staff Staff { get; set; }
        public int ReceiverID { get; set; }
        public virtual Receiver Receiver { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public double Length { get; set; }
        public double Volume
        {
            get
            {
                return Height * Width * Length;
            }
        }
        public double Weight { get; set; }
        public double Cost { get; set; }
        public double Price
        {
            get
            {
                return Math.Round( (Volume / 105) * Weight *1.25 + Cost / 110 ,2 );
            }
        }
    }

    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<Orders> From { get; set; }
        public virtual ICollection<Orders> To { get; set; }
    }

    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CityID { get; set; }
        public string Address { get; set; }        
        public City City { get; set; }
        public virtual ICollection<Orders> From { get; set; }
        public virtual ICollection<Orders> To { get; set; }
        public string Description
        {
            get
            {
                return string.Format("{0} {1}", Name, Address);
            }
        }

    }
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DirectorFName { get; set; }
        public string DirectorLName { get; set; }
        public string DirectorPName { get; set; }
        public virtual ICollection<Staff> Stafs { get; set; }
    }
    public class Receiver
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
    public class Client
    {
        public int Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string PName { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime BirthDate { get; set; }
        public string Phone { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
        public string Description
        {
            get
            {
                return string.Format("{0} {1} {2}", FName, LName, PName);
            }
        }


    }
    public class Staff
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set;}
        public string FName { get; set; }
        public string LName { get; set; }
        public string PName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Phone { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    
        public string Description
        {
            get
            {
                return string.Format("{0} {1} {2}", FName, LName, PName);
            }
        }
    }
   
}