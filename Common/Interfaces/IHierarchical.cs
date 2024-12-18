namespace Common.Interfaces
{
    public interface IHierarchical<T> : IKeyable
    {
        T Parent { get; }        
    }
}
