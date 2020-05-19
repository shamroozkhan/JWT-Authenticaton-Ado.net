using JwtAuthentication.Models;
using JwtAuthentication.Translator;
using JwtAuthentication.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace JwtAuthentication.Repository
{
    public class UserDbClient
    {
        public List<UserModel> GetAllUsers(string connString)
        {
            return SqlHelper.ExtecuteProcedureReturnData<List<UserModel>>(connString, "GetUsers", r => r.TranslateAsUsersList());
        }

        public string InsertUser(UserModel model, string connString)
        {
            var outParam = new SqlParameter("@ReturnCode", SqlDbType.NVarChar, 20)
            {
                Direction = ParameterDirection.Output
            };

            SqlParameter[] param = {
                new SqlParameter("@FirstName",model.FirstName),
                new SqlParameter("@LastName",model.LastName),
                new SqlParameter("@Email",model.Email),
                new SqlParameter("@Gender",model.Gender),
                new SqlParameter("@DateOfBirth",model.DateOfBirth),
                new SqlParameter("@Password",model.Password),
                outParam
            };
            SqlHelper.ExecuteProcedureReturnString(connString, "InsertUser", param);
            return (string)outParam.Value;
        }

        public string UpdateUser(UserModel model, string connString)
        {
            var outParam = new SqlParameter("@ReturnCode", SqlDbType.NVarChar, 20)
            {
                Direction = ParameterDirection.Output
            };

            SqlParameter[] param = {
                new SqlParameter("@Id",model.Id),
                new SqlParameter("@FirstName",model.FirstName),
                new SqlParameter("@LastName",model.LastName),
                new SqlParameter("@Email",model.Email),
                new SqlParameter("@Gender",model.Gender),
                new SqlParameter("@DateOfBirth",model.DateOfBirth),
                new SqlParameter("@Password",model.Password),
                outParam
            };
            SqlHelper.ExecuteProcedureReturnString(connString, "UpdateUser", param);
            return (string)outParam.Value;
        }

        public string DeleteUser(int id, string connString)
        {
            var outParam = new SqlParameter("@ReturnCode", SqlDbType.NVarChar, 20)
            {
                Direction = ParameterDirection.Output
            };
            SqlParameter[] param =
            {
                new SqlParameter("Id",id),
                outParam
            };
            SqlHelper.ExecuteProcedureReturnString(connString, "DeleteUser", param);
            return (string)outParam.Value;
        }
        public string AuthenticateUser(UserModel model, string connString)
        {
            var outParam = new SqlParameter("@ReturnCode", SqlDbType.NVarChar, 20)
            {
                Direction = ParameterDirection.Output
            };

            SqlParameter[] param = {
                new SqlParameter("@Email",model.Email),
                new SqlParameter("@Password",model.Password),
                outParam
            };
            SqlHelper.ExecuteProcedureReturnString(connString, "LoginUser", param);
            return (string)outParam.Value;
        }
    }
}
