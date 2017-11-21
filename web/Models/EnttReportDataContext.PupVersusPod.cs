﻿using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Entt.Ers.Models
{
    public partial class EnttReportDataContext
    {
        public DataSet GetPupVsPodReportDataSet(DateTime reportDate, int day)
        {
            var dataset = new DataSet();

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EnttConnectionString"].ConnectionString))
            using (var cmd = new SqlCommand("[Entt].[usp_pup_vs_pod]", conn))
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

        public DataSet GetPupVsPodBranchReportDataSet(DateTime reportDate, int day, string branchCode)
        {
            var dataset = new DataSet();
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EnttConnectionString"].ConnectionString))
            using (var cmd = new SqlCommand("[Entt].[usp_pup_vs_pod_branch]", conn))
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
    }
}