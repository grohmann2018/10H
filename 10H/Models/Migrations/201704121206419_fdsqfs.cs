namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fdsqfs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Comments = c.String(),
                        Note = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Music_ID = c.Int(),
                        User_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Musics", t => t.Music_ID)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .Index(t => t.Music_ID)
                .Index(t => t.User_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "User_ID", "dbo.Users");
            DropForeignKey("dbo.Comments", "Music_ID", "dbo.Musics");
            DropIndex("dbo.Comments", new[] { "User_ID" });
            DropIndex("dbo.Comments", new[] { "Music_ID" });
            DropTable("dbo.Comments");
        }
    }
}
