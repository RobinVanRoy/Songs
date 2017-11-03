namespace Songs.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Annotations : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.SongPerformers", newName: "PerformerSongs");
            RenameColumn(table: "dbo.SongAlbums", name: "SongId", newName: "Song_SongId");
            RenameColumn(table: "dbo.SongAlbums", name: "AlbumId", newName: "Album_AlbumId");
            RenameColumn(table: "dbo.PerformerSongs", name: "SongId", newName: "Song_SongId");
            RenameColumn(table: "dbo.PerformerSongs", name: "PerformerId", newName: "Performer_PerformerId");
            RenameIndex(table: "dbo.SongAlbums", name: "IX_SongId", newName: "IX_Song_SongId");
            RenameIndex(table: "dbo.SongAlbums", name: "IX_AlbumId", newName: "IX_Album_AlbumId");
            RenameIndex(table: "dbo.PerformerSongs", name: "IX_PerformerId", newName: "IX_Performer_PerformerId");
            RenameIndex(table: "dbo.PerformerSongs", name: "IX_SongId", newName: "IX_Song_SongId");
            DropPrimaryKey("dbo.PerformerSongs");
            AlterColumn("dbo.Albums", "ReleaseDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Songs", "Duration", c => c.Time(nullable: false, precision: 7));
            AlterColumn("dbo.Songs", "ReleaseDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Performers", "Birthday", c => c.DateTime(nullable: false));
            AddPrimaryKey("dbo.PerformerSongs", new[] { "Performer_PerformerId", "Song_SongId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.PerformerSongs");
            AlterColumn("dbo.Performers", "Birthday", c => c.DateTime());
            AlterColumn("dbo.Songs", "ReleaseDate", c => c.DateTime());
            AlterColumn("dbo.Songs", "Duration", c => c.Time(precision: 7));
            AlterColumn("dbo.Albums", "ReleaseDate", c => c.DateTime());
            AddPrimaryKey("dbo.PerformerSongs", new[] { "SongId", "PerformerId" });
            RenameIndex(table: "dbo.PerformerSongs", name: "IX_Song_SongId", newName: "IX_SongId");
            RenameIndex(table: "dbo.PerformerSongs", name: "IX_Performer_PerformerId", newName: "IX_PerformerId");
            RenameIndex(table: "dbo.SongAlbums", name: "IX_Album_AlbumId", newName: "IX_AlbumId");
            RenameIndex(table: "dbo.SongAlbums", name: "IX_Song_SongId", newName: "IX_SongId");
            RenameColumn(table: "dbo.PerformerSongs", name: "Performer_PerformerId", newName: "PerformerId");
            RenameColumn(table: "dbo.PerformerSongs", name: "Song_SongId", newName: "SongId");
            RenameColumn(table: "dbo.SongAlbums", name: "Album_AlbumId", newName: "AlbumId");
            RenameColumn(table: "dbo.SongAlbums", name: "Song_SongId", newName: "SongId");
            RenameTable(name: "dbo.PerformerSongs", newName: "SongPerformers");
        }
    }
}
