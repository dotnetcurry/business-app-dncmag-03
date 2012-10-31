using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeSheetGistics.Data
{
    public class Project : EntityBase
    {
        private string _name;
        private DateTime _startDate;
        private DateTime _endDate;
        private string _referenceNumber;

        private Branch _parentBranch;
        private int _branchId;

        [ForeignKey("ParentBranch")]
        public int BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }
        
        public Branch ParentBranch
        {
            get { return _parentBranch; }
            set { _parentBranch = value; }
        }
        

        [Key]
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


        public string ReferenceNumber
        {
            get { return _referenceNumber; }
            set { _referenceNumber = value; }
        }
        

        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }
        

        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }
        

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        
    }
}
