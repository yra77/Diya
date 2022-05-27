
using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;


namespace MyApp.Controls
{
    class Selector_Documents_DataTemplate : DataTemplateSelector
    {

        public DataTemplate Page0 { get; set; }
        public DataTemplate Page1 { get; set; }
        public DataTemplate Page2 { get; set; }
        public DataTemplate Page3 { get; set; }
        public DataTemplate Page4 { get; set; }


        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            
            switch (((Models.Documents)item).NumberOfPage)
            {
                case 0:
                    return Page0;
                case 1:
                    return Page1;
                case 2:
                    return Page2;
                case 3:
                    return Page3;
                case 4:
                    return Page4;
                default:
                    return Page4;

            }
        }

    }
}
