namespace MVCexample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateMembership : DbMigration
    {
        public override void Up()
        {
            Sql("Insert MembershipTypes(Type,Duration,SignUpFee,Discount)values('Yearly',12,1200,20)");
            Sql("Insert MembershipTypes(Type,Duration,SignUpFee,Discount)values('Half-Yearly',6,600,15)");
            Sql("Insert MembershipTypes(Type,Duration,SignUpFee,Discount)values('Quarterly',3,300,10)");
        }
        
        public override void Down()
        {
        }
    }
}
