
namespace Butter.Web.Models
{
    public class UserModelForm
    {

        public int UserId { get; set; }
    
        public string NickName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public DateTime BirthDate { get; set; }

        public string Town { get; set; }

        public string Genre { get; set; }
    }
}
