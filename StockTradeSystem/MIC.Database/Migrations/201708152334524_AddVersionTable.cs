namespace MIC.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVersionTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo._Version",
                c => new
                    {
                        _VersionId = c.Int(nullable: false, identity: true),
                        Sequence = c.String(nullable: false, maxLength: 30),
                        FileName = c.String(nullable: false, maxLength: 30),
                        Version = c.String(nullable: false, maxLength: 30),
                        UpdateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t._VersionId)
                .Index(t => t.Sequence, unique: true, name: "_VersionIndex");
            
        }
        
        public override void Down()
        {
            DropIndex("dbo._Version", "_VersionIndex");
            DropTable("dbo._Version");
        }
    }
}
