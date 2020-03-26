using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SomerenDAL;
using SomerenModel;

namespace SomerenLogic
{
    public class Supervisor_Service
    {
        Supervisors_DAO supervisor_db = new Supervisors_DAO();

        public List<Supervisor> GetSupervisors()
        {
            List<Supervisor> supervisor = supervisor_db.Db_Get_All_Supervisors();
            return supervisor;
        }
        public void RemoveSup(int id)
        {
            supervisor_db.RemoveSupervisor(id);
        }
    }
}
