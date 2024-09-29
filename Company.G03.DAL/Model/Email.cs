using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G03.DAL.Model
{
   public class Email
    {
		public string To { get; set; }
		public string Subject { get; set; }
		public string Body { get; set; }
	}
}
