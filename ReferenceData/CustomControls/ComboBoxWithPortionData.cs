using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ReferenceData.CustomControls
{
    public class ComboBoxWithPortionData : ComboBox
    {
        int countOfRecords;

        public ComboBoxWithPortionData()
        {
            SetResourceReference(StyleProperty, typeof(ComboBox));
        }

        //public override 

    }
}
