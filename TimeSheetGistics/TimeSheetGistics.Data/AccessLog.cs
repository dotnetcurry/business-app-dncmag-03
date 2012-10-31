using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeSheetGistics.Data
{
    public class AccessLog : EntityBase
    {
        [ForeignKey("RelatedCompanyBranchUser")]
        public int CompanyBranchUserId { get; set; }
        public CompanyBranchUser RelatedCompanyBranchUser { get; set; }
        public string IpFrom { get; set; }
        public string Country { get; set; }

        public override int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }
    }
}
