using blogapi.model.Models;
using blogapi.model.Models.DTOs;
using Microsoft.AspNetCore.Components.Authorization;
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
        [HttpGet("all")]
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
        [HttpGet("byId")]
        public object GetBloggerById(int id )
        {
            var connector = new MySqlConnection(ConectionString);
            connector.Open();

            string sql = @"SELECT * FROM blogger WHERE id = @id;";
            var cmd = new MySqlCommand(sql, connector);
            cmd.Parameters.AddWithValue("@id", id);

            var reader = cmd.ExecuteReader();

            reader.Read();

            var blogger = new Blogger
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Email = reader.GetString(2),
                Age = reader.GetInt32(3),
                Password = reader.GetString(4),
                RegistrationTime = reader.GetDateTime(5)
            };


            connector.Close();
            return new { message = "sikeres talalat", result = blogger };
        }

        [HttpPost("register")]

        public object AddNewBlogger(AddNewBloggerDto addnewbloggerdto)

        {
        var connector = new MySqlConnection(ConectionString);
            connector.Open();
            string sql = @"INSERT INTO `blogger`(`Name`, `Email`, `Age`, `Password`, `RegistrationTime`) VALUES (@name, @email, @age, @password, @registrationtime)";
            var cmd = new MySqlCommand(sql, connector);

            cmd.Parameters.AddWithValue("@name", addnewbloggerdto.Name);
            cmd.Parameters.AddWithValue("@email", addnewbloggerdto.Email);
            cmd.Parameters.AddWithValue("@age", addnewbloggerdto.Age);
            cmd.Parameters.AddWithValue("@password", addnewbloggerdto.Password);
            cmd.Parameters.AddWithValue("@registrationtime", DateTime.Now);
            
            cmd.ExecuteNonQuery();

            connector.Close();

            return new { message = "sikeres felvetel", result = addnewbloggerdto};
        }
        
    }
}
