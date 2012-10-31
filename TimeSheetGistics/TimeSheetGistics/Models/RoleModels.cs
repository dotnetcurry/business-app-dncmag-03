using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TimeSheetGistics.Models
{

    public class RoleModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }

        [Key]
        public string RoleId { get; set; }

    }


    public class UserRolesModel
    {
        private List<RoleModel> _roles = new List<RoleModel>();

        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Display(Name = "Roles")]
        public List<RoleModel> Roles
        {
            get
            {
                return _roles;
            }
            private set
            {
                _roles = value;
            }
        }
    }

}