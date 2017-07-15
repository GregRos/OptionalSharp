# Optional#
[![Build status](https://ci.appveyor.com/api/projects/status/u95y7i721ngdo35b?svg=true)](https://ci.appveyor.com/project/GregRos/optionalsharp)
[![Code Coverage](https://codecov.io/gh/GregRos/OptionalSharp/branch/master/graph/badge.svg)](https://codecov.io/gh/GregRos/OptionalSharp)
[![NuGet](https://img.shields.io/nuget/v/OptionalSharp.svg)](https://www.nuget.org/packages/OptionalSharp)


Optional# implements what's known as an optional type: A type that represents a value that may not exist. You can use it as the return type of a function that may or may not return an int, such as:

	Optional<T> TryFirst(Func<T, bool> predicate) 
	{
		//...
	}

Or as the parameter type of a function that accepts an optional value of type `T`:

	T FirstOrDefault(Optional<T> @default = default(Optional<T>))
	{
		//...
	}

Or as the type of a member that may or may not have a value:

	public class Creature
	{
		public Optional<bool> IsEnemy {get;}
	}


Now, it's true that reference types *can* be null to represent a missing value, and nullable value types are also a thing. But `null` isn't very good at that job (or any job, really).

## Why not null?
If you're not interested in these rationalizations, you can skip ahead.

### `null` is ambiguous
All reference types are nullable, and technically `null` is a valid value for them. So getting `null` is totally ambiguous: it may indicate that the value exists and it is `null`, that the `null` got in there due to a bug, or that it was used explicitly to indicate a missing value.

Similarly, there is no way to indicate in a function signature that a parameter or return value is optional.

### `null` doesn't work with generics
Say you have a generic method and you want it to return an optional value of type `T` (that is, the value may or may not exist). You might write it like this:

	public T TryGetValue<T>(string key) 
	{
		return null;
	}

That doesn't work though, since regular structs can't have the value `null`.

So you might write it like this:

	public T? TryGetValue<T>(string key) 
	{
		return null;
	}

Nope, doesn't work either. In that case, `T` has to be a struct because only structs can be `Nullable<T>`.

If you *do* have a proper optional type, you can write stuff like this:

	public Optional<T> TryGetValue<T>(string key) 
	{
		return Optional.None();
	}

### `null` breaks type safety
`null` isn't really a proper value for any type (except arguably for nullable value types) because it doesn't support its methods, not even basic ones like `GetHashCode`. When you write this:

	object o = null;

You're going behind the type system's back and calling an `object` something that just isn't one.

You might as well give up and just use `dynamic`.

### `null`s have a tendency to spread
Even if you use `null` as an optional value, `null` values have a tendency to spread. Some of the reasons involve the `as` and `?.` operators, which aren't always the best thing:

	public string MakeIntoString(object maybeNull) 
	{
		//return maybeNull.ToString();
		//ugh, NullReferenceException! Fix it real quick:
		return maybeNull?.ToString();
		//whew
	}

The above code doesn't really solve the problem, it just passes it along to the next consumer who'll also have to wonder how a `null` got there. 

### `null` has no type information
`null` is just `null`. It's something that really doesn't exist. It doesn't have a type, even though it exists under the illusion of one.

When your program crashes because of a `NullReferenceException`, you can't tell the type of the missing value at runtime nor can you see any relevant debug information. You just see the word `null`. Good luck figuring out why it's there and what it's supposed to be. This is especially important if you're working on `object`s.

## Definitions
An **Optional** is an instance of the `Optional<T>` class. It indicates a potentially missing value of type `T`, which is called the *inner type*. This potentially missing value is called the Optional's inner value.

An Optional is immutable, and can be constructed in one of two states:
1. The `Some` state, also written `Some(x)`. It wraps an inner value, which is the `x`. If we want to talk about an Optional with a particular inner type `T`, we can say it's a `Some<T>`.
2. The `None` state, which indicates a missing value. The `None` state is another instance of `Optional<T>`, so the missing value is still of a particular type. This is an implementation detail in some ways (all Nones are equal, as we'll discuss later), but it does provide more debug information. When we want to talk about `None` of a particular inner type, we can say `NoneOf<T>`.

The `Optional<T>.Value` property lets us access the inner value of the Optional. If there is no inner value, the operation throws an exception. Almost all other methods work even when called on None Optionals, though.

## The Basics of Optional#
The fundamental aspects of the Optional type provided by Optional#.

### `Optional<T>` is a `struct`
The `Optional<T>` type is a `struct`, so it can't be `null` itself.

Its uninitialized/default state is `None`. In particular, `default(Optional<T>)` returns a Optional with the inner type of `T`, in its `None` state. This enables all kinds of cool behaviors, like using it as an optional parameter:

	public void Example(Optional<int> opt = default(Optional<int>)) 
	{
		if (opt.IsNone) 
		{
			//...
		}
	}

This has a few cool consequences:

1. It supports all of its methods in all states, except those that require an inner value (e.g. the `.Value` property) `.
2. Even if it's in its `None` state, it can hold additional information, such as the inner type and a reason for the lack of a value.

### Immutability
Optionals are immutable objects that cannot be changed once created. This allows them to be safely used as keys in a dictionary or for other purposes. An Optional cannot change its state from `Some(x)` to `None`.

## The Features of Optional#
Optional# has tons of cool features that should cover almost every use-case involving manipulating optional values by themselves.

### Untyped `None` value
In additional to regular typed Optionals that may indicate a missing value, we use another struct to indicate a missing value where the type of the value is unknown or unspecified. Its purpose is to make syntax more concise in some cases.

This object can be accessed through `Optional.None()`. It is of the type `ImplicitNoneValue`, which is normally hidden from the IDE using a collection of attributes (the reason being, you should never need to refer to this type explicitly and seeing it will only confuse users).

This object is implicitly convertible to any `Optional<T>`, resulting in a valid Optional indicating a missing value. This allows code like this:

	Optional<int> x = Optional.None(); //implicit conversion
	Optional<string> y = Optional.None();

That's also the main reason for its existence.

An `ImplicitNoneValue` implements only them main methods available to `Optional<T>`, but it behaves like a valid Optional when used polymorphically through the interface `IAnyOptional`. It is also _equal_ to a `None` value of any type.

It can be seen as the equivalent of `IEnumerable` as compared to `IEnumerable<T>`, with the added restriction that it can only be empty.

Like other Optionals, it too has a Reason property. This property is conserved when it is converted to an `Optional<T>`.

### Supports equality, including `GetHashCode`
This means that it can be used in dictionaries and sets. 

Optionals support equality with other Optionals, even if they are of a different type. Two Optionals are equal if their inner values are equal, or if they are both None. Optionals aren't equal to concrete values, even in their Some state.

Equality to all is provided by the `.Equals(object)` overridden method. However, `Optional`s implement `IEquatable` for several types and also provide overloaded `==` and `!=` operators.

### Enhanced debugging and error reporting
Optional# uses all all kinds of features to make debugging as clear and intuitive as possible. Error messages thrown by Optionals are also a lot more informative than `NullReferenceExceptions` and than most other libraries.

### You can abstract over the `T`
All Optionals implement the `IAnyOptional` interface, which lets you abstract over all Optionals, whatever their inner type, in a polymorphic manner. This is used by a number of features, such as equality tests.

### LINQ-like Transformations
Optionals support many transformations that follow the LINQ naming conventions and API. This makes sense because you can view Optionals as collections with zero or one elements, and many LINQ operations make sense on them.

Some of these are also similar to conditional operators such as `??` and `?.`, but safer.

Examples include:
1. `o.Select(x => x + 5)`, which transform the inner value or returns None.
2. `o.Where(x => x % 2)`, which filters the inner value.
3. `o.OfType<T>()`, which makes the sure the inner value is of some type `T` and returns an Optional with that inner type.
4. `o.Or(5)`, returns the inner value or another default value.

These methods also allow you to use syntax like this:

	var x = from v in Optional.Some(5)
			where v % 2 == 0
			select 2 * v;

Doing so isn't recommended though.

### Conversion Operators
Optionals support explicit conversion to their inner type `T` (that can throw an exception) and implicit conversion from an inner type.

### Conversion Methods
Furthermore, Optional# provides a variety of conversion methods that allow converting to and from other objects, such as reference types and nullable value types.

### User-customizable`Reason` property
Sometimes, it's important (from debugging purposes) to know *why* a value does not exist. Valid reasons for an Optional having no inner value include:

1. Conversion from a `null` value.
2. A search in a collection produces no elements.
3. Parsing of a value as a particular type `T` failed.

Optionals can carry additional information to describe these and other situations through their `Reason` property. This is a nullable property that contains additional information (the reason it's not Optional is that this would create a circular dependency that's hard to get around). Transformations on Optionals intelligently preserve the reason and many methods that result in Optionals accept an optional `reason` parameter. The reason can then be propagated to debugging or error handling code that can log it.

It's also possible to explicitly set the `Reason` through the `WithReason` method that returns a new Optional with a different `Reason`.

The `Reason` information is supplied when a `MissingOptionalValueException` is thrown.

**Warning:** This property is included to make debugging easier. Don't use it to store important information (i.e. information that can change what your code does). 

The `Reason` property is ignored when testing for equality and in many other situations.

## Optional#.More
Optional# comes with a companion assembly that helps integrate it into the .NET Framework. This library offers a number of cool features that aren't necessarily *for* manipulating Optionals directly. Instead, they are features simply best implemented using an Optional type.

### Conditional Access
`Optional#.More` offers a number of *conditional access* extension methods that try to get an element using a predicate, index, or key. If such an element exists, it's wrapped in `Some` and returned. If no such element exists, `None` is returned.

Examples include:

1. `TryFirst`
2. `TryPick`
3. `TryKey`
4. `TryElementAt`

### Extra LINQ methods
This library also provides a number of LINQ methods that are best expressed using Optionals.

**Example:** The `Choose` method is a blend of `Where` and `Select`. It applies a function on every element in a collection, returning either `Some(x)` or `None`. Then it returns the inner values in a (possibly empty) sequence.

**Also:** Provides methods such as `FlattenSequence` that work on a sequence of Optionals or an Optional of a sequence that flattens it into a sequence of values (possibly an empty one).

### Parsing
This library also includes static methods for parsing different data types, such as `long` or even `DateTime`. 

These are included in the `TryParse` static class, containing pretty much every rendition of a `T.TryParse` method that appears in the .NET Framework, returning an `Optional<T>` instead of something involving an `out` parameter.

Examples include:

1. `TryParse.Int32("12")`
2. `TryParse.Char('a')`
3. And even, `TryParse.Enum<TEnum>`

### Parallelism

#### Await on `Optional<Task<T>>`
*Requires C# 7 *
Includes extension methods for objects of type `Optional<Task<T>>` that allow you to write syntax like this:

	Optional<Task<T>> task = Optional.NoneOf<Task<T>>();
	Optional<T> result = await task;

This has the effect of returning `None<T>` when `task` is `None` immediately (i.e. synchronously) or, if a task is present, running the task and returning the value.