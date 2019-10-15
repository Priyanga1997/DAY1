namespace MVCexample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyMembership1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MembershipTypes", "SignUpFee", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MembershipTypes", "SignUpFee");
        }
    }
}
