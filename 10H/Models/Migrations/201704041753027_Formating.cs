namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Formating : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Formatings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FormatID = c.Int(nullable: false),
                        MusicID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Formatings");
        }
    }
}
