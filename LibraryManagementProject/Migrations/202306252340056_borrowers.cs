namespace LibraryManagementProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class borrowers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Borrowers",
                c => new
                    {
                        BorrowerId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        EmailId = c.String(),
                        PhoneNo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BorrowerId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Borrowers");
        }
    }
}
