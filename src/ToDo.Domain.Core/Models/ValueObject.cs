namespace ToDo.Domain.Core.Models
{
    public abstract class ValueObject<TEntity> where TEntity : ValueObject<TEntity>
    {
        public override bool Equals(object obj)
        {
            var valueObject = obj as TEntity;
            return EqualsCore(valueObject);
        }

        protected abstract bool EqualsCore(TEntity other);

        public override int GetHashCode()
        {
            return GetHashCodeCore();
        }

        protected abstract int GetHashCodeCore();

        public static bool operator ==(ValueObject<TEntity> a, ValueObject<TEntity> b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(ValueObject<TEntity> a, ValueObject<TEntity> b)
        {
            return !(a == b);
        }
    }
}
