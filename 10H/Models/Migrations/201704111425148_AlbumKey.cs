namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlbumKey : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Musics", "Album_ID", c => c.Int());
            CreateIndex("dbo.Musics", "Album_ID");
            AddForeignKey("dbo.Musics", "Album_ID", "dbo.Albums", "ID");
            DropColumn("dbo.Musics", "AlbumID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Musics", "AlbumID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Musics", "Album_ID", "dbo.Albums");
            DropIndex("dbo.Musics", new[] { "Album_ID" });
            DropColumn("dbo.Musics", "Album_ID");
        }
    }
}
