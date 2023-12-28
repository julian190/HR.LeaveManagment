using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HR.LeaveManagment.MVC.Models
{
    public class LeaveTypeVM:CreateLeaveTypeVM
    {
        public int Id { get; set; }
    }
    public class CreateLeaveTypeVM
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Deafult Number Of Days")]
        public int DefaultDays { get; set; }
    }
}
