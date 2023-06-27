namespace LibraryManagementProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class transactions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        TransactionId = c.Int(nullable: false, identity: true),
                        BookId = c.Int(nullable: false),
                        BorrowerId = c.Int(nullable: false),
                        IssueDate = c.DateTime(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        ReturnDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TransactionId)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Borrowers", t => t.BorrowerId, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.BorrowerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "BorrowerId", "dbo.Borrowers");
            DropForeignKey("dbo.Transactions", "BookId", "dbo.Books");
            DropIndex("dbo.Transactions", new[] { "BorrowerId" });
            DropIndex("dbo.Transactions", new[] { "BookId" });
            DropTable("dbo.Transactions");
        }
    }
}
