namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 200),
                        Address = c.String(nullable: false, maxLength: 200),
                        City = c.String(nullable: false, maxLength: 30),
                        Country = c.String(nullable: false, maxLength: 30),
                        RegistrationDate = c.DateTime(nullable: false),
                        Age = c.Int(nullable: false),
                        IsPremium = c.Boolean(nullable: false),
                        Phone = c.String(nullable: false, maxLength: 12),
                        CreatedBy = c.Int(nullable: false),
                        CreatedOn = c.DateTime(),
                        UpdatedBy = c.Int(nullable: false),
                        UpdatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ShippingAddress = c.String(nullable: false, maxLength: 200),
                        IsShipped = c.Boolean(nullable: false),
                        PaymentMethod = c.String(),
                        CreatedBy = c.Int(nullable: false),
                        CreatedOn = c.DateTime(),
                        UpdatedBy = c.Int(nullable: false),
                        UpdatedOn = c.DateTime(),
                        Customers_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customers_Id)
                .ForeignKey("dbo.products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.Customers_Id);
            
            CreateTable(
                "dbo.products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(nullable: false, maxLength: 400),
                        Category = c.String(nullable: false, maxLength: 55),
                        ReleaseDate = c.DateTime(nullable: false),
                        UnitsInStock = c.Int(nullable: false),
                        IsAvailable = c.Boolean(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreatedOn = c.DateTime(),
                        UpdatedBy = c.Int(nullable: false),
                        UpdatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "ProductId", "dbo.products");
            DropForeignKey("dbo.Orders", "Customers_Id", "dbo.Customers");
            DropIndex("dbo.Orders", new[] { "Customers_Id" });
            DropIndex("dbo.Orders", new[] { "ProductId" });
            DropTable("dbo.products");
            DropTable("dbo.Orders");
            DropTable("dbo.Customers");
        }
    }
}
