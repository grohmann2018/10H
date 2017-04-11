namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Thumbnails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "Thumbnail", c => c.Int(nullable: false));
            AddColumn("dbo.Musics", "Thumbnail", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Musics", "Thumbnail");
            DropColumn("dbo.Albums", "Thumbnail");
        }
    }
}
