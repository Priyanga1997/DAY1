namespace MVCexample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InsertDetails : DbMigration
    {
        public override void Up()
        {
            Sql("Update Movies set AvailableStock=1 where Id=7");
            Sql("Update Movies set AvailableStock=2 where Id=8");
        }
        
        public override void Down()
        {
           
        }
    }
}
