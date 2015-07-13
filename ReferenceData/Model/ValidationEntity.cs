using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ReferenceData
{
    public class ValidationEntity<TEntity> : NotificationEntity, IDataErrorInfo where TEntity : ValidationEntity<TEntity>
    {
        private static Dictionary<string, Binder<TEntity>> ruleMap;

        private List<string> errors;

        static ValidationEntity()
        {
            ruleMap = new Dictionary<string, Binder<TEntity>>();
        }

        public ValidationEntity()
        {
            errors = new List<string>();
        }

        public static void RegisterValidator<TProperty>(Expression<Func<TEntity, TProperty>> property, Func<TEntity, bool> rule, string message)
        {
            string propertyName = GetPropertyName(property);
            ruleMap[propertyName] = new Binder<TEntity>(rule, message);
        }

        protected static string GetPropertyName<TProperty>(Expression<Func<TEntity, TProperty>> expression)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            Expression body = expression.Body;
            MemberExpression memberExpression = body as MemberExpression;
            if (memberExpression == null)
            {
                memberExpression = (MemberExpression)((UnaryExpression)body).Operand;
            }

            return memberExpression.Member.Name;
        }

        private class Binder<T>
        {
            private readonly Func<T, bool> rule;
            private readonly string errorMessage;

            public Binder(Func<T, bool> rule, string errorMessage)
	        {
                this.rule = rule;
                this.errorMessage = errorMessage;
	        }

            public Func<T, bool> Rule { get { return rule; } }
            public string ErrorMessage { get { return errorMessage; } }
        }

        public string Error
        {
            get
            {
                
                return string.Join("\n", errors);
            }
        }

        public string this[string columnName]
        {
            get
            {
                if (ruleMap.ContainsKey(columnName))
                {
                    if (ruleMap[columnName].Rule((TEntity)this))
                        return string.Empty;

                    string errorMessage = ruleMap[columnName].ErrorMessage;
                    return errorMessage;
                }
                return null;
            }
        }


    }
}
