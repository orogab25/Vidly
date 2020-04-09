namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserRoles : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'b622dd7e-c566-4e62-ad91-597f32052a47', N'CanManageCustomers')
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd39d393f-fd13-4f67-a4d8-8c54e5e6dcea', N'b622dd7e-c566-4e62-ad91-597f32052a47')
            ");
        }
        
        public override void Down()
        {
        }
    }
}
