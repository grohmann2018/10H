namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class commentMusidID : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "Music_ID", "dbo.Musics");
            DropIndex("dbo.Comments", new[] { "Music_ID" });
            AddColumn("dbo.Comments", "MusicID", c => c.Int(nullable: false));
            DropColumn("dbo.Comments", "Music_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "Music_ID", c => c.Int());
            DropColumn("dbo.Comments", "MusicID");
            CreateIndex("dbo.Comments", "Music_ID");
            AddForeignKey("dbo.Comments", "Music_ID", "dbo.Musics", "ID");
        }
    }
}
