"WordWiz" (Short for word wizard) is a .NET9 console app, coded in C#, that tries to solve the DB coding challenge "Word Counter". Some focus points for the choice of implementation are:
* Expressive code, that is intuitively easy to understand
* Clear separation of responsibility
* Avoid complexity in code
* Offer standardization (Use of interface and factories) of implementation to ensure easy extension of functionality
* Standardization ensures option to execute word count (And future) operations in parallel.
* Standardization should help, the next developer that adds extensions avoids thread issues

# Project files

## WordWiz.App
A thin console app executing the main WordWizzard service found in the Parsers project.

Execution parameter examples:
* WordWizApp.exe _(Uses defaults)_
* WordWizApp.exe --sourcedirectory myfiles --targetdirectory myresults
* WordWizApp.exe  -s myfiles -t myresults
* WordWizApp.exe  -s .\myfiles -t .\myresults
* WordWizApp.exe  -s ..\\..\myfiles -t ..\\..\myresults

Note: target directory will be created if it doesn't exist. If the source directory doesn't exist a warning is issued.

## WordWiz.Parsers
Contains the main service and interfaces that defines the actions that can be performed on the text files.
The main service also uses two parsers on
* File level
* Line level 

_(The parsers don't do much, but they encapsulate the responsibilities of the main service well)_

## WordWiz.Components
Contains custom actions that uses the interfaces defined in the Parsers project. Realistically this project would nto be part of the main solution.

# Data structure
The operations performed on the text files are dined as actions on two levels:
* File action: performs actions on an individual file
* Main Action: Produces a file action for each file, collects the results and persist them.

# Testing
Two test projects exist for
* WordWiz General: Parsers etc.
* WordWiz Components: Custom operations

## Test mocking
File access is abstracted out, so the WordWiz service

# Parallelism
To optimize the code for multiple CPUs, the WordWiz service runs all files in parallel, and pushes any thread unsafe operations till after the files are processed, like collecting the reading from all the individual file actions.

# Required optimization
* Option to fine tune parallelism, based on the files count and size
* Better memory cleanup of intermediate in-mem dictionaries
* Word recognition is way too simple. Alternatively NLP can be used.
* A Factory pattern is required to make it possible to further customize the operations on the files. E.g. via late binding.
* A Chain of Responsibility pattern should be used, so the actions could perform chains of operations on the same data.

# Still missing
* No tests exist for the FileReader og ResultWriter. It would require use of actual test files.
* A lot of tests in general.
* Exception handling in the Parsers surrounding calls to the customizable action classes.
* Logging options and levels
