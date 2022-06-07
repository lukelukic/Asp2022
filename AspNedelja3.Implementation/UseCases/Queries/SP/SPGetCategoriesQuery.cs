//using ASPNedelja3.Application.UseCases.DTO;
//using ASPNedelja3.Application.UseCases.DTO.Searches;
//using ASPNedelja3.Application.UseCases.Queries;
//using Dapper;
//using Microsoft.Data.SqlClient;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace AspNedelja3.Implementation.UseCases.Queries.SP
//{
//    public class SPGetCategoriesQuery : IGetCategoriesQuery
//    {
//        public int Id => 2;

//        public string Name => "SP Search Categories";

//        public string Description => "";

//        public IEnumerable<CategoryDto> Execute(BaseSearch search)
//        {
//            var connection = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=AspVezbe_2;Integrated Security=True");

//            var result = connection.Query<CategoryDto>("SearchCategories", 
//                                           new { search.Keyword }, 
//                                           commandType: CommandType.StoredProcedure);
//            return result;
//        }
//    }
//}
