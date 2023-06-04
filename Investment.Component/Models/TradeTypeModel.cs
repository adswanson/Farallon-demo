using Investment.Component.Domains.Trading;

namespace Investment.Component.Models
{
    public enum TradeTypeModel
    {
        Unknown = 0,
        Buy = 1,
        Sell = 2,
    }

    public static class TransactionTypeModelExtensions
    {
        public static TradeTypeModel ToTransactionType(this TradeType tradeType)
        {
            switch (tradeType)
            {
                default:
                case TradeType.Unknown:
                    return TradeTypeModel.Unknown;
                case TradeType.Buy:
                    return TradeTypeModel.Buy;
                case TradeType.Sell:
                    return TradeTypeModel.Sell;
            }
        }
    }
}
