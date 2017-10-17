using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Entt.Ers.Models
{
    public class EnttReportDataContext
    {
        public DataSet GetDeliveryExceptionReportDataSet(DateTime reportDate, int day)
        {
            var dataset = new DataSet();

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EnttConnectionString"].ConnectionString))
            using (var cmd = new SqlCommand("usp_dex_vs_pod", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@reportDate", SqlDbType.Date).Value = reportDate;
                cmd.Parameters.Add("@day", SqlDbType.Int).Value = day;
                var sqlDataAapter = new SqlDataAdapter(cmd);
                sqlDataAapter.Fill(dataset);
            }
            return dataset;
        }

        public DataSet GetPupVsPodReportDataSet(DateTime reportDate, int day)
        {
            var dataset = new DataSet();

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EnttConnectionString"].ConnectionString))
            using (var cmd = new SqlCommand("[Entt].[usp_pup_vs_pod]", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@reportDate", SqlDbType.Date).Value = reportDate;
                cmd.Parameters.Add("@day", SqlDbType.Int).Value = day;
                var sqlDataAapter = new SqlDataAdapter(cmd);
                sqlDataAapter.Fill(dataset);
            }
            return dataset;
        }

        public DataSet GetPupVsPodDetailsReportDataSet(DateTime reportDate, int day, string branchCode)
        {
            var dataset = new DataSet();

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EnttConnectionString"].ConnectionString))
            using (var cmd = new SqlCommand("[Entt].[usp_pup_vs_pod_details]", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@reportDate", SqlDbType.Date).Value = reportDate;
                cmd.Parameters.Add("@day", SqlDbType.Int).Value = day;
                cmd.Parameters.Add("@branchCode", SqlDbType.NVarChar, 10).Value = branchCode;
                var sqlDataAapter = new SqlDataAdapter(cmd);
                sqlDataAapter.Fill(dataset);
            }
            return dataset;
        }


        public DataSet ExpectedArrivalReportDataSet(DateTime reportDate)
        {
            var dataset = new DataSet();
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EnttConnectionString"].ConnectionString))
            using (var cmd = new SqlCommand("Entt.usp_expected_arrival_report", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@reportDate", SqlDbType.Date).Value = reportDate;
                var sqlDataAapter = new SqlDataAdapter(cmd);
                sqlDataAapter.Fill(dataset);
            }
            return dataset;
        }

        public DataSet NoAcceptanceReportDataSet(DateTime reportDate)
        {
            var dataset = new DataSet();
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EnttConnectionString"].ConnectionString))
            using (var cmd = new SqlCommand("Entt.usp_no_acceptance", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@reportDate", SqlDbType.NVarChar, 8).Value = reportDate.ToString("ddMMyyyy");
                var sqlDataAapter = new SqlDataAdapter(cmd);
                sqlDataAapter.Fill(dataset);
            }
            return dataset;
        }

        public IList<Branch> GetBranchInfo(string branchCode)
        {
            var list = new List<Branch>();

            var connString = ConfigurationManager.ConnectionStrings["EnttConnectionString"].ConnectionString;
            var conn = new SqlConnection(connString);
            var sql = $"SELECT [BranchCode],[BranchName] FROM [Entt].[BranchStates] WHERE [BranchCode] = '{branchCode}'";

            using (var cmd = new SqlCommand(sql, conn))
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        list.Add(new Branch { Code = reader.GetString(0), Name = reader.GetString(1) });
                    }
                }
            }
            return list;
        }
    }
}