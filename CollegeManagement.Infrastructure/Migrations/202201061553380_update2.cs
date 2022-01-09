namespace CollegeManagement.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Courses", "Name", c => c.String(nullable: false, maxLength: 16, unicode: false));
            AlterColumn("dbo.Subjects", "Name", c => c.String(nullable: false, maxLength: 16, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Subjects", "Name", c => c.String(nullable: false, maxLength: 8, unicode: false));
            AlterColumn("dbo.Courses", "Name", c => c.String(nullable: false, maxLength: 8, unicode: false));
        }
    }
}
