using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Entt.Ers.Models
{
    public class EnttReportDataContext
    {
        public async Task<DailyStatistics> GetDashboardData(DateTime date)
        {
            var stats = new DailyStatistics();

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EnttConnectionString"].ConnectionString))
            using (var cmd = new SqlCommand("[Entt].[usp_home_dashboard]", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@date", SqlDbType.Date).Value = date;

                await conn.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        stats.Acceptances = reader.GetInt32(0);
                        stats.Deliveries = reader.GetInt32(1);
                        stats.Unknowns = reader.GetInt32(2);
                    }
                }
            }
            return stats;
        }


        public DataSet GetDeliveryExceptionReportDataSet(DateTime reportDate, int day)
        {
            var dataset = new DataSet();

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EnttConnectionString"].ConnectionString))
            using (var cmd = new SqlCommand("usp_dex_vs_pod", conn))
            {
                cmd.CommandTimeout = 80;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@reportDate", SqlDbType.Date).Value = reportDate;
                cmd.Parameters.Add("@day", SqlDbType.Int).Value = day;
                var sqlDataAapter = new SqlDataAdapter(cmd);
                sqlDataAapter.Fill(dataset);
            }
            return dataset;
        }

        public DataSet GetDeliveryExceptionBranchReportDataSet(DateTime reportDate, int day, string branchCode)
        {
            var dataset = new DataSet();

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EnttConnectionString"].ConnectionString))
            using (var cmd = new SqlCommand("usp_dex_vs_pod_branch", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@reportDate", SqlDbType.Date).Value = reportDate;
                cmd.Parameters.Add("@day", SqlDbType.Int).Value = day;
                cmd.Parameters.Add("@branchCode", SqlDbType.NVarChar, 50).Value = branchCode;
                var sqlDataAapter = new SqlDataAdapter(cmd);
                sqlDataAapter.Fill(dataset);
            }
            return dataset;
        }

        public DataSet HandoverPodReportDataSet(DateTime reportDate, int prefixDay)
        {
            var dataset = new DataSet();
            var prefix = ApplicationHelper.GetPrefix(prefixDay);
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EnttConnectionString"].ConnectionString))
            using (var cmd = new SqlCommand("Entt.usp_handover3rdparty_vs_pod", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@reportDate", SqlDbType.DateTime).Value = reportDate;
                cmd.Parameters.Add("@prefixStart", SqlDbType.Int).Value = prefix.Start;
                cmd.Parameters.Add("@prefixEnd", SqlDbType.Int).Value = prefix.End;
                var sqlDataAapter = new SqlDataAdapter(cmd);
                sqlDataAapter.Fill(dataset);
            }
            return dataset;
        }

        public DataSet HandoverPodBranchReportDataSet(DateTime reportDate, int prefixDay, string branchCode)
        {
            var dataset = new DataSet();
            var prefix = ApplicationHelper.GetPrefix(prefixDay);
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EnttConnectionString"].ConnectionString))
            using (var cmd = new SqlCommand("Entt.usp_handover3rdparty_vs_pod_branch", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@reportDate", SqlDbType.DateTime).Value = reportDate;
                cmd.Parameters.Add("@prefixStart", SqlDbType.Int).Value = prefix.Start;
                cmd.Parameters.Add("@prefixEnd", SqlDbType.Int).Value = prefix.End;
                cmd.Parameters.Add("@branchCode", SqlDbType.NVarChar, 50).Value = branchCode;
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

        public DataSet GetPupVsPodBranchReportDataSet(DateTime reportDate, int day, string branchCode)
        {
            var dataset = new DataSet();
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EnttConnectionString"].ConnectionString))
            using (var cmd = new SqlCommand("[Entt].[usp_pup_vs_pod_branch]", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@reportDate", SqlDbType.Date).Value = reportDate;
                cmd.Parameters.Add("@day", SqlDbType.Int).Value = day;
                cmd.Parameters.Add("@branchCode", SqlDbType.NVarChar,50).Value = branchCode;
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
                cmd.CommandTimeout = 80;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@reportDate", SqlDbType.Date).Value = reportDate;
                cmd.Parameters.Add("@day", SqlDbType.Int).Value = day;
                cmd.Parameters.Add("@branchCode", SqlDbType.NVarChar, 10).Value = branchCode;
                var sqlDataAapter = new SqlDataAdapter(cmd);
                sqlDataAapter.Fill(dataset);
            }
            return dataset;
        }

        public DataSet GetPodVsPupReportDataSet(DateTime reportDate, int day)
        {
            var dataset = new DataSet();

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EnttConnectionString"].ConnectionString))
            using (var cmd = new SqlCommand("[Entt].[usp_pod_vs_pup]", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@reportDate", SqlDbType.Date).Value = reportDate;
                cmd.Parameters.Add("@day", SqlDbType.Int).Value = day;
                var sqlDataAapter = new SqlDataAdapter(cmd);
                sqlDataAapter.Fill(dataset);
            }
            return dataset;
        }

        public DataSet GetPodVsPupDetailsReportDataSet(DateTime reportDate, int day, string branchCode)
        {
            var dataset = new DataSet();

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EnttConnectionString"].ConnectionString))
            using (var cmd = new SqlCommand("[Entt].[usp_pod_vs_pup_details]", conn))
            {
                cmd.CommandTimeout = 80;
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

        public DataSet ExpectedArrivalBranchReportDataSet(DateTime reportDate, string branchCode)
        {
            var dataset = new DataSet();
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EnttConnectionString"].ConnectionString))
            using (var cmd = new SqlCommand("Entt.usp_expected_arrival_branch_report", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@reportDate", SqlDbType.Date).Value = reportDate;
                cmd.Parameters.Add("@branchCode", SqlDbType.NVarChar, 50).Value = branchCode;
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

        public DataSet NoAcceptanceBranchReportDataSet(DateTime reportDate, string branchCode)
        {
            var dataset = new DataSet();
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EnttConnectionString"].ConnectionString))
            using (var cmd = new SqlCommand("Entt.usp_no_acceptance_branch", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@reportDate", SqlDbType.NVarChar, 8).Value = reportDate.ToString("ddMMyyyy");
                cmd.Parameters.Add("@branchCode", SqlDbType.NVarChar, 50).Value = branchCode;
                var sqlDataAapter = new SqlDataAdapter(cmd);
                sqlDataAapter.Fill(dataset);
            }
            return dataset;
        }

        public DataSet NoAcceptanceDetailsReportDataSet(DateTime reportDate, string branchCode)
        {
            var dataset = new DataSet();
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EnttConnectionString"].ConnectionString))
            using (var cmd = new SqlCommand("Entt.usp_no_acceptance_details", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@reportDate", SqlDbType.NVarChar,8).Value = reportDate.ToString("ddMMyyyy");
                cmd.Parameters.Add("@branchCode", SqlDbType.NVarChar,10).Value = branchCode;
                var sqlDataAapter = new SqlDataAdapter(cmd);
                sqlDataAapter.Fill(dataset);
            }
            return dataset;
        }

        public IList<Branch> GetPplBranches()
        {
            var list = new List<Branch>();

            var connString = ConfigurationManager.ConnectionStrings["EnttConnectionString"].ConnectionString;
            var conn = new SqlConnection(connString);
            var sql = $"[Entt].[usp_get_ppl_list]";

            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
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