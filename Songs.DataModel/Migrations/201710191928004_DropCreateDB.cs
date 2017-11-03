namespace Songs.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropCreateDB : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure(
                "dbo.Album_Insert",
                p => new
                    {
                        Title = p.String(maxLength: 50),
                        ReleaseDate = p.DateTime(),
                        GenreId = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[Albums]([Title], [ReleaseDate], [GenreId])
                      VALUES (@Title, @ReleaseDate, @GenreId)
                      
                      DECLARE @AlbumId int
                      SELECT @AlbumId = [AlbumId]
                      FROM [dbo].[Albums]
                      WHERE @@ROWCOUNT > 0 AND [AlbumId] = scope_identity()
                      
                      SELECT t0.[AlbumId]
                      FROM [dbo].[Albums] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[AlbumId] = @AlbumId"
            );
            
            CreateStoredProcedure(
                "dbo.Album_Update",
                p => new
                    {
                        AlbumId = p.Int(),
                        Title = p.String(maxLength: 50),
                        ReleaseDate = p.DateTime(),
                        GenreId = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[Albums]
                      SET [Title] = @Title, [ReleaseDate] = @ReleaseDate, [GenreId] = @GenreId
                      WHERE ([AlbumId] = @AlbumId)"
            );
            
            CreateStoredProcedure(
                "dbo.Album_Delete",
                p => new
                    {
                        AlbumId = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Albums]
                      WHERE ([AlbumId] = @AlbumId)"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.Album_Delete");
            DropStoredProcedure("dbo.Album_Update");
            DropStoredProcedure("dbo.Album_Insert");
        }
    }
}
