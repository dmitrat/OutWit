using OutWit.Common.Configuration;
using OutWit.Common.Controls.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutWit.Demo.Converters
{
    public class StringToResourceConverter : StringToResourceConverterBase
    {
        public StringToResourceConverter() :
            base(ServiceLocator.Get.Resources)
        {
        }
    }
}
