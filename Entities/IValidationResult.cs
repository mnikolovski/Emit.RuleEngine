using System;
using System.Collections.Generic;

namespace Emit.RuleEngine.Entities
{
    public interface IValidationResult
    {
        /// <summary>
        /// Result of the validation
        /// </summary>
        bool Result { get; }

        /// <summary>
        /// Return the types of the failed rules
        /// </summary>
        /// <returns></returns>
        List<Type> GetTypesFailedRulesTypes();
    }
}
