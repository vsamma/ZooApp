namespace ZooApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedCreationDatetonullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Animals", "CreationDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Animals", "CreationDate", c => c.DateTime(nullable: false));
        }
    }
}
