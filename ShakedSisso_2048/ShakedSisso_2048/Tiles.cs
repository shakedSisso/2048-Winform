using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakedSisso_2048
{
    public class Tiles
    {
        private int value;
        public Tiles()
        {
            this.value = 0;
        }
        public bool IsZeroValue()
        {
            return this.value == 0;
        }
        public void SetZeroValue()
        {
            this.value = 0;
        }
        public void SetFirstValue()
        {
            this.value = 2;
        }
        public void SetValue(int givenValue)
        {
            this.value = givenValue;
        }

        public int GetValue()
        {
            return this.value;
        }
        public void DoubleValue()
        {
            this.value *= 2;
        }
    }
}
