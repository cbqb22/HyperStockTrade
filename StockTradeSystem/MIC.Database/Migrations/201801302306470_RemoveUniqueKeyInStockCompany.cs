namespace MIC.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveUniqueKeyInStockCompany : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.StockCompanies", "StockCompanyIndex");
            CreateIndex("dbo.StockCompanies", new[] { "StockCode", "MarketCode" }, name: "StockCompanyIndex");
        }
        
        public override void Down()
        {
            DropIndex("dbo.StockCompanies", "StockCompanyIndex");
            CreateIndex("dbo.StockCompanies", new[] { "StockCode", "MarketCode" }, unique: true, name: "StockCompanyIndex");
        }
    }
}
