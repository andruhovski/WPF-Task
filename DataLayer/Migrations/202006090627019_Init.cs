namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CardResources",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Kind = c.Int(nullable: false),
                        Card_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cards", t => t.Card_Id)
                .Index(t => t.Card_Id);
            
            CreateTable(
                "dbo.Cards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Parent = c.Int(nullable: false),
                        Header = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rows",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(unicode: false),
                        StringTableResource_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StringTable", t => t.StringTableResource_Id)
                .Index(t => t.StringTableResource_Id);
            
            CreateTable(
                "dbo.ImageResource",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Image = c.Binary(),
                        Description = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CardResources", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.StringTable",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CardResources", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StringTable", "Id", "dbo.CardResources");
            DropForeignKey("dbo.ImageResource", "Id", "dbo.CardResources");
            DropForeignKey("dbo.Rows", "StringTableResource_Id", "dbo.StringTable");
            DropForeignKey("dbo.CardResources", "Card_Id", "dbo.Cards");
            DropIndex("dbo.StringTable", new[] { "Id" });
            DropIndex("dbo.ImageResource", new[] { "Id" });
            DropIndex("dbo.Rows", new[] { "StringTableResource_Id" });
            DropIndex("dbo.CardResources", new[] { "Card_Id" });
            DropTable("dbo.StringTable");
            DropTable("dbo.ImageResource");
            DropTable("dbo.Rows");
            DropTable("dbo.Cards");
            DropTable("dbo.CardResources");
        }
    }
}
