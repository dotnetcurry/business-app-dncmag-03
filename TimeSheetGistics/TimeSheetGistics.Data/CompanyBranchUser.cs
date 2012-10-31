using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeSheetGistics.Data
{
    public class CompanyBranchUser : EntityBase
    {
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
        [ForeignKey("ParentBranch")]
        public int BranchId { get; set; }

        public Branch ParentBranch { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string GravatarUrl { get; set; }
    }
}
