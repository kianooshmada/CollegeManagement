namespace CollegeManagement.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 8, unicode: false),
                    })
                .PrimaryKey(t => t.CourseID);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        SubjectID = c.Int(nullable: false, identity: true),
                        CourseID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 8, unicode: false),
                    })
                .PrimaryKey(t => t.SubjectID)
                .ForeignKey("dbo.Courses", t => t.CourseID, cascadeDelete: true)
                .Index(t => t.CourseID);
            
            CreateTable(
                "dbo.Enrollmens",
                c => new
                    {
                        EnrollmentID = c.Int(nullable: false, identity: true),
                        RegistrationNumber = c.Int(nullable: false),
                        StudentID = c.Int(nullable: false),
                        SubjectID = c.Int(nullable: false),
                        RegistrationDate = c.DateTime(nullable: false, precision: 0, storeType: "datetime2"),
                        Grade = c.Double(),
                    })
                .PrimaryKey(t => t.EnrollmentID)
                .ForeignKey("dbo.Students", t => t.StudentID, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.SubjectID, cascadeDelete: true)
                .Index(t => t.StudentID)
                .Index(t => t.SubjectID);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 16, unicode: false),
                        BirthDate = c.DateTime(nullable: false, precision: 0, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.StudentID);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        SubjectID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 16, unicode: false),
                        BirthDate = c.DateTime(nullable: false),
                        Salary = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.SubjectID)
                .ForeignKey("dbo.Subjects", t => t.SubjectID, cascadeDelete: true)
                .Index(t => t.SubjectID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teachers", "SubjectID", "dbo.Subjects");
            DropForeignKey("dbo.Enrollmens", "SubjectID", "dbo.Subjects");
            DropForeignKey("dbo.Enrollmens", "StudentID", "dbo.Students");
            DropForeignKey("dbo.Subjects", "CourseID", "dbo.Courses");
            DropIndex("dbo.Teachers", new[] { "SubjectID" });
            DropIndex("dbo.Enrollmens", new[] { "SubjectID" });
            DropIndex("dbo.Enrollmens", new[] { "StudentID" });
            DropIndex("dbo.Subjects", new[] { "CourseID" });
            DropTable("dbo.Teachers");
            DropTable("dbo.Students");
            DropTable("dbo.Enrollmens");
            DropTable("dbo.Subjects");
            DropTable("dbo.Courses");
        }
    }
}
