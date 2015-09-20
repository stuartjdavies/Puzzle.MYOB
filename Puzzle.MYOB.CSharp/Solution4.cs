using System;
using System.Linq;

namespace Puzzle.MYOB.CSharp
{
    public class EliminatorSol4<T>
    {       
        public static T[] getOrderOfRemovals(int n, T[] items)
        {
            if (items.Length < 1 || n < 1) return new T[] { };

            Func<T, T[], T[]> join = (l, r) => new[] { l }.Concat(r).ToArray(); 
            Func<T[]> Empty = () => new T[] { };

            var src = (T[]) items.Clone();
            var acc = Empty();

            while (src.Length > 0)
            {
                int index = 0;
               
                for (var i = 0; i < n; i++) index = (index + 1 >= src.Length) ? 0 : index + 1; 
                
                if (src.Length <= 1) { acc = join(src[index], acc); src = Empty(); }
                else
                { 
                  if (index == 0) { acc = join(src[index], acc); src = src.Skip(index + 1).ToArray(); }
                  else { acc = join(src[index],acc); src = src.Skip(index + 1).Concat<T>(src.Take(index)).ToArray(); }
                }
            }

            return acc;
        }
    }
}
