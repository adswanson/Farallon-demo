namespace Investment.Component.Domain.Symbols
{
    public interface ISymbolRepository
    {
        SymbolRecord GetSymbol(int symbolId);
    }
}