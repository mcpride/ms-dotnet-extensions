namespace MS.Extensions.IO
{
    public interface IParser
    {
    }

    public interface IParser<TIn, TOut> : IParser
    {
        TOut Parse(TIn input);
    }
}