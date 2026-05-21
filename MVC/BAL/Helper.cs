using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC.Models;
using Npgsql;

namespace MVC.BAL
{
    public class Helper
    {
        private readonly NpgsqlConnection _conn;
        public Helper(NpgsqlConnection conn)
        {
            _conn = conn;
        }

        public int Register(Emp item)
        {
            var q = "INSERT INTO t_emp (c_name ,c_email , c_password , c_gender , c_mobile ) VALUES (@name ,@email,@pw,@gender ,@mobile)";
            try
            {
                _conn.Close();
                using (var cm = new NpgsqlCommand("SELECT c_email FROM t_emp WHERE c_email = @email", _conn))
                {
                    cm.Parameters.AddWithValue("@email", item.Email);
                    _conn.Open();
                    var res = cm.ExecuteReader();

                    if (res.HasRows)
                    {

                        return -1;
                    }

                    _conn.Close();
                    using (var cmd = new NpgsqlCommand(q, _conn))
                    {
                        cmd.Parameters.AddWithValue("@name", item.Name);
                        cmd.Parameters.AddWithValue("@email", item.Email);
                        cmd.Parameters.AddWithValue("@pw", item.Password);
                        cmd.Parameters.AddWithValue("@gender", item.Gender);
                        cmd.Parameters.AddWithValue("@mobile", item.Mobile);

                        _conn.Open();

                        var reder = cmd.ExecuteNonQuery();

                        return reder > 0 ? 1 : 0;

                    }

                }
            }
            catch
            {
                return 0;
            }
            finally
            {
                _conn.Close();
            }
        }

        public Emp Login(vm_Login item)
        {
            var q = "SELECT c_empid ,c_name ,c_email,c_password ,c_gender ,c_mobile,c_salary FROM t_emp WHERE c_email = @email AND c_password = @pw";
            Emp emp = new Emp();
            try
            {
                _conn.Close();
                using (var cmd = new NpgsqlCommand(q, _conn))
                {
                    cmd.Parameters.AddWithValue("@email", item.Email);
                    cmd.Parameters.AddWithValue("@pw", item.Password);

                    _conn.Open();

                    var res = cmd.ExecuteReader();

                    if (res.Read())
                    {
                        emp.EmpId = (int)res["c_empid"];
                        emp.Name = res["c_name"].ToString();
                        emp.Email = res["c_email"].ToString();
                        emp.Password = res["c_password"].ToString();
                        emp.Mobile = res["c_mobile"].ToString();
                        emp.Gender = res["c_gender"].ToString();
                        emp.Salary = (decimal)res["c_salary"];
                    }

                    return emp;
                }

            }
            catch (Exception e)
            {
                System.Console.WriteLine("Login Error : " + e.Message);
            }
            return null;
        }


        public Admin AdminLogin(vm_Login item)
        {
            System.Console.WriteLine(item.Email+item.Password);
            var q = "SELECT c_id, c_email,c_password  FROM t_admin WHERE c_email = @email AND c_password = @pw";
            Admin emp = new Admin();
            try
            {
                _conn.Close();
                using (var cmd = new NpgsqlCommand(q, _conn))
                {
                    cmd.Parameters.AddWithValue("@email", item.Email.Trim());
                    cmd.Parameters.AddWithValue("@pw", item.Password.Trim());

                    _conn.Open();

                    var res = cmd.ExecuteReader();
                    
                    if (res.Read())
                    {
                        emp.Id = (int)res["c_id"];
                        emp.Email = res["c_email"].ToString();
                        emp.Password = res["c_password"].ToString();

                    }
                    System.Console.WriteLine(emp.Id);
                     System.Console.WriteLine(emp.Email);

                    return emp;
                }

            }
            catch (Exception e)
            {
                System.Console.WriteLine("Login Error : " + e.Message);
            }
            return null;
        }


