# OptionalSharp
[![Build status](https://ci.appveyor.com/api/projects/status/u95y7i721ngdo35b?svg=true)](https://ci.appveyor.com/project/GregRos/optionalsharp)
[![Code Coverage](https://codecov.io/gh/GregRos/OptionalSharp/branch/master/graph/badge.svg)]

This library introduces a powerful, yet simple optional type to C# that can be consumed from other languages too.

## Features

### It's a struct
The `Optional<T>` type is a `struct` with the default state of `None` for its type. This means that an uninitialized `Optional<T>` value is still a valid object, and that you can never get a `NullReferenceException` while manipulating it.

Its default value is `None`.

### You can use it as an optional parameter
Because it's a struct with a valid default value, you can use it in parameter lists as an optional parameter, albeit in a rather wordy fashion:

	public void Example(Optional<int> opt = default(Optional<int>)) {
		if (opt.IsNone) {
			//...
		}
	}

### The `T` can be abstracted over
Every `Optional<T>` implements the interface `IAnyOptional`. This interface unites all optional types, and enables some interesting behavior.

### It supports equality members and operators
It's meaningfully equatable to any and all of the following:

1. Other `Optional<T>` objects.
2. `T` objects.
3. Any `IAnyOptional` object, such as `Optional<S>` for another type `S`.

This is supported through `IEquatable` interface implementations, overriding standard equality members, and overloading equality operators.

Because it's a struct, `None` values can be used as keys for a dictionary.
### Enhanced debugging experience
The `Optional<T>` type supports a number of features that allow a convenient debugging experience.

### Better error messages
`Optional` values throw exceptions with more meaningful information than `NullReferenceExceptions` and allow you to identify the source of the error more quickly.

### Tons of useful transformations
`Optional<T>` supports many useful and powerful transformations that allow you to manipulate it in ways that make it seem almost like first-class element of the language.

Among the useful things you can do to it:

1. The `opt.Map(func)` method serves a similar role to the `?.` operator. It lets you transform the underlying value, if it exists. If it doesn't exist, it remains `None` and the function isn't applied. The value returned by the mapping function can also be an `Optional`, in which case the result is flattened.

2. The `opt.Flatten()` set of overloads allows you to convert certain nested structures into a single `Optional<T>` value.

	Examples include, `Optional<Optional<T>>`, `Optional<T?>`, and more.

3. `opt.Cast<S>` operator, similar to a manual cast, but unrestricted in terms of type. 

4. `opt.Else(v)` method, which is similar to the `??` operator.

## Advanced Features

### Special support for `Optional<Task<T>>`
in C# 7, an `Optional<Task<T>>` has a `GetAwaiter` extension method, which allows it to be the target of an `await` command.

When awaited, `Optional<Task<T>>` returns `Optional<T>`. If the underlying task exists, its result is returned as `Some`. Otherwise, `None` is returned synchronously.

