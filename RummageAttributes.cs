// The following code is distributed under the CC0 license whose text can be found at
// http://creativecommons.org/publicdomain/zero/1.0/
// You are permitted to use this code in any way you like, including commercial use.
using System;

namespace orGenta_NNv
{
    /// <summary>
    /// Instructs Rummage to keep a specific type, method, constructor or field.
    /// </summary>
    [AttributeUsage(
        AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Delegate | AttributeTargets.Interface |
        AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Field,
        Inherited = false, AllowMultiple = false)]
    public sealed class RummageNoRemoveAttribute : Attribute
    {
    }

    /// <summary>
    /// Instructs Rummage to keep the original name of a specific element.
    /// </summary>
    [AttributeUsage(
        AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Delegate | AttributeTargets.Interface |
        AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Event |
        AttributeTargets.Parameter | AttributeTargets.GenericParameter,
        Inherited = false, AllowMultiple = false)]
    public sealed class RummageNoRenameAttribute : Attribute
    {
    }

    /// <summary>
    /// Instructs Rummage to keep the original name of a specific type, all of its members, and all the members
    /// in all of its nested types.
    /// </summary>
    [AttributeUsage(
        AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Delegate | AttributeTargets.Interface,
        Inherited = false, AllowMultiple = false)]
    public sealed class RummageNoRenameAnythingAttribute : Attribute
    {
    }

    /// <summary>
    /// Instructs Rummage to avoid un-nesting the specified type.
    /// </summary>
    [AttributeUsage(
        AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Delegate | AttributeTargets.Interface,
        Inherited = false, AllowMultiple = false)]
    public sealed class RummageNoUnnestAttribute : Attribute
    {
    }

    /// <summary>
    /// Instructs Rummage to keep the original access modifier of a specific element.
    /// </summary>
    [AttributeUsage(
        AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Delegate | AttributeTargets.Interface |
        AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Event,
        Inherited = false, AllowMultiple = false)]
    public sealed class RummageNoMarkPublicAttribute : Attribute
    {
    }

    /// <summary>
    /// Instructs Rummage not to inline a specific method or property that would otherwise be automatically inlined.
    /// This attribute takes precedence over <see cref="RummageInlineAttribute"/> if both are specified on the
    /// same method or property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class RummageNoInlineAttribute : Attribute
    {
    }

    /// <summary>
    /// Instructs Rummage to inline a specific method or property that would otherwise not be automatically inlined.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class RummageInlineAttribute : Attribute
    {
    }

    /// <summary>
    /// Instructs Rummage to refrain from making any changes to a specific type.
    /// </summary>
    [AttributeUsage(
        AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Delegate | AttributeTargets.Interface |
        AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Event |
        AttributeTargets.Constructor | AttributeTargets.Parameter | AttributeTargets.GenericParameter,
        Inherited = false, AllowMultiple = false), RummageKeepUsersReflectionSafe]
    public sealed class RummageKeepReflectionSafeAttribute : Attribute
    {
    }

    /// <summary>
    /// Instructs Rummage to keep all the types reflection-safe which are passed in for the given generic parameter.
    /// </summary>
    [AttributeUsage(AttributeTargets.GenericParameter, Inherited = false, AllowMultiple = false)]
    public sealed class RummageKeepArgumentsReflectionSafeAttribute : Attribute
    {
    }

    /// <summary>
    /// Use only on custom-attribute class declarations. Instructs Rummage to keep everything reflection-safe that
    /// uses the given custom attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class RummageKeepUsersReflectionSafeAttribute : Attribute
    {
    }

    /// <summary>
    /// Use on a method or constructor parameter of type "Type". Instructs Rummage that this method uses the Type
    /// passed in in a way that is fully compatible with all obfuscations, including removing members not directly
    /// referenced, renaming members, unnesting types and so on.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.GenericParameter, Inherited = false, AllowMultiple = false)]
    public sealed class RummageAssumeTypeSafeAttribute : Attribute
    {
    }

    /// <summary>Contains methods used to augment the program with Rummage-related information.</summary>
    public static class Rummage
    {
        /// <summary>
        /// Returns the type passed in. Use around a <c>typeof(SomeType)</c> to override Rummage's reflection safety
        /// analysis and make Rummage believe that this particular use is entirely safe.
        /// </summary>
        public static Type Safe([RummageAssumeTypeSafe] Type type) { return type; }
    }
}
