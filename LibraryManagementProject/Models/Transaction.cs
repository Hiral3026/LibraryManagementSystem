using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Owin.Security;

namespace LibraryManagementProject.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }

        [ForeignKey("Book")]
        public int BookId { get; set; }
        public virtual Book Book { get; set; }

        [ForeignKey("Borrower")]
        public int BorrowerId { get; set; }
        public virtual Borrower Borrower { get; set; }

        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public decimal FineAmount { get; set; }
        

   
    }
}