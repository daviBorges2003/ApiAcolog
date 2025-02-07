using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AcologAPI.src.Domain.ModelViews
{
    public class UserModelView
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}