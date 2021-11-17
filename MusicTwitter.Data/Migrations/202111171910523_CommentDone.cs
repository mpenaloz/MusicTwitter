namespace MusicTwitter.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommentDone : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        OwnerId = c.Guid(nullable: false),
                        Content = c.String(),
                        SongPostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.SongPost", t => t.SongPostId, cascadeDelete: true)
                .Index(t => t.SongPostId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comment", "SongPostId", "dbo.SongPost");
            DropIndex("dbo.Comment", new[] { "SongPostId" });
            DropTable("dbo.Comment");
        }
    }
}
