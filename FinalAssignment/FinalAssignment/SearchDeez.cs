using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalAssignment
{
    internal class SearchDeez
    {
        public SearchDeez() { }

        public Anime SearchFor(CoolerArrayList<Anime> arrayList, string title)
        {
            for(int i = 0; i <= arrayList.Count(); i++)
            {
                if(title == arrayList[i].Title)
                {
                    return arrayList[i];
                }
            }
            return null;
        }
    }
}
