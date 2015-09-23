using System;
namespace Glib.XNA
{
    public interface IObjectPool<TPooled>
     where TPooled : IResettable
    {
        TPooled GetObject();
        void ReturnObject(TPooled element);
        int Size { get; }
    }
}
