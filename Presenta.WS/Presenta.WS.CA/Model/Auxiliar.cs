using Presenta.Common.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presenta.WS.CA.Model
{
    public static class Auxiliar
    {
        public static string GetFullDescription(this Enum _enum)
        {
            return String.Format("({0}) {1}", _enum.ToInt().ToString(), _enum.GetDescription());
        }

        public static string GetTextDescription(this Enum _enum)
        {
            return String.Format("{0}", _enum.GetDescription());
        }
    }
}