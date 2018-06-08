using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Entt.Ers.Models
{
    public class AimsReportDataContext
    {
        public DataSet GetBranchDeviceSummaryAtPplDataSet(string[] branches)
        {
            var dataset = new DataSet();
            var branchList = string.Join(",", branches.Select(item => "'" + item + "'"));
            using (var sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["NonCoreConnectionString"].ConnectionString))
            {
                string queryString = $"SELECT ROW_NUMBER() OVER(PARTITION BY [BranchName] Order By [BranchName]) AS [Row], BranchName, AssetNumber, CategoryName, AssetName, COUNT(*) AS Total FROM  dbo.uv_GetAssetByItemReport WHERE BranchCode IN ({branchList}) GROUP BY BranchName, AssetNumber, CategoryName, AssetName ORDER BY BranchName, CategoryName, AssetName";
                var sqlDataAapter = new SqlDataAdapter(queryString, sqlConnection);
                sqlDataAapter.Fill(dataset);
            }
            return dataset;
        }
        
        public DataSet GetBranchDeviceSummaryOutRepairDataSet(string[] branches)
        {
            var dataset = new DataSet();
            var branchList = string.Join(",", branches.Select(item => "'" + item + "'"));
            using (var sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["NonCoreConnectionString"].ConnectionString))
            {
                string queryString = $"SELECT ROW_NUMBER() OVER(PARTITION BY [BranchCode] Order By [BranchCode]) AS [Row],[BranchCode],[BranchName],[CategoryName],[AssetNumber],[AssetName],[DateSendDeviceToHQ], COUNT(*) AS Total FROM [dbo].[uv_Aims_GetAssetRepairDetailByBranch] WHERE BranchCode IN ({branchList}) AND AssetRepairStatus != 'Successfully Repaired' GROUP BY BranchCode,BranchName,AssetNumber,CategoryName,AssetName,[DateSendDeviceToHQ] ORDER BY [BranchCode], [CategoryName],[AssetNumber]";
                var sqlDataAapter = new SqlDataAdapter(queryString, sqlConnection);
                sqlDataAapter.Fill(dataset);
            }
            return dataset;
        }

        public DataSet GetBranchDeviceSummaryPendingDeliveryDataSet(string[] branches)
        {
            var dataset = new DataSet();
            var branchList = string.Join(",", branches.Select(item => "'" + item + "'"));
            using (var sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["NonCoreConnectionString"].ConnectionString))
            {
                string queryString = $"SELECT ROW_NUMBER() OVER(PARTITION BY [BranchName] Order By [BranchName]) AS [Row],[BranchName],[CategoryName],[AssetNumber],[AssetName],[NoOfAssetRequested],[NoOfAssetDelivered],[NoOfAssetPendingDelivery],[RequestDate], COUNT(*) AS [Total] FROM  [dbo].[uv_GetStockOutForRequest] WHERE BranchCode IN ({branchList}) GROUP BY BranchName, AssetNumber, CategoryName, AssetName, NoOfAssetRequested, NoOfAssetDelivered, NoOfAssetPendingDelivery, RequestDate ORDER BY BranchName, CategoryName, AssetName";
                var sqlDataAapter = new SqlDataAdapter(queryString, sqlConnection);
                sqlDataAapter.Fill(dataset);
            }
            return dataset;
        }

        public DataSet GetBranchDeviceSummaryItemRelocatedDataSet(string[] branches)
        {
            var dataset = new DataSet();
            var branchList = string.Join(",", branches.Select(item => "'" + item + "'"));
            using (var sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["NonCoreConnectionString"].ConnectionString))
            {
                string queryString = $"SELECT ROW_NUMBER() OVER(PARTITION BY [BranchOwnerShip] Order By [BranchOwnerShip]) AS [Row],[BranchOwnerShip],[CategoryName],[AssetNumber],[AssetName], COUNT(*) AS [Total] FROM [dbo].[uv_GetAssetRelocationByBranch] WHERE BranchCode IN ({branchList}) GROUP BY BranchOwnerShip, AssetNumber, CategoryName, AssetName ORDER BY BranchOwnerShip, CategoryName, AssetName";
                var sqlDataAapter = new SqlDataAdapter(queryString, sqlConnection);
                sqlDataAapter.Fill(dataset);
            }
            return dataset;
        }

        //
        //Branch Device Details

        public DataSet GetBranchDeviceDetailsAtPplDataSet(string[] branches)
        {
            var dataset = new DataSet();
            var branchList = string.Join(",", branches.Select(item => "'" + item + "'"));
            using (var sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["NonCoreConnectionString"].ConnectionString))
            {
                string queryString = $"SELECT ROW_NUMBER() OVER(PARTITION BY [BranchOwnerShip] Order By [BranchOwnerShip]) AS [Row],[BranchOwnerShip],[AssetNumber],[CategoryName],[AssetName],[SerialNumber],[StatusName] FROM [dbo].[uv_GetAssetDetailByBranch]  WHERE BranchCode IN ({branchList}) GROUP BY BranchOwnerShip, AssetNumber, CategoryName, AssetName,SerialNumber,StatusName ORDER BY BranchOwnerShip, CategoryName, AssetName";
                var sqlDataAapter = new SqlDataAdapter(queryString, sqlConnection);
                sqlDataAapter.Fill(dataset);
            }
            return dataset;
        }

        public DataSet GetBranchDeviceDetailsOutRepairDataSet(string[] branches)
        {
            var dataset = new DataSet();
            var branchList = string.Join(",", branches.Select(item => "'" + item + "'"));
            using (var sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["NonCoreConnectionString"].ConnectionString))
            {
                string queryString = $"SELECT ROW_NUMBER() OVER(PARTITION BY [BranchCode] Order By [BranchCode]) AS [Row],[BranchCode],[BranchName],[CategoryName],[AssetNumber],[AssetName],[SerialNumber],[DateSendDeviceToHQ],[DateSendDeviceToVendor],[ReasonForRepair],[Details],[AssetRepairStatus] FROM [dbo].[uv_Aims_GetAssetRepairDetailByBranch] WHERE BranchCode IN ({branchList}) AND AssetRepairStatus != 'Successfully Repaired' ORDER BY [BranchCode], [CategoryName],[AssetNumber]";
                var sqlDataAapter = new SqlDataAdapter(queryString, sqlConnection);
                sqlDataAapter.Fill(dataset);
            }
            return dataset;
        }

        public DataSet GetBranchDeviceDetailsItemRelocatedDataSet(string[] branches)
        {
            var dataset = new DataSet();
            var branchList = string.Join(",", branches.Select(item => "'" + item + "'"));
            using (var sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["NonCoreConnectionString"].ConnectionString))
            {
                string queryString = $"SELECT ROW_NUMBER() OVER(PARTITION BY [BranchCode] Order By [BranchCode]) AS [Row] ,[BranchCode],[BranchOwnerShip] ,[CategoryName] ,[AssetNumber] ,[AssetName] ,[SerialNumber] ,[Remark] ,[DateOfAssetTransfer] ,[AssetToRelocateTo] ,[UserId] FROM [dbo].[uv_GetAssetRelocationByBranch] WHERE BranchCode IN ({branchList}) ORDER BY BranchCode, CategoryName, AssetNumber, SerialNumber";
                var sqlDataAapter = new SqlDataAdapter(queryString, sqlConnection);
                sqlDataAapter.Fill(dataset);
            }
            return dataset;
        }

        public DataSet GetStockStatusDataSet(string[] categories)
        {
            var dataset = new DataSet();
            var searchCats = string.Join(",", categories.Select(item => "'" + item + "'"));
            using (var sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["NonCoreConnectionString"].ConnectionString))
            {
                string queryString = $"SELECT CategoryName,SerialNumber,AssetNumber,AssetName,AssetStatus,DateRegistered,Vendor,Remark FROM [dbo].[uv_GetAssetByItemReportRegistered] WHERE CategoryName IN ({searchCats})";
                var sqlDataAapter = new SqlDataAdapter(queryString, sqlConnection);
                sqlDataAapter.Fill(dataset);
            }
            return dataset;
        }

        public IList<ItemAtPpl> LoadPplItems(string[] branchCodes)
        {
            var items = new List<ItemAtPpl>();

            //var connString = @"Data Source=10.1.2.217;Initial Catalog=oal_dbo_repl;Application Name=posentt;User Id=bizadm;password=bizadm123";
            //var connString = @"Data Source=10.1.3.182;Initial Catalog=PittisNonCore;Application Name=posentt;User Id=sa;password=p@ssword12";
            var connString = ConfigurationManager.ConnectionStrings["NonCoreConnectionString"].ConnectionString;
            var conn = new SqlConnection(connString);
            var code = string.Empty;
            var stringArray = string.Join(",", branchCodes.Select(item => "'" + item + "'"));
            var sql = $"SELECT BranchName, AssetNumber, CategoryName, AssetName, COUNT(*) AS Total FROM  dbo.uv_GetAssetByItemReport WHERE BranchCode IN ({stringArray}) GROUP BY BranchName, AssetNumber, CategoryName, AssetName ORDER BY BranchName, CategoryName, AssetName";
           
            using (var cmd = new SqlCommand(sql, conn))
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        items.Add(new ItemAtPpl { BranchName = reader.GetString(0), ProductNo = reader.GetString(1), AssetCategory = reader.GetString(2), AssetName = reader.GetString(3), Quantity = reader.GetInt32(4) });
                    }
                }
            }

            var index = 0;
            var key = string.Empty;
            foreach (var item in items)
            {
                if (key == item.BranchName)
                {
                    index++;
                }
                else
                {
                    index = 1;
                }
                item.No = index;
                key = item.BranchName;
            }
            return items;
        }

        public IList<ItemAtPpl> LoadPplOutRepairItems(string[] branchCodes)
        {
            var items = new List<ItemAtPpl>();

            var connString = ConfigurationManager.ConnectionStrings["NonCoreConnectionString"].ConnectionString;
            var conn = new SqlConnection(connString);
            var code = string.Empty;
            var stringArray = string.Join(",", branchCodes.Select(item => "'" + item + "'"));
            var sql = $"SELECT [BranchOwnerShip], [AssetNumber], [CategoryName], [AssetName], [DateSendDeviceToVendor], COUNT(*) AS Total FROM [dbo].[uv_GetAssetRepairDetailByBranch] WHERE BranchCode IN ({stringArray}) GROUP BY BranchCode, BranchOwnerShip,AssetNumber,CategoryName,AssetName,DateSendDeviceToVendor,Ownership ORDER BY Ownership,AssetNumber,DateSendDeviceToVendor";

            using (var cmd = new SqlCommand(sql, conn))
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        items.Add(new ItemAtPpl { BranchName = reader.GetString(0), ProductNo = reader.GetString(1), AssetCategory = reader.GetString(2), AssetName = reader.GetString(3), DateSendRepair = reader.GetDateTime(4), Quantity = reader.GetInt32(5) });
                    }
                }
            }

            var index = 0;
            var key = string.Empty;
            foreach (var item in items)
            {
                if (key == item.BranchName)
                {
                    index++;
                }
                else
                {
                    index = 1;
                }
                item.No = index;
                key = item.BranchName;
            }
            return items;
        }

        public IList<string> GetAssetCategoryList()
        {
            var list = new List<string>();

            var connString = ConfigurationManager.ConnectionStrings["NonCoreConnectionString"].ConnectionString;
            var conn = new SqlConnection(connString);
            var sql = $"SELECT [CategoryName] FROM [dbo].[AIMSCategory] ORDER BY [CategoryName]";

            using (var cmd = new SqlCommand(sql, conn))
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        list.Add(reader.GetString(0));
                    }
                }
            }
            return list;

        }

        public IList<Branch> GetBranches(bool addHq)
        {
            var list = new List<Branch>();
            if (addHq) list.Add(new Branch { Code = "HQ", Name = "Headquarters" });

            var connString = ConfigurationManager.ConnectionStrings["NonCoreConnectionString"].ConnectionString;
            var conn = new SqlConnection(connString);
            var sql = $"SELECT [BranchCode],[BranchName] FROM [dbo].[BROMBranchProfile] WHERE [BranchTypeId] IN ('B50372AD-E605-4729-872D-4D1C74F6FE6E','1BE640C1-F0A8-4E7C-9D32-2967960E207B','033C76A9-B0F3-4726-8F40-2C3345E3E0A6','9A8413D0-E3DE-4B6C-9853-A74300D539C6') ORDER BY [BranchCode]";

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