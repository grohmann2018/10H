namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MusicNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Musics", "Number", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Musics", "Number");
        }
    }
}
