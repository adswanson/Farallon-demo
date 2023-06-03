using System.Collections.Generic;
using System.Linq;

namespace Investment.Component.Domain.Symbols
{
    public class XmlFileSymbolRepository : ISymbolRepository
    {
        private static Dictionary<int, SymbolRecord> _mockTable =
            new Dictionary<int, SymbolRecord>
            {
                { 0, new SymbolRecord { SymbolId = 0, SymbolName = "AAPL"} }
            };

        public SymbolRecord GetSymbol(int symbolId)
        {
            return _mockTable
                .Values
                .FirstOrDefault(s => s.SymbolId == symbolId);
        }
    }
}
