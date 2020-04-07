namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'd39d393f-fd13-4f67-a4d8-8c54e5e6dcea', N'admin@vidly.com', 0, N'ADOMO9bdvY6G+EdyrXvfwk/0iy/wtejH8ZbGGNld1LzoH1lktlLlhhkbnKf+ZSh0ug==', N'ff7a954b-1182-48b7-82c0-f83f6cf0e41d', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'b621dd7e-c566-4e62-ad91-597f32052a47', N'CanManageMovies')
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd39d393f-fd13-4f67-a4d8-8c54e5e6dcea', N'b621dd7e-c566-4e62-ad91-597f32052a47')
            ");
        }
        
        public override void Down()
        {
        }
    }
}
