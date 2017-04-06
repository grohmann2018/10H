namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Album : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Musics", "AlbumID", c => c.Int(nullable: false));
            AddColumn("dbo.Musics", "duration", c => c.Int(nullable: false));
            DropColumn("dbo.Musics", "Album");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Musics", "Album", c => c.String());
            DropColumn("dbo.Musics", "duration");
            DropColumn("dbo.Musics", "AlbumID");
        }
    }
}
