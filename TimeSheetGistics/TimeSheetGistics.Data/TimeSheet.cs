using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeSheetGistics.Data
{
    public class TimeSheet : EntityBase
    {
        private DateTime _startDate;
        private DateTime _endDate;
        private Project _parentProject;
        private List<TimeCard> _timeCards;

        public IQueryable<TimeCard> TimeCardsQuery
        {
            get { return _timeCards.AsQueryable<TimeCard>(); }
        }

        public List<TimeCard> TimeCards
        {
            get { return _timeCards; }
            set { _timeCards = value; }
        }
        
        public TimeSheet()
        {
            StartDate = DateTime.MinValue;
            EndDate = DateTime.MinValue;
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

        [ForeignKey("ParentProject")]
        public int ProjectId { get; set; }

        public Project ParentProject
        {
            get { return _parentProject; }
            set { _parentProject = value; }
        }

        public CompanyBranchUser TimeSheetBy { get; set; }

        public CompanyBranchUser TimeSheetApprover { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

        [DataType(DataType.Date)]
        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

    }
}
