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
                        ReleaseDate = c.DateTime(),
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
                        Duration = c.Time(precision: 7),
                        ReleaseDate = c.DateTime(),
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
                        Birthday = c.DateTime(),
                        BirthPlace = c.String(maxLength: 50),
                        Country = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.PerformerId);
            
            CreateTable(
                "dbo.SongAlbums",
                c => new
                    {
                        SongId = c.Int(nullable: false),
                        AlbumId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SongId, t.AlbumId })
                .ForeignKey("dbo.Songs", t => t.SongId, cascadeDelete: true)
                .ForeignKey("dbo.Albums", t => t.AlbumId, cascadeDelete: true)
                .Index(t => t.SongId)
                .Index(t => t.AlbumId);
            
            CreateTable(
                "dbo.SongPerformers",
                c => new
                    {
                        SongId = c.Int(nullable: false),
                        PerformerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SongId, t.PerformerId })
                .ForeignKey("dbo.Songs", t => t.SongId, cascadeDelete: true)
                .ForeignKey("dbo.Performers", t => t.PerformerId, cascadeDelete: true)
                .Index(t => t.SongId)
                .Index(t => t.PerformerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Albums", "GenreId", "dbo.Genres");
            DropForeignKey("dbo.Songs", "GenreId", "dbo.Genres");
            DropForeignKey("dbo.SongPerformers", "PerformerId", "dbo.Performers");
            DropForeignKey("dbo.SongPerformers", "SongId", "dbo.Songs");
            DropForeignKey("dbo.SongAlbums", "AlbumId", "dbo.Albums");
            DropForeignKey("dbo.SongAlbums", "SongId", "dbo.Songs");
            DropIndex("dbo.SongPerformers", new[] { "PerformerId" });
            DropIndex("dbo.SongPerformers", new[] { "SongId" });
            DropIndex("dbo.SongAlbums", new[] { "AlbumId" });
            DropIndex("dbo.SongAlbums", new[] { "SongId" });
            DropIndex("dbo.Songs", new[] { "GenreId" });
            DropIndex("dbo.Albums", new[] { "GenreId" });
            DropTable("dbo.SongPerformers");
            DropTable("dbo.SongAlbums");
            DropTable("dbo.Performers");
            DropTable("dbo.Songs");
            DropTable("dbo.Genres");
            DropTable("dbo.Albums");
        }
    }
}
