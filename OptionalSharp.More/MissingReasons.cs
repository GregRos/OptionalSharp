namespace OptionalSharp.More
{
	internal static class MissingReasons {
		public static readonly string KeyNotFound = $"A key was not found in a dictionary.";

		public static readonly string IndexNotFound = "An element was not found at a given index in a collection.";

		public static readonly string CollectionWasEmpty = "A sequence or collection contained no elements.";

		public static readonly string NoElementsFound = "No element matching the given predicate was found.";

		public static class CouldNotBeParsedAs<T> {
			public static readonly string Value = $"A string could not be parsed as a value of type {typeof(T).PrettyName()}";
		}
	}
}
