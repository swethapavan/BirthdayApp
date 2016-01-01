using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayApp
{
    public class BirthdayComparer : IComparer<BirthdayDetails>
    {
        public int Compare(BirthdayDetails x, BirthdayDetails y)
        {
            if (x.Month == y.Month){

                if(x.Day == y.Day){
                    return 0;
                }
                if(x.Day < y.Day){
                    return -1;
                }
                if (x.Day > y.Day)
                {
                    return 1;
                }
            }
            if (x.Month < y.Month){
                return -1;
            }

            return 1;

       
        }
    }
}
