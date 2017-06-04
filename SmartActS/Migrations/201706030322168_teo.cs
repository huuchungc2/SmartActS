namespace SmartActS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class teo : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UserRoles", newName: "UserRole");
            RenameTable(name: "dbo.Users", newName: "User");
            RenameTable(name: "dbo.UserLogins", newName: "UserLogin");
            RenameColumn(table: "dbo.Roles", name: "Id", newName: "RoleId");
            RenameColumn(table: "dbo.User", name: "Id", newName: "UserId");
            RenameColumn(table: "dbo.UserClaims", name: "Id", newName: "UserClaimId");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.UserClaims", name: "UserClaimId", newName: "Id");
            RenameColumn(table: "dbo.User", name: "UserId", newName: "Id");
            RenameColumn(table: "dbo.Roles", name: "RoleId", newName: "Id");
            RenameTable(name: "dbo.UserLogin", newName: "UserLogins");
            RenameTable(name: "dbo.User", newName: "Users");
            RenameTable(name: "dbo.UserRole", newName: "UserRoles");
        }
    }
}
