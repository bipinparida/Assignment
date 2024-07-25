using System.Linq.Expressions;

namespace CloudVOffice.Data.Repository
{
    public interface ISqlRepository<T> where T : class
    {
        List<T> SelectAll();
        List<T> SelectAllByClause(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");
        T SelectById(object id);
        T Insert(T obj);
        T Update(T obj);
        T Delete(object id);
    }
}
