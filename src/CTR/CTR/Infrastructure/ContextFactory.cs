namespace CTR.Infrastructure
{
    public static class ContextFactory
    {
        public static CTRContext Create()
        {
            return new CTRContext();
        }
    }
}
