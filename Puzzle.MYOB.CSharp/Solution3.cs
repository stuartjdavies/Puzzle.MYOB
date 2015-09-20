using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle.MYOB.CSharp
{   
    public class EliminatorSol3<T>
    {   
        private static int getElimIndex(int counter, int pointer, int length) 
        {
            return (counter == 1) ? pointer : (pointer + 1 >= length) ? getElimIndex(counter - 1, 0, length) 
                                                                      : getElimIndex(counter - 1, pointer + 1, length);
        }
        
        public static T[] getOrderOfRemovals(int n, T[] items)
        {
            if (items.Length < 1 || n < 1) return new T[] { };
            
            var src = (T[]) items.Clone();
            var acc = new T[] { };

            while (src.Length > 0)
            {
                var index = getElimIndex(n, 0, src.Length);                                        

                var tp = (src.Length <= 1) ? new Tuple<T, T[]>(src[index], new T[] { }) :
                                               (index == 0) ? new Tuple<T, T[]>(src[index], src.Skip(index + 1).ToArray()) :
                                                              new Tuple<T, T[]>(src[index], src.Skip(index + 1).Concat(src.Take(index)).ToArray());  

                acc = new[] { tp.Item1 }.Concat(acc).ToArray();                
                src = tp.Item2;
            }

            return acc;  
        } 
    }
}
