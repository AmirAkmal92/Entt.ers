using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Entt.Ers.Models
{
    public partial class EnttReportDataContext
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

        public async Task<DailyStatistics> GetBranchDashboardSummaryData(DateTime date, string branchCode)
        {
            var stats = new DailyStatistics();

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EnttConnectionString"].ConnectionString))
            using (var cmd = new SqlCommand("[Entt].[usp_home_dashboard_branch]", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@date", SqlDbType.Date).Value = date;
                cmd.Parameters.Add("@branchCode", SqlDbType.NVarChar, 50).Value = branchCode;

                await conn.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        stats.Acceptances = reader.GetInt32(0);
                        stats.Deliveries = reader.GetInt32(1);
                        stats.Unknowns = reader.GetInt32(2);
                        stats.BranchName = reader.GetString(3);
                        stats.BranchCode = branchCode;
                    }
                }
            }
            return stats;
        }

        public async Task<List<AcceptanceByCategory>> GetBranchAcceptanceByCategory(DateTime date, string branchCode)
        {
            var list = new List<AcceptanceByCategory>();

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EnttConnectionString"].ConnectionString))
            using (var cmd = new SqlCommand("[Entt].[usp_home_acceptance_category_branch]", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@date", SqlDbType.Date).Value = date;
                cmd.Parameters.Add("@branchCode", SqlDbType.NVarChar, 50).Value = branchCode;

                await conn.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var acceptance = new AcceptanceByCategory { CategoryName = reader.GetString(0), Count = reader.GetInt32(1) };
                        list.Add(acceptance);
                    }
                }
            }
            return list;
        }

        public async Task<List<AcceptanceByCategory>> GetBranchAcceptanceBySource(DateTime date, string branchCode)
        {
            var list = new List<AcceptanceByCategory>();

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EnttConnectionString"].ConnectionString))
            using (var cmd = new SqlCommand("[Entt].[usp_home_acceptance_system_branch]", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@date", SqlDbType.Date).Value = date;
                cmd.Parameters.Add("@branchCode", SqlDbType.NVarChar, 50).Value = branchCode;

                await conn.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var acceptance = new AcceptanceByCategory { CategoryName = reader.GetString(0), Count = reader.GetInt32(1) };
                        list.Add(acceptance);
                    }
                }
            }
            return list;
        }

        public async Task<HourlyInfo> GetBranchAcceptanceDataByHours(DateTime date, string branchCode)
        {
            HourlyInfo hourlyInfo = null;
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EnttConnectionString"].ConnectionString))
            using (var cmd = new SqlCommand("[Entt].[usp_home_acceptance_branch]", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@reportDate", SqlDbType.Date).Value = date;
                cmd.Parameters.Add("@branchCode", SqlDbType.NVarChar, 50).Value = branchCode;

                await conn.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        hourlyInfo = new HourlyInfo
                        {
                            Hour00 = reader.GetInt32(0),
                            Hour01 = reader.GetInt32(1),
                            Hour02 = reader.GetInt32(2),
                            Hour03 = reader.GetInt32(3),
                            Hour04 = reader.GetInt32(4),
                            Hour05 = reader.GetInt32(5),
                            Hour06 = reader.GetInt32(6),
                            Hour07 = reader.GetInt32(7),
                            Hour08 = reader.GetInt32(8),
                            Hour09 = reader.GetInt32(9),
                            Hour10 = reader.GetInt32(10),
                            Hour11 = reader.GetInt32(11),
                            Hour12 = reader.GetInt32(12),
                            Hour13 = reader.GetInt32(13),
                            Hour14 = reader.GetInt32(14),
                            Hour15 = reader.GetInt32(15),
                            Hour16 = reader.GetInt32(16),
                            Hour17 = reader.GetInt32(17),
                            Hour18 = reader.GetInt32(18),
                            Hour19 = reader.GetInt32(19),
                            Hour20 = reader.GetInt32(20),
                            Hour21 = reader.GetInt32(21),
                            Hour22 = reader.GetInt32(22),
                            Hour23 = reader.GetInt32(23),
                            Total = reader.GetInt32(24)
                        };
                    }
                }
            }
            return hourlyInfo;
        }

        public async Task<HourlyInfo> GetBranchDeliveryDataByHours(DateTime date, string branchCode)
        {
            HourlyInfo hourlyInfo = null;
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EnttConnectionString"].ConnectionString))
            using (var cmd = new SqlCommand("[Entt].[usp_home_delivery_branch]", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@reportDate", SqlDbType.Date).Value = date;
                cmd.Parameters.Add("@branchCode", SqlDbType.NVarChar, 50).Value = branchCode;

                await conn.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        hourlyInfo = new HourlyInfo
                        {
                            Hour00 = reader.GetInt32(0),
                            Hour01 = reader.GetInt32(1),
                            Hour02 = reader.GetInt32(2),
                            Hour03 = reader.GetInt32(3),
                            Hour04 = reader.GetInt32(4),
                            Hour05 = reader.GetInt32(5),
                            Hour06 = reader.GetInt32(6),
                            Hour07 = reader.GetInt32(7),
                            Hour08 = reader.GetInt32(8),
                            Hour09 = reader.GetInt32(9),
                            Hour10 = reader.GetInt32(10),
                            Hour11 = reader.GetInt32(11),
                            Hour12 = reader.GetInt32(12),
                            Hour13 = reader.GetInt32(13),
                            Hour14 = reader.GetInt32(14),
                            Hour15 = reader.GetInt32(15),
                            Hour16 = reader.GetInt32(16),
                            Hour17 = reader.GetInt32(17),
                            Hour18 = reader.GetInt32(18),
                            Hour19 = reader.GetInt32(19),
                            Hour20 = reader.GetInt32(20),
                            Hour21 = reader.GetInt32(21),
                            Hour22 = reader.GetInt32(22),
                            Hour23 = reader.GetInt32(23),
                            Total = reader.GetInt32(24)
                        };
                    }
                }
            }
            return hourlyInfo;
        }
    }
}