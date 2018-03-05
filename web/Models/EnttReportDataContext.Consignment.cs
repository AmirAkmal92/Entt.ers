using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Entt.Ers.Models
{
    public partial class EnttReportDataContext
    {
        public async Task<Acceptance> GetConsignmentInfoById(string consignmentNo)
        {
            Acceptance acceptance = null;
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EnttConnectionString"].ConnectionString))
            using (var cmd = new SqlCommand("[Entt].[usp_search_consignment]", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@connote", SqlDbType.NVarChar, 50).Value = consignmentNo;

                await conn.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection))
                {
                    while (await reader.ReadAsync())
                    {
                        acceptance = new Acceptance
                        {
                            ConsignmentNo = reader["ConsignmentNo"].ToNullableString(),
                            DateTime = reader["DateTime"].ToDateTime(),
                            ProductType = reader["ProductType"].ToNullableString(),
                            ProductTypeDesc = reader["ProductTypeDescription"].ToNullableString(),
                            ItemCategory = reader["ItemCategory"].ToNullableString(),
                            ItemCategoryDesc = reader["ItemCategoryDescription"].ToNullableString(),
                            CourierId = reader["CourierId"].ToNullableString(),
                            LocationId = reader["LocationId"].ToNullableString(),
                            LocationName = reader["LocationName"].ToNullableString(),
                            Weight = reader["Weight"].ToDecimal(),
                            Width = reader["Width"].ToDecimal(),
                            Height = reader["Height"].ToDecimal(),
                            Length = reader["Length"].ToDecimal(),
                            Comment = reader["Comment"].ToNullableString(),
                            ScannerId = reader["ScannerId"].ToNullableString(),
                            CreatedDate = reader["CreatedDate"].ToDateTime()
                        };
                    }
                }
            }
            return acceptance;
        }
    }
}