        public List<Emp> GetAllEmp()
        {
            List<Emp> emps = new List<Emp>();
            var q = "SELECT c_empid ,c_name ,c_email,c_gender ,c_mobile ,c_salary FROM t_emp";

            _conn.Close();
            using (NpgsqlCommand cmd = new NpgsqlCommand(q, _conn))
            {
                _conn.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var emp = new Emp();
                    emp.EmpId = (int)reader["c_empid"];
                    emp.Name = reader["c_name"].ToString();
                    emp.Email = reader["c_email"].ToString();
                    emp.Gender = reader["c_gender"].ToString();
                    emp.Mobile = reader["c_mobile"].ToString();
                    emp.Salary = (decimal)reader["c_salary"];

                    // System.Console.WriteLine(emp.Salary);

                    emps.Add(emp);

                }

                return emps;

            }


        }


        public Emp GetEmp(int id)
        {
            System.Console.WriteLine("id is :" + id);
            Emp emp = new Emp();
            var q = "SELECT c_empid ,c_name ,c_email,c_gender ,c_mobile ,c_salary FROM t_emp WHERE c_empid = @id";

            _conn.Close();
            using (NpgsqlCommand cmd = new NpgsqlCommand(q, _conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                _conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {

                    emp.EmpId = (int)reader["c_empid"];
                    emp.Name = reader["c_name"].ToString();
                    emp.Email = reader["c_email"].ToString();
                    emp.Gender = reader["c_gender"].ToString();
                    emp.Mobile = reader["c_mobile"].ToString();
                    emp.Salary = (decimal)reader["c_salary"];
                }
                System.Console.WriteLine(emp.EmpId);

                return emp;

            }
        }

        public int SetSalary(SetSalary item)
        {
            var q = @"UPDATE t_emp set c_salary=@c_salary WHERE c_empid=@c_empid";
            _conn.Close();
            using (NpgsqlCommand cmd = new NpgsqlCommand(q, _conn))
            {
                cmd.Parameters.AddWithValue("@c_empid", item.EmpId);
                cmd.Parameters.AddWithValue("@c_salary", item.Salary);

                _conn.Open();
                var reader = cmd.ExecuteNonQuery();

                return reader > 0 ? 1 : 0;
            }

        }

        public int DeleteEmp(int id)
        {
            var q = @"DELETE From t_emp WHERE c_empid=@c_empid";
            _conn.Close();
            using (NpgsqlCommand cmd = new NpgsqlCommand(q, _conn))
            {
                cmd.Parameters.AddWithValue("@c_empid", id);


                _conn.Open();
                var reader = cmd.ExecuteNonQuery();

                return reader > 0 ? 1 : 0;
            }

        }

        public int Delete(int id)
        {
            var q = @"DELETE From t_empSalary WHERE c_salaryid=@c_salaryid";
            _conn.Close();
            using (NpgsqlCommand cmd = new NpgsqlCommand(q, _conn))
            {
                cmd.Parameters.AddWithValue("@c_salaryid", id);


                _conn.Open();
                var reader = cmd.ExecuteNonQuery();

                return reader > 0 ? 1 : 0;
            }

        }

        public List<EmpSal> GetAllEmpSal()
        {
            List<EmpSal> record = new List<EmpSal>();
            var q = "SELECT c_salaryid ,e.c_empid ,e.c_name,c_month , c_year ,e.c_salary , c_allow , c_deduct , c_net FROM t_empSalary as t join t_emp as e  on e.c_empid = t.c_empid ";
            _conn.Close();
            using (NpgsqlCommand cmd = new NpgsqlCommand(q, _conn))
            {
                _conn.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    EmpSal u1 = new EmpSal();
                    u1.SalaryId = (int)reader["c_salaryid"];
                    u1.EmpId = (int)reader["c_empid"];
                    u1.Name =  (string)reader["c_name"];
                    u1.Month = (int)reader["c_month"];
                    u1.Year = (int)reader["c_year"];
                    u1.Basic = (decimal)reader["c_salary"];
                    u1.Allow = (decimal)reader["c_allow"];
                    u1.Deduct = (decimal)reader["c_deduct"];
                    u1.Net = (decimal)reader["c_net"];

                    record.Add(u1);
                }

                return record;

            }


        }



