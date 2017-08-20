namespace MIC.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DailyPrices",
                c => new
                    {
                        DailyPriceId = c.Int(nullable: false, identity: true),
                        StockCompanyId = c.Int(nullable: false),
                        DealDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        OpeningPrice = c.Double(),
                        HighPrice = c.Double(),
                        LowPrice = c.Double(),
                        ClosingPrice = c.Double(),
                        Volume = c.Double(nullable: false),
                        Turnover = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.DailyPriceId)
                .ForeignKey("dbo.StockCompanies", t => t.StockCompanyId, cascadeDelete: true)
                .Index(t => t.StockCompanyId);
            
            CreateTable(
                "dbo.StockCompanies",
                c => new
                    {
                        StockCompanyId = c.Int(nullable: false, identity: true),
                        StockCode = c.String(nullable: false, maxLength: 10),
                        MarketCode = c.Byte(nullable: false),
                        CompanyName = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.StockCompanyId)
                .Index(t => new { t.StockCode, t.MarketCode }, unique: true, name: "StockCompanyIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DailyPrices", "StockCompanyId", "dbo.StockCompanies");
            DropIndex("dbo.StockCompanies", "StockCompanyIndex");
            DropIndex("dbo.DailyPrices", new[] { "StockCompanyId" });
            DropTable("dbo.StockCompanies");
            DropTable("dbo.DailyPrices");
        }
    }
}
