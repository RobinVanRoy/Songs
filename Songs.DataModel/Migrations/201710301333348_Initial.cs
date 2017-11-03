namespace Songs.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Albums",
                c => new
                    {
                        AlbumId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        ReleaseDate = c.DateTime(nullable: false),
                        GenreId = c.Int(),
                    })
                .PrimaryKey(t => t.AlbumId)
                .ForeignKey("dbo.Genres", t => t.GenreId)
                .Index(t => t.GenreId);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        GenreId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.GenreId);
            
            CreateTable(
                "dbo.Songs",
                c => new
                    {
                        SongId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        Duration = c.Time(nullable: false, precision: 7),
                        ReleaseDate = c.DateTime(nullable: false),
                        GenreId = c.Int(),
                    })
                .PrimaryKey(t => t.SongId)
                .ForeignKey("dbo.Genres", t => t.GenreId)
                .Index(t => t.GenreId);
            
            CreateTable(
                "dbo.Performers",
                c => new
                    {
                        PerformerId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        NickName = c.String(maxLength: 50),
                        Birthday = c.DateTime(nullable: false),
                        BirthPlace = c.String(maxLength: 50),
                        Country = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.PerformerId);
            
            CreateTable(
                "dbo.SongAlbums",
                c => new
                    {
                        Song_SongId = c.Int(nullable: false),
                        Album_AlbumId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Song_SongId, t.Album_AlbumId })
                .ForeignKey("dbo.Songs", t => t.Song_SongId, cascadeDelete: true)
                .ForeignKey("dbo.Albums", t => t.Album_AlbumId, cascadeDelete: true)
                .Index(t => t.Song_SongId)
                .Index(t => t.Album_AlbumId);
            
            CreateTable(
                "dbo.PerformerSongs",
                c => new
                    {
                        Performer_PerformerId = c.Int(nullable: false),
                        Song_SongId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Performer_PerformerId, t.Song_SongId })
                .ForeignKey("dbo.Performers", t => t.Performer_PerformerId, cascadeDelete: true)
                .ForeignKey("dbo.Songs", t => t.Song_SongId, cascadeDelete: true)
                .Index(t => t.Performer_PerformerId)
                .Index(t => t.Song_SongId);
            
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
            DropForeignKey("dbo.Albums", "GenreId", "dbo.Genres");
            DropForeignKey("dbo.Songs", "GenreId", "dbo.Genres");
            DropForeignKey("dbo.PerformerSongs", "Song_SongId", "dbo.Songs");
            DropForeignKey("dbo.PerformerSongs", "Performer_PerformerId", "dbo.Performers");
            DropForeignKey("dbo.SongAlbums", "Album_AlbumId", "dbo.Albums");
            DropForeignKey("dbo.SongAlbums", "Song_SongId", "dbo.Songs");
            DropIndex("dbo.PerformerSongs", new[] { "Song_SongId" });
            DropIndex("dbo.PerformerSongs", new[] { "Performer_PerformerId" });
            DropIndex("dbo.SongAlbums", new[] { "Album_AlbumId" });
            DropIndex("dbo.SongAlbums", new[] { "Song_SongId" });
            DropIndex("dbo.Songs", new[] { "GenreId" });
            DropIndex("dbo.Albums", new[] { "GenreId" });
            DropTable("dbo.PerformerSongs");
            DropTable("dbo.SongAlbums");
            DropTable("dbo.Performers");
            DropTable("dbo.Songs");
            DropTable("dbo.Genres");
            DropTable("dbo.Albums");
        }
    }
}
