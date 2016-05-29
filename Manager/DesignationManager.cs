using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystemByReturnNull.Gateway;
using UniversityManagementSystemByReturnNull.Models;

namespace UniversityManagementSystemByReturnNull.Manager
{
    public class DesignationManager
    {
        public List<Designation> GetAll()
        {
            DesignationGateway designationGateway = new DesignationGateway();
            return designationGateway.GetAll();
        }
    }
}