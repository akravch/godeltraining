namespace Training.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPostDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "CreationDateTimeUtc", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "CreationDateTimeUtc");
        }
    }
}