        public List<EmpSal> GetEmpSal(int id)
        {
            List<EmpSal> record = new List<EmpSal>();
            var q = "SELECT c_salaryid ,e.c_empid,c_month , c_year ,e.c_salary , c_allow , c_deduct , c_net FROM t_empSalary as t join t_emp as e  on e.c_empid = t.c_empid  where e.c_empid = @id";
            _conn.Close();
            using (NpgsqlCommand cmd = new NpgsqlCommand(q, _conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                _conn.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    EmpSal u1 = new EmpSal();
                    u1.SalaryId = (int)reader["c_salaryid"];
                    u1.EmpId = (int)reader["c_empid"];
                    u1.Month = (int)reader["c_month"];
                    u1.Year = (int)reader["c_year"];
                    u1.Basic = (decimal)reader["c_salary"];
                    u1.Allow = (decimal)reader["c_allow"];
                    u1.Deduct = (decimal)reader["c_deduct"];
                    u1.Net = (decimal)reader["c_net"];

                    record.Add(u1);
                }

                return record;

            }
        }

        public List<SalCompo> GetSalCompo()
        {
            List<SalCompo> comp = new List<SalCompo>();
            _conn.Close();
            using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM t_salaryComp", _conn))
            {
                _conn.Open();
                var r = cmd.ExecuteReader();
                while (r.Read())
                {
                    SalCompo com = new SalCompo();
                    com.Id = (int)r["c_id"];
                    com.Name =  r["c_name"].ToString();
                    com.Type = r["c_type"].ToString();
                    com.Per = (int)r["c_percentage"];

                    comp.Add(com);
                }
            }
            return comp;
        }


        public int calculateSalary(EmpSal item)
        {
            var checkQuery = @"SELECT 1 FROM t_empSalary 
                   WHERE c_empid = @empid AND c_month = @month AND c_year = @year";

            using (var checkCmd = new NpgsqlCommand(checkQuery, _conn))
            {
                checkCmd.Parameters.AddWithValue("@empid", item.EmpId);
                checkCmd.Parameters.AddWithValue("@month", item.Month);
                checkCmd.Parameters.AddWithValue("@year", item.Year);

                _conn.Open();
                var exists = checkCmd.ExecuteScalar();

                if (exists != null)
                {
                    return -1; // already exists
                }
                _conn.Close();
            }

            try
            {
                _conn.Close();

                var q = @"
        WITH comp AS (
            SELECT 
                SUM(CASE WHEN c_type = 'Allowance' THEN c_percentage ELSE 0 END) AS allow_per,
                SUM(CASE WHEN c_type = 'Deduction'  THEN c_percentage ELSE 0 END) AS deduct_per
            FROM t_salaryComp
        ),
        emp_data AS (
            SELECT 
                e.c_empid,
                e.c_salary,
                c.allow_per,
                c.deduct_per
            FROM t_emp e
            CROSS JOIN comp c
            WHERE e.c_empid = @empid
        )
        INSERT INTO t_empSalary (c_empid, c_month, c_year, c_allow, c_deduct, c_net)
        SELECT 
            c_empid,
            @month,
            @year,
            (c_salary * allow_per / 100),
            (c_salary * deduct_per / 100),
            (c_salary + (c_salary * allow_per / 100) - (c_salary * deduct_per / 100))
        FROM emp_data;
        ";

                using (NpgsqlCommand cmd = new NpgsqlCommand(q, _conn))
                {
                    cmd.Parameters.AddWithValue("@empid", item.EmpId);
                    cmd.Parameters.AddWithValue("@month", item.Month);
                    cmd.Parameters.AddWithValue("@year", item.Year);

                    _conn.Open();
                    var res = cmd.ExecuteNonQuery();

                    return res > 0 ? 1 : 0;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Salary Calc Error: " + e.Message);
                return 0;
            }
            finally
            {
                _conn.Close();
            }
        }





    }
}