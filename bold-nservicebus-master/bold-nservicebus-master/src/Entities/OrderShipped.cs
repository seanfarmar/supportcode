using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OrderShipped
    {
        public virtual int Id { get; set; }
        public virtual Guid OrderId { get; set; }
        public virtual DateTime ShippingDate { get; set; }
    }
}
