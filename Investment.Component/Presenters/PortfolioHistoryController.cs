using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilities;

namespace Investment.Component.Controllers
{
    public class PortfolioRecord
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }

    public class DataAccessResult<TRecord>
    {
        public bool IsSuccess { get; set; }
        public TRecord Value { get; set; }
        public Exception Exception { get; set; }
        public string Message { get; set; }

        public static DataAccessResult<TRecord> Success(TRecord value, string message = null)
        {
            return new DataAccessResult<TRecord>
            {
                Value = value,
                IsSuccess = true,
                Exception = null,
                Message = message
            };
        }

        public static DataAccessResult<TRecord> Failure(string message)
        {
            return new DataAccessResult<TRecord>
            {
                Value = default(TRecord),
                IsSuccess = false,
                Exception = null,
                Message = message
            };
        }

        public static DataAccessResult<TRecord> Error(Exception exception, string message = null)
        {
            return new DataAccessResult<TRecord>
            {
                Value = default(TRecord),
                IsSuccess = false,
                Exception = exception,
                Message = message ?? exception.Message
            };
        }
    }

    public class PortfolioRepository
    {
        private static Dictionary<int, PortfolioRecord> _mockPortfolio =
            new Dictionary<int, PortfolioRecord>
            {
                { 1, new PortfolioRecord{ Id = 1, Name = "Test" } }
            };


        public IEnumerable<PortfolioRecord> GetPortfolios()
        {
            return _mockPortfolio.Values;
        }

        public PortfolioRecord GetPortfolio(int id)
        {
            return _mockPortfolio
                .Values
                .FirstOrDefault(p => p.Id == id);
        }

        public int AddPortfolio(string name)
        {
            var index = _mockPortfolio.Keys.Max() + 1;
            _mockPortfolio.Add(index, new PortfolioRecord
            {
                Id = index,
                Name = name
            });

            return index;
        }
    }

    public enum TradeType
    {
        Unknown = 0,
        Buy = 1,
        Sell = 2
    }

    public static class EnumExtensions
    {
        public static string ToDescription(this TradeType tradeType)
        {
            switch(tradeType)
            {
                default:
                case TradeType.Unknown:
                    return "Undefined";
                case TradeType.Buy:
                    return "Buy";
                case TradeType.Sell:
                    return "Sell";
            }
        }
    }

    public class TradeLogRecord
    {
        public int PortfolioId { get; set; }
        public int TradeLogId { get; set; }
        public int SymbolId { get; set; }
        public TradeType TradeType { get; set; }
        public decimal UnitAmount { get; set; }
        public decimal Price { get; set; }
        public DateTimeOffset TransactionDateUtc { get; set; }
    }

    public class PortfolioTradeLogRepository
    {
        private static Dictionary<int, TradeLogRecord> _mockTable =
            new Dictionary<int, TradeLogRecord>
            {

            };

        public IEnumerable<TradeLogRecord> GetPortfolioTradeLog(int portfolioId)
        {
            return _mockTable
                .Values
                .Where(tlr => tlr.PortfolioId == portfolioId);
        }
    }

    public class SymbolRecord
    {
        public int SymbolId { get; set; }
        public string SymbolName { get; set; }
    }

    public class SymbolRepository
    {
        private static Dictionary<int, SymbolRecord> _mockTable =
            new Dictionary<int, SymbolRecord>
            {

            };

        public SymbolRecord GetSymbol(int symbolId)
        {
            return _mockTable
                .Values
                .FirstOrDefault(s => s.SymbolId == symbolId);
        }

        public SymbolRecord FindOrCreate(string symbolName)
        {
            SymbolRecord record;

            record = _mockTable
                .Values
                .FirstOrDefault(s => s.SymbolName == symbolName);

            if (record == null)
            {
                var index = _mockTable.Keys.Max() + 1;

                record = new SymbolRecord
                {
                    SymbolId = index,
                    SymbolName = symbolName
                };


                _mockTable.Add(index, record);
            }

            return record;
        }
    }

    public class ReportLineItem
    {
        public decimal Cost { get; set; }
        public decimal RealizedGains { get; set; }
        public decimal Quantity { get;set; }
    }

    public class ProfitsAndLossesCalculationService
    {
        public IEnumerable<ReportLineItem> GenerateReport(int portfolioId)
        {
            var result = Enumerable.Empty<ReportLineItem>();
            var tradeLogRepository = new PortfolioTradeLogRepository();

            var symbolGroups = tradeLogRepository
                .GetPortfolioTradeLog(portfolioId)
                .GroupBy(t => t.SymbolId);
                
            var lineItems = new ConcurrentDictionary<int, ReportLineItem>();

            Parallel.ForEach(symbolGroups, group =>
            {
                decimal realizedGains = 0;
                decimal unrelizedGains = 0;

                decimal unitsOnHand = 0;
                decimal cost = 0;

                foreach(var trade in group.OrderBy(t => t.TransactionDateUtc))
                {
                    if(trade.TradeType == TradeType.Buy)
                    {
                        unitsOnHand += trade.UnitAmount;
                        cost += trade.UnitAmount * trade.Price;
                    }
                    else if(trade.TradeType == TradeType.Sell)
                    {
                        unitsOnHand -= trade.UnitAmount;
                        realizedGains += trade.UnitAmount * trade.Price;
                    }
                }


            });

            return result;
        }
    }

    public class ProfitsAndLossesPresenter
    {

    }

    public class PortfolioTransactionModel
    {
        public string SymbolName { get; set; }
        public string Operation { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal PurchaseAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTimeOffset TradeDate { get; set; }
    }

    public interface IPortfolioHistoryView
    {
        void SetTransactionHistory(IEnumerable<PortfolioTransactionModel> transactions);
    }

    public class PortfolioHistoryPresenter
    {
        private IPortfolioHistoryView _view;

        public PortfolioHistoryPresenter()
        {
        }

        public void Initialize(IPortfolioHistoryView view)
        {
            _view = view;
        }

        public void ChangeActivePortfolio(int? portfolioId)
        {
            if (!portfolioId.HasValue)
            {
                ClearActivePortfolio();
                return;
            }

            UpdateTransactionHistory(portfolioId.Value);
        }

        private void UpdateTransactionHistory(int portfolioId)
        {
            var portfolioRepository = new PortfolioRepository();
            var tradeRepository = new PortfolioTradeLogRepository();

            var trades = tradeRepository
                .GetPortfolioTradeLog(portfolioId)
                .OrderByDescending(t => t.TransactionDateUtc)
                .Select(SelectTransactionModel);

            _view?.SetTransactionHistory(trades);
        }

        private PortfolioTransactionModel SelectTransactionModel(TradeLogRecord trade)
        {
            var symbolRepository = new SymbolRepository();
            var symbol = symbolRepository.GetSymbol(trade.SymbolId);

            return new PortfolioTransactionModel
            {
                Operation = trade.TradeType.ToDescription(),
                PurchaseAmount = trade.UnitAmount,
                PurchasePrice = trade.Price,
                SymbolName = symbol?.SymbolName ?? "Undefined",
                TradeDate = trade.TransactionDateUtc
            };
        }

        private void ClearActivePortfolio()
        {
            _view.SetTransactionHistory(Enumerable.Empty<PortfolioTransactionModel>());
        }
    }
}

