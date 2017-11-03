namespace Songs.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRepository : DbMigration
    {
        public override void Up()
        {
            DropStoredProcedure("dbo.Album_Insert");
            DropStoredProcedure("dbo.Album_Update");
            DropStoredProcedure("dbo.Album_Delete");
        }
        
        public override void Down()
        {
            throw new NotSupportedException("Scaffolding create or alter procedure operations is not supported in down methods.");
        }
    }
}
