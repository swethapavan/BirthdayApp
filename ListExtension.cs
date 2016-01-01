using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayApp
{
    public static class ListExtension
    {

        public static List<T> BinarySearchMultipleMatches<T>(this List<T> items, T item, IComparer<T> comparer)
        {
           
            List<T> matchedItems = new List<T>();
            var matchedItemIndex = items.BinarySearch(item, comparer);

            if (matchedItemIndex < 0)
            {
                return matchedItems;
            }

            for(int i = matchedItemIndex; i >= 0; i--){
                if(comparer.Compare(items[i], items[matchedItemIndex]) == 0){

                    matchedItems.Add(items[i]);
                }
                else
                    break;
            }
            for(int i = matchedItemIndex + 1; i< items.Count ;i++){
                if(comparer.Compare(items[i], items[matchedItemIndex]) == 0){

                    matchedItems.Add(items[i]);
                }
                else
                    break;
            }

            return matchedItems;
        }
    }
}
