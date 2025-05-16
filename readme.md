# Project files

## WordWiz.App
A thin console app executing the main WordWizz service fiund in the Parsers project.

Execution parameter examples:
* WordWizApp.exe _(Uses defaults)_
* WordWizApp.exe --sourcedirectory \myfiles --targetdirectory \myresults
* WordWizApp.exe  -s \myfiles -t \myresults

## WordWiz.Parsers
Contains the main service and interfaces that defines the actiosn that can be performed on the text files.
The main service also uses to parsers on
* File level
* Line level 

_(The parsers don't do much, but they encapsulat the responsibilities of the main service well)_

## WordWiz.Components
Contains custom actions that uses the interfaces defined in the Parsers project. Realistically this project would nto be part of the main solution.

# Data structure
The operations performed on the text files are dined as actions on two levels:
* File action: performs actions on an individual file
* Main Action: Produces a file action for each file, collects the results and persist them.

# Testing
Two test projects exist for
* WordWiz Generel: Parsers etc.
* WordWiz Components: Custom operations

## Test mocking
File access is abstracted out, so the WordWiz service

# Parallelism
To optimize the code for multiple CPUs, the WordWiz service runs all files in parallel, and pushes any threadunsafe operations till after the files are processed, like collecting the reading from all the individual file actions.

# Required optimization
* Option to finetune parallism, based on the files count and size
* Better memory cleanup of intermediate in-mem dictionaries
* Word recognision is way too simple. Alternatively NLP can be used.
* A Factory pattern is required to make it possible to further customize the operations on the files. E.g. via late binding.
* A Chain of Responsibility pattern shouold be used, so the actions could perform chains of operations on the same data. 
