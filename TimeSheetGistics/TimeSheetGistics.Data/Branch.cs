using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeSheetGistics.Data
{
    public class Branch : EntityBase
    {
        private Company _parentCompany;
        private string _branchName;
        private string _branchDescription;
        private Location _location;
        private List<Project> _projects;

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


        public string Description
        {
            get { return _branchDescription; }
            set { _branchDescription = value; }
        }

        [Required]
        public string Name
        {
            get { return _branchName; }
            set { _branchName = value; }
        }

        private int _locationId;

        [ForeignKey("Location")]
        public int LocationId
        {
            get { return _locationId; }
            set { _locationId = value; }
        }
        
        public Location Location
        {
            get { return _location; }
            set { _location = value; }
        }

        private int _companyId;

        [ForeignKey("ParentCompany")]
        public int CompanyId
        {
            get { return _companyId; }
            set { _companyId = value; }
        }
        
        public Company ParentCompany
        {
            get { return _parentCompany; }
            set { _parentCompany = value; }
        }

    }
}
