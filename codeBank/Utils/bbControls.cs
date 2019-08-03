using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codeBank.Utils
{
    public class bbItems
    {
        public int Value { get; set; }
        public string  Text { get; set; }

        public bbItems(string _text, int _val) {
            this.Text = _text;
            this.Value = _val;
        }
        public bbItems() {

        }

        public override string ToString() { return this.Text; }
    }
}
