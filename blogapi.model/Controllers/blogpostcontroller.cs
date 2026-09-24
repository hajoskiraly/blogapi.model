using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Diagnostics.Eventing.Reader;
using blogapi.model.Models;
using blogapi.model.Models.DTOs;

namespace blogapi.model.Controllers
{
    [Route("blogposts")]
    [ApiController]

    public class blogpostcontroller : ControllerBase
    {
        public string ConectionString = "server=localhost;database=blogpost;";


        [HttpGet("all")]
        public object GetAllBlogPosts()
        {
            List<blogpost> blogPosts = new List<blogpost>();
            var connector = new MySqlConnection(ConectionString);
            connector.Open();
            string sql = @"SELECT * FROM blogpost";
            var cmd = new MySqlCommand(sql, connector);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var blogPost = new blogpost
                {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Content = reader.GetString(2),
                    postTime = reader.GetDateTime(3),
                    updateTime = reader.GetDateTime(4),
                    blogId = reader.GetInt32(5)
                };
                blogPosts.Add(blogPost);
            }
            connector.Close();
            return new
            {
                message = "sikeres lekeres",
                result = blogPosts
            };

        }
        [HttpPost("register")]
        public object CreateBlogPost(addNewBlogPostDto blogPost)
        {
            var connector = new MySqlConnection(ConectionString);
            connector.Open();
            string sql = @"INSERT INTO blogpost (Title, Content, postTime, ) VALUES (@Title, @Content, @postTimeS)";
            var cmd = new MySqlCommand(sql, connector);
            cmd.Parameters.AddWithValue("@Title", blogPost.Title);
            cmd.Parameters.AddWithValue("@Content", blogPost.Content);
            cmd.Parameters.AddWithValue("@postTime", blogPost.postTime);
            cmd.ExecuteNonQuery();
            connector.Close();
            return new
            {
                message = "sikeres letrehozas"
            };
        }

        [HttpDelete("delete")]
        public object DeleteBlogPost(DeleteBlogPostDto deleteDto)
        {
            var connector = new MySqlConnection(ConectionString);
            connector.Open();
            string sql = @"DELETE FROM blogpost WHERE Id = @Id AND blogId = @blogId";
            var cmd = new MySqlCommand(sql, connector);
            cmd.Parameters.AddWithValue("@Id", deleteDto.id);
            cmd.Parameters.AddWithValue("@blogId", deleteDto.blogId);
            cmd.ExecuteNonQuery();
            connector.Close();
            return new
            {
                message = "sikeres torles"
            };
        }

        [HttpPost("login")]

        public object LoginBlogPost(LoginBlogPostDto loginDto)
        {
            var connector = new MySqlConnection(ConectionString);
            connector.Open();
            string sql = @"SELECT * FROM blogpost WHERE Title = @Title AND Content = @Content";
            var cmd = new MySqlCommand(sql, connector);
            cmd.Parameters.AddWithValue("@Title", loginDto.Title);
            cmd.Parameters.AddWithValue("@Content", loginDto.Content);
            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                var blogPost = new blogpost
                {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Content = reader.GetString(2),
                    postTime = reader.GetDateTime(3),
                    updateTime = reader.GetDateTime(4),
                    blogId = reader.GetInt32(5)
                };
                connector.Close();
                return new
                {
                    message = "sikeres bejelentkezes",
                    result = blogPost
                };
            }
            else
            {
                connector.Close();
                return new
                {
                    message = "sikertelen bejelentkezes"
                };
            }
        }

        [HttpGet("bybloggernameandemail")]


        public object GetBlogPostsByBloggerNameAndEmail(string name, string email)
        {
            List<blogpost> blogPosts = new List<blogpost>();
            var connector = new MySqlConnection(ConectionString);
            connector.Open();
            string sql = @"SELECT bp.* FROM blogpost bp JOIN blogger b ON bp.blogId = b.Id WHERE b.Name = @Name AND b.Email = @Email";
            var cmd = new MySqlCommand(sql, connector);
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Email", email);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var blogPost = new blogpost
                {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Content = reader.GetString(2),
                    postTime = reader.GetDateTime(3),
                    updateTime = reader.GetDateTime(4),
                    blogId = reader.GetInt32(5)
                };
                blogPosts.Add(blogPost);
            }
            connector.Close();
            return new
            {
                message = "sikeres lekeres",
                result = blogPosts
            };

        }

        [HttpGet("bybloggernamegetposttitleandcontent")]

        public object GetBlogPostTitlesAndContentByBloggerName(string name)
        {
            List<object> blogPostInfo = new List<object>();
            var connector = new MySqlConnection(ConectionString);
            connector.Open();
            string sql = @"SELECT bp.Title, bp.Content FROM blogpost bp JOIN blogger b ON bp.blogId = b.Id WHERE b.Name = @Name";
            var cmd = new MySqlCommand(sql, connector);
            cmd.Parameters.AddWithValue("@Name", name);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                blogPostInfo.Add(new
                {
                    Title = reader.GetString(0),
                    Content = reader.GetString(1)
                });
            }
            connector.Close();
            return new
            {
                message = "sikeres lekeres",
                result = blogPostInfo
            };
        }

        [HttpGet("getallposts")]
        public object GetAllposts()
        {
            List<blogpost> blogPosts = new List<blogpost>();
            var connector = new MySqlConnection(ConectionString);
            connector.Open();
            string sql = @"SELECT * FROM blogpost";
            var cmd = new MySqlCommand(sql, connector);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var blogPost = new blogpost
                {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Content = reader.GetString(2),
                    postTime = reader.GetDateTime(3),
                    updateTime = reader.GetDateTime(4),
                    blogId = reader.GetInt32(5)
                };
                blogPosts.Add(blogPost);
            }
            connector.Close();
            return new
            {
                message = "sikeres lekeres",
                result = blogPosts
            };
        }

        [HttpGet("bloggerhowmanyposts")]

        public object GetBloggerHowManyPosts(string name, string email)
        {
            var connector = new MySqlConnection(ConectionString);
            connector.Open();
            string sql = @"SELECT COUNT(*) FROM blogpost bp JOIN blogger b ON bp.blogId = b.Id WHERE b.Name = @Name AND b.Email = @Email";
            var cmd = new MySqlCommand(sql, connector);
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Email", email);
            var count = Convert.ToInt32(cmd.ExecuteScalar());
            connector.Close();
            return new
            {
                message = "sikeres lekeres",
                result = count
            };
        }
    }
}