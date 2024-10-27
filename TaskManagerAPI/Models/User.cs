using System.Collections.Generic;

namespace TaskManagerAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public  Address? Address { get; set; }  //Navigations Reference
        public List<TaskItem>? Tasks { get; set; }  //Navigations Reference
    }
}
