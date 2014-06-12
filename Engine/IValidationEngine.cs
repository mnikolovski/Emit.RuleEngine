using System;
using System.Collections.Generic;
using Emit.RuleEngine.Entities;
using Emit.RuleEngine.Enums;
using Emit.RuleEngine.Rules;

namespace Emit.RuleEngine.Engine
{
    /// <summary>
    /// Contract defining containers for the rules
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValidationEngine<T>
    {
        /// <summary>
        /// Represent the exeution result
        /// </summary>
        ValidationResult<T> ExecutionResult { get; set; }

        /// <summary>
        /// Register a validation container before execution
        /// </summary>
        /// <param name="container"></param>
        IValidationEngine<T> Register(IValidationRuleContainer<T> container);

        /// <summary>
        /// Register a validation rule
        /// </summary>
        /// <param name="validationRule"></param>
        IValidationEngine<T> Register(IValidationRule<T> validationRule);
        
        /// <summary>
        /// Exeute the registered validation rules and validation rule 
        /// containers on the provided entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        IValidationEngine<T> Execute(T entity);

        /// <summary>
        /// Exeute the registered validation rules and validation rule 
        /// containers on the provided entity
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        IValidationEngine<T> ExecuteIf(T entity, Func<T, bool> condition);

        /// <summary>
        /// Action to be executed if the result is false
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        void OnFalse(Action<ValidationResult<T>> action);

        /// <summary>
        /// Return validation rule by execution status
        /// </summary>
        /// <param name="executionStatus"></param>
        /// <returns></returns>
        List<IValidationRule<T>> GetRulesByStatus(ValidationRuleExecutionStatus executionStatus);

        /// <summary>
        /// Restore the validation engine to initial setup state
        /// </summary>
        IValidationEngine<T> Restore();

        /// <summary>
        /// Remove registered rules and clears executed data
        /// </summary>
        IValidationEngine<T> ReSetup();
    }
}
