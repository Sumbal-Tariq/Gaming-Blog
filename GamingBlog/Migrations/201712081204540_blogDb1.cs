namespace GamingBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class blogDb1 : DbMigration
    {
        public override void Up()
        {
            //DropColumn("dbo.Ratings", "Active");
        }

        public override void Down()
        {
            //AddColumn("dbo.Ratings", "Active", c => c.Boolean(nullable: false));
        }
    }
}
