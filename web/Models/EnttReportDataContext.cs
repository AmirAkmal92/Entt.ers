using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Entt.Ers.Models
{
    public partial class EnttReportDataContext
    {
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
            var timeReportDate = reportDate;
            if (DateTime.Now.Hour < 9 && reportDate.Date == DateTime.Today.Date)
            {
                timeReportDate = timeReportDate.AddDays(-1);
            }

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EnttConnectionString"].ConnectionString))
            using (var cmd = new SqlCommand("Entt.usp_expected_arrival_branch_report", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@reportDate", SqlDbType.Date).Value = timeReportDate;
                cmd.Parameters.Add("@branchCode", SqlDbType.NVarChar, 50).Value = branchCode;
                var sqlDataAapter = new SqlDataAdapter(cmd);
                sqlDataAapter.Fill(dataset);
            }
            return dataset;
        }

        public DataSet AcceptanceBranchReportDataSet(DateTime reportDate, string branchCode)
        {
            var dataset = new DataSet();
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EnttConnectionString"].ConnectionString))
            using (var cmd = new SqlCommand("Entt.usp_acceptance", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@reportDate", SqlDbType.DateTime).Value = reportDate;
                cmd.Parameters.Add("@branchCode", SqlDbType.NVarChar, 50).Value = branchCode;
                var sqlDataAapter = new SqlDataAdapter(cmd);
                sqlDataAapter.Fill(dataset);
            }
            return dataset;
        }

        public DataSet AcceptanceBranchDetailsReportDataSet(DateTime reportDate, string branchCode)
        {
            var dataset = new DataSet();
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EnttConnectionString"].ConnectionString))
            using (var cmd = new SqlCommand("Entt.usp_acceptance_details", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@reportDate", SqlDbType.DateTime).Value = reportDate;
                cmd.Parameters.Add("@branchCode", SqlDbType.NVarChar, 50).Value = branchCode;
                var sqlDataAapter = new SqlDataAdapter(cmd);
                sqlDataAapter.Fill(dataset);
            }
            return dataset;
        }

        public async Task<IList<Branch>> SearchNearbyBranch(decimal lattitude, decimal longitude, int limit)
        {
            var branches = new List<Branch>();
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EnttConnectionString"].ConnectionString))
            using (var cmd = new SqlCommand("[Entt].[usp_search_nearest_branch]", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@lat", SqlDbType.Decimal).Value = lattitude;
                cmd.Parameters.Add("@lng", SqlDbType.Decimal).Value = longitude;

                await conn.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var branch = new Branch { Code = reader["BranchCode"].ToString(), Name = reader["BranchName"].ToString(), Distance = decimal.Parse(reader["Distance"].ToString()) };
                        branches.Add(branch);
                    }
                }
            }
            return branches.Take(limit).ToList();
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

        public IList<Branch> GetBranchGeoLocations()
        {
            var list = new List<Branch>();

            var connString = ConfigurationManager.ConnectionStrings["EnttConnectionString"].ConnectionString;
            var conn = new SqlConnection(connString);
            var sql = $"SELECT [BranchCode],[BranchName],[StateName],[Lattitude],[Longitude] FROM [Entt].[BranchProfiles]";

            using (var cmd = new SqlCommand(sql, conn))
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        list.Add(new Branch { Code = reader.GetString(0), Name = reader.GetString(1), Lattitude = reader.GetDecimal(3), Longitude = reader.GetDecimal(4) });
                    }
                }
            }
            return list;
        }

        public Acceptance SearchAcceptance(string consignmentNo)
        {
            Acceptance acceptance = null;

            var connString = ConfigurationManager.ConnectionStrings["EnttConnectionString"].ConnectionString;
            var conn = new SqlConnection(connString);
            var sql = $"SELECT [ConsignmentNo],[DateTime],[CourierId],[LocationId],[Comment],[ScannerId],[CreatedDate] FROM [Entt].[Acceptance] WHERE [ConsignmentNo] = '{consignmentNo}'";

            using (var cmd = new SqlCommand(sql, conn))
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        acceptance = new Acceptance {
                            ConsignmentNo = reader.GetValue(0).ToString(),
                            DateTime = reader.GetDateTime(1),
                            CourierId = reader.GetValue(2).ToString(),
                            LocationId = reader.GetValue(3).ToString(),
                            Comment = reader.GetValue(4).ToString(),
                            ScannerId = reader.GetValue(5).ToString(),
                            CreatedDate = reader.GetDateTime(6)
                        };
                    }
                }
            }
            return acceptance;
        }
    }
}