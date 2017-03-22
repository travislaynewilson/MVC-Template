using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace RxLocal.Data
{
    /// <summary>
    /// Base class for entities
    /// </summary>
    public abstract partial class BaseEntity
    {
        protected BaseEntity()
        {
            CustomProperties = new Dictionary<string, object>();
            PostInitialize();
        }

        /// <summary>
        /// Developers can override this method in custom partial classes
        /// in order to add some custom initialization code to constructors
        /// </summary>
        protected virtual void PostInitialize()
        {

        }

        /// <summary>
        /// Use this property to store any custom value for your models. 
        /// </summary>
        public Dictionary<string, object> CustomProperties { get; set; }
        
        public override bool Equals(object obj)
        {
            return Equals(obj as BaseEntity);
        }
        public bool Equals(BaseEntity other)
        {
            if (other == null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return false;
        }
        public static bool operator ==(BaseEntity x, BaseEntity y)
        {
            return Equals(x, y);
        }
        public static bool operator !=(BaseEntity x, BaseEntity y)
        {
            return !(x == y);
        }

        #region Helpers
        protected static int GenerateHashCode(params object[] values)
        {
            var sb = new StringBuilder();
            var sha = new SHA256Managed();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(string.Join("|", values)));
            foreach (var b in bytes)
                sb.Append(b.ToString("X2"));

            return sb.ToString().GetHashCode();
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion
    }
}
