using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartActS.DataModels;
namespace SmartActS.DataModels
{
   
    public class CategoryContext
    {
        private SmartActSModel db = new SmartActSModel();

        public IEnumerable<Category> GetCategoryList()
        {
            string query = "SELECT * FROM Category order by CategoryName DESC";
            var result = db.Database.SqlQuery<Category>(query);
            return result;
        }
    }

}