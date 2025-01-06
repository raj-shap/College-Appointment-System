namespace College_Appointment_System.Helpers
{
    public static class IdGenerator
    {
        public static string GenerateUniqueId()
        {
            Guid guid = Guid.NewGuid();
            return guid.ToString("N");
        }
    }
}
