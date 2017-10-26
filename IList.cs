using System;

namespace Svetomech.Collections.Generic
{
    interface IList<T>
    {
        void Add(T item);

        void Remove(T item);

        void Clear();
    }
}