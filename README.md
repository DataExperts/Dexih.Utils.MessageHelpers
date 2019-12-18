# Dexih.Utils.MessageHelpers

[build]:    https://ci.appveyor.com/project/dataexperts/dexih-utils-messagehelpers 
[build-img]: https://ci.appveyor.com/api/projects/status/fq6bgmha22ai36mg?svg=true
[nuget]:     https://www.nuget.org/packages/dexih.utils.messagehelpers
[nuget-img]: https://badge.fury.io/nu/dexih.utils.messagehelpers.svg
[nuget-name]: dexih.utils.messagehelpers
[dex-img]: https://dataexpertsgroup.com/assets/img/dex_web_logo.png
[dex]: https://dataexpertsgroup.com

[![][dex-img]][dex]

[![Build status][build-img]][build] [![Nuget][nuget-img]][nuget]

The `MessageHelpers` provide some helper classes that allow methods to return values along with success status, and error messages.  The class is serializable, and useful when returning values from controller API's.

Benefits:
* Provide a return value along with success and messaging information.
* Automatically parses exceptions (including inner exceptions) into a serializable string.  Note, exceptions are generally NOT serializable.

Note, for method to method calling, Microsoft recommend using exceptions to return messages.  Using this library for this, can therefor be considered an anti-pattern in most use-cases.

Here is an example of providing a message via exception:

```csharp
int myMethod()
{
    // method routines 

    if(thisFailed) 
    {
        throw new Exception("I failed badly");
    }

    return value;
}
```

Using the MessageHelper to achieve a similar result would be:

```csharp
int myMethod()
{
    // method routines 

    if(thisFailed) 
    {
        return new ReturnValue<int>(false, "I failed badly", null);
    }

    return new ReturnValue<int>(true, value);
}
```

## Installation

Add the [latest version][nuget] of the package "dexih.utils.messagehelpers" to a .net core/.net project.  This requires .net standard framework 2.0 or newer, or the .net framework 4.6 or newer.

## Usage

Returning no value

```csharp
return new ReturnValue(true); // return success

return new ReturnValue(false, message, exception); // return fail.
```

To return a value, use a generic definition
```csharp
return new ReturnValue<int>(true, 123); // returns success and 123

return new ReturnValue<int>(false, message, exception); // return fail.
```

To return multiple messages, use `ReturnValueMultiple`

```csharp
var returnValueMultiple = new ReturnValueMultiple<string>();

returnValueMultiple.Add(new ReturnValue<string>(false, "The message 1", new Exception("The exception 1"), "value 1"));
returnValueMultiple.Add(new ReturnValue<string>(false, "The message 2", new Exception("The exception 2"), "value 2"));

// a list containing "value 1" and "value 2"
var values = returnValueMultiple.ReturnValues;

// a combined message of the above errors.
var message = returnValueMultiple.Message;

// a combined message of the above errors.
var details = returnValueMultiple.ExceptionDetails;
```

The `ReturnMessage`, is serializable to Json and looks as follows when serialized:

```json
{
    "success": false,
    "value": 123,
    "message": "the call failed"
    "exceptionDetails": "<long details of the exception>"
}
```

## Issues and Feedback

This library is provided free of charge under the MIT licence and is actively maintained by the [Data Experts Group](https://dataexpertsgroup.com)

Raise issues or bugs through the issues section of the git hub repository ([here](https://github.com/DataExperts/Dexih.Utils.MessageHelpers/issues))


