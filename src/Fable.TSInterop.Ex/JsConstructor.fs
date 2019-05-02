module Fable.Core.JsInterop

open Fable.Core

// these typed CTors have been removed from Fable.Core with 2.1.0: https://github.com/fable-compiler/Fable/commit/bda083367c967f84d57ac286dd198a573fa134c9


/// Use it when importing a constructor from a JS library.
type [<AllowNullLiteral>] JsConstructor<'Out> =
    [<Emit("new $0()")>]
    abstract Create: unit->'Out

/// Use it when importing a constructor from a JS library.
type [<AllowNullLiteral>] JsConstructor<'Arg1,'Out> =
    [<Emit("new $0($1...)")>]
    abstract Create: 'Arg1->'Out

/// Use it when importing a constructor from a JS library.
type [<AllowNullLiteral>] JsConstructor<'Arg1,'Arg2,'Out> =
    [<Emit("new $0($1...)")>]
    abstract Create: 'Arg1*'Arg2->'Out

/// Use it when importing a constructor from a JS library.
type [<AllowNullLiteral>] JsConstructor<'Arg1,'Arg2,'Arg3,'Out> =
    [<Emit("new $0($1...)")>]
    abstract Create: 'Arg1*'Arg2*'Arg3->'Out

/// Use it when importing a constructor from a JS library.
type [<AllowNullLiteral>] JsConstructor<'Arg1,'Arg2,'Arg3,'Arg4,'Out> =
    [<Emit("new $0($1...)")>]
    abstract Create: 'Arg1*'Arg2*'Arg3*'Arg4->'Out

/// Use it when importing a constructor from a JS library.
type [<AllowNullLiteral>] JsConstructor<'Arg1,'Arg2,'Arg3,'Arg4,'Arg5,'Out> =
    [<Emit("new $0($1...)")>]
    abstract Create: 'Arg1*'Arg2*'Arg3*'Arg4*'Arg5->'Out

/// Use it when importing a constructor from a JS library.
type [<AllowNullLiteral>] JsConstructor<'Arg1,'Arg2,'Arg3,'Arg4,'Arg5,'Arg6,'Out> =
    [<Emit("new $0($1...)")>]
    abstract Create: 'Arg1*'Arg2*'Arg3*'Arg4*'Arg5*'Arg6->'Out
