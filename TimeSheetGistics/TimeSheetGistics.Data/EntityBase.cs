using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeSheetGistics.Data
{
    public abstract class EntityBase
    {
        protected int _id;

        public abstract int Id
        {
            get;
            set;
        }

    }
}
