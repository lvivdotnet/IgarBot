namespace IgarBot
{
    public sealed class SqlRepository<TIgar> : IRepository<Igar>
    {
        
        public Igar GetIgar()
        {
            // TODO: AbstractIgarFactory
            return Igar.IgarInstance;
        }
    }
}
