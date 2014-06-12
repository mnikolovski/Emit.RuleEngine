using System.Collections.Generic;

namespace Emit.RuleEngine.Rules
{
    /// <summary>
    /// Contract defining containers for the rules
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValidationRuleContainer<T>
    {
        /// <summary>
        /// Register a rule
        /// </summary>
        /// <param name="validationRule"></param>
        void AddValidationRule(IValidationRule<T> validationRule);

        /// <summary>
        /// Return all validation rules registered in the container
        /// </summary>
        List<IValidationRule<T>> GetValidationRules();
    }
}
