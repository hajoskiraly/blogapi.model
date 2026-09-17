using blogapi.model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace blogapi.model.Controllers
{
    [Route("bloggers")]
    [ApiController]
    public class bloggercontroller : ControllerBase
    {

        public string ConectionString = "server=localhost;database=blog;uid=root;password=;";
        [HttpGet]
        public object GetAllBlogger()
        {

            List<Blogger> bloggers = new List<Blogger>();
            var connector = new MySqlConnection(ConectionString);
            connector.Open();

            string sql = @"SELECT * FROM blogger";

            var cmd = new MySqlCommand(sql, connector);

            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var skibidi = new Blogger
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Email = reader.GetString(2),
                    Age = reader.GetInt32(3),
                    Password = reader.GetString(4),
                    RegistrationTime = reader.GetDateTime(5)

                };
                bloggers.Add(skibidi);
            }
            connector.Close();

            return new
            {
                message = "sikeres lekerdezes", result = bloggers
            
            };
        }
    }
}
