using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyShoppingWeb.Models
{
    public class UserRoles
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }

        [ForeignKey("UserId")]
        public virtual TblUsers TblUsers { get; set; }

        [ForeignKey("RoleId")]
        public virtual Roles Roles { get; set; }
    }
}