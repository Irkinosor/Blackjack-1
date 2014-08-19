using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Limits
{
    using Blackjack.Abstractions;

    public class SmallStakes : IBetLimit
    {
        public SmallStakes()
        {
            this.Min = 1;
            this.Max = 30;
        }

        public decimal Min { get; private set; }

        public decimal Max { get; private set; }
    }
}
