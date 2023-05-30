using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using SchoolFeeding.Model.Entities;

namespace SchoolFeeding.Model.Utilities
{
    public class DataGridTemplate : DataTemplateSelector
    {
        public DataTemplate StudentTemplate { get; set; }
        public DataTemplate PaymnetsTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is Student)
            {
                return StudentTemplate;
            }
            else if (item is Payment)
            {
                return PaymnetsTemplate;
            }
            else
            {
                return StudentTemplate;
            }
        }
    }
}
