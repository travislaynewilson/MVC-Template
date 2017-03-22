using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using RxLocal.Core.Configuration;
using RxLocal.Core.Infrastructure;
using RxLocal.Data.Entities;

namespace RxLocal.Data.Contexts
{
    public class RxLocalSyncSqlDbContext : DbContext
    {
        public RxLocalSyncSqlDbContext()
            : base(EngineContext.Current.Resolve<RxLocalConfig>().ConnectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<RxLocalSyncSqlDbContext>(null);

            modelBuilder.Entity<Customer>()
                .HasRequired(c => c.CustomerType);

            modelBuilder.Entity<Customer>()
                .HasRequired(c => c.CustomerStatus);
        }


        #region Entity Sets
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerType> CustomerTypes { get; set; }
        public virtual DbSet<CustomerStatus> CustomerStatuses { get; set; }
        #endregion

        #region Stored Procedures
        public virtual int AuthenticateCustomer(string loginName, string password)
        {
            var loginNameParameter = new SqlParameter("@LoginName", loginName);
            var passwordParameter = new SqlParameter("@Password", password);

            var response = Database.SqlQuery<AuthenticateCustomerResult>("AuthenticateCustomer @LoginName, @Password", loginNameParameter, passwordParameter)
                .FirstOrDefault();

            if (response != null)
                return response.CustomerID;

            return 0;
        }
        #endregion

        #region SQL Queries
        /// <summary>
        /// Creates a raw SQL query that will return elements of the given generic type.  The type can be any type that has properties that match the names of the columns returned from the query, or can be a simple primitive type. The type does not have to be an entity type. The results of this query are never tracked by the context even if the type of object returned is an entity type.
        /// </summary>
        /// <typeparam name="TElement">The type of object returned by the query.</typeparam>
        /// <param name="sql">The SQL query string.</param>
        /// <param name="parameters">The parameters to apply to the SQL query string.</param>
        /// <returns>Result</returns>
        public IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
        {
            return this.Database.SqlQuery<TElement>(sql, parameters);
        }

        /// <summary>
        /// Executes the given DDL/DML command against the database.
        /// </summary>
        /// <param name="sql">The command string</param>
        /// <param name="doNotEnsureTransaction">false - the transaction creation is not ensured; true - the transaction creation is ensured.</param>
        /// <param name="timeout">Timeout value, in seconds. A null value indicates that the default value of the underlying provider will be used</param>
        /// <param name="parameters">The parameters to apply to the command string.</param>
        /// <returns>The result returned by the database after executing the command.</returns>
        public int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters)
        {
            int? previousTimeout = null;
            if (timeout.HasValue)
            {
                //store previous timeout
                previousTimeout = ((IObjectContextAdapter)this).ObjectContext.CommandTimeout;
                ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = timeout;
            }

            var transactionalBehavior = doNotEnsureTransaction
                ? TransactionalBehavior.DoNotEnsureTransaction
                : TransactionalBehavior.EnsureTransaction;
            var result = this.Database.ExecuteSqlCommand(transactionalBehavior, sql, parameters);

            if (timeout.HasValue)
            {
                //Set previous timeout back
                ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = previousTimeout;
            }

            //return result
            return result;
        }
        #endregion

        #region Nested Classes
        private class AuthenticateCustomerResult
        {
            public int CustomerID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }
        #endregion
    }
}
