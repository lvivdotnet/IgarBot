namespace IgarBot
{
    public interface IRepository<TIgar> where TIgar : Igar
    {
        TIgar GetIgar();
    }
}
