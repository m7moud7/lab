using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labyrinth.Model;

namespace Labyrinth.Data
{
    public partial class Order
    {
        public static Order Clone(OrderVM OrderVM)
        {
            return new Order()
            {
                ID = OrderVM.ID,
                Type = OrderVM.Type,
                NewsID = OrderVM.NewsID,
                SecID = OrderVM.SecID,
                Index = OrderVM.Index
            };
        }

        public static Order AntherClone(OrderVM OrderVM)
        {
            return new Order()
            {
                ID = OrderVM.ID,
                Type = OrderVM.Type,
                NewsID = OrderVM.NewsID,
                SecID = OrderVM.SecID,
                Index = OrderVM.Index
            };
        }
    }
}
