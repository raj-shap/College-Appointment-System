﻿namespace College_Appointment_System.Models
{
    public class Student
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public Guid Role {  get; set; }
        public string Password { get; set; }
    }
}
