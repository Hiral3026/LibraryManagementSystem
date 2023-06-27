using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementProject.Models
{
    public class Borrower
    {
        [Key]
        public int BorrowerId { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public int PhoneNo { get; set; }
    }
}