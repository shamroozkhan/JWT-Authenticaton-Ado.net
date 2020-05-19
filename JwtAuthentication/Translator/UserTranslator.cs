using JwtAuthentication.Models;
using JwtAuthentication.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace JwtAuthentication.Translator
{
    public static class UserTranslator
    {
        public static UserModel TranslateAsUser(this SqlDataReader reader, bool isList = false)
        {
            if (!isList)
            {
                if (!reader.HasRows)
                    return null;
                reader.Read();
            }
            var item = new UserModel();

            if (reader.IsColumnExists("Id"))
                item.Id = SqlHelper.GetNullableInt32(reader, "Id");

            if (reader.IsColumnExists("FirstName"))
                item.FirstName = SqlHelper.GetNullableString(reader, "FirstName");

            if (reader.IsColumnExists("LastName"))
                item.LastName = SqlHelper.GetNullableString(reader, "LastName");

            if (reader.IsColumnExists("Email"))
                item.Email = SqlHelper.GetNullableString(reader, "Email");

            if (reader.IsColumnExists("Gender"))
                item.Gender = SqlHelper.GetNullableString(reader, "Gender");

            if (reader.IsColumnExists("DateOfBirth"))
                item.DateOfBirth = SqlHelper.GetNullableString(reader, "DateOfBirth");
            
            if (reader.IsColumnExists("Password"))
                item.Password = SqlHelper.GetNullableString(reader, "Password");

            return item;
        }

        public static List<UserModel> TranslateAsUsersList(this SqlDataReader reader)
        {
            var list = new List<UserModel>();
            while (reader.Read())
            {
                list.Add(TranslateAsUser(reader, true));
            }
            return list;
        }
    }
}
