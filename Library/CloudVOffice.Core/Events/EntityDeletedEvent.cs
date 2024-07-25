namespace CloudVOffice.Core.Events
{
    public partial class EntityDeletedEvent<T> where T : BaseEntity
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="entity">Entity</param>
        public EntityDeletedEvent(T entity)
        {
            Entity = entity;
        }

        /// <summary>
        /// Entity
        /// </summary>
        public T Entity { get; }
    }
}
