using System;
using System.Collections.Generic;
using System.Linq;
using Emit.RuleEngine.Rules;

namespace Emit.RuleEngine.Entities
{
    /// <summary>
    /// Represent validation result of the rules execution
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ValidationResult<T> : IValidationResult
    {
        public ValidationResult(bool result, List<IValidationRule<T>> falseReturnValidationRules = null)
        {
            Result = result;
            FalseReturnValidationRules = falseReturnValidationRules ?? new List<IValidationRule<T>>();
        }

        public List<IValidationRule<T>> FalseReturnValidationRules { get; protected set; }
        public bool Result { get; protected set; }

        /// <summary>
        /// Return the types of the failed rules
        /// </summary>
        /// <returns></returns>
        public List<Type> GetTypesFailedRulesTypes()
        {
            return FalseReturnValidationRules.Select(x => x.GetType()).ToList();
        }
    }
}
