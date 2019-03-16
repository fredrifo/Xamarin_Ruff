using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAB330_Scruff
{

    public class MDPMenuItem
    {
        public MDPMenuItem()
        {
            TargetType = typeof(MDPDetail);
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public Type TargetType { get; set; }
    }
}