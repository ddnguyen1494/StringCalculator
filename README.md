# StringCalculator
Calculator that takes a single formatted string as input and output result.

### Prerequisites
+ .NET Framework 4.6.2

### Command-line arguments
Here's the list of arguments the calculator accepts:

##### d: Delimiter
Using this flag looks like this:
```sh
StringCalculator.exe -d"delim"
```
The calculator will use "delim" as an alternate delimiter.
For example: "100delim100" returns 200

##### n: NegativeNumber
Using this flag looks like this:
```sh
StringCalculator.exe -n
```
The calculator will allow the use of negative number.
For example: "100,100" returns 200

##### u: UpperBound
Using this flag looks like this:
```sh
StringCalculator.exe -u100
```
The calculator will use 100 as the upper bound for number.
For example: "101,5" returns 5