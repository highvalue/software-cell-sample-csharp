# Sample C# Project

## Key Architecture Goals & Principles

- Simple Ruleset
- Clear Separation of Concerns
- Supports an Evolutionary Approach
- Supports Encapsulated Modules (esting is a first class citizen)

# C# Project Structure

## High Level Project Structure

![](doc/img/csharp1.png)

The default C# solution consists of three C# projects:
* **SampleAPI** - the regular code lives here
* **SampleAPI.Contracts** - the place where to put external model definitions
* **SampleAPI.Tests** - put tests in here


## Project Folder Structure

**Cell Structure**
![](doc/img/csharp2.png)

* 1: **Core** - contains the pure business logic and rules
* 2: **Gate** - this is the entry point for external calls (controllers, EventHandlers, ...)
* 3: **Provider** - encapsulates knowledge and communication with external systems

[read more](#Software-Cell)

**Code Classifications**

![](doc/img/csharp3.png)

* A: **business** code
* B: **custom tailored** code (business + technology mixed)
* C: **retail** code (pure technology, common libraries, e.g. MongoDB, RabbitMQ, ...)
* D: **neutral** code (pure technology, small snippets/extensions, almost no dependencies)

[read more](#Code-Classifications)

### SampleAPI Example

![](doc/img/csharp4.png)



