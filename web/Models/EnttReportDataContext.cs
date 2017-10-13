using System;
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
    }
}