using System;
using System.Linq.Expressions;

namespace Emit.RuleEngine.Rules
{
    /// <summary>
    /// Represents validation rule
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValidationRule<T>
    {
        /// <summary>
        /// Validation expression that must be fullfilled
        /// </summary>
        /// <returns></returns>
        Expression<Func<T, bool>> ValidationExpression { get; }

        /// <summary>
        /// Execution result
        /// </summary>
        bool Result { get; }

        /// <summary>
        /// Execute the defined expression
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Execute(T entity);
    }
}
