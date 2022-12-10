using Core.Domain.DI;
using Core.Domain.Postgre.DI;
using Core.Domain.Shared.Common.Logger;
using Core.Domain.Shared.Enums;
using CORE.Application.Bases;
using CORE.Application.Contracts.Bases;
using CORE.Application.Contracts.DI;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace CORE.Application.DI
{
    public class StudentService : CrudBaseService<IStudentRepo, StudentEntity, StudentDtoEdit>, IStudentService
    {

        public StudentService(IStudentRepo repo) : base(repo)
        {
        }

        public async Task DeleteStudent(Guid id)
        {
            IDbConnection cnn = null;
            try
            {
                cnn = _repo.GetOpenConnection(DatabaseSide.WriteSide);
                using(var transaction = cnn.BeginTransaction())
                {
                    string sql = "delete from public.student where id =:id";
                    _repo.ExecuteNonQuery(CommandType.Text, cnn, transaction, sql, new { id = id }, 90);
                    transaction.Commit();
                }
            }
            catch (Exception)
            {

                throw;
            } 
            finally
            {
                _repo.CloseConnection(cnn);
            }
        }

        public async Task InsertStudent(List<StudentEntity> students)
        {
            IDbConnection cnn = null;
            try
            {
                cnn = _repo.GetOpenConnection(DatabaseSide.WriteSide);
                using (var transaction = cnn.BeginTransaction())
                {
                    string sql = "select public.insert_batch_student(:lists)";
                    var lists = JsonConvert.SerializeObject(students);
                    NLogger.LogInfo("info : bắt đầu thêm dữ liệu vào database");
                    NLogger.LogWarning("warning : chú ý dữ liệu truyền vào");
                    _repo.ExecuteNonQuery(CommandType.Text, cnn, transaction, sql, new { lists = lists }, 90);
                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                NLogger.LogError("Lỗi : " + ex);
                throw;
            }
            finally
            {
                _repo.CloseConnection(cnn);
            }
        }
    }
}
