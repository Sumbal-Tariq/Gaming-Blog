namespace GamingBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class blogDb : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Ratings", "UserID", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ratings", "UserID", c => c.Int(nullable: false));
        }
    }
}
