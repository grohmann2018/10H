namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class soldeuser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Solde", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Solde");
        }
    }
}
