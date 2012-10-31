using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeSheetGistics.Data
{
    public class TimeCard : EntityBase
    {
        private string _taskTitle;
        private string _taskDetails;
        private TimeSheet _parentTimeSheet;
        private int _hours;

        public int Hours
        {
            get { return _hours; }
            set { _hours = value; }
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

        private int _timeSheetId;

        [ForeignKey("ParentTimeSheet")]
        public int TimeSheetId
        {
            get { return _timeSheetId; }
            set { _timeSheetId = value; }
        }


        public TimeSheet ParentTimeSheet
        {
            get { return _parentTimeSheet; }
            set { _parentTimeSheet = value; }
        }

        public string TaskDetails
        {
            get { return _taskDetails; }
            set { _taskDetails = value; }
        }

        public string Title
        {
            get { return _taskTitle; }
            set { _taskTitle = value; }
        }

        public DateTime CardDate { get; set; }
    }
}
