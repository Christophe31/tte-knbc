using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Client.BusinessService;

namespace Client.BusinessService
{
    public partial class RoleData : Object, System.Runtime.Serialization.IExtensibleDataObject
    {
        public string RoleName { get; set; }
        public string TargetName { get; set; }
    }
}
