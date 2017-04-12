namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MusicMark : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Musics", "Mark", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Musics", "NumberOfComments", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Musics", "NumberOfComments");
            DropColumn("dbo.Musics", "Mark");
        }
    }
}
