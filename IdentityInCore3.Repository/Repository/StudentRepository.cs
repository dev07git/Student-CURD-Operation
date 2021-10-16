using IdentityInCore3.DAL.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityInCore3.Repository
{
    public class StudentRepository : IStudentRepository
    {
        public Core3DBContext context { get; set; }

        public StudentRepository(Core3DBContext _context)
        {
            context = _context;
        }
        public async Task<Students> GetDomainObjectByName(string name, long id)
        {
            var result = await Task.Run(() => context.Students.Where(w => w.Name.Trim().ToLower().Equals(name.Trim().ToLower())));

            if (id > 0)
                result = result.Where(w => !w.Id.Equals(id));

            return result.FirstOrDefault();
        }
        public async Task<Students> GetDomainObject(long id)
        {
            var result = await Task.Run(() => context.Students.Where(w => w.Id.Equals(id)).Include(x => x.Subjects).ThenInclude(x => x.SubjectMaster).SingleOrDefault());

            return result;
        }
        public async Task<List<Students>> GetDomainObjects()
        {
            var result = await Task.Run(() => context.Students.Include(x => x.Subjects).ThenInclude(x => x.SubjectMaster).ToList());

            return result;
        }
        public async Task<List<Students>> GetDomainObjects(IList<long> studentIds)
        {
            var result = await Task.Run(() => context.Students.Where(x => studentIds.Contains(x.Id)).Include(x => x.Subjects).ThenInclude(x => x.SubjectMaster).ToList());

            return result;
        }

        public async Task<bool> UpdateDomainObjectUsing_SP(Contracts.StudentsViewModel students)
        {

             List<SqlParameter> parms = new List<SqlParameter>
            {
                // Create parameter(s)    
                new SqlParameter { ParameterName = "@Id", Value = students.Id },
                new SqlParameter { ParameterName = "@Name", Value = students.Name },new SqlParameter { ParameterName = "@PhoneNumber", Value = students.PhoneNumber },
                        new SqlParameter { ParameterName = "@Address", Value = students.Address },
                        new SqlParameter { ParameterName = "@PostalCode", Value = students.PostalCode },
            };
            string sqlQuery = "EXEC sp_UpdateStudent @Id, @Name,@PhoneNumber,@Address,@PostalCode";
            await Task.Run(()=> context.Database.ExecuteSqlRaw(sqlQuery, parms.ToArray()));
            return true;
        }

        public async Task<bool> DeleteDomainObjectUsing_SP(long id)
        {

            string constring = context.Database.GetDbConnection().ConnectionString;
            using (SqlConnection con = new SqlConnection(constring))
            {
                using SqlCommand cmd = new SqlCommand("DeleteStudent", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            return true;
        }
    }
}
