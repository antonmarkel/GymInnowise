namespace GymInnowise.SectionService.Logic.Features.Mappers.Interfaces
{
    public interface IMapper<TSource, TDestiny>
    {
        public TDestiny Map(TSource source);
    }
}
