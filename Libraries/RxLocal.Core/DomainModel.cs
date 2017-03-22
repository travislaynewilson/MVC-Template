using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;

namespace RxLocal.Core
{
    /// <summary>
    /// Base class for domain models
    /// </summary>
    public abstract partial class DomainModel
    {
        protected DomainModel()
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

        public virtual void BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
        }

        /// <summary>
        /// Use this property to store any custom value for your models. 
        /// </summary>
        public Dictionary<string, object> CustomProperties { get; set; }
        
        public override bool Equals(object obj)
        {
            return Equals(obj as DomainModel);
        }
        public bool Equals(DomainModel other)
        {
            if (other == null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return false;
        }
        public static bool operator ==(DomainModel x, DomainModel y)
        {
            return Equals(x, y);
        }
        public static bool operator !=(DomainModel x, DomainModel y)
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
