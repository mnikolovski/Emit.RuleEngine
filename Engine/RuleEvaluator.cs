using System;
using System.Collections.Generic;
using Emit.RuleEngine.Entities;
using Emit.RuleEngine.Enums;
using Emit.RuleEngine.Rules;

namespace Emit.RuleEngine.Engine
{
    public class RuleEvaluator<T> : IValidationEngine<T>
    {
        /// <summary>
        /// Private constructor
        /// </summary>
        private RuleEvaluator()
        {
            ValidationRulesForExecutution = new List<IValidationRule<T>>();
        }

        /// <summary>
        /// Represent all registered validation rules
        /// </summary>
        private List<IValidationRule<T>> ValidationRulesForExecutution { get; set; }

        /// <summary>
        /// Represent all registered validation rules
        /// </summary>
        private List<IValidationRule<T>> FalseReturnValidationRules { get; set; }

        /// <summary>
        /// Represent all registered validation rules
        /// </summary>
        private List<IValidationRule<T>> TrueReturnValidationRules { get; set; }

        /// <summary>
        /// Represent the exeution result
        /// </summary>
        public ValidationResult<T> ExecutionResult { get; set; }

        /// <summary>
        /// Restore the validation engine to initial setup state
        /// </summary>
        public IValidationEngine<T> Restore()
        {
            this.TrueReturnValidationRules = new List<IValidationRule<T>>();
            this.FalseReturnValidationRules = new List<IValidationRule<T>>();
            return this;
        }

        /// <summary>
        /// Create new instance of the rule engine
        /// </summary>
        /// <returns></returns>
        public IValidationEngine<T> ReSetup()
        {
            this.TrueReturnValidationRules = new List<IValidationRule<T>>();
            this.FalseReturnValidationRules = new List<IValidationRule<T>>();
            this.ValidationRulesForExecutution = new List<IValidationRule<T>>();
            return this;
        }

        #region Implementation of IValidationEngine<T>

        /// <summary>
        /// Register a validation container before execution
        /// </summary>
        /// <param name="container"></param>
        public IValidationEngine<T> Register(IValidationRuleContainer<T> container)
        {
            if (container != null)
            {
                var _validationRules = container.GetValidationRules();
                if (_validationRules != null)
                {
                    ValidationRulesForExecutution.AddRange(_validationRules);
                }
            }

            return this;
        }

        /// <summary>
        /// Register a validation rule
        /// </summary>
        /// <param name="validationRule"></param>
        public IValidationEngine<T> Register(IValidationRule<T> validationRule)
        {
            if (validationRule != null)
            {
                ValidationRulesForExecutution.Add(validationRule);
            }

            return this;
        }

        /// <summary>
        /// Exeute the registered validation rules and validation rule 
        /// containers on the provided entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IValidationEngine<T> Execute(T entity)
        {
            FalseReturnValidationRules = new List<IValidationRule<T>>();
            TrueReturnValidationRules = new List<IValidationRule<T>>();
            
            // if no rules are defined return false
            var _isValid = ValidationRulesForExecutution.Count > 0;
            try
            {
                foreach (var _validationRule in ValidationRulesForExecutution)
                {
                    bool _result;

                    try
                    {
                        _result = _validationRule.Execute(entity);
                    }
                    catch (Exception)
                    {
                        _result = false;
                    }

                    _isValid = _isValid && _result;
                    if (!_isValid)
                    {
                        FalseReturnValidationRules.Add(_validationRule);
                    }
                    else
                    {
                        TrueReturnValidationRules.Add(_validationRule);
                    }
                }
            }
            catch (Exception)
            {
                _isValid = false;
            }

            ExecutionResult = new ValidationResult<T>(_isValid, FalseReturnValidationRules);
            return this;
        }

        public IValidationEngine<T> ExecuteIf(T entity, Func<T, bool> condition)
        {
            var _result = condition.Invoke(entity);
            if (_result)
            {
                Execute(entity);
            }
            else
            {
                ExecutionResult = new ValidationResult<T>(true, new List<IValidationRule<T>>());
            }
            return this;
        }

        /// <summary>
        /// Action to be executed if the result is false
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public void OnFalse(Action<ValidationResult<T>> action)
        {
            if (!ExecutionResult.Result)
            {
                action(ExecutionResult);
            }
        }


        /// <summary>
        /// Return validation rule by execution status
        /// </summary>
        /// <param name="executionStatus"></param>
        /// <returns></returns>
        public List<IValidationRule<T>> GetRulesByStatus(ValidationRuleExecutionStatus executionStatus)
        {
            switch (executionStatus)
            {
                case ValidationRuleExecutionStatus.True:
                    return TrueReturnValidationRules;
                case ValidationRuleExecutionStatus.False:
                    return FalseReturnValidationRules;
                default:
                    return new List<IValidationRule<T>>();
            }
        }

        #endregion

        /// <summary>
        /// Create new instance of the rule engine
        /// </summary>
        /// <returns></returns>
        public static IValidationEngine<T> New()
        {
            return new RuleEvaluator<T>();
        }
    }
}
