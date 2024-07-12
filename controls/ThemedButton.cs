using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pctv.controls
{
    public class ThemedButton : Button
    {
        public ThemedButton()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;

            this.BackColor = Color.LightGray;
        }
    }
}
