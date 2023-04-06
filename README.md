# Wordify Number
An app to change numbers into words, just like people use in cheques. 

## How to use without build
Just go to `dist/portable` folder and execute `WordifyNumberApp.exe`. 

```
[path-to-root]\wordify-number\dist\portable>.\WordifyNumberApp.exe
Please input number:
123.45
Number to wordify: $123.45
Hit any key to proceed

Wordified Number: one hundred and twenty three dollars and forty five cents

Again? [Y] Yes, [n] No:
```
You can also uses another distributions if your machine do not have .NET 6.0 runtime installed. E.g `dist/win-x64` is a distribution for windows 64 bit.

## Build, Run, and Tests
This App is written in .NET 6.0, C#.  
Just open the sln with Visual Studio 2022, preferrably. Hit F6 to build, F5 to run, and Ctrl+R,A to run all tests.

## Algorithm Explanation
First, the number text is separated by dot (.) sign into two: **dollar text** and **cent text**.

For **dollar text**, it is splitted into an array of three letters. 

```
For example: "100" => ["100"], 
             "1000" => ["1", "000"], 
             "10203040" => ["10", "203", "040"] 
```			 
And then each of those three letters is wordifed by hundred-or-less function. 

```
For example: ["100"]
                 |--> "one hundred"

             ["1", "000"]
               |      |--> ""
               |--> "one"

             ["10", "203", "040"] 
                |      |      |--> "forty"
                |      |--> "two hundred and three"
                |--> "ten"
```

At every three letters, append the word suitable for its position. 

```
For example: ["100"]
                 |--> "one hundred". Since this is the last position, we don't have to add a word, since it is already suitable.

             ["1", "000"]
               |      |--> "". Last position, don't add a word
               |--> "one". Last-1 position, add "thousand" here. So it would become "one thousand"

             ["10", "203", "040"] 
                |      |      |--> "forty". Last position, don't add a word
                |      |--> "two hundred and three". Last-1 position, add "thousand" here. So it would become "two hundred and three thousand"
                |--> "ten". Last-2 position, add "million" here. So it would become "tem million"

```

And so on until it is "quintillion".

For **cent text**, it is just wordified with hundred-or-less-function.

And then the results are all appended, with addition of "dollars" and "cents".

### Hundred-or-Less function
A function a three-letters number into word. So it would be hundreds or less (as the name suggests).

```
"100"
 |||--> Ones position
 ||--> Tens position
 |--> Hundreds position
```

#### Hundred and Tens position
As long the number isn't Zero ("0"), the wordified number is appended along with the wordified position.

```
For example: "7X" 
              |
              |--> "seven" is the wordified number, appended with the wordified position "ty", becomes "seventy"

              "2XX"
               |
               |--> "two" is the wordified number, appended with the wordified position "hundred", becomes "two hundred"
```

Except "1X" i guess, because it is tightly couple with the ones. E.g "eleven", "thirteen", etc

#### Ones position
Quite handful here.
- if the current ones is "0" ("zero") and tens is "1" ("one"), then append "ten"
- if the current ones is not "0" and then the tens is "1" ("one"), then wordify the number and append "teen". e.g "sixteen", seventeen". There are correction, like "eleven", "twelve", etc
- if the current ones is not "0" and then the tens is other than "1" ("one"), just wordify the number.

