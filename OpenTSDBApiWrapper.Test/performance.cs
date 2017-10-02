using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTSDBApiWrapper.CustomAttributes;

namespace OpenTSDBApiWrapper.Test
{
    public class PerformanceStorageData
    {
        [IsMetricValueArray]
        public StatsData StatisticData { get; set; }

        [IsMetricTime]
        public DateTime Date { get; set; }

        [IsMetricName(1)]
        public string CustomerName { get; set; }

        [IsMetricName(2)]
        public string ShopName { get; set; }

        [IsMetricTag("TopCategory")]
        public string TopCategory { get; set; }

        [IsMetricTag("MidCategory")]
        public string MidCategory { get; set; }

        [IsMetricTag("LowCategory")]
        public string LowCategory { get; set; }

        [IsMetricTag("Brand")]
        public string Brand { get; set; }

        [IsMetricTag("Product")]
        public string ArticleId { get; set; }

        [IsMetricTag("Channel")]
        public string ChannelName { get; set; }



    }
    public class StatsData
    {
        /// <summary>
        /// Categorypath for grouping data (old functionality)
        /// </summary>
        [Obsolete("Categorypath was used in the old performance data extracting and will be deleted when AWS is operational")]
        public string categorypath { get; set; }

        /// <summary>
        /// Number of product sold
        /// </summary>
        [IsMetricValue]
        [IsMetricName("AmmountSold")]
        public int amountsold { get; set; }

        /// <summary>
        /// Number of product sold online
        /// </summary>
        [IsMetricValue]
        [IsMetricName("AmmountSoldOnline")]
        public int amountsold_online { get; set; }

        /// <summary>
        /// Number of clicks registered
        /// </summary>
        [IsMetricValue]
        [IsMetricName("Clicks")]
        public int clicks { get; set; }

        /// <summary>
        /// Price ratio of customer sales price relative to the average sales price of the competitors
        /// </summary>
        [IsMetricValue]
        [IsMetricName("PriceRatio")]
        public double price_ratio { get; set; }

        /// <summary>
        /// Priceratio weight is set to 0 if price ratio is NaN else 1
        /// </summary>

        [IsMetricValue]
        [IsMetricName("PriceRatioWeight")]
        public int price_ratio_weight { get; set; }

        /// <summary>
        /// Sum marketing spend of all source_mediums
        /// </summary>
        [IsMetricValue]
        [IsMetricName("TotalMarketingSpend")]
        public double total_maketing_spend { get; set; }

        /// <summary>
        /// Unique pageviews for a product
        /// </summary>

        [IsMetricValue]
        [IsMetricName("UniqueProductPageViews")]
        public int unique_product_pageviews { get; set; }

        /// <summary>
        /// Number of omnichannel orders (is not used)
        /// </summary>
        [IsMetricValue]
        [IsMetricName("OmniChannelOrders")]
        public double omnichannel_orders { get; set; }

        /// <summary>
        /// Number of online orders (is not used)
        /// </summary>
        [IsMetricValue]
        [IsMetricName("OnlineOrders")]
        public double online_orders { get; set; }

        /// <summary>
        /// A calculated field for the average shipping revenues per order.
        /// </summary>

        [IsMetricValue]
        [IsMetricName("AvgShippingRevenuesPerOrder")]
        public double avg_shipping_revenues_per_order { get; set; }

        /// <summary>
        /// Omnichannel sales with the calculated shop_avg_shipping_revenues_per_order
        /// the shop_avg_shipping_revenues_per_order is set in SetShopPerformanceData()
        /// </summary>
        [IsMetricValue]
        [IsMetricName("OmniChannelSalesInclShippingRevenues")]
        public double omnichannel_sales_incl_shipping_revenues { get; set; }

        /// <summary>
        /// Online sales with the calculated shop_avg_shipping_revenues_per_order
        /// the shop_avg_shipping_revenues_per_order is set in SetShopPerformanceData()
        /// </summary>
        [IsMetricValue]
        [IsMetricName("OnlineSalesInclShippingRevenues")]
        public double online_sales_incl_shipping_revenues { get; set; }

        /// <summary>
        /// Amountsold_online * (price ex vat - purchase price)
        /// Price is incl vat and purchase price is ex vat
        /// GrossMargin is a function in the Formulas
        /// </summary>
        [IsMetricValue]
        [IsMetricName("OnlineGrossMargin")]
        public double online_gross_margin { get; set; }

        /// <summary>
        /// Aamountsold * (price ex vat - purchase price)
        /// Price is feeded incl vat and purchase price is feeded ex vat
        /// GrossMargin is a function in the Formulas
        /// </summary>
        [IsMetricValue]
        [IsMetricName("OmniChannelGrossMargin")]
        public double omnichannel_gross_margin { get; set; }

        /// <summary>
        /// Omnichannel_gross_margin + marketing costs + logistical cost
        /// </summary>
        [IsMetricValue]
        [IsMetricName("OmniChannelNetContributionMargin")]
        public double omnichannel_net_contribution_margin { get; set; }

        /// <summary>
        /// Amountsold * price ex vat
        /// </summary>
        [IsMetricValue]
        [IsMetricName("OmniChannelSales")]
        public double omnichannel_sales { get; set; }

        /// <summary>
        /// Online_gross_margin + marketing costs + logistical cost
        /// </summary>
        [IsMetricValue]
        [IsMetricName("OnlineNetContributtionMargin")]
        public double online_net_contribution_margin { get; set; }

        /// <summary>
        /// Amountsold_online * price ex vat
        /// </summary>
        [IsMetricValue]
        [IsMetricName("OnlineSales")]
        public double online_sales { get; set; }

        /// <summary>
        /// Sales price ex vat - purchase price + logistical costs
        /// </summary>
        [IsMetricValue]
        [IsMetricName("Margin")]
        public double margin { get; set; }

        /// <summary>
        /// Represents the sales price with the shipping costs included
        /// </summary>
        [IsMetricValue]
        [IsMetricName("SalesPriceInclShippingCost")]
        public double sales_price_incl_shippingcost { get; set; }

        public bool IsPaid { get; set; }
    }

}

