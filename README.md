# GSearch
A lightning fast, portable library for fetching Google autocomplete suggestions at just 6kb.

## Setting Up
After downloading the library, add it to the application's references.
Then, import the library.
C#:
```csharp
using GSearch;
```

VB.NET:
```vb
Imports GSearch
```

## Usage
Declare an array to store the Google Search results.
C#:
```csharp
string[] suggestions = GSearch.GetResultsAsArray(your_query);
```
VB.NET:
```vb
Dim suggestions As String() = GSearch.GetResultsAsArray(your_query)
```

To get the raw XML, just call `GSearch.GetResultsAsString(your_query);`.

## Future Updates
* VB.Net sample
* Option to change locales in tester application
* Sample with UI

