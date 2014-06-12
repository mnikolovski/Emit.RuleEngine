# What is Emit.RuleEngine?

Emit.RuleEngine is a simple yet powerful rule engine that embrace the specification pattern to validate input data.

Functionalities:
- Register validation rule
- Register validation container
- Execute registered rules against input
- Execute If – execute registered rules if condition is fulfilled
- OnFalse – action to be executed if the validated input is not valid
- Get rules by status - to provide information of successfully v.s unsuccessful rules
- Restore – restore the validation engine to original setup
- Resetup – remove registered rules and clears executed data

## Usage with specifications
```csharp
public int CreateUser(User user){
    var engine = RuleEvaluator<User>.New();
    engine.Register(new IsValidEmailSpecification())
          .Register(new HasAlreadyRegisteredWithEmailSpecification())
          .Register(new IsValidUsernameSpecification())
          .Register(new IsUsernameInUseSpecification())
          .Register(new IsValidPassword())
          .Register(new ArePasswordsMatchingSpecification())
          .Execute(user);
    if (!engine.ExecutionResult.Result)
    {
        // check the results and create proper exception
    }
    //...
    var id = UserRepository.CreateUser(user);
    return id;
}
```
## Usage with specification containers
```csharp
/// <summary>
/// Create new user in the system
/// </summary>
/// <param name="user">User pending for registration</param>
/// <returns></returns>
public int CreateUser(User user){
    var engine = RuleEvaluator<User>.New();
    engine.Register(new NewUserValidationContainer())
          .Execute(user);
    if (!engine.ExecutionResult.Result)
    {
        // check the results and create proper exception
    }
    //...
    var id = UserRepository.CreateUser(user);
    return id;
}
```
