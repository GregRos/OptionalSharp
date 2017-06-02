# Optional#
[![Build status](https://ci.appveyor.com/api/projects/status/u95y7i721ngdo35b?svg=true)](https://ci.appveyor.com/project/GregRos/optionalsharp)
[![Code Coverage](https://codecov.io/gh/GregRos/OptionalSharp/branch/master/graph/badge.svg)](https://codecov.io/gh/GregRos/OptionalSharp)
Optional# implements what's known as an optional type: A type that represents a value that may not exist.

Now, it's true that reference types *can* be null to represent a missing value, and nullable value types are also a thing. But `null` isn't very good at that job (or any job, really).

## Why not null?
If you're not interested in these rationalizations, you can skip ahead.

### `null` is ambiguous
All reference types are nullable, and technically `null` is a valid value for them. So getting `null` is totally ambiguous: it may indicate that the value exists and it is `null`, that the `null` got in there due to a bug, or that it was used explicitly to indicate a missing value.

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
		return Optional.None;
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

## The Features of Optional#
Optional# has tons of cool features that should cover almost every use-case involving manipulating optional values by themselves.

### First some terms
An **Optional** is an instance of the `Optional<T>` class. It indicates a potentially missing value of type `T`, which is called the *inner type*. This potentially missing value is called the Optional's inner value.

An Optional is immutable, and can be constructed in one of two states:
1. The `Some` state, also written `Some(x)`. It wraps an inner value, which is the `x`. If we want to talk about an Optional with a particular inner type `T`, we can say it's a `Some<T>`.
2. The `None` state, which indicates a missing value. The `None` state is another instance of `Optional<T>`, so the missing value is still of a particular type. This is an implementation detail in some ways (all Nones are equal, as we'll discuss later), but it does provide more debug information. When we want to talk about `None` of a particular inner type, we can say `NoneOf<T>`.

The `Optional<T>.Value` property lets us access the inner value of the Optional. If there is no inner value, the operation throws an exception. Almost all other methods work even when called on None Optionals, though.

### It's a struct
The `Optional<T>` type is a `struct`, so it can't be `null` itself (otherwise, what would be the point?)

Its uninitialized/default state is `None`. In particular, `default(Optional<T>)` returns a Optional with the inner type of `T`, in its `None` state. This enables all kinds of cool behaviors, like using it as an optional parameter:

	public void Example(Optional<int> opt = default(Optional<int>)) {
		if (opt.IsNone) {
			//...
		}
	}

### Supports all of its methods in both states
An `Optional<T>` always supports all of its methods, even in its `None` state. The exception is the `Value` property that returns the inner value, which throws an exception except for `Some` Optionals.

### Supports equality, including `GetHashCode`
This means that it can be used in dictionaries and sets. 

Optionals support equality with other Optionals, even if they are of a different type. Two Optionals are equal if their inner values are equal, or if they are both None. Optionals aren't equal to concrete values, even in their Some state.

Equality to all is provided by the `.Equals(object)` overridden method. However, `Optional`s implement `IEquatable` for several types and also provide overloaded `==` and `!=` operators.

### Enhanced debugging and error reporting
Optional# uses all all kinds of features to make debugging as clear and intuitive as possible. Error messages thrown by Optionals are also a lot more informative than `NullReferenceExceptions`.

### You can abstract over the `T`
All Optionals implement the `IAnyOptional` interface, which lets you abstract over all Optionals, whatever their inner type in a polymorphic manner. This is used by a number of features, such as equality tests.

### Creating Optionals
You can create Optionals in a few different ways.

The `Optional` static class has factory methods that create them, such as `Optional.Some(5)`. It's even better if you use the `using static` statement on the class.

### Transformations similar to `??`, `?.`
Optionals support many kinds of useful transformations.

Basically, a transformation is a method that does something to an Optional and either returns another Optional or something else, but always succeeds regardless what state the Optional is in.

Transformations accomplish tasks similar to operators like `??`, `?.`, and so on. These operators are convenient to use and, more importantly, are safer.


