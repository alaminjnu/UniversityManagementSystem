using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystemByReturnNull.Gateway;
using UniversityManagementSystemByReturnNull.Models;

namespace UniversityManagementSystemByReturnNull.Manager
{
    public class DepartmentManager
    {
        public int Save(Department department)
        {

            DepartmentGateway departmentGateway = new DepartmentGateway();
            return departmentGateway.Save(department);
        }

        public bool IsCodeExist(string codeName)
        {
            DepartmentGateway departmentGateway = new DepartmentGateway();
            return departmentGateway.IsCodeExist(codeName);
        }

        public List<Department> GetAll()
        {
            DepartmentGateway departmentGateway = new DepartmentGateway();
            return departmentGateway.GetAll();
        }

        public bool IsNameExist(string name)
        {
            DepartmentGateway departmentGateway = new DepartmentGateway();
            return departmentGateway.IsNameExist(name);
        }
    }
}