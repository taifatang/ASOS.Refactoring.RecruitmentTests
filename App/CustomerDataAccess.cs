﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace App
{
    public interface ICustomerDataAccess
    {
        void AddCustomer(Customer customer);
    }
    public class CustomerDataAccess : ICustomerDataAccess
    {
        private readonly string _connectionString;
        public CustomerDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }
   
        public void AddCustomer(Customer customer)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "uspAddCustomer"
                };

                var firstNameParameter = new SqlParameter("@Firstname", SqlDbType.VarChar, 50) { Value = customer.Firstname };
                command.Parameters.Add(firstNameParameter);
                var surnameParameter = new SqlParameter("@Surname", SqlDbType.VarChar, 50) { Value = customer.Surname };
                command.Parameters.Add(surnameParameter);
                var dateOfBirthParameter = new SqlParameter("@DateOfBirth", SqlDbType.DateTime) { Value = customer.DateOfBirth };
                command.Parameters.Add(dateOfBirthParameter);
                var emailAddressParameter = new SqlParameter("@EmailAddress", SqlDbType.VarChar, 50) { Value = customer.EmailAddress };
                command.Parameters.Add(emailAddressParameter);
                var hasCreditLimitParameter = new SqlParameter("@HasCreditLimit", SqlDbType.Bit) { Value = customer.HasCreditLimit };
                command.Parameters.Add(hasCreditLimitParameter);
                var creditLimitParameter = new SqlParameter("@CreditLimit", SqlDbType.Int) { Value = customer.CreditLimit };
                command.Parameters.Add(creditLimitParameter);
                var companyIdParameter = new SqlParameter("@CompanyId", SqlDbType.Int) { Value = customer.Company.Id };
                command.Parameters.Add(companyIdParameter);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
